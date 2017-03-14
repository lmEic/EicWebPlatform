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
        private int _startnumber;
        /// <summary>
        ///启始数
        /// </summary>
        public int StartNumber
        {
            set { _startnumber = value; }
            get { return _startnumber; }
        }
        private int _endnumber;
        /// <summary>
        ///结束数
        /// </summary>
        public int EndNumber
        {
            set { _endnumber = value; }
            get { return _endnumber; }
        }
        private int _inspectioncount;
        /// <summary>
        ///检验数
        /// </summary>
        public int InspectionCount
        {
            set { _inspectioncount = value; }
            get { return _inspectioncount; }
        }
        private int _acceptcount;
        /// <summary>
        ///接授数
        /// </summary>
        public int AcceptCount
        {
            set { _acceptcount = value; }
            get { return _acceptcount; }
        }
        private int _refusecount;
        /// <summary>
        ///拒授数
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
    ///  检验方式转换配置文件
    /// </summary>
    public class InspectionModeSwitchConfigModel
    {
        public InspectionModeSwitchConfigModel()
        { }
        #region Model
        private string _swithmemo;
        /// <summary>
        ///转换模式
        /// </summary>
        public string SwitChMemo
        {
            set { _swithmemo = value; }
            get { return _swithmemo; }
        }
        private string _swithcategory;
        /// <summary>
        ///类别
        /// </summary>
        public string SwitchCategory
        {
            set { _swithcategory = value; }
            get { return _swithcategory; }
        }
        private string _swithproperty;
        /// <summary>
        ///属性
        /// </summary>
        public string SwitchProperty
        {
            set { _swithproperty = value; }
            get { return _swithproperty; }
        }
        private string _isenable;
        /// <summary>
        ///是否启用
        /// </summary>
        public string IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }
        private string _currentstatus;
        /// <summary>
        ///当前状态
        /// </summary>
        public string CurrentStatus
        {
            set { _currentstatus = value; }
            get { return _currentstatus; }
        }
        private string _swithsatus;
        /// <summary>
        ///转换状态
        /// </summary>
        public string SwitchSatus
        {
            set { _swithsatus = value; }
            get { return _swithsatus; }
        }
        private int _swithvaule;
        /// <summary>
        ///值
        /// </summary>
        public int SwitchVaule
        {
            set { _swithvaule = value; }
            get { return _swithvaule; }
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

    #region IQC
    /// <summary>
    /// IQC物料检验配置文件
    /// </summary>
    public class InspectionIqCItemConfigModel
    {

        public InspectionIqCItemConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///料号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _inspectionitem;
        /// <summary>
        ///检验项目
        /// </summary>
        public string InspectionItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private int _inspectionitemindex;
        /// <summary>
        ///检验项目序号
        /// </summary>
        public int InspectionItemIndex
        {
            set { _inspectionitemindex = value; }
            get { return _inspectionitemindex; }
        }
        private double _sizeusl;
        /// <summary>
        ///上限
        /// </summary>
        public double SizeUSL
        {
            set { _sizeusl = value; }
            get { return _sizeusl; }
        }
        private double _sizelsl;
        /// <summary>
        ///下限
        /// </summary>
        public double SizeLSL
        {
            set { _sizelsl = value; }
            get { return _sizelsl; }
        }
        private string _sizememo;
        /// <summary>
        ///规格值
        /// </summary>
        public string SizeMemo
        {
            set { _sizememo = value; }
            get { return _sizememo; }
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
        private string _inspectiondatagathertype;
        /// <summary>
        ///数据采集类型
        /// </summary>
        public string InspectionDataGatherType
        {
            set { _inspectiondatagathertype = value; }
            get { return _inspectiondatagathertype; }
        }
        private string _equipmentid;
        /// <summary>
        ///测量工具
        /// </summary>
        public string EquipmentId
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        private string _sipinspectionstandard;
        /// <summary>
        ///Sip规格值
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
        ///检验AQl值
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
        ///自增健
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
    public class InspectionIqcMasterModel
    {
        public InspectionIqcMasterModel()
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
    /// IQC单据检验项目
    /// </summary>
    public class InspectionIqcDetailModel
    {
        public InspectionIqcDetailModel()
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

        private string _inspecitonitem;
        /// <summary>
        ///检验项目
        /// </summary>
        public string InspecitonItem
        {
            set { _inspecitonitem = value; }
            get { return _inspecitonitem; }
        }
        private string _inspectionMode;
        /// <summary>
        /// /检验方式
        /// </summary>
        public string InspectionMode
        {
            set { _inspectionMode = value; }
            get { return _inspectionMode; }
        }
        private double _materialcount;
        /// <summary>
        ///物料进货数量
        /// </summary>
        public double MaterialCount
        {
            set { _materialcount = value; }
            get { return _materialcount; }
        }
        private DateTime _materialindate;
        /// <summary>
        ///物料进货日期
        /// </summary>
        public DateTime MaterialInDate
        {
            set { _materialindate = value; }
            get { return _materialindate; }
        }
        private string _equipmentid;
        /// <summary>
        ///量测工个财产编号
        /// </summary>
        public string EquipmentId
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        private double _inspectioncount;
        /// <summary>
        ///检验数量
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
        ///检验记录数据
        /// </summary>
        public string InspectionItemDatas
        {
            set { _inspectionitemdatas = value; }
            get { return _inspectionitemdatas; }
        }
        private string _inspectionitemsatus;
        /// <summary>
        ///检验项记录状态
        /// </summary>
        public string InspectionItemSatus
        {
            set { _inspectionitemsatus = value; }
            get { return _inspectionitemsatus; }
        }
        private string _inspectionitemresult;
        /// <summary>
        ///检验项最后结果
        /// </summary>
        public string InspectionItemResult
        {
            set { _inspectionitemresult = value; }
            get { return _inspectionitemresult; }
        }
        private DateTime _inspectiondate;
        /// <summary>
        ///检验的日期
        /// </summary>
        public DateTime InspectionDate
        {
            set { _inspectiondate = value; }
            get { return _inspectiondate; }
        }
        private string _memo;
        /// <summary>
        ///备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
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



    public class InspectionIqcItemDataSummaryLabelModel
    {
        #region Model
        /// <summary>
        ///单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        ///料号
        /// </summary>
        public string MaterialId { get; set; }
        /// <summary>
        /// 物料进货日期
        /// </summary>
        public DateTime  MaterialInDate { set; get; }
        /// <summary>
        /// 物料进货数量
        /// </summary>
        public double   MaterialInCount { set; get; }
        /// <summary>
        /// 数据采集类型
        /// </summary>
         public string InspectionDataGatherType { set; get; }
        /// <summary>
        /// 测量量具财产编号
        /// </summary>
        public string EquipmentId { set; get; }
        /// <summary>
        ///检验项目
        /// </summary>
        public string InspectionItem { get; set; }


        /// <summary>
        ///检验方法
        /// </summary>
        public string InspectionMethod{ set; get;}
        /// <summary>
        ///检验方式
        /// </summary>
        public string InspectionMode { get; set; }
        /// <summary>
        ///检验水平
        /// </summary>
        public string InspectionLevel { get; set; }
        /// <summary>
        ///检验AQL值
        /// </summary>
        public string InspectionAQL { get; set; }
        /// <summary>
        ///检验数
        /// </summary>
        public int InspectionCount { get; set; }
        /// <summary>
        ///接授数
        /// </summary>
        public int AcceptCount { get; set; }
        /// <summary>
        ///拒授数
        /// </summary>
        public int RefuseCount { get; set; }
        /// <summary>
        /// 规格上限
        /// </summary>
        public double SizeUSL { get; set; }
        /// <summary>
        ///下限
        /// </summary>
        public double SizeLSL { set; get; }

        /// <summary>
        ///规格值 说明
        /// </summary>
        public string SizeMemo {set; get; }
        /// <summary>
        ///检验所有所得的数据
        /// </summary>
        public string InspectionItemDatas { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Memo { set; get; }

        /// <summary>
        /// 需要完成数据数量
        /// </summary>
        public int NeedFinishDataNumber { set; get; }
        /// <summary>
        /// 已经完成数据数量
        /// </summary>
        public int HaveFinishDataNumber { set; get; }
        /// <summary>
        /// 检验结果Ok 还是Ng  初始值为空
        /// </summary>
        public string InspectionItemResult { get; set; }
        /// <summary>
        ///此项的检验状态(完成True，未完成False)
        /// </summary>
        public bool InsptecitonItemIsFinished { get; set; }

     
        /// <summary>
        ///加载的自增健
        /// </summary>
        public decimal Id_Key{ set;get; }

        #endregion Model
    }


}
