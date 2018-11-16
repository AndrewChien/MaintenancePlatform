using System;
using System.Runtime.Serialization;

namespace ZNC.DataEntiry
{
    [Serializable]
    [DataContract]
    public class FuncModule : ZNC.Utility.DataModelBase
    {
        public FuncModule()
        { }

        // 用户编号
        private long _ID;
        [DataMember]
        public long ID
        {
            get { return _ID; }
            set { base.SetValue(ref _ID, value, () => this.ID, false); }
        }
        // 功能编号
        private string _FuncID;
        [DataMember]
        public string FuncID
        {
            get { return _FuncID; }
            set { base.SetValue(ref _FuncID, value, () => this.FuncID, false); }
        }
        // 关联系统
        private long _SysID;
        [DataMember]
        public long SysID
        {
            get { return _SysID; }
            set { base.SetValue(ref _SysID, value, () => this.SysID, false); }
        }
        // 功能名称
        private string _FuncName;
        [DataMember]
        public string FuncName
        {
            get { return _FuncName; }
            set { base.SetValue(ref _FuncName, value, () => this.FuncName, false); }
        }
        // 上级功能编号
        private string _PreFuncID;
        [DataMember]
        public string PreFuncID
        {
            get { return _PreFuncID; }
            set { base.SetValue(ref _PreFuncID, value, () => this.PreFuncID, false); }
        }
        // 功能链接
        private long _Grade;
        [DataMember]
        public long Grade
        {
            get { return _Grade; }
            set { base.SetValue(ref _Grade, value, () => this.Grade, false); }
        }
        // 功能图片
        private long _IsFinalGrade;
        [DataMember]
        public long IsFinalGrade
        {
            get { return _IsFinalGrade; }
            set { base.SetValue(ref _IsFinalGrade, value, () => this.IsFinalGrade, false); }
        }
        // 注销标志
        private long _Invalid;
        [DataMember]
        public long Invalid
        {
            get { return _Invalid; }
            set { base.SetValue(ref _Invalid, value, () => this.Invalid, false); }
        }

        // 功能图片
        private string _Path;
        [DataMember]
        public string Path
        {
            get { return _Path; }
            set { base.SetValue(ref _Path, value, () => this.Path, false); }
        }

        // 功能图片
        private string _FuncImg;
        [DataMember]
        public string FuncImg
        {
            get { return _FuncImg; }
            set { base.SetValue(ref _FuncImg, value, () => this.FuncImg, false); }
        }

        // 排序
        private long _SortID;
        [DataMember]
        public long SortID
        {
            get { return _SortID; }
            set { base.SetValue(ref _SortID, value, () => this.SortID, false); }
        }

        // 系统名称
        private string _SysName;
        [DataMember]
        public string SysName
        {
            get { return _SysName; }
            set { base.SetValue(ref _SysName, value, () => this.SysName, false); }
        }

        // 按钮样式
        private string _Style;
        [DataMember]
        public string Style
        {
            get { return _Style; }
            set { base.SetValue(ref _Style, value, () => this.Style, false); }
        }
    }

    //public class LOCATE_GNMK : EWell.Component.DataModelBase
    //{
    //    public LOCATE_GNMK()
    //    { }

    //    /// <summary>
    //    /// 功能ID
    //    /// </summary>
    //    private int _GNID;
    //    [DataMember]
    //    public int GNID
    //    {
    //        get { return _GNID; }
    //        set { base.SetValue(ref _GNID, value, () => this.GNID, false); }
    //    }
    //    // 关联系统
    //    private int _GLXT;
    //    [DataMember]
    //    public int GLXT
    //    {
    //        get { return _GLXT; }
    //        set { base.SetValue(ref _GLXT, value, () => this.GLXT, false); }
    //    }
    //    // 功能编号
    //    private string _GNBH;
    //    [DataMember]
    //    public string GNBH
    //    {
    //        get { return _GNBH; }
    //        set { base.SetValue(ref _GNBH, value, () => this.GNBH, false); }
    //    }
    //    // 功能名称
    //    private string _GNMC;
    //    [DataMember]
    //    public string GNMC
    //    {
    //        get { return _GNMC; }
    //        set { base.SetValue(ref _GNMC, value, () => this.GNMC, false); }
    //    }
    //    // 上级功能编号
    //    private string _SJBH;
    //    [DataMember]
    //    public string SJBH
    //    {
    //        get { return _SJBH; }
    //        set { base.SetValue(ref _SJBH, value, () => this.SJBH, false); }
    //    }
    //    // 功能链接
    //    private string _GNLJ;
    //    [DataMember]
    //    public string GNLJ
    //    {
    //        get { return _GNLJ; }
    //        set { base.SetValue(ref _GNLJ, value, () => this.GNLJ, false); }
    //    }
    //    // 功能图片
    //    private string _PIC;
    //    [DataMember]
    //    public string PIC
    //    {
    //        get { return _PIC; }
    //        set { base.SetValue(ref _PIC, value, () => this.PIC, false); }
    //    }
    //    // 注销标志
    //    private int _ZXBZ;
    //    [DataMember]
    //    public int ZXBZ
    //    {
    //        get { return _ZXBZ; }
    //        set { base.SetValue(ref _ZXBZ, value, () => this.ZXBZ, false); }
    //    }
    //}
}
