using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
    #region  共公配置
    /// <summary>
    /// 检验方式配置文件
    /// </summary>
    [Serializable]
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
    [Serializable]
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
    /// <summary>
    ///测试核对模式
    /// </summary>
    [Serializable]
    public partial class InspectionItemConfigCheckModel
    {
        public InspectionItemConfigCheckModel()
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
        private string _materailname;
        /// <summary>
        ///品名
        /// </summary>
        public string MaterailName
        {
            set { _materailname = value; }
            get { return _materailname; }
        }
        private string _materialspecify;
        /// <summary>
        ///规格
        /// </summary>
        public string MaterialSpecify
        {
            set { _materialspecify = value; }
            get { return _materialspecify; }
        }
        private string _materialrawid;
        /// <summary>
        ///图号
        /// </summary>
        public string MaterialrawID
        {
            set { _materialrawid = value; }
            get { return _materialrawid; }
        }
        private string _materialbelongdepartment;
        /// <summary>
        ///部门
        /// </summary>
        public string MaterialBelongDepartment
        {
            set { _materialbelongdepartment = value; }
            get { return _materialbelongdepartment; }
        }
        private string _inspectionitemdatas;
        /// <summary>
        ///数据
        /// </summary>
        public string InspectionItemDatas
        {
            set { _inspectionitemdatas = value; }
            get { return _inspectionitemdatas; }
        }
        private DateTime _inspectioniteminputdate;
        /// <summary>
        ///项目
        /// </summary>
        public DateTime InspectionItemInPutDate
        {
            set { _inspectioniteminputdate = value; }
            get { return _inspectioniteminputdate; }
        }
        private int _inspectionitemcount;
        /// <summary>
        ///数据
        /// </summary>
        public int InspectionItemCount
        {
            set { _inspectionitemcount = value; }
            get { return _inspectionitemcount; }
        }
        private string _checkstatus;
        /// <summary>
        ///状态
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        private string _itemconfigversion;
        /// <summary>
        ///版本
        /// </summary>
        public string ItemConfigVersion
        {
            set { _itemconfigversion = value; }
            get { return _itemconfigversion; }
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
    #endregion


    #region IQC
    /// <summary>
    ///IQC物料检验配置文件
    /// </summary>
    [Serializable]
    public partial class InspectionIqcItemConfigModel
    {
        public InspectionIqcItemConfigModel()
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
        private DateTime _inspectioniteminputdate;
        /// <summary>
        ///录入日期
        /// </summary>
        public DateTime InspectionItemInPutDate
        {
            set { _inspectioniteminputdate = value; }
            get { return _inspectioniteminputdate; }
        }
        private string _isactivate;
        /// <summary>
        ///是否激活
        /// </summary>
        public string IsActivate
        {
            set { _isactivate = value; }
            get { return _isactivate; }
        }
        private string _checkstatus;
        /// <summary>
        ///审核状态
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        private string _checkperson;
        /// <summary>
        ///审核人
        /// </summary>
        public string CheckPerson
        {
            set { _checkperson = value; }
            get { return _checkperson; }
        }
        private DateTime _checkdate;
        /// <summary>
        ///审核日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        private string _configversion;
        /// <summary>
        ///配置版次
        /// </summary>
        public string ConfigVersion
        {
            set { _configversion = value; }
            get { return _configversion; }
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
        ///品名   MaterialName
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
        private string _inspectionresult;
        /// <summary>
        ///检测结果
        /// </summary>
        public string InspectionResult
        {
            set { _inspectionresult = value; }
            get { return _inspectionresult; }
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
        private string _inspectionitem;
        /// <summary>
        ///检验项目 inspectionItem  InspecitonItem
        /// </summary>
        public string InspecitonItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private double _materialcount;
        /// <summary>
        ///进料数量
        /// </summary>
        public double MaterialCount
        {
            set { _materialcount = value; }
            get { return _materialcount; }
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
        private DateTime _materialindate;
        /// <summary>
        ///进料日期
        /// </summary>
        public DateTime MaterialInDate
        {
            set { _materialindate = value; }
            get { return _materialindate; }
        }
        private string _equipmentid;
        /// <summary>
        ///设备编号
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
        ///检验数据
        /// </summary>
        public string InspectionItemDatas
        {
            set { _inspectionitemdatas = value; }
            get { return _inspectionitemdatas; }
        }
        private string _documentpath;
        /// <summary>
        ///文件路径
        /// </summary>
        public string DocumentPath
        {
            set { _documentpath = value; }
            get { return _documentpath; }
        }
        private string _filename;
        /// <summary>
        ///文件名
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        private string _inspectionitemstatus;
        /// <summary>
        ///检验状态
        /// </summary>
        public string InspectionItemStatus
        {
            set { _inspectionitemstatus = value; }
            get { return _inspectionitemstatus; }
        }
        private string _inspectionitemresult;
        /// <summary>
        ///检验结果
        /// </summary>
        public string InspectionItemResult
        {
            set { _inspectionitemresult = value; }
            get { return _inspectionitemresult; }
        }
        private DateTime _inspectiondate;
        /// <summary>
        ///检验日期
        /// </summary>
        public DateTime InspectionDate
        {
            set { _inspectiondate = value; }
            get { return _inspectiondate; }
        }
        private double _inspectionngcount;
        /// <summary>
        ///不良数量
        /// </summary>
        public double InspectionNGCount
        {
            set { _inspectionngcount = value; }
            get { return _inspectionngcount; }
        }
        private string _inspectionRuleDatas;
        /// <summary>
        ///抽样规则历史记录数据
        /// </summary>
        public string InspectionRuleDatas
        {
            set { _inspectionRuleDatas = value; }
            get { return _inspectionRuleDatas; }
        }
        private string _memo;
        /// <summary>
        ///说明
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


    #region FQC
    /// <summary>
    ///FQC项目配置
    /// </summary>
    [Serializable]
    public partial class InspectionFqcItemConfigModel
    {
        public InspectionFqcItemConfigModel()
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
        private string _productdepartment;
        /// <summary>
        ///生产部门
        /// </summary>
        public string ProductDepartment
        {
            set { _productdepartment = value; }
            get { return _productdepartment; }
        }
        private string _isneedort;
        /// <summary>
        ///是否要ORT
        /// </summary>
        public string IsNeedORT
        {
            set { _isneedort = value; }
            get { return _isneedort; }
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
        ///检验序次
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
        ///限制说明
        /// </summary>
        public string SizeMemo
        {
            set { _sizememo = value; }
            get { return _sizememo; }
        }
        private string _keylevel;
        /// <summary>
        ///主要次要
        /// </summary>
        public string KeyLevel
        {
            set { _keylevel = value; }
            get { return _keylevel; }
        }
        private string _equipmentid;
        /// <summary>
        ///设备编号
        /// </summary>
        public string EquipmentId
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
        private string _inspectiondatagathertype;
        /// <summary>
        ///数据录入方式
        /// </summary>
        public string InspectionDataGatherType
        {
            set { _inspectiondatagathertype = value; }
            get { return _inspectiondatagathertype; }
        }
        private string _sipinspectionstandard;
        /// <summary>
        ///Sip检验规则
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
        ///AQL值
        /// </summary>
        public string InspectionAQL
        {
            set { _inspectionaql = value; }
            get { return _inspectionaql; }
        }
        private string _isactivate;
        /// <summary>
        ///是否启用
        /// </summary>
        public string IsActivate
        {
            set { _isactivate = value; }
            get { return _isactivate; }
        }
        private string _checkstatus;
        /// <summary>
        ///审核状态
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        private string _checkperson;
        /// <summary>
        ///审核人
        /// </summary>
        public string CheckPerson
        {
            set { _checkperson = value; }
            get { return _checkperson; }
        }
        private DateTime _inspectioniteminputdate;
        /// <summary>
        ///项目录入日期
        /// </summary>
        public DateTime InspectionItemInPutDate
        {
            set { _inspectioniteminputdate = value; }
            get { return _inspectioniteminputdate; }
        }
        private DateTime _checkdate;
        /// <summary>
        ///审核日期
        /// </summary>
        public DateTime CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        private string _configversion;
        /// <summary>
        ///版本号
        /// </summary>
        public string ConfigVersion
        {
            set { _configversion = value; }
            get { return _configversion; }
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
    // <summary>
    ///物料ORT配置
    /// </summary>
    [Serializable]
    public partial class MaterialOrtConfigModel
    {
        public MaterialOrtConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///品号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _materailname;
        /// <summary>
        ///品名
        /// </summary>
        public string MaterailName
        {
            set { _materailname = value; }
            get { return _materailname; }
        }
        private string _materialspecify;
        /// <summary>
        ///规格
        /// </summary>
        public string MaterialSpecify
        {
            set { _materialspecify = value; }
            get { return _materialspecify; }
        }
        private string _orttype;
        /// <summary>
        ///类别
        /// </summary>
        public string OrtType
        {
            set { _orttype = value; }
            get { return _orttype; }
        }
        private int _ortcoefficient;
        /// <summary>
        ///系数
        /// </summary>
        public int OrtCoefficient
        {
            set { _ortcoefficient = value; }
            get { return _ortcoefficient; }
        }
        private string _isvalid;
        /// <summary>
        ///是否有效
        /// </summary>
        public string IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
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
        ///自增
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
    public class InspectionFqcDetailModel
    {
        public InspectionFqcDetailModel()
        {
        }

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
        private int _orderidnumber;
        /// <summary>
        ///单号序号
        /// </summary>
        public int OrderIdNumber
        {
            set { _orderidnumber = value; }
            get { return _orderidnumber; }
        }
        private string _productdepartment;
        /// <summary>
        ///部门
        /// </summary>
        public string ProductDepartment
        {
            set { _productdepartment = value; }
            get { return _productdepartment; }
        }
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
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
        private string _inspectionitem;
        /// <summary>
        ///检验项目
        /// </summary>
        public string InspectionItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private string _documentPath;
        /// <summary>
        /// 文件存放路经
        /// </summary>
        public string DocumentPath
        {
            set { _documentPath = value; }
            get { return _documentPath; }
        }
        private string _fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            set { _fileName = value; }
            get { return _fileName; }

        }
        private double _orderidcount;
        /// <summary>
        ///订单总数
        /// </summary>
        public double OrderIdCount
        {
            set { _orderidcount = value; }
            get { return _orderidcount; }
        }
        private DateTime _materialindate;
        /// <summary>
        ///订单生产日期
        /// </summary>
        public DateTime MaterialInDate
        {
            set { _materialindate = value; }
            get { return _materialindate; }
        }
        private double _materialCount;
        /// <summary>
        ///生产批量数
        /// </summary>
        public double MaterialCount
        {
            set { _materialCount = value; }
            get { return _materialCount; }
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
        private string _equipmentid;
        /// <summary>
        ///检验冶具
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
        private string _inspectionmode;
        /// <summary>
        ///检验方式
        /// </summary>
        public string InspectionMode
        {
            set { _inspectionmode = value; }
            get { return _inspectionmode; }
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
        ///拒收数量
        /// </summary>
        public double InspectionRefuseCount
        {
            set { _inspectionrefusecount = value; }
            get { return _inspectionrefusecount; }
        }
        /// <summary>
        /// 需录入的数据
        /// </summary>
        private int _needPutInDataCount = 1;
        public int NeedPutInDataCount
        {
            set { _needPutInDataCount = value; }
            get { return _needPutInDataCount; }
        }
        private string _inspectionitemdatas;
        /// <summary>
        ///检验数据
        /// </summary>
        public string InspectionItemDatas
        {
            set { _inspectionitemdatas = value; }
            get { return _inspectionitemdatas; }
        }
        private string _inspectionitemstatus;
        /// <summary>
        ///检验状态
        /// </summary>
        public string InspectionItemStatus
        {
            set { _inspectionitemstatus = value; }
            get { return _inspectionitemstatus; }
        }
        private string _inspectionitemresult;
        /// <summary>
        ///项目检验结果
        /// </summary>
        public string InspectionItemResult
        {
            set { _inspectionitemresult = value; }
            get { return _inspectionitemresult; }
        }
        private string _inspectiondatatimeregion;
        /// <summary>
        ///检验时间段
        /// </summary>
        public string InspectionDataTimeRegion
        {
            set { _inspectiondatatimeregion = value; }
            get { return _inspectiondatatimeregion; }
        }
        private string _instorageorderid;
        /// <summary>
        ///入库单
        /// </summary>
        public string InStorageOrderId
        {
            set { _instorageorderid = value; }
            get { return _instorageorderid; }
        }
        private int _inspectionngcount;
        /// <summary>
        ///不良数
        /// </summary>
        public int InspectionNGCount
        {
            set { _inspectionngcount = value; }
            get { return _inspectionngcount; }
        }
        private DateTime _inspectiondate;
        /// <summary>
        ///检验日期
        /// </summary>
        public DateTime InspectionDate
        {
            set { _inspectiondate = value; }
            get { return _inspectiondate; }
        }
        private string _inspectionRuleDatas;
        /// <summary>
        ///抽样规则历史记录数据
        /// </summary>
        public string InspectionRuleDatas
        {
            set { _inspectionRuleDatas = value; }
            get { return _inspectionRuleDatas; }
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
    /// <summary>
    /// 
    /// </summary>
    public class InspectionFqcMasterModel
    {
        public InspectionFqcMasterModel()
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
        private int _orderidnumber;
        /// <summary>
        ///单号序次  OrderIdNumber
        /// </summary>
        public int OrderIdNumber
        {
            set { _orderidnumber = value; }
            get { return _orderidnumber; }
        }
        private string _productdepartment;
        /// <summary>
        ///部门
        /// </summary>
        public string ProductDepartment
        {
            set { _productdepartment = value; }
            get { return _productdepartment; }
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
        ///订单数量
        /// </summary>
        public double MaterialInCount
        {
            set { _materialcount = value; }
            get { return _materialcount; }
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
        private string _inspectionresult;
        /// <summary>
        ///检验结果
        /// </summary>
        public string InspectionResult
        {
            set { _inspectionresult = value; }
            get { return _inspectionresult; }
        }
        private double _inspectioncount;
        /// <summary>
        ///送检数量
        /// </summary>
        public double InspectionCount
        {
            set { _inspectioncount = value; }
            get { return _inspectioncount; }
        }
        private string _inspectionstatus;
        /// <summary>
        ///检验状态
        /// </summary>
        public string InspectionStatus
        {
            set { _inspectionstatus = value; }
            get { return _inspectionstatus; }
        }
        private int _inspectionItemCount = 0;
        /// <summary>
        /// 需要检验项目数量
        /// </summary>
        public int InspectionItemCount
        {
            set { _inspectionItemCount = value; }
            get { return _inspectionItemCount; }
        }
        private string _inspectionitems;
        /// <summary>
        ///检验项目（以豆号分开）
        /// </summary>
        public string InspectionItems
        {
            set { _inspectionitems = value; }
            get { return _inspectionitems; }
        }


        /// ////////////////////////////////////////////////////

        private double _inspectionMaxNumber;
        /// <summary>
        ///检验最大数
        /// </summary>
        public double InspectionMaxNumber
        {
            set { _inspectionMaxNumber = value; }
            get { return _inspectionMaxNumber; }
        }

        private int _inspectionNgNumber;
        /// <summary>
        ///检验不良数
        /// </summary>
        public int InspectionNgNumber
        {
            set { _inspectionNgNumber = value; }
            get { return _inspectionNgNumber; }
        }

        private string _inspectionItemInspectors;
        /// <summary>
        ///检验的检查员
        /// </summary>
        public string InspectionItemInspectors
        {
            set { _inspectionItemInspectors = value; }
            get { return _inspectionItemInspectors; }
        }

        private string _inspectionItemDetails;
        /// <summary>
        /// 项目详细列表
        /// </summary>
        public string InspectionItemDetails
        {
            set { _inspectionItemDetails = value; }
            get { return _inspectionItemDetails; }
        }

        /// //////////////////////////////////////////




        private DateTime _finishdate;
        /// <summary>
        ///完成日期
        /// </summary>
        public DateTime FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
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

    /// <summary>
    /// FQC工单信息模块
    /// </summary>
    public class InspectionFqcOrderIdVm
    {
        public InspectionFqcOrderIdVm()
        {

        }

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
        /// 品名
        /// </summary>
        public string MaterialName { set; get; }
        /// <summary>
        /// 规格
        /// </summary>
        public string MaterialSpec { set; get; }
        /// <summary>
        /// 图号
        /// </summary>
        public string MaterialDrawId { set; get; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string MaterialSupplier { set; get; }
        /// <summary>
        /// 物料进货日期
        /// </summary>
        public DateTime MaterialInDate { set; get; }
        /// <summary>
        /// 物料进货数量（及需要检验批量总数）
        /// </summary>
        public double MaterialInCount { set; get; }
        /// <summary>
        /// 已经检验的批量总数
        /// </summary>
        public double HaveInspectionSumCount { set; get; }
        /// <summary>
        ///  未检验的批量总数
        /// </summary>
        public double NotInspectionSumCount { get { return MaterialInCount - HaveInspectionSumCount; } }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, string> orderIdNumberStatus { set; get; }
        /// <summary>
        /// 是否有项目配置
        /// </summary>
        public bool IsHaveItemConfig { set; get; }

        #endregion Model
    }


    #endregion

    #region  IQPC
    /// <summary>
    /// 数据详细表
    /// </summary>
    public class InspectionIpqcDetailModel
    {

        public InspectionIpqcDetailModel()
        { }
        #region Model
        private string _orderid;
        /// <summary>
        ///制令单
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private int _orderidnumber;
        /// <summary>
        ///序号
        /// </summary>
        public int OrderIdNumber
        {
            set { _orderidnumber = value; }
            get { return _orderidnumber; }
        }
        private string _productdepartment;
        /// <summary>
        ///部门
        /// </summary>
        public string ProductDepartment
        {
            set { _productdepartment = value; }
            get { return _productdepartment; }
        }
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
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
        private string _inspectionitem;
        /// <summary>
        ///检验项目
        /// </summary>
        public string InspectionItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private double _orderidcount;
        /// <summary>
        ///制令单数量
        /// </summary>
        public double OrderIdCount
        {
            set { _orderidcount = value; }
            get { return _orderidcount; }
        }
        private DateTime _materialindate;
        /// <summary>
        ///生产日期
        /// </summary>
        public DateTime MaterialInDate
        {
            set { _materialindate = value; }
            get { return _materialindate; }
        }
        private double _materialcount;
        /// <summary>
        ///生产数量
        /// </summary>
        public double MaterialCount
        {
            set { _materialcount = value; }
            get { return _materialcount; }
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
        private string _equipmentid;
        /// <summary>
        ///检验冶具
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
        private string _inspectionitemdatas;
        /// <summary>
        ///检验数据
        /// </summary>
        public string InspectionItemDatas
        {
            set { _inspectionitemdatas = value; }
            get { return _inspectionitemdatas; }
        }
        private string _documentpath;
        /// <summary>
        ///文档路经
        /// </summary>
        public string DocumentPath
        {
            set { _documentpath = value; }
            get { return _documentpath; }
        }
        private string _filename;
        /// <summary>
        ///文档名称
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        private string _inspectionitemstatus;
        /// <summary>
        ///检验状态
        /// </summary>
        public string InspectionItemStatus
        {
            set { _inspectionitemstatus = value; }
            get { return _inspectionitemstatus; }
        }
        private string _inspectionitemresult;
        /// <summary>
        ///检验结果
        /// </summary>
        public string InspectionItemResult
        {
            set { _inspectionitemresult = value; }
            get { return _inspectionitemresult; }
        }
        private string _inspectiondatatimeregion;
        /// <summary>
        ///检验时间段
        /// </summary>
        public string InspectionDataTimeRegion
        {
            set { _inspectiondatatimeregion = value; }
            get { return _inspectiondatatimeregion; }
        }
        private int _inspectionngcount;
        /// <summary>
        ///不良数
        /// </summary>
        public int InspectionNGCount
        {
            set { _inspectionngcount = value; }
            get { return _inspectionngcount; }
        }
        private DateTime _inspectiondate;
        /// <summary>
        ///检验日期
        /// </summary>
        public DateTime InspectionDate
        {
            set { _inspectiondate = value; }
            get { return _inspectiondate; }
        }
        private string _inspectionruledatas;
        /// <summary>
        ///检验具体的数据
        /// </summary>
        public string InspectionRuleDatas
        {
            set { _inspectionruledatas = value; }
            get { return _inspectionruledatas; }
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

    /// <summary>
    ///IPQC项目配置模
    /// </summary>
    [Serializable]
    public partial class InspectionIpqcConfigModel
    {
        public InspectionIpqcConfigModel()
        { }
        #region Model
        private string _materialid;
        /// <summary>
        ///机台编号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        private string _productdepartment;
        /// <summary>
        ///部门
        /// </summary>
        public string ProductDepartment
        {
            set { _productdepartment = value; }
            get { return _productdepartment; }
        }
        private string _inspectionitem;
        /// <summary>
        ///测试项目
        /// </summary>
        public string InspectionItem
        {
            set { _inspectionitem = value; }
            get { return _inspectionitem; }
        }
        private int _inspectionitemindex;
        /// <summary>
        ///序号
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
        ///规格
        /// </summary>
        public string SizeMemo
        {
            set { _sizememo = value; }
            get { return _sizememo; }
        }
        private string _keylevel;
        /// <summary>
        ///关键值
        /// </summary>
        public string KeyLevel
        {
            set { _keylevel = value; }
            get { return _keylevel; }
        }
        private string _equipmentid;
        /// <summary>
        ///测试设备
        /// </summary>
        public string EquipmentId
        {
            set { _equipmentid = value; }
            get { return _equipmentid; }
        }
        private string _inspectionmethod;
        /// <summary>
        ///测试方法
        /// </summary>
        public string InspectionMethod
        {
            set { _inspectionmethod = value; }
            get { return _inspectionmethod; }
        }
        private string _inspectiondatagathertype;
        /// <summary>
        ///收集数据类型
        /// </summary>
        public string InspectionDataGatherType
        {
            set { _inspectiondatagathertype = value; }
            get { return _inspectiondatagathertype; }
        }
        private string _sipinspectionstandard;
        /// <summary>
        ///SIP抽样准标
        /// </summary>
        public string SIPInspectionStandard
        {
            set { _sipinspectionstandard = value; }
            get { return _sipinspectionstandard; }
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

    /// <summary>
    ///Ipqc检验日报信息
    /// </summary>
    [Serializable]
    public partial class InspectionIpqcReportModel
    {
        public InspectionIpqcReportModel()
        { }
        #region Model
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _machineid;
        /// <summary>
        ///机台
        /// </summary>
        public string MachineId
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
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
        ///物料料号
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
        private string _materialsupplier;
        /// <summary>
        ///生产部门
        /// </summary>
        public string MaterialSupplier
        {
            set { _materialsupplier = value; }
            get { return _materialsupplier; }
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
        private int _needinspectioncount;
        /// <summary>
        ///需要检验数量
        /// </summary>
        public int NeedInspectionCount
        {
            set { _needinspectioncount = value; }
            get { return _needinspectioncount; }
        }
        private string _datastartsign;
        /// <summary>
        ///开始标识
        /// </summary>
        public string DataStartSign
        {
            set { _datastartsign = value; }
            get { return _datastartsign; }
        }
        private string _class;
        /// <summary>
        ///班别
        /// </summary>
        public string Class
        {
            set { _class = value; }
            get { return _class; }
        }
        private string _inspectionresult;
        /// <summary>
        ///抽检结果
        /// </summary>
        public string InspectionResult
        {
            set { _inspectionresult = value; }
            get { return _inspectionresult; }
        }
        private int _inspectioncount;
        /// <summary>
        ///抽验数量
        /// </summary>
        public int InspectionCount
        {
            set { _inspectioncount = value; }
            get { return _inspectioncount; }
        }
        private int _inspectionitemcount;
        /// <summary>
        ///抽验项次数量
        /// </summary>
        public int InspectionItemCount
        {
            set { _inspectionitemcount = value; }
            get { return _inspectionitemcount; }
        }
        private string _inspectionitems;
        /// <summary>
        ///抽验项次
        /// </summary>
        public string InspectionItems
        {
            set { _inspectionitems = value; }
            get { return _inspectionitems; }
        }
        private string _inspectionitemdetails;
        /// <summary>
        ///抽验项目详细表
        /// </summary>
        public string InspectionItemDetails
        {
            set { _inspectionitemdetails = value; }
            get { return _inspectionitemdetails; }
        }
        private string _inspectioniteminspectors;
        /// <summary>
        ///抽验人
        /// </summary>
        public string InspectionItemInspectors
        {
            set { _inspectioniteminspectors = value; }
            get { return _inspectioniteminspectors; }
        }
        private string _inspectionstatus;
        /// <summary>
        ///抽验状态
        /// </summary>
        public string InspectionStatus
        {
            set { _inspectionstatus = value; }
            get { return _inspectionstatus; }
        }
        private DateTime _finishdate;
        /// <summary>
        ///完成日期
        /// </summary>
        public DateTime FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        private DateTime _inspectiondate;
        /// <summary>
        ///检验日期
        /// </summary>
        public DateTime InspectionDate
        {
            set { _inspectiondate = value; }
            get { return _inspectiondate; }
        }
        private string _inspectiondatamodel;
        /// <summary>
        ///检验数据
        /// </summary>
        public string InspectionDataModel
        {
            set { _inspectiondatamodel = value; }
            get { return _inspectiondatamodel; }
        }
        private string _oppseson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPseson
        {
            set { _oppseson = value; }
            get { return _oppseson; }
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


    #region  数据展示
    /// <summary>
    /// Fqc数据查询信息表
    /// </summary>
    public class ProductFqcMaterailInfoVm
    {
        /// <summary>
        /// ERP导出的物料模块
        /// </summary>
        public ProductFqcMaterailInfoVm()
        {
        }
        #region model
        /// <summary>
        /// 工单单号  
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 进料日期 
        /// </summary>
        public DateTime ProduceInDate { get; set; }
        /// <summary>
        ///  产品品号 
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 产品品名  
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string ProductStandard { get; set; }
        /// <summary>
        /// 供应商 
        /// </summary>
        public string ProductSupplier { get; set; }
        /// <summary>
        /// 产品图号
        /// </summary>
        public string ProductDrawID { get; set; }
        /// <summary>
        /// 进料数量 
        /// </summary>
        public double ProduceNumber { get; set; }
        /// <summary>
        /// 已抽检数
        /// </summary>
        public double HaveInspectionCount
        {
            set; get;
        }
        /// <summary>
        /// 未抽检数
        /// </summary>
        public double NoInspectionCount
        {
            set; get;
        }

        /// <summary>
        /// 检验的项数
        /// </summary>
        public double InspectionNumber { set; get; }
        #endregion

    }

    /// <summary>
    /// 检验物料信息
    /// </summary>
    public class ProductMaterailDto
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
    /// <summary>
    /// 检验项目界面输入输出总表
    /// </summary>
    public class InspectionItemDataSummaryVM
    {

        ///// InspectionMethod  InspectionLevel InspectionAQL InspectionCount AcceptCount
        //// RefuseCount SizeLSL SizeUSL
        ////
        #region Model
        /// <summary>
        ///单号*
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        ///序号* 
        /// </summary>
        public int OrderIdNumber { get; set; }
        /// <summary>
        ///料号*
        /// </summary>
        public string MaterialId { get; set; }
        /// <summary>
        /// MaterialName*
        /// </summary>
        public string MaterialName { set; get; }
        /// <summary>
        /// 产品规格*
        /// </summary>
        public string MaterialSpec { set; get; }
        /// <summary>
        ///图号
        /// </summary>
        public string MaterialDrawId { set; get; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string MaterialSupplier { set; get; }
        /// <summary>
        /// 站点根路径
        /// </summary>
        public string SiteRootPath { get; set; }
        /// <summary>
        /// 文件存放路径*
        /// </summary>
        public string DocumentPath { set; get; }
        /// <summary>
        /// 文件名*
        /// </summary>
        public string FileName { set; get; }
        /// <summary>
        /// 机台编号
        /// </summary>
        public string MachineId { set; get; }
        /// <summary>
        /// 班别*
        /// </summary>
        public string ClassType { set; get; }
        /// <summary>
        /// 抽检验部门*
        /// </summary>
        public string ProductDepartment { set; get; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { set; get; }
        /// <summary>
        /// 录入数据的时间段*
        /// </summary>
        public string InspectionDataTimeRegion { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string InStorageOrderId { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int InspectionItemSumCount { set; get; }
        /// <summary>
        /// 不良数
        /// </summary>
        public int InspectionNGCount { set; get; }
        /// <summary>
        /// 物料进货日期
        /// </summary>
        private DateTime _materialInDate = DateTime.MinValue;

        public DateTime MaterialInDate
        {

            set { _materialInDate = value; }
            get { return _materialInDate; }

        }
        /// <summary>
        /// 物料进货数量
        /// </summary>
        public double MaterialInCount { set; get; }
        /// <summary>
        /// 物料抽检此批数量
        /// </summary>
        public double MaterialCount { set; get; }
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
        public string InspectionMethod { set; get; }
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
        public double InspectionCount { get; set; }
        /// <summary>
        ///接授数
        /// </summary>
        public double AcceptCount { get; set; }
        /// <summary>
        ///拒授数
        /// </summary>
        public double RefuseCount { get; set; }
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
        public string SizeMemo { set; get; }
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
        /// Doing Done处理状况（初始值为Doing）
        /// </summary>
        public string InspectionItemStatus { get; set; }
        /// <summary>
        ///此项的检验状态(完成True，未完成False)
        /// </summary>
        public bool InsptecitonItemIsFinished { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OpPerson { get; set; }
        /// <summary>
        /// 操作标识
        /// </summary>

        public string OpSign { set; get; }
        /// <summary>
        ///加载的自增健
        /// </summary>
        public decimal Id_Key { set; get; }

        #endregion Model

    }

    /// <summary>
    ///配置文件主表显示
    /// </summary>
    public class InspectionConfigMasterVm
    {
      public   ProductMaterailDto MaterialInfo
        {
            set;get;
        }
      public int InspectionItemCount
        {
            get
            {
               if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                {
                    var mm = MaterialIqcInspetionItem.Where(e => e.IsActivate.ToUpper() == "TRUE").ToList();
                    return (mm != null && mm.Count > 0) ?
                      mm.Count : MaterialIqcInspetionItem.Count;
                }
                    
               else return  0;
            }
        }
        public  string Status
        {
            get
            {
                if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                    return MaterialIqcInspetionItem.LastOrDefault().CheckStatus;
                else return string.Empty;
            }
        }
        public string CheckPerson
        {
            get
            {
                if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                    return MaterialIqcInspetionItem.LastOrDefault().CheckPerson;
                else return string.Empty;
            }
        }
        public string CheckDate
        {
            get
            {
                if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                    return MaterialIqcInspetionItem.LastOrDefault().CheckDate.ToShortTimeString();
               else  return string.Empty;
            }
        }
        public  List<string> ItemConfigVersionList
        {
            get
            {
                if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                    return MaterialIqcInspetionItem.Select(e=>e.ConfigVersion).Distinct().ToList();
                else return new List<string>();
            }
        }
        public string ItemConfigVersion
        {
            get
            {
                if (MaterialIqcInspetionItem != null && MaterialIqcInspetionItem.Count > 0)
                {
                    var mm = MaterialIqcInspetionItem.Where(e => e.IsActivate.ToUpper() == "TRUE").ToList();
                    return (mm != null && mm.Count > 0) ?
                      mm.LastOrDefault().ConfigVersion : MaterialIqcInspetionItem.LastOrDefault().ConfigVersion;
                }
                else return string.Empty;
            }
        }

        public string OpPerson
        {
            set;get;
        }
        public List<InspectionIqcItemConfigModel> MaterialIqcInspetionItem
        {
            set;get;
        }

    }
    #endregion

}