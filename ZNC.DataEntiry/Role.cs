using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 角色表
    public class Role : ZNC.Utility.DataModelBase
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

        private long jurisdictionID;

        [DataMember]
        public long JurisdictionID
        {
            get { return jurisdictionID; }
            set { base.SetValue(ref jurisdictionID, value, () => this.JurisdictionID, false); }
        }

        private string menuPermission;

        [DataMember]
        public string MenuPermission
        {
            get { return menuPermission; }
            set { base.SetValue(ref menuPermission, value, () => this.MenuPermission, false); }
        }

        private string modulePermission;

        [DataMember]
        public string ModulePermission
        {
            get { return modulePermission; }
            set { base.SetValue(ref modulePermission, value, () => this.ModulePermission, false); }
        }

        private string sourcePermission;

        [DataMember]
        public string SourcePermission
        {
            get { return sourcePermission; }
            set { base.SetValue(ref sourcePermission, value, () => this.SourcePermission, false); }
        }

        private string cRUDPermission;

        [DataMember]
        public string CRUDPermission
        {
            get { return cRUDPermission; }
            set { base.SetValue(ref cRUDPermission, value, () => this.CRUDPermission, false); }
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