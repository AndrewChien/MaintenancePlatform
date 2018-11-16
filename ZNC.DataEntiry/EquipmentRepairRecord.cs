using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    public class EquipmentRepairRecord : ZNC.Utility.DataModelBase
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

        private string billCode;

        [DataMember]
        public string BillCode
        {
            get { return billCode; }
            set { base.SetValue(ref billCode, value, () => this.BillCode, false); }
        }

        private long reimburseDepartID;

        [DataMember]
        public long ReimburseDepartID
        {
            get { return reimburseDepartID; }
            set { base.SetValue(ref reimburseDepartID, value, () => this.ReimburseDepartID, false); }
        }

        private string reimburseDepartName;

        [DataMember]
        public string ReimburseDepartName
        {
            get { return reimburseDepartName; }
            set { base.SetValue(ref reimburseDepartName, value, () => this.ReimburseDepartName, false); }
        }

        private string factoryCode;

        [DataMember]
        public string FactoryCode
        {
            get { return factoryCode; }
            set { base.SetValue(ref factoryCode, value, () => this.FactoryCode, false); }
        }

        private long errorDicID;

        [DataMember]
        public long ErrorDicID
        {
            get { return errorDicID; }
            set { base.SetValue(ref errorDicID, value, () => this.ErrorDicID, false); }
        }

        private string errorAppearance;

        [DataMember]
        public string ErrorAppearance
        {
            get { return errorAppearance; }
            set { base.SetValue(ref errorAppearance, value, () => this.ErrorAppearance, false); }
        }

        private string errorDescribe;

        [DataMember]
        public string ErrorDescribe
        {
            get { return errorDescribe; }
            set { base.SetValue(ref errorDescribe, value, () => this.ErrorDescribe, false); }
        }

        private string repairMan;

        [DataMember]
        public string RepairMan
        {
            get { return repairMan; }
            set { base.SetValue(ref repairMan, value, () => this.RepairMan, false); }
        }

        private long repairTypeID;

        [DataMember]
        public long RepairTypeID
        {
            get { return repairTypeID; }
            set { base.SetValue(ref repairTypeID, value, () => this.RepairTypeID, false); }
        }

        private double repairFee;

        [DataMember]
        public double RepairFee
        {
            get { return repairFee; }
            set { base.SetValue(ref repairFee, value, () => this.RepairFee, false); }
        }

        private double repairHours;

        [DataMember]
        public double RepairHours
        {
            get { return repairHours; }
            set { base.SetValue(ref repairHours, value, () => this.RepairHours, false); }
        }

        private DateTime repairStartTime;

        [DataMember]
        public DateTime RepairStartTime
        {
            get { return repairStartTime; }
            set { base.SetValue(ref repairStartTime, value, () => this.RepairStartTime, false); }
        }

        private DateTime repairEndTime;

        [DataMember]
        public DateTime RepairEndTime
        {
            get { return repairEndTime; }
            set { base.SetValue(ref repairEndTime, value, () => this.RepairEndTime, false); }
        }

        private double repairTerm;

        [DataMember]
        public double RepairTerm
        {
            get { return repairTerm; }
            set { base.SetValue(ref repairTerm, value, () => this.RepairTerm, false); }
        }

        private long equipStatusID;

        [DataMember]
        public long EquipStatusID
        {
            get { return equipStatusID; }
            set { base.SetValue(ref equipStatusID, value, () => this.EquipStatusID, false); }
        }

        private string recorder;

        [DataMember]
        public string Recorder
        {
            get { return recorder; }
            set { base.SetValue(ref recorder, value, () => this.Recorder, false); }
        }

        private DateTime recordTime;

        [DataMember]
        public DateTime RecordTime
        {
            get { return recordTime; }
            set { base.SetValue(ref recordTime, value, () => this.RecordTime, false); }
        }

        private string repairStatus;

        [DataMember]
        public string RepairStatus
        {
            get { return repairStatus; }
            set { base.SetValue(ref repairStatus, value, () => this.RepairStatus, false); }
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