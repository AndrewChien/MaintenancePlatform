using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 系统模块表（菜单表）
    public class SystemModule : ZNC.Utility.DataModelBase
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

        private long sort;

        [DataMember]
        public long Sort
        {
            get { return sort; }
            set { base.SetValue(ref sort, value, () => this.Sort, false); }
        }

        private string url;

        [DataMember]
        public string Url
        {
            get { return url; }
            set { base.SetValue(ref url, value, () => this.Url, false); }
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

        private string picUrl;

        [DataMember]
        public string PicUrl
        {
            get { return picUrl; }
            set { base.SetValue(ref picUrl, value, () => this.PicUrl, false); }
        }
    }
}