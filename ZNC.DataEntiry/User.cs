using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 用户表
    public class User : ZNC.Utility.DataModelBase
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

        private long departmentID;

        [DataMember]
        public long DepartmentID
        {
            get { return departmentID; }
            set { base.SetValue(ref departmentID, value, () => this.DepartmentID, false); }
        }

        private string workNum;

        [DataMember]
        public string WorkNum
        {
            get { return workNum; }
            set { base.SetValue(ref workNum, value, () => this.WorkNum, false); }
        }

        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { base.SetValue(ref name, value, () => this.Name, false); }
        }

        private string phone;

        [DataMember]
        public string Phone
        {
            get { return phone; }
            set { base.SetValue(ref phone, value, () => this.Phone, false); }
        }

        private string job;

        [DataMember]
        public string Job
        {
            get { return job; }
            set { base.SetValue(ref job, value, () => this.Job, false); }
        }

        private long roleID;

        [DataMember]
        public long RoleID
        {
            get { return roleID; }
            set { base.SetValue(ref roleID, value, () => this.RoleID, false); }
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