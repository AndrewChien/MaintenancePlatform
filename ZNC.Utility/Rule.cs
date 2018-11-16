using System;
using System.ComponentModel;

namespace ZNC.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class Rule : IDataErrorInfo
    {
        #region Fields

        private string _description;
        private string _propertyName;
        private ValidateRuleDelegate _ruleValidation;

        #endregion

        #region Consturctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        public Rule()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="brokenDescription">The broken description.</param>
        /// <param name="ruleDelegate">The rule delegate.</param>
        public Rule(string propertyName, string brokenDescription, ValidateRuleDelegate ruleDelegate)
        {
            _propertyName = propertyName;
            _description = brokenDescription;
            _ruleValidation = ruleDelegate;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates that the rule has not been broken.
        /// </summary>
        /// <returns>True if the rule has not been broken, or false if it has.</returns>
        public virtual bool ValidateRule()
        {
            return RuleValidation();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets error message text about this broken rule.
        /// </summary>
        public virtual string ErrorMessage
        {
            get { return _description; }
            protected set { _description = value; }
        }

        /// <summary>
        /// Gets the name of the property the rule belongs to.
        /// </summary>
        public virtual string PropertyName
        {
            get { return (_propertyName ?? string.Empty).Trim(); }
            protected set { _propertyName = value; }
        }

        /// <summary>
        /// Gets or sets the delegate used to validate this rule.
        /// </summary>
        public virtual ValidateRuleDelegate RuleValidation
        {
            get { return _ruleValidation; }
            protected set { _ruleValidation = value; }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public delegate bool ValidateRuleDelegate();
}
