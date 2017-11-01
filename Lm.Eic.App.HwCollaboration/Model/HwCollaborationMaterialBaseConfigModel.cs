using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    ///华为协同物料基础配置模型
    /// </summary>
    [Serializable]
    public class HwCollaborationMaterialBaseConfigModel
    {
        public HwCollaborationMaterialBaseConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///物料编号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _materialname;
        /// <summary>
        ///物料名称
        /// </summary>
        public string MaterialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        private string _parentmaterialid;
        /// <summary>
        ///父阶物料编号
        /// </summary>
        public string ParentMaterialId
        {
            set { _parentmaterialid = value; }
            get { return _parentmaterialid; }
        }
        private int _displayorder;
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        private string _vendorproductmodel;
        /// <summary>
        ///供应商产品型号
        /// </summary>
        public string VendorProductModel
        {
            set { _vendorproductmodel = value; }
            get { return _vendorproductmodel; }
        }
        private string _vendoritemdesc;
        /// <summary>
        ///供应商物料描述
        /// </summary>
        public string VendorItemDesc
        {
            set { _vendoritemdesc = value; }
            get { return _vendoritemdesc; }
        }
        private string _itemcategory;
        /// <summary>
        ///物料小类
        /// </summary>
        public string ItemCategory
        {
            set { _itemcategory = value; }
            get { return _itemcategory; }
        }
        private string _customervendorcode;
        /// <summary>
        ///客户代码
        /// </summary>
        public string CustomerVendorCode
        {
            set { _customervendorcode = value; }
            get { return _customervendorcode; }
        }
        private string _customeritemcode;
        /// <summary>
        ///客户物料编码
        /// </summary>
        public string CustomerItemCode
        {
            set { _customeritemcode = value; }
            get { return _customeritemcode; }
        }
        private string _customerproductmodel;
        /// <summary>
        ///客户产品型号
        /// </summary>
        public string CustomerProductModel
        {
            set { _customerproductmodel = value; }
            get { return _customerproductmodel; }
        }
        private string _unitofmeasure;
        /// <summary>
        ///单位
        /// </summary>
        public string UnitOfMeasure
        {
            set { _unitofmeasure = value; }
            get { return _unitofmeasure; }
        }
        private string _inventorytype;
        /// <summary>
        ///供应商Item类别
        /// </summary>
        public string InventoryType
        {
            set { _inventorytype = value; }
            get { return _inventorytype; }
        }
        private double _goodpercent;
        /// <summary>
        ///良率
        /// </summary>
        public double GoodPercent
        {
            set { _goodpercent = value; }
            get { return _goodpercent; }
        }
        private double _leadtime;
        /// <summary>
        ///货期(天）
        /// </summary>
        public double LeadTime
        {
            set { _leadtime = value; }
            get { return _leadtime; }
        }
        private string _lifecyclestatus;
        /// <summary>
        ///生命周期状态
        /// </summary>
        public string LifeCycleStatus
        {
            set { _lifecyclestatus = value; }
            get { return _lifecyclestatus; }
        }
        private double _quantity;
        /// <summary>
        ///用量
        /// </summary>
        public double Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        private string _substitutegroup;
        /// <summary>
        ///替代组
        /// </summary>
        public string SubstituteGroup
        {
            set { _substitutegroup = value; }
            get { return _substitutegroup; }
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
