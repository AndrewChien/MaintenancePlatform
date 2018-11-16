using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]
    public class AcquisitionSetting : ZNC.Utility.DataModelBase
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
   private string acquisitionName;
        [DataMember]
        public string AcquisitionName
        {
            get { return acquisitionName; }
            set { base.SetValue(ref acquisitionName, value, () => this.AcquisitionName, false); }
        }
   private long acquisitionTypeID;
        [DataMember]
        public long AcquisitionTypeID
        {
            get { return acquisitionTypeID; }
            set { base.SetValue(ref acquisitionTypeID, value, () => this.AcquisitionTypeID, false); }
        }
   private string col1;
        [DataMember]
        public string Col1
        {
            get { return col1; }
            set { base.SetValue(ref col1, value, () => this.Col1, false); }
        }
   private string col2;
        [DataMember]
        public string Col2
        {
            get { return col2; }
            set { base.SetValue(ref col2, value, () => this.Col2, false); }
        }
   private string col3;
        [DataMember]
        public string Col3
        {
            get { return col3; }
            set { base.SetValue(ref col3, value, () => this.Col3, false); }
        }
   private string col4;
        [DataMember]
        public string Col4
        {
            get { return col4; }
            set { base.SetValue(ref col4, value, () => this.Col4, false); }
        }
   private string col5;
        [DataMember]
        public string Col5
        {
            get { return col5; }
            set { base.SetValue(ref col5, value, () => this.Col5, false); }
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