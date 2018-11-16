using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 系统配置表
    public class SystemSetting : ZNC.Utility.DataModelBase
    {
        private long id;

        [DataMember]
        public long ID
        {
            get { return id; }
            set { base.SetValue(ref id, value, () => this.ID, false); }
        }

        private long code;

        [DataMember]
        public long Code
        {
            get { return code; }
            set { base.SetValue(ref code, value, () => this.Code, false); }
        }

        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { base.SetValue(ref name, value, () => this.Name, false); }
        }

        private string _value;

        [DataMember]
        public string Value
        {
            get { return _value; }
            set { base.SetValue(ref _value, value, () => this.Value, false); }
        }

        private long enableStatus;

        [DataMember]
        public long EnableStatus
        {
            get { return enableStatus; }
            set { base.SetValue(ref enableStatus, value, () => this.EnableStatus, false); }
        }

        private string remark;

        [DataMember]
        public string Remark
        {
            get { return remark; }
            set { base.SetValue(ref remark, value, () => this.Remark, false); }
        }

    }
}