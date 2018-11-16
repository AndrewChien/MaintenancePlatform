using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 设备日志表
    public class EquipmentLog : ZNC.Utility.DataModelBase
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

        private long equipID;

        [DataMember]
        public long EquipID
        {
            get { return equipID; }
            set { base.SetValue(ref equipID, value, () => this.EquipID, false); }
        }

        private string equipName;

        [DataMember]
        public string EquipName
        {
            get { return equipName; }
            set { base.SetValue(ref equipName, value, () => this.EquipName, false); }
        }

        private long logTypeID;

        [DataMember]
        public long LogTypeID
        {
            get { return logTypeID; }
            set { base.SetValue(ref logTypeID, value, () => this.LogTypeID, false); }
        }

        private long equipTypeID;

        [DataMember]
        public long EquipTypeID
        {
            get { return equipTypeID; }
            set { base.SetValue(ref equipTypeID, value, () => this.EquipTypeID, false); }
        }

        private long departID;

        [DataMember]
        public long DepartID
        {
            get { return departID; }
            set { base.SetValue(ref departID, value, () => this.DepartID, false); }
        }

        private string logInfo;

        [DataMember]
        public string LogInfo
        {
            get { return logInfo; }
            set { base.SetValue(ref logInfo, value, () => this.LogInfo, false); }
        }

        private string logURL;

        [DataMember]
        public string LogURL
        {
            get { return logURL; }
            set { base.SetValue(ref logURL, value, () => this.LogURL, false); }
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