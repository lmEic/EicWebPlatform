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
        private string _customerid;
        /// <summary>
        ///客户编号
        /// </summary>
        public string CustomerId
        {
            set { _customerid = value; }
            get { return _customerid; }
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
        private string _orderid;
        /// <summary>
        ///订单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private DateTime _productsshipdate;
        /// <summary>
        ///原出货日期
        /// </summary>
        public DateTime ProductsShipDate
        {
            set { _productsshipdate = value; }
            get { return _productsshipdate; }
        }
        private string _customerdemandmode;
        /// <summary>
        ///客户需求方式
        /// </summary>
        public string CustomerDemandMode
        {
            set { _customerdemandmode = value; }
            get { return _customerdemandmode; }
        }
        private DateTime _customerdemandhandledate;
        /// <summary>
        ///客户需求处理日期
        /// </summary>
        public DateTime CustomerDemandHandleDate
        {
            set { _customerdemandhandledate = value; }
            get { return _customerdemandhandledate; }
        }
        private string _feepaymentway;
        /// <summary>
        ///付费方式
        /// </summary>
        public string FeePaymentWay
        {
            set { _feepaymentway = value; }
            get { return _feepaymentway; }
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
        private double _rmasumcount;
        /// <summary>
        ///数量
        /// </summary>
        public double RmaSumCount
        {
            set { _rmasumcount = value; }
            get { return _rmasumcount; }
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
        private string _opsgin;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSgin
        {
            set { _opsgin = value; }
            get { return _opsgin; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
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
        private string _returndescrption;
        /// <summary>
        ///客户退货陈述
        /// </summary>
        public string ReturnDescrption
        {
            set { _returndescrption = value; }
            get { return _returndescrption; }
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
        private double _storehousereceivecount;
        /// <summary>
        ///仓管点
        /// </summary>
        public double StorehouseReceiveCount
        {
            set { _storehousereceivecount = value; }
            get { return _storehousereceivecount; }
        }
        private string _productpackunit;
        /// <summary>
        ///单位
        /// </summary>
        public string ProductPackUnit
        {
            set { _productpackunit = value; }
            get { return _productpackunit; }
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
        private string _opsgin;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSgin
        {
            set { _opsgin = value; }
            get { return _opsgin; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
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
        private string _liabilitybelongto;
        /// <summary>
        ///责任归属
        /// </summary>
        public string LiabilityBelongTo
        {
            set { _liabilitybelongto = value; }
            get { return _liabilitybelongto; }
        }
        private string _liabilitybelongdescription;
        /// <summary>
        ///责任归属具体单位
        /// </summary>
        public string LiabilityBelongDescription
        {
            set { _liabilitybelongdescription = value; }
            get { return _liabilitybelongdescription; }
        }
        private string _handlemodeproperty;
        /// <summary>
        ///处理方式
        /// </summary>
        public string HandleModeProperty
        {
            set { _handlemodeproperty = value; }
            get { return _handlemodeproperty; }
        }
        private string _handlemodedescription;
        /// <summary>
        ///处理具体方式
        /// </summary>
        public string HandleModeDescription
        {
            set { _handlemodedescription = value; }
            get { return _handlemodedescription; }
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
        private string _opsgin;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSgin
        {
            set { _opsgin = value; }
            get { return _opsgin; }
        }
        private decimal _id_Key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_Key = value; }
            get { return _id_Key; }
        }
        #endregion Model
    }
}
