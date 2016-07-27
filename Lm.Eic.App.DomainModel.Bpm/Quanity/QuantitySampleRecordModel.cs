using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{  
    #region  进料检验 IQC，制程检验FQC，出货检验IPQC  Model
    /// <summary>
    /// IQC物料抽样模块 
    /// 对应表 QCMS_IQCSampleRecordTable
    /// </summary>
    public class IQCSampleRecordModel
    {
      public IQCSampleRecordModel()
		{}
		#region Model
		private string _orderid;
		private string _samplematerial;
		private string _samplematerialname;
		private string _samplematerialspec;
		private string _samplematerialsupplier;
		private DateTime? _samplematerialindate;
		private string _samplematerialdrawid;
		private decimal? _samplematerialnumber;
		private string _checkway;
		private int? _samplenumber;
		private int? _samplebadnumber;
		private decimal? _sampleratio;
		private string _sampleresult;
		private string _badreanson;
		private string _samplepersons;
		private DateTime? _finishdate;
		private DateTime? _inputdate;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterial
		{
			set{ _samplematerial=value;}
			get{return _samplematerial;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialName
		{
			set{ _samplematerialname=value;}
			get{return _samplematerialname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSpec
		{
			set{ _samplematerialspec=value;}
			get{return _samplematerialspec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSupplier
		{
			set{ _samplematerialsupplier=value;}
			get{return _samplematerialsupplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SampleMaterialInDate
		{
			set{ _samplematerialindate=value;}
			get{return _samplematerialindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialDrawID
		{
			set{ _samplematerialdrawid=value;}
			get{return _samplematerialdrawid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SampleMaterialNumber
		{
			set{ _samplematerialnumber=value;}
			get{return _samplematerialnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleNumber
		{
			set{ _samplenumber=value;}
			get{return _samplenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SampleBadNumber
		{
			set{ _samplebadnumber=value;}
			get{return _samplebadnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SampleRatio
		{
			set{ _sampleratio=value;}
			get{return _sampleratio;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleResult
		{
			set{ _sampleresult=value;}
			get{return _sampleresult;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BadReanson
		{
			set{ _badreanson=value;}
			get{return _badreanson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SamplePersons
		{
			set{ _samplepersons=value;}
			get{return _samplepersons;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FinishDate
		{
			set{ _finishdate=value;}
			get{return _finishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? InPutDate
		{
			set{ _inputdate=value;}
			get{return _inputdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model
    }
   
    /// <summary>
    /// IQC物料抽样项目模块（用于记录打印内容）
    /// 对应表 QCMS_IQCPrintSampleTable
    /// </summary>
    public class IQCSampleItemRecordModel
    {

        public IQCSampleItemRecordModel()
		{}
		#region Model
		private string _orderid;
		private string _samplematerial;
		private string _samplematerialname;
		private string _samplematerialspec;
		private string _samplematerialsupplier;
		private string _samplematerialdrawid;
		private double ? _samplematerialnumber;
		private DateTime? _samplematerialindate;
		private string _sampleitem;
		private string _equipmentid;
		private string _checkmethod;
		private string _checklevel;
		private string _grade;
		private string _checkway;
		private string _sizespec;
		private string _sizespecup;
		private string _sizespecdown;
		private double ? _acceptgradenumber;
		private double ? _checknumber;
		private double ? _refusegradenumber;
		private int? _printcount;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string OrderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterial
		{
			set{ _samplematerial=value;}
			get{return _samplematerial;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialName
		{
			set{ _samplematerialname=value;}
			get{return _samplematerialname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSpec
		{
			set{ _samplematerialspec=value;}
			get{return _samplematerialspec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialSupplier
		{
			set{ _samplematerialsupplier=value;}
			get{return _samplematerialsupplier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleMaterialDrawID
		{
			set{ _samplematerialdrawid=value;}
			get{return _samplematerialdrawid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? SampleMaterialNumber
		{
			set{ _samplematerialnumber=value;}
			get{return _samplematerialnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SampleMaterialInDate
		{
			set{ _samplematerialindate=value;}
			get{return _samplematerialindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SampleItem
		{
			set{ _sampleitem=value;}
			get{return _sampleitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EquipmentID
		{
			set{ _equipmentid=value;}
			get{return _equipmentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckMethod
		{
			set{ _checkmethod=value;}
			get{return _checkmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckLevel
		{
			set{ _checklevel=value;}
			get{return _checklevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpec
		{
			set{ _sizespec=value;}
			get{return _sizespec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpecUP
		{
			set{ _sizespecup=value;}
			get{return _sizespecup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SizeSpecDown
		{
			set{ _sizespecdown=value;}
			get{return _sizespecdown;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? AcceptGradeNumber
		{
			set{ _acceptgradenumber=value;}
			get{return _acceptgradenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? CheckNumber
		{
			set{ _checknumber=value;}
			get{return _checknumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double ? RefuseGradeNumber
		{
			set{ _refusegradenumber=value;}
			get{return _refusegradenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintCount
		{
			set{ _printcount=value;}
			get{return _printcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model

    }
    
    /// <summary>
    /// 不合格产品记录
    ///对应表 QCMS_IQCSampleRecordNGTable
     /// </summary>
    public class ProductNgRecordModel
    {
        public ProductNgRecordModel()
        { }
        #region Model
        private string _orderid;
        private string _samplematerial;
        private string _samplematerialname;
        private string _samplematerialspec;
        private string _samplematerialsupplier;
        private DateTime? _samplematerialindate;
        private string _samplematerialdrawid;
        private double? _samplematerialnumber;
        private string _checkway;
        private int? _samplenumber;
        private int? _samplebadnumber;
        private double ? _sampleratio;
        private string _sampleresult;
        private string _badreanson;
        private string _samplepersons;
        private string _resultdoway;
        private string _specialorderid;
        private int? _fullcheckworktime;
        private DateTime? _finishdate;
        private DateTime? _inputdate;
        private string _memo;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterial
        {
            set { _samplematerial = value; }
            get { return _samplematerial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialName
        {
            set { _samplematerialname = value; }
            get { return _samplematerialname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialSpec
        {
            set { _samplematerialspec = value; }
            get { return _samplematerialspec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialSupplier
        {
            set { _samplematerialsupplier = value; }
            get { return _samplematerialsupplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SampleMaterialInDate
        {
            set { _samplematerialindate = value; }
            get { return _samplematerialindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterialDrawID
        {
            set { _samplematerialdrawid = value; }
            get { return _samplematerialdrawid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double ? SampleMaterialNumber
        {
            set { _samplematerialnumber = value; }
            get { return _samplematerialnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleNumber
        {
            set { _samplenumber = value; }
            get { return _samplenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleBadNumber
        {
            set { _samplebadnumber = value; }
            get { return _samplebadnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? SampleRatio
        {
            set { _sampleratio = value; }
            get { return _sampleratio; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleResult
        {
            set { _sampleresult = value; }
            get { return _sampleresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BadReanson
        {
            set { _badreanson = value; }
            get { return _badreanson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SamplePersons
        {
            set { _samplepersons = value; }
            get { return _samplepersons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResultDoWay
        {
            set { _resultdoway = value; }
            get { return _resultdoway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpecialOrderId
        {
            set { _specialorderid = value; }
            get { return _specialorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FullCheckWorkTime
        {
            set { _fullcheckworktime = value; }
            get { return _fullcheckworktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FinishDate
        {
            set { _finishdate = value; }
            get { return _finishdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? InPutDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
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
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
   
    /// <summary>
    /// 设置物料抽测项次
    /// 对应表 QCMS_MaterialSampleSet
    /// </summary>
    public class  MaterialSampleItemModel
    {
        public MaterialSampleItemModel ()
        { }
        #region Model
        private string _samplematerial;
        private string _sampleitem;
        private string _sizespec;
        private string _sizespecup;
        private string _sizespecdown;
        private string _equipmetnid;
        private string _checkmethod;
        private string _checkstandard;
        private string _checkway;
        private string _checklevel;
        private string _grade;
        private string _department;
        private string _sampleclass;
        private string _materiaattribute;
        private int? _prioritylevel;
        private string _producttype;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string SampleMaterial
        {
            set { _samplematerial = value; }
            get { return _samplematerial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleItem
        {
            set { _sampleitem = value; }
            get { return _sampleitem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpec
        {
            set { _sizespec = value; }
            get { return _sizespec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpecUP
        {
            set { _sizespecup = value; }
            get { return _sizespecup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SizeSpecDown
        {
            set { _sizespecdown = value; }
            get { return _sizespecdown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipmetnID
        {
            set { _equipmetnid = value; }
            get { return _equipmetnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckMethod
        {
            set { _checkmethod = value; }
            get { return _checkmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStandard
        {
            set { _checkstandard = value; }
            get { return _checkstandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckLevel
        {
            set { _checklevel = value; }
            get { return _checklevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleClass
        {
            set { _sampleclass = value; }
            get { return _sampleclass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MateriaAttribute
        {
            set { _materiaattribute = value; }
            get { return _materiaattribute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PriorityLevel
        {
            set { _prioritylevel = value; }
            get { return _prioritylevel; }
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
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
    
    /// <summary>
    /// 物料抽样规则表
    /// 对应表 QCMS_SamplePlanTable
    /// </summary>
    public class SampleRuleTableModel
    {
        public SampleRuleTableModel ()
        {  }
        #region Model
        private string _checkway;
        private string _checklevel;
        private string _grade;
        private string _startnumber;
        private string _endnumber;
        private string _checknumber;
        private string _acceptgradenumber;
        private string _refusegradenumber;
        private decimal _id_key;
        /// <summary>
        /// 
        /// </summary>
        public string CheckWay
        {
            set { _checkway = value; }
            get { return _checkway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckLevel
        {
            set { _checklevel = value; }
            get { return _checklevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartNumber
        {
            set { _startnumber = value; }
            get { return _startnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndNumber
        {
            set { _endnumber = value; }
            get { return _endnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckNumber
        {
            set { _checknumber = value; }
            get { return _checknumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AcceptGradeNumber
        {
            set { _acceptgradenumber = value; }
            get { return _acceptgradenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RefuseGradeNumber
        {
            set { _refusegradenumber = value; }
            get { return _refusegradenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }
    
    /// <summary>
     /// 抽样放宽加严限制控制
    ///  对应表 QCMS_SampleControlParamter
     /// </summary>
    public class SampleWayLawModel
    {
        public SampleWayLawModel ()
        { }
		#region Model
		private string _classification;
		private string _judgeway;
		private string _ab;
		private string _ac;
		private string _ba;
		private string _ca;
		private string _abi;
		private string _aci;
		private string _bai;
		private string _cai;
		private decimal _id_key;
		/// <summary>
		/// 
		/// </summary>
		public string Classification
		{
			set{ _classification=value;}
			get{return _classification;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JudgeWay
		{
			set{ _judgeway=value;}
			get{return _judgeway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AB
		{
			set{ _ab=value;}
			get{return _ab;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AC
		{
			set{ _ac=value;}
			get{return _ac;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BA
		{
			set{ _ba=value;}
			get{return _ba;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CA
		{
			set{ _ca=value;}
			get{return _ca;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ABI
		{
			set{ _abi=value;}
			get{return _abi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ACI
		{
			set{ _aci=value;}
			get{return _aci;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BAI
		{
			set{ _bai=value;}
			get{return _bai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CAI
		{
			set{ _cai=value;}
			get{return _cai;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Id_Key
		{
			set{ _id_key=value;}
			get{return _id_key;}
		}
		#endregion Model
    }
#endregion 
   /// <summary>
   /// IPQC 抽测SPC记录数值
   /// </summary>
   public class IPQC_SampleItemDataModel
   {
        public void  IPQC_SamPleItemDataModel()
       {

       }

        #region Model
        private string _orderid;
        private string _materialid;
        private string _materialname;
        private string _materialspec;
        private string _materialdrawid;
        private string _materialproductiondepartment;
        private decimal? _materialnumber;
        private DateTime? _instoredate;
        private string _classtype;
        private string _machineid;
        private string _sampleitem;
        private string _sizespec;
        private decimal? _sizemax;
        private decimal? _sizemin;
        private string _sampleequipmentid;
        private string _samplemethod;
        private string _data1;
        private string _data2;
        private string _data3;
        private string _data4;
        private string _data5;
        private string _datacollection;
        private string _datasign;
        private string _datarecordname;
        private string _datagroupid;
        private string _sampleresult;
        private DateTime? _inputdate;
        private DateTime? _inputtime;
        private decimal _id_key;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 抽测品号
        /// </summary>
        public string MaterialId
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string MaterialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        /// <summary>
        /// 物料规格
        /// </summary>
        public string MaterialSpec
        {
            set { _materialspec = value; }
            get { return _materialspec; }
        }
        /// <summary>
        /// 物品图号
        /// </summary>
        public string MaterialDrawId
        {
            set { _materialdrawid = value; }
            get { return _materialdrawid; }
        }
        /// <summary>
        /// 物料生产部门
        /// </summary>
        public string MaterialProductionDepartment
        {
            set { _materialproductiondepartment = value; }
            get { return _materialproductiondepartment; }
        }
        /// <summary>
        /// 生产数量
        /// </summary>
        public decimal? MaterialNumber
        {
            set { _materialnumber = value; }
            get { return _materialnumber; }
        }
        /// <summary>
        /// 物料入库日期
        /// </summary>
        public DateTime? InStoreDate
        {
            set { _instoredate = value; }
            get { return _instoredate; }
        }
        /// <summary>
        /// 班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 生产机器
        /// </summary>
        public string MachineID
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        /// <summary>
        /// 抽测项目
        /// </summary>
        public string SampleItem
        {
            set { _sampleitem = value; }
            get { return _sampleitem; }
        }
        /// <summary>
        /// 抽测规格
        /// </summary>
        public string SizeSpec
        {
            set { _sizespec = value; }
            get { return _sizespec; }
        }
        /// <summary>
        /// 规格最大值
        /// </summary>
        public decimal? SizeMax
        {
            set { _sizemax = value; }
            get { return _sizemax; }
        }
        /// <summary>
        /// 规格最小值
        /// </summary>
        public decimal? SizeMin
        {
            set { _sizemin = value; }
            get { return _sizemin; }
        }
        /// <summary>
        /// 抽测设备编号
        /// </summary>
        public string SampleEquipmentID
        {
            set { _sampleequipmentid = value; }
            get { return _sampleequipmentid; }
        }
        /// <summary>
        /// 抽测方法
        /// </summary>
        public string SampleMethod
        {
            set { _samplemethod = value; }
            get { return _samplemethod; }
        }
        /// <summary>
        /// 数据1
        /// </summary>
        public string Data1
        {
            set { _data1 = value; }
            get { return _data1; }
        }
        /// <summary>
        /// 数据2
        /// </summary>
        public string Data2
        {
            set { _data2 = value; }
            get { return _data2; }
        }
        /// <summary>
        /// 数据3
        /// </summary>
        public string Data3
        {
            set { _data3 = value; }
            get { return _data3; }
        }
        /// <summary>
        /// 数据4
        /// </summary>
        public string Data4
        {
            set { _data4 = value; }
            get { return _data4; }
        }
        /// <summary>
        /// 数据5
        /// </summary>
        public string Data5
        {
            set { _data5 = value; }
            get { return _data5; }
        }
        /// <summary>
        /// 数据集合
        /// </summary>
        public string DataCollection
        {
            set { _datacollection = value; }
            get { return _datacollection; }
        }
        /// <summary>
        /// 数据标识
        /// </summary>
        public string DataSign
        {
            set { _datasign = value; }
            get { return _datasign; }
        }
        /// <summary>
        /// 记录人
        /// </summary>
        public string DataRecordName
        {
            set { _datarecordname = value; }
            get { return _datarecordname; }
        }
        /// <summary>
        /// 数据分组编号
        /// </summary>
        public string DataGroupID
        {
            set { _datagroupid = value; }
            get { return _datagroupid; }
        }
        /// <summary>
        /// 抽测结果
        /// </summary>
        public string SampleResult
        {
            set { _sampleresult = value; }
            get { return _sampleresult; }
        }
        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime? InPutDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        /// <summary>
        /// 输入时间
        /// </summary>
        public DateTime? InputTime
        {
            set { _inputtime = value; }
            get { return _inputtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model

   }


}

