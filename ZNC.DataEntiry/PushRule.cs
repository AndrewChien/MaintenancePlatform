using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 消息报警推送规则表
    public class PushRule : ZNC.Utility.DataModelBase
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

        private string ruleName;

        [DataMember]
        public string RuleName
        {
            get { return ruleName; }
            set { base.SetValue(ref ruleName, value, () => this.RuleName, false); }
        }

        private long moduleID;

        [DataMember]
        public long ModuleID
        {
            get { return moduleID; }
            set { base.SetValue(ref moduleID, value, () => this.ModuleID, false); }
        }

        private long pushTypeID;

        [DataMember]
        public long PushTypeID
        {
            get { return pushTypeID; }
            set { base.SetValue(ref pushTypeID, value, () => this.PushTypeID, false); }
        }

        private string format;

        [DataMember]
        public string Format
        {
            get { return format; }
            set { base.SetValue(ref format, value, () => this.Format, false); }
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