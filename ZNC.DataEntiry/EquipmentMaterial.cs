using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 设备资料表
    public class EquipmentMaterial : ZNC.Utility.DataModelBase
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

        private string materialName1;

        [DataMember]
        public string MaterialName1
        {
            get { return materialName1; }
            set { base.SetValue(ref materialName1, value, () => this.MaterialName1, false); }
        }

        private long materialNum1;

        [DataMember]
        public long MaterialNum1
        {
            get { return materialNum1; }
            set { base.SetValue(ref materialNum1, value, () => this.MaterialNum1, false); }
        }

        private string materialAddr1;

        [DataMember]
        public string MaterialAddr1
        {
            get { return materialAddr1; }
            set { base.SetValue(ref materialAddr1, value, () => this.MaterialAddr1, false); }
        }

        private string materialName2;

        [DataMember]
        public string MaterialName2
        {
            get { return materialName2; }
            set { base.SetValue(ref materialName2, value, () => this.MaterialName2, false); }
        }

        private long materialNum2;

        [DataMember]
        public long MaterialNum2
        {
            get { return materialNum2; }
            set { base.SetValue(ref materialNum2, value, () => this.MaterialNum2, false); }
        }

        private string materialAddr2;

        [DataMember]
        public string MaterialAddr2
        {
            get { return materialAddr2; }
            set { base.SetValue(ref materialAddr2, value, () => this.MaterialAddr2, false); }
        }

        private string materialName3;

        [DataMember]
        public string MaterialName3
        {
            get { return materialName3; }
            set { base.SetValue(ref materialName3, value, () => this.MaterialName3, false); }
        }

        private long materialNum3;

        [DataMember]
        public long MaterialNum3
        {
            get { return materialNum3; }
            set { base.SetValue(ref materialNum3, value, () => this.MaterialNum3, false); }
        }

        private string materialAddr3;

        [DataMember]
        public string MaterialAddr3
        {
            get { return materialAddr3; }
            set { base.SetValue(ref materialAddr3, value, () => this.MaterialAddr3, false); }
        }

        private string materialName4;

        [DataMember]
        public string MaterialName4
        {
            get { return materialName4; }
            set { base.SetValue(ref materialName4, value, () => this.MaterialName4, false); }
        }

        private long materialNum4;

        [DataMember]
        public long MaterialNum4
        {
            get { return materialNum4; }
            set { base.SetValue(ref materialNum4, value, () => this.MaterialNum4, false); }
        }

        private string materialAddr4;

        [DataMember]
        public string MaterialAddr4
        {
            get { return materialAddr4; }
            set { base.SetValue(ref materialAddr4, value, () => this.MaterialAddr4, false); }
        }

        private string materialName5;

        [DataMember]
        public string MaterialName5
        {
            get { return materialName5; }
            set { base.SetValue(ref materialName5, value, () => this.MaterialName5, false); }
        }

        private long materialNum5;

        [DataMember]
        public long MaterialNum5
        {
            get { return materialNum5; }
            set { base.SetValue(ref materialNum5, value, () => this.MaterialNum5, false); }
        }

        private string materialAddr5;

        [DataMember]
        public string MaterialAddr5
        {
            get { return materialAddr5; }
            set { base.SetValue(ref materialAddr5, value, () => this.MaterialAddr5, false); }
        }

        private string relatedDocuments;

        [DataMember]
        public string RelatedDocuments
        {
            get { return relatedDocuments; }
            set { base.SetValue(ref relatedDocuments, value, () => this.RelatedDocuments, false); }
        }

        private string equipPicture;

        [DataMember]
        public string EquipPicture
        {
            get { return equipPicture; }
            set { base.SetValue(ref equipPicture, value, () => this.EquipPicture, false); }
        }

        private string enclosure;

        [DataMember]
        public string Enclosure
        {
            get { return enclosure; }
            set { base.SetValue(ref enclosure, value, () => this.Enclosure, false); }
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