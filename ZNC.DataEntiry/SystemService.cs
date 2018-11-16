using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 系统服务配置中表
    public class SystemService : ZNC.Utility.DataModelBase
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

        private string serviceName;

        [DataMember]
        public string ServiceName
        {
            get { return serviceName; }
            set { base.SetValue(ref serviceName, value, () => this.ServiceName, false); }
        }

        private long serviceTypeID;

        [DataMember]
        public long ServiceTypeID
        {
            get { return serviceTypeID; }
            set { base.SetValue(ref serviceTypeID, value, () => this.ServiceTypeID, false); }
        }

        private string serviceURL;

        [DataMember]
        public string ServiceURL
        {
            get { return serviceURL; }
            set { base.SetValue(ref serviceURL, value, () => this.ServiceURL, false); }
        }

        private string describtion;

        [DataMember]
        public string Describtion
        {
            get { return describtion; }
            set { base.SetValue(ref describtion, value, () => this.Describtion, false); }
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