using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    /// <summary>
    /// 推送历史表
    /// </summary>
    [Serializable]
    [DataContract]

    public class AlarmHistory : ZNC.Utility.DataModelBase
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

        private DateTime pushTime;

        [DataMember]
        public DateTime PushTime
        {
            get { return pushTime; }
            set { base.SetValue(ref pushTime, value, () => this.PushTime, false); }
        }

        private string pushContent;

        [DataMember]
        public string PushContent
        {
            get { return pushContent; }
            set { base.SetValue(ref pushContent, value, () => this.PushContent, false); }
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

        private long pushRuleID;

        [DataMember]
        public long PushRuleID
        {
            get { return pushRuleID; }
            set { base.SetValue(ref pushRuleID, value, () => this.PushRuleID, false); }
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