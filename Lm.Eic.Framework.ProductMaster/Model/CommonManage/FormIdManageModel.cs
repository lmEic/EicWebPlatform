using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.CommonManage
{
    /// <summary>
    ///表单编号管理模型
    /// </summary>
    [Serializable]
    public partial class FormIdManageModel
    {
        public FormIdManageModel()
        { }
        #region Model
        private string _modulename;
        /// <summary>
        ///模块名称
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        private string _formid;
        /// <summary>
        ///表单编号
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        private string _yearmonth;
        /// <summary>
        ///年月分
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
        }
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private DateTime _createdate;
        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        private string _formstatus;
        /// <summary>
        ///表单状态
        /// </summary>
        public string FormStatus
        {
            set { _formstatus = value; }
            get { return _formstatus; }
        }
        private int _subid;
        /// <summary>
        ///子序列号
        /// </summary>
        public int SubId
        {
            set { _subid = value; }
            get { return _subid; }
        }
        private string _primarykey;
        /// <summary>
        ///表查询主键
        /// </summary>
        public string PrimaryKey
        {
            set { _primarykey = value; }
            get { return _primarykey; }
        }
        private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
        }
        private DateTime _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OpTime
        {
            set { _optime = value; }
            get { return _optime; }
        }
        private string _opperson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
        }
        private string _opsign;
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private decimal _id_key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }

    /// <summary>
    ///表单附件管理模型
    /// </summary>
    [Serializable]
    public partial class FormAttachFileManageModel
    {
        public FormAttachFileManageModel()
        { }
        #region Model
        private string _modulename;
        /// <summary>
        ///模块名称
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        private string _formid;
        /// <summary>
        ///表单编号
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        private string _filename;
        /// <summary>
        ///文件名称
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        private string _documentfilepath;
        /// <summary>
        ///文档路径
        /// </summary>
        public string DocumentFilePath
        {
            set { _documentfilepath = value; }
            get { return _documentfilepath; }
        }
        private int _subid;
        /// <summary>
        ///子序号
        /// </summary>
        public int SubId
        {
            set { _subid = value; }
            get { return _subid; }
        }
        private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
        }
        private DateTime _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OpTime
        {
            set { _optime = value; }
            get { return _optime; }
        }
        private string _opperson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
        }
        private string _opsign;
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private decimal _id_key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
}
