using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ZNC.Utility
{
    [Serializable]
    public class ModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler pceh = PropertyChanged;
            if (pceh != null)
            {
                pceh(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises this object's <see cref="ModelBase.PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression">A MemberExpression, containing the property that value changed.</param>
        /// <remarks>Use the following syntax: this.OnPropertyChanged(() => this.MyProperty); 
        /// instead of: this.OnPropertyChanged("MyProperty");</remarks>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            this.OnPropertyChanged((propertyExpression.Body as MemberExpression).Member.Name);
        }

        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            PropertyChangedEventHandler pceh = PropertyChanged;
            if (pceh != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    pceh(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        #endregion

        /// <summary>
        /// use this to  set value if property need validation
        /// </summary>
        /// <typeparam name="T">type of property, don't need pass it if it is called by set of Property</typeparam>
        /// <param name="target">property variable</param>
        /// <param name="value">property value</param>
        /// <param name="propertyName">property name</param>
        /// <returns></returns>
        protected virtual bool SetValue<T>(ref T target, T value, string propertyName)
        {
            if (Object.Equals(target, value))
            {
                return false;
            }

            target = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// use this to set value if property need validation
        /// </summary>
        /// <typeparam name="T">type of property, don't need pass it if it is called by set of Property</typeparam>
        /// <param name="target">property variable</param>
        /// <param name="value">property value</param>
        /// <param name="propertyExpression">property expression</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected virtual bool SetValue<T>(ref T target, T value, Expression<Func<T>> propertyExpression)
        {
            if (Object.Equals(target, value))
            {
                return false;
            }

            target = value;
            OnPropertyChanged(propertyExpression);
            return true;
        }
    }
}
