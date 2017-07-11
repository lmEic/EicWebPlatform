using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
    /// <summary>
    ///8D记录处理主表
    /// </summary>
    [Serializable]
    public partial class EightReportMasterModel
    {
        public EightReportMasterModel()
        { }
        #region Model
        private string _reportid;
        /// <summary>
        ///编号
        /// </summary>
        public string ReportId
        {
            set { _reportid = value; }
            get { return _reportid; }
        }
        private string _discoverposition;
        /// <summary>
        ///来源
        /// </summary>
        public string DiscoverPosition
        {
            set { _discoverposition = value; }
            get { return _discoverposition; }
        }
        private string _accountabilitydepartment;
        /// <summary>
        ///负责部门
        /// </summary>
        public string AccountabilityDepartment
        {
            set { _accountabilitydepartment = value; }
            get { return _accountabilitydepartment; }
        }
        private string _productname;
        /// <summary>
        ///产品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productspec;
        /// <summary>
        ///产品规格
        /// </summary>
        public string ProductSpec
        {
            set { _productspec = value; }
            get { return _productspec; }
        }
        private string _inputhouseorder;
        /// <summary>
        ///交货单/入库单号
        /// </summary>
        public string InPutHouseOrder
        {
            set { _inputhouseorder = value; }
            get { return _inputhouseorder; }
        }
        private int _batchnumber;
        /// <summary>
        ///批量
        /// </summary>
        public int BatchNumber
        {
            set { _batchnumber = value; }
            get { return _batchnumber; }
        }
        private int _inspectnumber;
        /// <summary>
        ///抽检数量
        /// </summary>
        public int InspectNumber
        {
            set { _inspectnumber = value; }
            get { return _inspectnumber; }
        }
        private int _failqty;
        /// <summary>
        ///不良数
        /// </summary>
        public int FailQty
        {
            set { _failqty = value; }
            get { return _failqty; }
        }
        private string _failclass;
        /// <summary>
        ///不良类
        /// </summary>
        public string FailClass
        {
            set { _failclass = value; }
            get { return _failclass; }
        }
        private DateTime _createreportdate;
        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime CreateReportDate
        {
            set { _createreportdate = value; }
            get { return _createreportdate; }
        }
        private string _status;
        /// <summary>
        ///单号状态
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
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
        private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
        }
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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
    ///8D记录处理详表
    /// </summary>
    [Serializable]
    public partial class EightDReportDetailModel
    {
        public EightDReportDetailModel()
        { }
        #region Model
        private string _reportid;
        /// <summary>
        ///编号
        /// </summary>
        public string ReportId
        {
            set { _reportid = value; }
            get { return _reportid; }
        }
        private int _stepid;
        /// <summary>
        ///步骤
        /// </summary>
        public int StepId
        {
            set { _stepid = value; }
            get { return _stepid; }
        }
        private string _stepdescription;
        /// <summary>
        ///步骤描述
        /// </summary>
        public string StepDescription
        {
            set { _stepdescription = value; }
            get { return _stepdescription; }
        }
        private string _describetype;
        /// <summary>
        ///描述类型
        /// </summary>
        public string DescribeType
        {
            set { _describetype = value; }
            get { return _describetype; }
        }
        private string _describecontent;
        /// <summary>
        ///描述内容
        /// </summary>
        public string DescribeContent
        {
            set { _describecontent = value; }
            get { return _describecontent; }
        }
        private string _filepath;
        /// <summary>
        ///路径
        /// </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
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
        private string _aboutdepartment;
        /// <summary>
        ///单位部门
        /// </summary>
        public string AboutDepartment
        {
            set { _aboutdepartment = value; }
            get { return _aboutdepartment; }
        }
        private string _signaturepeoples;
        /// <summary>
        ///签名
        /// </summary>
        public string SignaturePeoples
        {
            set { _signaturepeoples = value; }
            get { return _signaturepeoples; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
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
