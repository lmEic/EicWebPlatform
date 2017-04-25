using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
    /// <summary>
    /// 
    /// </summary>
    public class RmaReportInitiateModel
    {
        #region Model
        private string _rmaid;
        /// <summary>
        ///单号
        /// </summary>
        public string RmaId
        {
            set { _rmaid = value; }
            get { return _rmaid; }
        }
        private string _customershortname;
        /// <summary>
        ///客户简称
        /// </summary>
        public string CustomerShortName
        {
            set { _customershortname = value; }
            get { return _customershortname; }
        }
        private string _productname;
        /// <summary>
        ///品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _rmaidstatus;
        /// <summary>
        ///单号状态
        /// </summary>
        public string RmaIdStatus
        {
            set { _rmaidstatus = value; }
            get { return _rmaidstatus; }
        }
        private string _rmayear;
        /// <summary>
        ///年份
        /// </summary>
        public string RmaYear
        {
            set { _rmayear = value; }
            get { return _rmayear; }
        }
        private string _rmamonth;
        /// <summary>
        ///月份
        /// </summary>
        public string RmaMonth
        {
            set { _rmamonth = value; }
            get { return _rmamonth; }
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
    /// 
    /// </summary>
    public class RmaBussesDescriptionModel
    {
        #region Model
        private string _rmaid;
        /// <summary>
        ///单号
        /// </summary>
        public string RmaId
        {
            set { _rmaid = value; }
            get { return _rmaid; }
        }
        private int _rmaidnumber;
        /// <summary>
        ///单号的序号
        /// </summary>
        public int RmaIdNumber
        {
            set { _rmaidnumber = value; }
            get { return _rmaidnumber; }
        }
        private string _returnhandleorder;
        /// <summary>
        ///退销单
        /// </summary>
        public string ReturnHandleOrder
        {
            set { _returnhandleorder = value; }
            get { return _returnhandleorder; }
        }
        private string _prodcutid;
        /// <summary>
        ///料号
        /// </summary>
        public string ProdcutId
        {
            set { _prodcutid = value; }
            get { return _prodcutid; }
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
        private double _productcount;
        /// <summary>
        ///数量
        /// </summary>
        public double ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        private string _salesorder;
        /// <summary>
        ///订单号
        /// </summary>
        public string SalesOrder
        {
            set { _salesorder = value; }
            get { return _salesorder; }
        }
        private string _productsshipdate;
        /// <summary>
        ///原出货日期
        /// </summary>
        public string ProductsShipDate
        {
            set { _productsshipdate = value; }
            get { return _productsshipdate; }
        }
        private string _badreason;
        /// <summary>
        ///不良原因
        /// </summary>
        public string BadReason
        {
            set { _badreason = value; }
            get { return _badreason; }
        }
        private string _customerhandleidea;
        /// <summary>
        ///客户处理意见
        /// </summary>
        public string CustomerHandleIdea
        {
            set { _customerhandleidea = value; }
            get { return _customerhandleidea; }
        }
        private string _feepaymentway;
        /// <summary>
        ///付费方工式
        /// </summary>
        public string FeePaymentWay
        {
            set { _feepaymentway = value; }
            get { return _feepaymentway; }
        }
        private string _rmaidstatus;
        /// <summary>
        ///单号状态
        /// </summary>
        public string RmaIdStatus
        {
            set { _rmaidstatus = value; }
            get { return _rmaidstatus; }
        }
        private string _handlestatus;
        /// <summary>
        ///处理状态
        /// </summary>
        public string HandleStatus
        {
            set { _handlestatus = value; }
            get { return _handlestatus; }
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
    /// 
    /// </summary>
    public class RmaInspectionManageModel
    {
        #region Model
        private string _rmaid;
        /// <summary>
        ///单号
        /// </summary>
        public string RmaId
        {
            set { _rmaid = value; }
            get { return _rmaid; }
        }
        private int _rmaidnumber;
        /// <summary>
        ///单号的序号
        /// </summary>
        public int RmaIdNumber
        {
            set { _rmaidnumber = value; }
            get { return _rmaidnumber; }
        }
        private string _badphenomenon;
        /// <summary>
        ///不良现象
        /// </summary>
        public string BadPhenomenon
        {
            set { _badphenomenon = value; }
            get { return _badphenomenon; }
        }
        private string _badreadson;
        /// <summary>
        ///不良原因
        /// </summary>
        public string BadReadson
        {
            set { _badreadson = value; }
            get { return _badreadson; }
        }
        private string _handlemode;
        /// <summary>
        ///处理方式
        /// </summary>
        public string HandleMode
        {
            set { _handlemode = value; }
            get { return _handlemode; }
        }
        private string _pesponsibleperson;
        /// <summary>
        ///负责人
        /// </summary>
        public string PesponsiblePerson
        {
            set { _pesponsibleperson = value; }
            get { return _pesponsibleperson; }
        }
        private string _finishdate;
        /// <summary>
        ///完成日期
        /// </summary>
        public string FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        private string _paytime;
        /// <summary>
        ///花费时间
        /// </summary>
        public string PayTime
        {
            set { _paytime = value; }
            get { return _paytime; }
        }
        private string _liabilitybelongto;
        /// <summary>
        ///责任归属
        /// </summary>
        public string LiabilityBelongTo
        {
            set { _liabilitybelongto = value; }
            get { return _liabilitybelongto; }
        }
        private string _handlestatus;
        /// <summary>
        ///处理状态
        /// </summary>
        public string HandleStatus
        {
            set { _handlestatus = value; }
            get { return _handlestatus; }
        }
        private string _rmaidstatus;
        /// <summary>
        ///单号状态
        /// </summary>
        public string RmaIdStatus
        {
            set { _rmaidstatus = value; }
            get { return _rmaidstatus; }
        }
        private string _opperson;
        /// <summary>
        ///操作人员
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
