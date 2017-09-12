using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
    /// <summary>
    ///8D记录处理主表
    /// </summary>

    /// <summary>
    ///8D记录处理主表
    /// </summary>
    [Serializable]
    public partial class Qua8DReportMasterModel
    {
        public Qua8DReportMasterModel()
        { }
        #region Model
        private double _materialcount;
        /// <summary>
        ///
        /// </summary>
        public double MaterialCount
        {
            set { _materialcount = value; }
            get { return _materialcount; }
        }
        private string _materialcountunit;
        /// <summary>
        ///
        /// </summary>
        public string MaterialCountUnit
        {
            set { _materialcountunit = value; }
            get { return _materialcountunit; }
        }
        private int _inspectcount;
        /// <summary>
        ///
        /// </summary>
        public int InspectCount
        {
            set { _inspectcount = value; }
            get { return _inspectcount; }
        }
        private string _inspectcountunit;
        /// <summary>
        ///
        /// </summary>
        public string InspectCountUnit
        {
            set { _inspectcountunit = value; }
            get { return _inspectcountunit; }
        }
        private string _yearmonth;
        /// <summary>
        ///
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
        }
        private string _filepath;
        /// <summary>
        ///
        /// </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        private string _filename;
        /// <summary>
        ///
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
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
        ///负责单位
        /// </summary>
        public string AccountabilityDepartment
        {
            set { _accountabilitydepartment = value; }
            get { return _accountabilitydepartment; }
        }
        private string _orderid;
        /// <summary>
        ///产生8D来源的单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private string _materialname;
        /// <summary>
        ///品名
        /// </summary>
        public string MaterialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        private string _materialspec;
        /// <summary>
        ///规格
        /// </summary>
        public string MaterialSpec
        {
            set { _materialspec = value; }
            get { return _materialspec; }
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
        private string _failqtyunit;
        /// <summary>
        ///不良数量单位
        /// </summary>
        public string FailQtyUnit
        {
            set { _failqtyunit = value; }
            get { return _failqtyunit; }
        }
        private string _failclass;
        /// <summary>
        ///不良类型
        /// </summary>
        public string FailClass
        {
            set { _failclass = value; }
            get { return _failclass; }
        }
        private DateTime _createreportdate;
        /// <summary>
        ///产生8D的日期
        /// </summary>
        public DateTime CreateReportDate
        {
            set { _createreportdate = value; }
            get { return _createreportdate; }
        }
        private string _status;
        /// <summary>
        ///目前处理状态
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
    public partial class Qua8DReportDetailModel
    {
        public Qua8DReportDetailModel()
        { }
        #region Model
        private string _steptitle;
        /// <summary>
        ///标题
        /// </summary>
        public string StepTitle
        {
            set { _steptitle = value; }
            get { return _steptitle; }
        }
        private string _stephandlecontent;
        /// <summary>
        ///处理内容
        /// </summary>
        public string StepHandleContent
        {
            set { _stephandlecontent = value; }
            get { return _stephandlecontent; }
        }
        private string _department;
        /// <summary>
        ///处理部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _signaturepersons;
        /// <summary>
        ///签核人员
        /// </summary>
        public string SignaturePersons
        {
            set { _signaturepersons = value; }
            get { return _signaturepersons; }
        }
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
        private string _stepdescription;
        /// <summary>
        ///步骤描述
        /// </summary>
        public string StepDescription
        {
            set { _stepdescription = value; }
            get { return _stepdescription; }
        }
        private string _status;
        /// <summary>
        ///处理状态
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
    /// <summary>
    /// Show
    /// </summary>
    public class ShowStepViewModel
    {
        public ShowStepViewModel()
        {

        }
        bool _ischeck = true;
        public bool IsCheck
        {
            set { value = _ischeck; }
            get { return _ischeck; }
        }

        public string StepName
        {
            set;
            get;
        }

        public string StepTitle
        {
            set;
            get;
        }
        public string StepTitleConnect
        {
            get; set;
        }
        public int StepId
        {
            set;
            get;
        }

    }


}
