using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]
    public class Dictionary : ZNC.Utility.DataModelBase
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

        private string innerCode;

        [DataMember]
        public string InnerCode
        {
            get { return innerCode; }
            set { base.SetValue(ref innerCode, value, () => this.InnerCode, false); }
        }

        private string type;

        [DataMember]
        public string Type
        {
            get { return type; }
            set { base.SetValue(ref type, value, () => this.Type, false); }
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

    }
}