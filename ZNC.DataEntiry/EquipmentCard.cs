using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 设备卡片表
    public class EquipmentCard : ZNC.Utility.DataModelBase
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

        private string cardName;

        [DataMember]
        public string CardName
        {
            get { return cardName; }
            set { base.SetValue(ref cardName, value, () => this.CardName, false); }
        }

        private long equipID;

        [DataMember]
        public long EquipID
        {
            get { return equipID; }
            set { base.SetValue(ref equipID, value, () => this.EquipID, false); }
        }

        private string equipStatus;

        [DataMember]
        public string EquipStatus
        {
            get { return equipStatus; }
            set { base.SetValue(ref equipStatus, value, () => this.EquipStatus, false); }
        }

        private string enable;

        [DataMember]
        public string Enable
        {
            get { return enable; }
            set { base.SetValue(ref enable, value, () => this.Enable, false); }
        }

        private string enableAlarm;

        [DataMember]
        public string EnableAlarm
        {
            get { return enableAlarm; }
            set { base.SetValue(ref enableAlarm, value, () => this.EnableAlarm, false); }
        }

        private long monitoringLevelID;

        [DataMember]
        public long MonitoringLevelID
        {
            get { return monitoringLevelID; }
            set { base.SetValue(ref monitoringLevelID, value, () => this.MonitoringLevelID, false); }
        }

        private DateTime rebootTime;

        [DataMember]
        public DateTime RebootTime
        {
            get { return rebootTime; }
            set { base.SetValue(ref rebootTime, value, () => this.RebootTime, false); }
        }

        private DateTime lastOverhaulDate;

        [DataMember]
        public DateTime LastOverhaulDate
        {
            get { return lastOverhaulDate; }
            set { base.SetValue(ref lastOverhaulDate, value, () => this.LastOverhaulDate, false); }
        }

        private long equipHealthID;

        [DataMember]
        public long EquipHealthID
        {
            get { return equipHealthID; }
            set { base.SetValue(ref equipHealthID, value, () => this.EquipHealthID, false); }
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