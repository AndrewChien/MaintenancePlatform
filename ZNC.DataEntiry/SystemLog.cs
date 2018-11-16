using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 系统日志表
    public class SystemLog : ZNC.Utility.DataModelBase
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

        private long logTypeID;

        [DataMember]
        public long LogTypeID
        {
            get { return logTypeID; }
            set { base.SetValue(ref logTypeID, value, () => this.LogTypeID, false); }
        }

        private long moduleID;

        [DataMember]
        public long ModuleID
        {
            get { return moduleID; }
            set { base.SetValue(ref moduleID, value, () => this.ModuleID, false); }
        }

        private string infoContent;

        [DataMember]
        public string InfoContent
        {
            get { return infoContent; }
            set { base.SetValue(ref infoContent, value, () => this.InfoContent, false); }
        }

        private DateTime recordTime;

        [DataMember]
        public DateTime RecordTime
        {
            get { return recordTime; }
            set { base.SetValue(ref recordTime, value, () => this.RecordTime, false); }
        }

        private string causeReason;

        [DataMember]
        public string CauseReason
        {
            get { return causeReason; }
            set { base.SetValue(ref causeReason, value, () => this.CauseReason, false); }
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