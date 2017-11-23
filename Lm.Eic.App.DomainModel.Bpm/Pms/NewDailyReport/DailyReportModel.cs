using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport
{
    /// <summary>
    ///产品标准工艺流程模型
    /// </summary>
    [Serializable]
    public partial class StandardProductionFlowModel
    {
        public StandardProductionFlowModel()
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
        private string _productid;
        /// <summary>
        ///品号
        /// </summary>
        public string ProductId
        {
            set { _productid = value; }
            get { return _productid; }
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
        private int _processesindex;
        /// <summary>
        ///序列号
        /// </summary>
        public int ProcessesIndex
        {
            set { _processesindex = value; }
            get { return _processesindex; }
        }
        private string _processessign;
        /// <summary>
        ///标识
        /// </summary>
        public string ProcessesSign
        {
            set { _processessign = value; }
            get { return _processessign; }
        }
        private string _processesname;
        /// <summary>
        ///工艺名称
        /// </summary>
        public string ProcessesName
        {
            set { _processesname = value; }
            get { return _processesname; }
        }
        private string _processestype;
        /// <summary>
        ///工艺类型
        /// </summary>
        public string ProcessesType
        {
            set { _processestype = value; }
            get { return _processestype; }
        }
        private string _inputtype;
        /// <summary>
        ///输入类型
        /// </summary>
        public string InputType
        {
            set { _inputtype = value; }
            get { return _inputtype; }
        }
        private string _issum;
        /// <summary>
        ///是否算合计
        /// </summary>
        public string IsSum
        {
            set { _issum = value; }
            get { return _issum; }
        }
        private string _isvisualization;
        /// <summary>
        ///是否可视化
        /// </summary>
        public string IsVisualization
        {
            set { _isvisualization = value; }
            get { return _isvisualization; }
        }
        private string _isvalid;
        /// <summary>
        ///是否可见
        /// </summary>
        public string IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        private string _standardproductiontimetype;
        /// <summary>
        ///标准工时类型
        /// </summary>
        public string StandardProductionTimeType
        {
            set { _standardproductiontimetype = value; }
            get { return _standardproductiontimetype; }
        }
        private double _standardproductiontime;
        /// <summary>
        ///标准工时
        /// </summary>
        public double StandardProductionTime
        {
            set { _standardproductiontime = value; }
            get { return _standardproductiontime; }
        }
        private int _productcoefficient;
        /// <summary>
        ///生产系数
        /// </summary>
        public int ProductCoefficient
        {
            set { _productcoefficient = value; }
            get { return _productcoefficient; }
        }
        private double _uph;
        /// <summary>
        ///UPH
        /// </summary>
        public double UPH
        {
            set { _uph = value; }
            get { return _uph; }
        }
        private double _ups;
        /// <summary>
        ///UPS
        /// </summary>
        public double UPS
        {
            set { _ups = value; }
            get { return _ups; }
        }
        private int _productiontimeversionid;
        /// <summary>
        ///标准工时版本号
        /// </summary>
        public int ProductionTimeVersionID
        {
            set { _productiontimeversionid = value; }
            get { return _productiontimeversionid; }
        }
        private double _machinepersonratio;
        /// <summary>
        ///人机配比
        /// </summary>
        public double MachinePersonRatio
        {
            set { _machinepersonratio = value; }
            get { return _machinepersonratio; }
        }
        private string _mouldid;
        /// <summary>
        ///模具编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///模具名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldholecount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldHoleCount
        {
            set { _mouldholecount = value; }
            get { return _mouldholecount; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字段
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
        }
        private string _remark;
        /// <summary>
        ///备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
    ///生产订单分配模板
    /// </summary>
    [Serializable]
    public partial class ProductOrderDispatchModel
    {
        public ProductOrderDispatchModel()
        { }
        #region Model
        private string _orderid;
        /// <summary>
        ///订单
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private string _productiondepartment;
        /// <summary>
        ///生产部门
        /// </summary>
        public string ProductionDepartment
        {
            set { _productiondepartment = value; }
            get { return _productiondepartment; }
        }
        private string _productid;
        /// <summary>
        ///品号
        /// </summary>
        public string ProductId
        {
            set { _productid = value; }
            get { return _productid; }
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
        private string _productspec;
        /// <summary>
        ///规格
        /// </summary>
        public string ProductSpec
        {
            set { _productspec = value; }
            get { return _productspec; }
        }
        private double _putinstorenumber;
        /// <summary>
        ///ERP入库数
        /// </summary>
        public double PutInStoreNumber
        {
            set { _putinstorenumber = value; }
            get { return _putinstorenumber; }
        }
        private double _producenumber;
        /// <summary>
        ///预产量
        /// </summary>
        public double ProduceNumber
        {
            set { _producenumber = value; }
            get { return _producenumber; }
        }
        private DateTime _productiondate;
        /// <summary>
        ///生产日期
        /// </summary>
        public DateTime ProductionDate
        {
            set { _productiondate = value; }
            get { return _productiondate; }
        }
        private string _productstatus;
        /// <summary>
        ///订单状态
        /// </summary>
        public string ProductStatus
        {
            set { _productstatus = value; }
            get { return _productstatus; }
        }
        private int _isvirtualorderid;
        /// <summary>
        ///是否为虚工单
        /// </summary>
        public int IsVirtualOrderId
        {
            set { _isvirtualorderid = value; }
            get { return _isvirtualorderid; }
        }
        private string _dicpatchstatus;
        /// <summary>
        ///分配状态
        /// </summary>
        public string DicpatchStatus
        {
            set { _dicpatchstatus = value; }
            get { return _dicpatchstatus; }
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
        private DateTime _validdate;
        /// <summary>
        ///有效日期
        /// </summary>
        public DateTime ValidDate
        {
            set { _validdate = value; }
            get { return _validdate; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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
    /// <summary>
    ///每日生产报表
    /// </summary>
    [Serializable]
    public partial class DailyProductionReportModel
    {
        public DailyProductionReportModel()
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
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        private DateTime _inputdate;
        /// <summary>
        ///报表日期
        /// </summary>
        public DateTime InPutDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
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
        private string _productid;
        /// <summary>
        ///品号
        /// </summary>
        public string ProductId
        {
            set { _productid = value; }
            get { return _productid; }
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
        private string _productspec;
        /// <summary>
        ///规格
        /// </summary>
        public string ProductSpec
        {
            set { _productspec = value; }
            get { return _productspec; }
        }
        private double _orderquantity;
        /// <summary>
        ///订单数量
        /// </summary>
        public double OrderQuantity
        {
            set { _orderquantity = value; }
            get { return _orderquantity; }
        }
        private string _processestype;
        /// <summary>
        ///工序类别（人工/机台）
        /// </summary>
        public string ProcessesType
        {
            set { _processestype = value; }
            get { return _processestype; }
        }
        private int _processesindex;
        /// <summary>
        ///生产工序号
        /// </summary>
        public int ProcessesIndex
        {
            set { _processesindex = value; }
            get { return _processesindex; }
        }
        private string _processesname;
        /// <summary>
        ///工序名称
        /// </summary>
        public string ProcessesName
        {
            set { _processesname = value; }
            get { return _processesname; }
        }
        private double _standardproductiontime;
        /// <summary>
        ///标准工时
        /// </summary>
        public double StandardProductionTime
        {
            set { _standardproductiontime = value; }
            get { return _standardproductiontime; }
        }
        private string _workerid;
        /// <summary>
        ///作业员工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///作业员名称
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }
        private double _todayproductioncount;
        /// <summary>
        ///生产产量
        /// </summary>
        public double TodayProductionCount
        {
            set { _todayproductioncount = value; }
            get { return _todayproductioncount; }
        }
        private double _todaybadproductcount;
        /// <summary>
        ///不良产量
        /// </summary>
        public double TodayBadProductCount
        {
            set { _todaybadproductcount = value; }
            get { return _todaybadproductcount; }
        }
        private double _workerproductiontime;
        /// <summary>
        ///作业员生产工时
        /// </summary>
        public double WorkerProductionTime
        {
            set { _workerproductiontime = value; }
            get { return _workerproductiontime; }
        }
        private double _getproductiontime;
        /// <summary>
        ///得到工时
        /// </summary>
        public double GetProductionTime
        {
            set { _getproductiontime = value; }
            get
            {
                if (StandardProductionTime != 0&& TodayBadProductCount!=0)
                {
                    return Math.Round(TodayProductionCount * StandardProductionTime / 3600, 2);
                }
                else return _getproductiontime;
            }
        }
        private double _workernoproductiontime;
        /// <summary>
        ///作业员非生产工时
        /// </summary>
        public double WorkerNoProductionTime
        {
            set { _workernoproductiontime = value; }
            get { return _workernoproductiontime; }
        }
        private string _workernoproductionreason;
        /// <summary>
        ///作业员非生产原因
        /// </summary>
        public string WorkerNoProductionReason
        {
            set { _workernoproductionreason = value; }
            get { return _workernoproductionreason; }
        }
        private string _masterworkerid;
        /// <summary>
        ///师傅工号
        /// </summary>
        public string MasterWorkerId
        {
            set { _masterworkerid = value; }
            get { return _masterworkerid; }
        }
        private string _mastername;
        /// <summary>
        ///师傅名
        /// </summary>
        public string MasterName
        {
            set { _mastername = value; }
            get { return _mastername; }
        }
        private string _machineid;
        /// <summary>
        ///机械编号
        /// </summary>
        public string MachineId
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        private string _mouldid;
        /// <summary>
        ///模具编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private double _machinepersonratio;
        /// <summary>
        ///人机配比
        /// </summary>
        public double MachinePersonRatio
        {
            set { _machinepersonratio = value; }
            get { return _machinepersonratio; }
        }
        private int _mouldholecount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldHoleCount
        {
            set { _mouldholecount = value; }
            get { return _mouldholecount; }
        }
        private double _machinesetproductiontime;
        /// <summary>
        ///机械设置时数
        /// </summary>
        public double MachineSetProductionTime
        {
            set { _machinesetproductiontime = value; }
            get { return _machinesetproductiontime; }
        }
        private double _machineproductiontime;
        /// <summary>
        ///机台生产时数
        /// </summary>
        public double MachineProductionTime
        {
            set { _machineproductiontime = value; }
            get { return _machineproductiontime; }
        }
        private double _machineproductioncount;
        /// <summary>
        ///机台生产数量
        /// </summary>
        public double MachineProductionCount
        {
            set { _machineproductioncount = value; }
            get { return _machineproductioncount; }
        }
        private double _machineunproductivetime;
        /// <summary>
        ///机台非生产工时
        /// </summary>
        public double MachineUnproductiveTime
        {
            set { _machineunproductivetime = value; }
            get { return _machineunproductivetime; }
        }
        private string _machineunproductivereason;
        /// <summary>
        ///机台非生产原因
        /// </summary>
        public string MachineUnproductiveReason
        {
            set { _machineunproductivereason = value; }
            get { return _machineunproductivereason; }
        }
        private double _machineproductionbadcount;
        /// <summary>
        ///机台不良数
        /// </summary>
        public double MachineProductionBadCount
        {
            set { _machineproductionbadcount = value; }
            get { return _machineproductionbadcount; }
        }
        private string _field4;
        /// <summary>
        ///备用字4
        /// </summary>
        public string Field4
        {
            set { _field4 = value; }
            get { return _field4; }
        }
        private string _field5;
        /// <summary>
        ///备用字5
        /// </summary>
        public string Field5
        {
            set { _field5 = value; }
            get { return _field5; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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


    /// <summary>
    ///非生产代码模式
    /// </summary>
    [Serializable]
    public partial class ProductionCodeConfigModel
    {
        public ProductionCodeConfigModel()
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
        private string _datanodename;
        /// <summary>
        ///节点代码
        /// </summary>
        public string DataNodeName
        {
            set { _datanodename = value; }
            get { return _datanodename; }
        }
        private string _datanodetext;
        /// <summary>
        ///节点名称
        /// </summary>
        public string DataNodeText
        {
            set { _datanodetext = value; }
            get { return _datanodetext; }
        }
        private string _parentdatanodetext;
        /// <summary>
        ///上级节点
        /// </summary>
        public string ParentDataNodeText
        {
            set { _parentdatanodetext = value; }
            get { return _parentdatanodetext; }
        }
        private int _ishaschildren;
        /// <summary>
        ///是否有子节点
        /// </summary>
        public int IsHasChildren
        {
            set { _ishaschildren = value; }
            get { return _ishaschildren; }
        }
        private string _aboutcategory;
        /// <summary>
        ///大类别
        /// </summary>
        public string AboutCategory
        {
            set { _aboutcategory = value; }
            get { return _aboutcategory; }
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
        private string _primarykey;
        /// <summary>
        ///关键字
        /// </summary>
        public string PrimaryKey
        {
            set { _primarykey = value; }
            get { return _primarykey; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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
        ///关键字
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }


    #region  
    /// <summary>
    /// 查询操作Model
    /// </summary>
    public class QueryDailyProductReportDto
    {

        string department = string.Empty;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return department; }
            set { if (department != value) { department = value; } }
        }

        DateTime inputDate = DateTime.Now.Date;
        /// <summary>
        /// 输入日期
        /// </summary>
        public DateTime InputDate
        {
            get { return inputDate; }
            set { if (inputDate != value) { inputDate = value; } }
        }

        string productName = string.Empty;
        /// <summary>
        ///  产品名称
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { if (productName != value) { productName = value; } }
        }

        string processesname = string.Empty;
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string ProcessesName
        {
            set { if (processesname != value) { processesname = value; } }
            get { return processesname; }
        }

        string orderId = string.Empty;
        /// <summary>
        /// 工单单号
        /// </summary>
        public string OrderId
        {
            set { if (orderId != value) { orderId = value; } }
            get { return orderId; }
        }
        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set { if (searchMode != value) { searchMode = value; } }
        }
    }

    public class UserInfoVm
    {
        public string WorkerId { set; get; }
        public string WorkerName { set; get; }
        public double WorkerProductionTime { set; get; }
        public double WorkerNoProductionTime { set; get; }
        public string WorkerNoProductionReason { set; get; }
    }
    /// <summary>
    /// 产品工艺概述模型
    /// </summary>
    public class ProductFlowSummaryVm
    {
        /// <summary>
        /// 产品品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 工序总数
        /// </summary>
        public int ProductFlowCount { get; set; }

        /// <summary>
        /// 总工时
        /// </summary>
        public double StandardHoursCount { get; set; }
    }
    /// <summary>
    /// 日报工序表统计数据
    /// </summary>
    public class ProductFlowCountDatasVm
    {

        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _productid;
        /// <summary>
        ///品号
        /// </summary>
        public string ProductId
        {
            set { _productid = value; }
            get { return _productid; }
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
        private int _processesindex;
        /// <summary>
        ///序列号
        /// </summary>
        public int ProcessesIndex
        {
            set { _processesindex = value; }
            get { return _processesindex; }
        }
        private string _processessign;
        /// <summary>
        ///标识
        /// </summary>
        public string ProcessesSign
        {
            set { _processessign = value; }
            get { return _processessign; }
        }
        private string _processesname;
        /// <summary>
        ///工艺名称
        /// </summary>
        public string ProcessesName
        {
            set { _processesname = value; }
            get { return _processesname; }
        }
        private string _processestype;
        /// <summary>
        ///工艺类型
        /// </summary>
        public string ProcessesType
        {
            set { _processestype = value; }
            get { return _processestype; }
        }
        private string _inputtype;
        /// <summary>
        ///输入类型
        /// </summary>
        public string InputType
        {
            set { _inputtype = value; }
            get { return _inputtype; }
        }
        private string _issum;
        /// <summary>
        ///是否算合计
        /// </summary>
        public string IsSum
        {
            set { _issum = value; }
            get { return _issum; }
        }
        private string _isvisualization;
        /// <summary>
        ///是否可视化
        /// </summary>
        public string IsVisualization
        {
            set { _isvisualization = value; }
            get { return _isvisualization; }
        }
        private string _isvalid;
        /// <summary>
        ///是否可见
        /// </summary>
        public string IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        private string _standardproductiontimetype;
        /// <summary>
        ///标准工时类型
        /// </summary>
        public string StandardProductionTimeType
        {
            set { _standardproductiontimetype = value; }
            get { return _standardproductiontimetype; }
        }
        private double _standardproductiontime;
        /// <summary>
        ///标准工时
        /// </summary>
        public double StandardProductionTime
        {
            set { _standardproductiontime = value; }
            get { return _standardproductiontime; }
        }
        private int _productcoefficient;
        /// <summary>
        ///生产系数
        /// </summary>
        public int ProductCoefficient
        {
            set { _productcoefficient = value; }
            get { return _productcoefficient; }
        }
        private double _uph;
        /// <summary>
        ///UPH
        /// </summary>
        public double UPH
        {
            set { _uph = value; }
            get { return _uph; }
        }
        private double _ups;
        /// <summary>
        ///UPS
        /// </summary>
        public double UPS
        {
            set { _ups = value; }
            get { return _ups; }
        }
        private int _productiontimeversionid;
        /// <summary>
        ///标准工时版本号
        /// </summary>
        public int ProductionTimeVersionID
        {
            set { _productiontimeversionid = value; }
            get { return _productiontimeversionid; }
        }
        private double _machinepersonratio;
        /// <summary>
        ///人机配比
        /// </summary>
        public double MachinePersonRatio
        {
            set { _machinepersonratio = value; }
            get { return _machinepersonratio; }
        }
        private string _mouldid;
        /// <summary>
        ///模具编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///模具名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldholecount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldHoleCount
        {
            set { _mouldholecount = value; }
            get { return _mouldholecount; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字段
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        ///工单生产数量
        /// </summary>
        public double OrderProductNumber { get; set; }
        /// <summary>
        ///工序已录入数量
        /// </summary>
        public double OrderHavePutInNumber { get; set; }
        /// <summary>
        ///工序需录入数量
        /// </summary>
        public double OrderNeedPutInNumber { get; set; }

    }
    #endregion
}
