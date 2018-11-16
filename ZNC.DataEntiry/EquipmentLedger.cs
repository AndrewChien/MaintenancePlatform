using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]
    /// 设备台账表
    public class EquipmentLedger : ZNC.Utility.DataModelBase
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

        private long equipID;

        [DataMember]
        public long EquipID
        {
            get { return equipID; }
            set { base.SetValue(ref equipID, value, () => this.EquipID, false); }
        }

        private double purchaseFee;

        [DataMember]
        public double PurchaseFee
        {
            get { return purchaseFee; }
            set { base.SetValue(ref purchaseFee, value, () => this.PurchaseFee, false); }
        }

        private double freight;

        [DataMember]
        public double Freight
        {
            get { return freight; }
            set { base.SetValue(ref freight, value, () => this.Freight, false); }
        }

        private double installationFee;

        [DataMember]
        public double InstallationFee
        {
            get { return installationFee; }
            set { base.SetValue(ref installationFee, value, () => this.InstallationFee, false); }
        }

        private double assetOriginWorth;

        [DataMember]
        public double AssetOriginWorth
        {
            get { return assetOriginWorth; }
            set { base.SetValue(ref assetOriginWorth, value, () => this.AssetOriginWorth, false); }
        }

        private double assetNetWorth;

        [DataMember]
        public double AssetNetWorth
        {
            get { return assetNetWorth; }
            set { base.SetValue(ref assetNetWorth, value, () => this.AssetNetWorth, false); }
        }

        private double accumulatedDepreciation;

        [DataMember]
        public double AccumulatedDepreciation
        {
            get { return accumulatedDepreciation; }
            set { base.SetValue(ref accumulatedDepreciation, value, () => this.AccumulatedDepreciation, false); }
        }

        private string depreciationMethod;

        [DataMember]
        public string DepreciationMethod
        {
            get { return depreciationMethod; }
            set { base.SetValue(ref depreciationMethod, value, () => this.DepreciationMethod, false); }
        }

        private double depreciationLife;

        [DataMember]
        public double DepreciationLife
        {
            get { return depreciationLife; }
            set { base.SetValue(ref depreciationLife, value, () => this.DepreciationLife, false); }
        }

        private string fundsource;

        [DataMember]
        public string Fundsource
        {
            get { return fundsource; }
            set { base.SetValue(ref fundsource, value, () => this.Fundsource, false); }
        }

        private string assetType;

        [DataMember]
        public string AssetType
        {
            get { return assetType; }
            set { base.SetValue(ref assetType, value, () => this.AssetType, false); }
        }

    }
}