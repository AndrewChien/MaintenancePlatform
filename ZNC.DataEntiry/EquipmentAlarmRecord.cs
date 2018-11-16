using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 设备报警记录表
    public class EquipmentAlarmRecord : ZNC.Utility.DataModelBase
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

        private long alarmTypeID;

        [DataMember]
        public long AlarmTypeID
        {
            get { return alarmTypeID; }
            set { base.SetValue(ref alarmTypeID, value, () => this.AlarmTypeID, false); }
        }

        private DateTime alarmTime;

        [DataMember]
        public DateTime AlarmTime
        {
            get { return alarmTime; }
            set { base.SetValue(ref alarmTime, value, () => this.AlarmTime, false); }
        }

        private string handledStatus;

        [DataMember]
        public string HandledStatus
        {
            get { return handledStatus; }
            set { base.SetValue(ref handledStatus, value, () => this.HandledStatus, false); }
        }

        private string handledMan;

        [DataMember]
        public string HandledMan
        {
            get { return handledMan; }
            set { base.SetValue(ref handledMan, value, () => this.HandledMan, false); }
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