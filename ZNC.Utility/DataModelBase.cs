using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace ZNC.Utility
{
    [Serializable]
    public class DataModelBase : ModelBase, IDataErrorInfo, IValidatableObject
    {
        #region Constructors

        public DataModelBase()
        {
            this.ValidateObject();
        }
        public DataModelBase(string groupName)
        {
            this.ValidateGroup(groupName);
        }
        #endregion

        #region IDataErrorInfo

        private Dictionary<String, List<String>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// Clear all error information
        /// </summary>
        protected virtual void ClearError()
        {
            this._errors.Clear();
        }
        /// <summary>
        /// clear all error information for the specified property 
        /// <param name="propertyName">property name</param>
        /// </summary>
        protected virtual void ClearError(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName) && this._errors[propertyName] != null)
            {
                this._errors[propertyName].Clear();
            }
        }
        /// <summary>
        /// Adds the specified error to the errors collection if it is not
        /// already present, inserting it in the first position if isWarning is
        /// false. Raises the ErrorsChanged event if the collection changes.
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <param name="error">The error.</param>
        /// <param name="isWarning">Set to <c>true</c> if it's warning.</param>
        protected virtual void AddError(string propertyName, string error, bool isWarning)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                if (isWarning) _errors[propertyName].Add(error);
                else _errors[propertyName].Insert(0, error);
            }
        }

        /// <summary>
        /// Removes the specified error from the errors collection if it is
        /// present. Raises the ErrorsChanged event if the collection changes.
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <param name="error">The error.</param>
        protected virtual void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) &&
                _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0) _errors.Remove(propertyName);
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <value></value>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get
            {
                return _errors.Count > 0 ? "Object has errors." : null;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified property name.
        /// </summary>
        /// <value></value>
        public string this[string propertyName]
        {
            get
            {
                if (_errors.Count == 0 || !_errors.ContainsKey(propertyName))
                {
                    return null;
                }
                return String.Join(Environment.NewLine, _errors[propertyName]);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// use this to  set value if property need validation
        /// </summary>
        /// <typeparam name="T">type of property, don't need pass it if it is called by set of Property</typeparam>
        /// <param name="target">property variable</param>
        /// <param name="value">property value</param>
        /// <param name="propertyName">property name</param>
        /// <param name="needValidate">whether need validate property value, default is true</param>
        /// <returns></returns>
        protected virtual bool SetValue<T>(ref T target, T value, string propertyName, bool needValidate = true)
        {
            if (Object.Equals(target, value))
            {
                return false;
            }

            target = value;
            if (needValidate)
            {
                ValidateProperty(value, propertyName);
            }

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// use this to  set value if property need validation
        /// </summary>
        /// <typeparam name="T">type of property, don't need pass it if it is called by set of Property</typeparam>
        /// <param name="target">property variable</param>
        /// <param name="value">property value</param>
        /// <param name="propertyExpression">property name expression.</param>
        /// <param name="needValidate">whether need validate property value, default is true</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected virtual bool SetValue<T>(ref T target, T value, Expression<Func<T>> propertyExpression, bool needValidate = true)
        {
            if (Object.Equals(target, value))
            {
                return false;
            }

            target = value;
            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            if (needValidate)
            {
                ValidateProperty(value, propertyName);
            }

            OnPropertyChanged(propertyName);

            return true;
        }
        #endregion

        #region validate

        /// <summary>
        /// ValidateProperty
        /// </summary>
        /// <param name="value">property value</param>
        /// <param name="propertyName">propert name</param>
        public virtual bool ValidateProperty(object value, string propertyName)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName }, errors);
            ClearError(propertyName);

            if (errors.Count > 0)
            {
                foreach (ValidationResult result in errors)
                {
                    AddError(result.MemberNames.FirstOrDefault(), result.ErrorMessage, false);
                }

                return false;
            }
            IEnumerable<ValidationResult> ie = Validate(new ValidationContext(this, null, null) { MemberName = propertyName }, propertyName);
            if (ie.Count() > 0)
            {
                foreach (ValidationResult result in ie)
                {
                    AddError(result.MemberNames.FirstOrDefault(), result.ErrorMessage, false);
                }

                return false;
            }
            return true;
        }

        /// <summary>
        /// validate the group of data entity
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public virtual bool ValidateGroup(string groupName)
        {
            Type type = this.GetType();
            System.Reflection.PropertyInfo[] properties = type.GetProperties();
            bool ret = true;
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                object[] groupNames = property.GetCustomAttributes(typeof(GroupNameAttribute), false);
                if (groupNames == null || groupNames.Length == 0) continue;

                foreach (GroupNameAttribute group in groupNames)
                {
                    if (group.GroupName == groupName)
                    {
                        object value = property.GetValue(this, null);
                        if (!ValidateProperty(value, property.Name))
                        {
                            ret = false;
                        }
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Validate the whole object
        /// </summary>
        /// <returns></returns>
        public virtual bool ValidateObject()
        {
            ClearError();

            List<ValidationResult> errors = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), errors);
            if (errors.Count > 0)
            {
                foreach (ValidationResult result in errors)
                {
                    AddError(result.MemberNames.FirstOrDefault(), result.ErrorMessage, false);
                }

                return false;
            }

            return true;
        }

        public void NotifyErrorProperties()
        {
            if (_errors == null || _errors.Count == 0) return;

            foreach (var e in _errors)
            {
                this.OnPropertyChanged(e.Key);
            }
        }
        
        #endregion

        #region IValidatableObject Members

        private IList<Rule> _rules = new List<Rule>();

        /// <summary>
        /// Adds the specified validation rule.
        /// </summary>
        /// <param name="rule">The validation rule.</param>
        public void AddRule(Rule rule)
        {
            if (_rules.IndexOf(rule) < 0)
                _rules.Add(rule);
        }

        /// <summary>
        /// Removes the specified validation rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public void RemoveRule(Rule rule)
        {
            if (_rules.IndexOf(rule) >= 0)
                _rules.Remove(rule);
        }

        /// <summary>
        /// Clears the validation rule.
        /// </summary>
        public void ClearRule()
        {
            _rules.Clear();
        }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Validate(validationContext, string.Empty);
        }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="property">The specified property name, pass empty string ("") if need validate all properties.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext, string property)
        {
            IList<ValidationResult> result = new List<ValidationResult>();
            if (this._rules == null || this._rules.Count == 0) return result;

            foreach (Rule rule in this._rules)
            {
                // Ensure we only validate a rule 
                if (rule.PropertyName == property || property == string.Empty)
                {
                    bool isRuleBroken = rule.ValidateRule(/*validationContext.ObjectInstance*/);
                    if (isRuleBroken)
                    {
                        if (validationContext.MemberName != null)
                        {
                            result.Add(new ValidationResult(rule.ErrorMessage, new string[] { validationContext.MemberName }));
                        }
                        else
                        {
                            result.Add(new ValidationResult(rule.ErrorMessage, new string[] { rule.PropertyName }));
                        }
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
