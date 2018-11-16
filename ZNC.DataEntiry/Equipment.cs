using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 设备基本表
    public class Equipment : ZNC.Utility.DataModelBase
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

        private string factoryCode;

        [DataMember]
        public string FactoryCode
        {
            get { return factoryCode; }
            set { base.SetValue(ref factoryCode, value, () => this.FactoryCode, false); }
        }

        private long belongDepartID;

        [DataMember]
        public long BelongDepartID
        {
            get { return belongDepartID; }
            set { base.SetValue(ref belongDepartID, value, () => this.BelongDepartID, false); }
        }

        private long repairDepartID;

        [DataMember]
        public long RepairDepartID
        {
            get { return repairDepartID; }
            set { base.SetValue(ref repairDepartID, value, () => this.RepairDepartID, false); }
        }

        private long useDepartID;

        [DataMember]
        public long UseDepartID
        {
            get { return useDepartID; }
            set { base.SetValue(ref useDepartID, value, () => this.UseDepartID, false); }
        }

        private string usufruct;

        [DataMember]
        public string Usufruct
        {
            get { return usufruct; }
            set { base.SetValue(ref usufruct, value, () => this.Usufruct, false); }
        }

        private long sort;

        [DataMember]
        public long Sort
        {
            get { return sort; }
            set { base.SetValue(ref sort, value, () => this.Sort, false); }
        }

        private long enableStatus;

        [DataMember]
        public long EnableStatus
        {
            get { return enableStatus; }
            set { base.SetValue(ref enableStatus, value, () => this.EnableStatus, false); }
        }

        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { base.SetValue(ref name, value, () => this.Name, false); }
        }

        private long equipTypeID;

        [DataMember]
        public long EquipTypeID
        {
            get { return equipTypeID; }
            set { base.SetValue(ref equipTypeID, value, () => this.EquipTypeID, false); }
        }

        private string specification;

        [DataMember]
        public string Specification
        {
            get { return specification; }
            set { base.SetValue(ref specification, value, () => this.Specification, false); }
        }

        private string model;

        [DataMember]
        public string Model
        {
            get { return model; }
            set { base.SetValue(ref model, value, () => this.Model, false); }
        }

        private string manufacturer;

        [DataMember]
        public string Manufacturer
        {
            get { return manufacturer; }
            set { base.SetValue(ref manufacturer, value, () => this.Manufacturer, false); }
        }

        private string exFactoryNo;

        [DataMember]
        public string ExFactoryNo
        {
            get { return exFactoryNo; }
            set { base.SetValue(ref exFactoryNo, value, () => this.ExFactoryNo, false); }
        }

        private string manufactNo;

        [DataMember]
        public string ManufactNo
        {
            get { return manufactNo; }
            set { base.SetValue(ref manufactNo, value, () => this.ManufactNo, false); }
        }

        private DateTime manufactDate;

        [DataMember]
        public DateTime ManufactDate
        {
            get { return manufactDate; }
            set { base.SetValue(ref manufactDate, value, () => this.ManufactDate, false); }
        }

        private string weight;

        [DataMember]
        public string Weight
        {
            get { return weight; }
            set { base.SetValue(ref weight, value, () => this.Weight, false); }
        }

        private string installCapacity;

        [DataMember]
        public string InstallCapacity
        {
            get { return installCapacity; }
            set { base.SetValue(ref installCapacity, value, () => this.InstallCapacity, false); }
        }

        private string assetNumber;

        [DataMember]
        public string AssetNumber
        {
            get { return assetNumber; }
            set { base.SetValue(ref assetNumber, value, () => this.AssetNumber, false); }
        }

        private DateTime purchaseDate;

        [DataMember]
        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { base.SetValue(ref purchaseDate, value, () => this.PurchaseDate, false); }
        }

        private DateTime installDate;

        [DataMember]
        public DateTime InstallDate
        {
            get { return installDate; }
            set { base.SetValue(ref installDate, value, () => this.InstallDate, false); }
        }

        private DateTime workingDate;

        [DataMember]
        public DateTime WorkingDate
        {
            get { return workingDate; }
            set { base.SetValue(ref workingDate, value, () => this.WorkingDate, false); }
        }

        private DateTime toAssetDate;

        [DataMember]
        public DateTime ToAssetDate
        {
            get { return toAssetDate; }
            set { base.SetValue(ref toAssetDate, value, () => this.ToAssetDate, false); }
        }

        private long age;

        [DataMember]
        public long Age
        {
            get { return age; }
            set { base.SetValue(ref age, value, () => this.Age, false); }
        }

        private string location;

        [DataMember]
        public string Location
        {
            get { return location; }
            set { base.SetValue(ref location, value, () => this.Location, false); }
        }

        private string useProperty;

        [DataMember]
        public string UseProperty
        {
            get { return useProperty; }
            set { base.SetValue(ref useProperty, value, () => this.UseProperty, false); }
        }

        private string assetStatus;

        [DataMember]
        public string AssetStatus
        {
            get { return assetStatus; }
            set { base.SetValue(ref assetStatus, value, () => this.AssetStatus, false); }
        }

        private string manageType;

        [DataMember]
        public string ManageType
        {
            get { return manageType; }
            set { base.SetValue(ref manageType, value, () => this.ManageType, false); }
        }

        private double estimateLifeMonth;

        [DataMember]
        public double EstimateLifeMonth
        {
            get { return estimateLifeMonth; }
            set { base.SetValue(ref estimateLifeMonth, value, () => this.EstimateLifeMonth, false); }
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