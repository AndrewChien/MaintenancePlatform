using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    public class Department : ZNC.Utility.DataModelBase
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

        private long uplevelCode;

        [DataMember]
        public long UplevelCode
        {
            get { return uplevelCode; }
            set { base.SetValue(ref uplevelCode, value, () => this.UplevelCode, false); }
        }

        private string uplevelName;

        [DataMember]
        public string UplevelName
        {
            get { return uplevelName; }
            set { base.SetValue(ref uplevelName, value, () => this.UplevelName, false); }
        }

        private string innerCode;

        [DataMember]
        public string InnerCode
        {
            get { return innerCode; }
            set { base.SetValue(ref innerCode, value, () => this.InnerCode, false); }
        }

        private string _IsCompany;

        [DataMember]
        public string IsCompany
        {
            get { return _IsCompany; }
            set { base.SetValue(ref _IsCompany, value, () => this.IsCompany, false); }
        }

        private string _IsDepartment;

        [DataMember]
        public string IsDepartment
        {
            get { return _IsDepartment; }
            set { base.SetValue(ref _IsDepartment, value, () => this.IsDepartment, false); }
        }

        private string _IsJob;
        
        [DataMember]
        public string IsJob
        {
            get { return _IsJob; }
            set { base.SetValue(ref _IsJob, value, () => this.IsJob, false); }
        }
    }
}