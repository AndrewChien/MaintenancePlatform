using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 上传配置表
    public class UploadSetting : ZNC.Utility.DataModelBase
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

        private string format;

        [DataMember]
        public string Format
        {
            get { return format; }
            set { base.SetValue(ref format, value, () => this.Format, false); }
        }

        private string dataTranferTypeID;

        [DataMember]
        public string DataTranferTypeID
        {
            get { return dataTranferTypeID; }
            set { base.SetValue(ref dataTranferTypeID, value, () => this.DataTranferTypeID, false); }
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