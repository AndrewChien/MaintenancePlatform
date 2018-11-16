using System;

namespace ZNC.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GroupNameAttribute : Attribute
    {
        public GroupNameAttribute()
        {

        }
        public GroupNameAttribute(string groupName)
        {
            this._groupName = groupName;

        }
        private string _groupName;
        /// <summary>
        /// validation group, some of time we don't need validation the whole object, then we can validation only the property specified group
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
    }
}
