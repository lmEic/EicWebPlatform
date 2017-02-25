using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
  

        /// <summary>
        /// 检验方式配置文件
        /// </summary>
        public class InspectionModeConfigModel
        {
            public InspectionModeConfigModel()
            { }
            #region Model
            private string _inspectionmode;
            /// <summary>
            ///检测方式
            /// </summary>
            public string InspectionMode
            {
                set { _inspectionmode = value; }
                get { return _inspectionmode; }
            }
            private string _inspectionlevel;
            /// <summary>
            ///检测水平
            /// </summary>
            public string InspectionLevel
            {
                set { _inspectionlevel = value; }
                get { return _inspectionlevel; }
            }
            private string _inspectionaql;
            /// <summary>
            ///AQL值
            /// </summary>
            public string InspectionAQL
            {
                set { _inspectionaql = value; }
                get { return _inspectionaql; }
            }
            private int _startnumber;
            /// <summary>
            ///起始值
            /// </summary>
            public int StartNumber
            {
                set { _startnumber = value; }
                get { return _startnumber; }
            }
            private int _endnumber;
            /// <summary>
            ///结束值
            /// </summary>
            public int EndNumber
            {
                set { _endnumber = value; }
                get { return _endnumber; }
            }
            private int _inspectioncount;
            /// <summary>
            ///检验数量
            /// </summary>
            public int InspectionCount
            {
                set { _inspectioncount = value; }
                get { return _inspectioncount; }
            }
            private int _acceptcount;
            /// <summary>
            ///接授数量
            /// </summary>
            public int AcceptCount
            {
                set { _acceptcount = value; }
                get { return _acceptcount; }
            }
            private int _refusecount;
            /// <summary>
            ///拒收数量
            /// </summary>
            public int RefuseCount
            {
                set { _refusecount = value; }
                get { return _refusecount; }
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
            public string OPSign
            {
                set { _opsign = value; }
                get { return _opsign; }
            }
            private decimal _id_Key;
            /// <summary>
            ///自增建
            /// </summary>
            public decimal Id_Key
            {
                set { _id_Key = value; }
                get { return _id_Key; }
            }
            #endregion Model
        }


    #region IQC
    /// <summary>
    /// IQC物料检验配置文件
    /// </summary>
    public class IqcInspectionItemConfigModel
    {

        public IqcInspectionItemConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///物料料号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _inspectionitem;
        /// <summary>
        ///物料检验项目
        /// </summary>
        public string InspectionItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private int _inspectionItemIndex;
        /// <summary>
        ///检验项目的次序
        /// </summary>
        public int InspectionItemIndex
        {
            set { _inspectionItemIndex = value; }
            get { return _inspectionItemIndex; }
        }
        private double _sizeusl;
        /// <summary>
        ///规格上限
        /// </summary>
        public double SizeUSL
        {
            set { _sizeusl = value; }
            get { return _sizeusl; }
        }
        private double _sizelsl;
        /// <summary>
        ///规格上限
        /// </summary>
        public double SizeLSL
        {
            set { _sizelsl = value; }
            get { return _sizelsl; }
        }
        private string _sizememo;
        /// <summary>
        ///规格说明
        /// </summary>
        public string SizeMemo
        {
            set { _sizememo = value; }
            get { return _sizememo; }
        }
        private string _equipmentid;
        /// <summary>
        ///量具编号
        /// </summary>
        public string EquipmentID
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        private string _inspectionmethod;
        /// <summary>
        ///检验方法
        /// </summary>
        public string InspectionMethod
        {
            set { _inspectionmethod = value; }
            get { return _inspectionmethod; }
        }
        private string _sipinspectionstandard;
        /// <summary>
        ///SIP检验规范
        /// </summary>
        public string SIPInspectionStandard
        {
            set { _sipinspectionstandard = value; }
            get { return _sipinspectionstandard; }
        }
        private string _inspectionmode;
        /// <summary>
        ///检验方式
        /// </summary>
        public string InspectionMode
        {
            set { _inspectionmode = value; }
            get { return _inspectionmode; }
        }
        private string _inspectionlevel;
        /// <summary>
        ///检验水平
        /// </summary>
        public string InspectionLevel
        {
            set { _inspectionlevel = value; }
            get { return _inspectionlevel; }
        }
        private string _inspectionaql;
        /// <summary>
        ///检验AQL值
        /// </summary>
        public string InspectionAQL
        {
            set { _inspectionaql = value; }
            get { return _inspectionaql; }
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
    /// IQC单据检验
    /// </summary>
    public class IqcInspectionMasterModel
        {
            public IqcInspectionMasterModel()
            { }
            #region Model
            private string _orderid;
            /// <summary>
            ///单号
            /// </summary>
            public string OrderId
            {
                set { _orderid = value; }
                get { return _orderid; }
            }
            private string _materialid;
            /// <summary>
            ///料号
            /// </summary>
            public string MaterialId
            {
                set { _materialid = value; }
                get { return _materialid; }
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
            private string _materialsupplier;
            /// <summary>
            ///供应商
            /// </summary>
            public string MaterialSupplier
            {
                set { _materialsupplier = value; }
                get { return _materialsupplier; }
            }
            private DateTime _materialindate;
            /// <summary>
            ///进货日期
            /// </summary>
            public DateTime MaterialInDate
            {
                set { _materialindate = value; }
                get { return _materialindate; }
            }
            private string _materialdrawid;
            /// <summary>
            ///图号
            /// </summary>
            public string MaterialDrawId
            {
                set { _materialdrawid = value; }
                get { return _materialdrawid; }
            }
            private double _materialcount;
            /// <summary>
            ///进货数量
            /// </summary>
            public double MaterialCount
            {
                set { _materialcount = value; }
                get { return _materialcount; }
            }
            private string _inspectionmode;
            /// <summary>
            ///检测方式
            /// </summary>
            public string InspectionMode
            {
                set { _inspectionmode = value; }
                get { return _inspectionmode; }
            }
            private string _inspctionresult;
            /// <summary>
            ///检测结果
            /// </summary>
            public string InspctionResult
            {
                set { _inspctionresult = value; }
                get { return _inspctionresult; }
            }
            private string _inspectionstatus;
            /// <summary>
            ///检测状态
            /// </summary>
            public string InspectionStatus
            {
                set { _inspectionstatus = value; }
                get { return _inspectionstatus; }
            }
            private string _inspectionitems;
            /// <summary>
            ///检测项目列表
            /// </summary>
            public string InspectionItems
            {
                set { _inspectionitems = value; }
                get { return _inspectionitems; }
            }
            private DateTime _finishdate;
            /// <summary>
            ///检测日期
            /// </summary>
            public DateTime FinishDate
            {
                set { _finishdate = value; }
                get { return _finishdate; }
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
        /// IQC单据检验项目
        /// </summary>
     public class IqcInspectionDetailModel
        {
            public IqcInspectionDetailModel()
            { }
            #region Model
            private string _orderid;
            /// <summary>
            ///工单
            /// </summary>
            public string OrderId
            {
                set { _orderid = value; }
                get { return _orderid; }
            }
            private string _materialid;
            /// <summary>
            ///物料
            /// </summary>
            public string MaterialId
            {
                set { _materialid = value; }
                get { return _materialid; }
            }
            private double _materialcount;
            /// <summary>
            ///物料数量
            /// </summary>
            public double MaterialCount
            {
                set { _materialcount = value; }
                get { return _materialcount; }
            }
            private string _inprectionitem;
            /// <summary>
            ///检测的项目
            /// </summary>
            public string InprectionItem
            {
                set { _inprectionitem = value; }
                get { return _inprectionitem; }
            }
            private double _inspectioncount;
            /// <summary>
            ///检测数量
            /// </summary>
            public double InspectionCount
            {
                set { _inspectioncount = value; }
                get { return _inspectioncount; }
            }
            private double _inspectionacceptcount;
            /// <summary>
            ///接受数量
            /// </summary>
            public double InspectionAcceptCount
            {
                set { _inspectionacceptcount = value; }
                get { return _inspectionacceptcount; }
            }
            private double _inspectionrefusecount;
            /// <summary>
            ///拒受数量
            /// </summary>
            public double InspectionRefuseCount
            {
                set { _inspectionrefusecount = value; }
                get { return _inspectionrefusecount; }
            }
            private string _inspectionitemdatas;
            /// <summary>
            ///检测数据
            /// </summary>
            public string InspectionItemDatas
            {
                set { _inspectionitemdatas = value; }
                get { return _inspectionitemdatas; }
            }
            private string _insprectionitemsatus;
            /// <summary>
            ///检测状态
            /// </summary>
            public string InsprectionItemSatus
            {
                set { _insprectionitemsatus = value; }
                get { return _insprectionitemsatus; }
            }
            private string _insprectionitemresult;
            /// <summary>
            ///检测结果
            /// </summary>
            public string InsprectionItemResult
            {
                set { _insprectionitemresult = value; }
                get { return _insprectionitemresult; }
            }
            private DateTime _insprectiondate;
            /// <summary>
            ///检测日期
            /// </summary>
            public DateTime InsprectionDate
            {
                set { _insprectiondate = value; }
                get { return _insprectiondate; }
            }
            private string _more;
            /// <summary>
            ///备注
            /// </summary>
            public string More
            {
                set { _more = value; }
                get { return _more; }
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

    #endregion




    /// <summary>
    ///  产品物料信息
    /// </summary>
    public class ProductMaterailModel
    {

        /// <summary>
        /// 品号 MB001
        /// </summary>
        public string ProductMaterailId { get; set; }
        /// <summary>
        /// 品名 MB002
        /// </summary>
        public string MaterailName { get; set; }
        /// <summary>
        /// 规格 MB003
        /// </summary>
        public string MaterialSpecify { get; set; }
        /// <summary>
        /// 单位名称 MB004
        /// </summary>
        public string UnitedName { get; set; }
        /// <summary>
        /// 单位计量 MB015
        /// </summary>
        public string UniteCount { get; set; }
        /// <summary>
        /// 产品图号 MB029
        /// </summary>
        public string MaterialrawID { get; set; }
        /// <summary>
        /// 物料属于部门 TM068
        /// </summary>
        public string MaterialBelongDepartment
        { get; set; }
        /// <summary>
        /// 备注 TM028
        /// </summary>
        public string Memo { get; set; }

    }

    public class IqcInspectionItemConfigShowModel
    {

        public IqcInspectionItemConfigShowModel()
        {
            this.InspectionItemConfigModelList = new List<IqcInspectionItemConfigModel>();
            this.ProductMaterailModel = new Quanity.ProductMaterailModel();
        }
        #region Model
        /// <summary>
        /// 检验物料料号单头
        /// </summary>
        public ProductMaterailModel ProductMaterailModel { set; get; }

        /// <summary>
        /// 检验物料料号单身
        /// </summary>
        public List<IqcInspectionItemConfigModel> InspectionItemConfigModelList { set; get; }


        ///// <summary>
        ///// 品名 MB002
        ///// </summary>
        //public string MaterailName { get; set; }
        ///// <summary>
        ///// 规格 MB003
        ///// </summary>
        //public string MaterialSpecify { get; set; }
        ///// <summary>
        ///// 产品图号 MB029
        ///// </summary>
        //public string MaterialrawID { get; set; }
        ///// <summary>
        ///// 物料属于部门 TM068
        ///// </summary>
        //public string MaterialBelongDepartment
        //{ get; set; }
        ///// <summary>
        /////物料料号
        ///// </summary>
        //public string MaterialId { get; set; }
        ///// <summary>
        /////物料检验项目
        ///// </summary>
        //public string InspectionItem { get; set; }
        ///// <summary>
        /////检验项目的次序
        ///// </summary>
        //public int InspectiontermNumber { get; set; }
        ///// <summary>
        /////规格上限
        ///// </summary>
        //public double SizeUSL { get; set; }
        ///// <summary>
        /////规格上限
        ///// </summary>
        //public double SizeLSL { get; set; }
        ///// <summary>
        /////规格说明
        ///// </summary>
        //public string SizeMemo { get; set; }
        ///// <summary>
        /////量具编号
        ///// </summary>
        //public string EquipmentID { get; set; }
        ///// <summary>
        /////检验方法
        ///// </summary>
        //public string InspectionMethod { get; set; }
        ///// <summary>
        /////SIP检验规范
        ///// </summary>
        //public string SIPInspectionStandard { get; set; }
        ///// <summary>
        /////检验方式
        ///// </summary>
        //public string InspectionMode { get; set; }
        ///// <summary>
        /////检验水平
        ///// </summary>
        //public string InspectionLevel { get; set; }
        ///// <summary>
        /////检验AQL值
        ///// </summary>
        //public string InspectionAQL { get; set; }
        ///// <summary>
        /////操作人
        ///// </summary>
        //public string OpPerson { get; set; }
        ///// <summary>
        /////操作日期
        ///// </summary>
        //public DateTime OpDate { get; set; }
        ///// <summary>
        /////操作时间
        ///// </summary>
        //public DateTime OpTime { get; set; }
        ///// <summary>
        /////操作标识
        ///// </summary>
        //public string OpSign { get; set; }
        ///// <summary>
        /////自增键
        ///// </summary>
        //public decimal Id_Key { get; set; }

        #endregion Model
    }
}
