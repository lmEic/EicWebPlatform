using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Mes.Optical.ProductWip
{
    /// <summary>
    /// Wip_ProductedWipData:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductedWipDataModel
    {
        public ProductedWipDataModel()
        { }
        #region Model
        private string _departmentname;
        private DateTime _productdate;
        private string _classtype;
        private string _productstatus;
        private string _producttype;
        private string _productbigstation;
        private string _productstation;
        private string _workerid;
        private string _workername;
        private string _indepartment;
        private string _flowcardid;
        private int _goodcount;
        private string _memo;
        private DateTime _inputtime;
        private string _field1;
        private string _field2;
        private string _field3;
        private string _field4;
        private string _field5;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ProductDate
        {
            set { _productdate = value; }
            get { return _productdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductBigStation
        {
            set { _productbigstation = value; }
            get { return _productbigstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStation
        {
            set { _productstation = value; }
            get { return _productstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkerID
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InDepartment
        {
            set { _indepartment = value; }
            get { return _indepartment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FlowCardID
        {
            set { _flowcardid = value; }
            get { return _flowcardid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodCount
        {
            set { _goodcount = value; }
            get { return _goodcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InputTime
        {
            set { _inputtime = value; }
            get { return _inputtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Field1
        {
            set { _field1 = value; }
            get { return _field1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Field2
        {
            set { _field2 = value; }
            get { return _field2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Field3
        {
            set { _field3 = value; }
            get { return _field3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Field4
        {
            set { _field4 = value; }
            get { return _field4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Field5
        {
            set { _field5 = value; }
            get { return _field5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model

    }

    /// <summary>
    /// Wip_NormalFlowStationSet:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class WipNormalFlowStationSetModel
    {
        public WipNormalFlowStationSetModel()
        { }
        #region Model
        private int _flowid;
        private string _producttype;
        private string _productstation;
        private string _productstationdetail;
        private string _stationdetailprevious;
        private string _stationsign;
        private int _isvisible;
        private string _productstatus;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public int FlowID
        {
            set { _flowid = value; }
            get { return _flowid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStation
        {
            set { _productstation = value; }
            get { return _productstation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStationDetail
        {
            set { _productstationdetail = value; }
            get { return _productstationdetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StationDetailPrevious
        {
            set { _stationdetailprevious = value; }
            get { return _stationdetailprevious; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StationSign
        {
            set { _stationsign = value; }
            get { return _stationsign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsVisible
        {
            set { _isvisible = value; }
            get { return _isvisible; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model

    }
}
