using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]

    /// 故障字典表
    public class ErrorDictionary : ZNC.Utility.DataModelBase
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

        private string errorName;

        [DataMember]
        public string ErrorName
        {
            get { return errorName; }
            set { base.SetValue(ref errorName, value, () => this.ErrorName, false); }
        }

        private string errorDescribe;

        [DataMember]
        public string ErrorDescribe
        {
            get { return errorDescribe; }
            set { base.SetValue(ref errorDescribe, value, () => this.ErrorDescribe, false); }
        }

        private string solution;

        [DataMember]
        public string Solution
        {
            get { return solution; }
            set { base.SetValue(ref solution, value, () => this.Solution, false); }
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

        private string remark;

        [DataMember]
        public string Remark
        {
            get { return remark; }
            set { base.SetValue(ref remark, value, () => this.Remark, false); }
        }

    }
}