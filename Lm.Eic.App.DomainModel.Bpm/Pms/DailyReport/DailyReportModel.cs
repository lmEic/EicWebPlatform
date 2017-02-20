using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{

    #region  存储到数据库的数据模型 
    /// <summary>
    ///日报实体模型
    ///DailyReports
    /// </summary>
    [Serializable]
    public partial class DailyReportModel
    {
        public DailyReportModel()
        { }
        #region Model
        private string _nonproductionreasoncode;
        /// <summary>
        ///非生产原因代码
        /// </summary>
        public string NonProductionReasonCode
        {
            set { _nonproductionreasoncode = value; }
            get { return _nonproductionreasoncode; }
        }
        private string _nonproductionreason;
        /// <summary>
        ///非生产原因
        /// </summary>
        public string NonProductionReason
        {
            set { _nonproductionreason = value; }
            get { return _nonproductionreason; }
        }
        private string _remarks;
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private DateTime _dailyreportdate;
        /// <summary>
        ///日报日期
        /// </summary>
        public DateTime DailyReportDate
        {
            set { _dailyreportdate = value; }
            get { return _dailyreportdate; }
        }
        private string _dailyreportmonth;
        /// <summary>
        ///日报月份
        /// </summary>
        public string DailyReportMonth
        {
            set { _dailyreportmonth = value; }
            get { return _dailyreportmonth; }
        }
        private DateTime _inputtime;
        /// <summary>
        ///录入时间
        /// </summary>
        public DateTime InputTime
        {
            set { _inputtime = value; }
            get { return _inputtime; }
        }

        private string _machineid;
        /// <summary>
        ///机台编号
        /// </summary>
        public string MachineId
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        private string _equipmenteifficiency;
        /// <summary>
        ///稼动率（机台效率）
        /// </summary>
        public string EquipmentEifficiency
        {
            set { _equipmenteifficiency = value; }
            get { return _equipmenteifficiency; }
        }
        private decimal _difficultycoefficient;
        /// <summary>
        ///难度系数
        /// </summary>
        public decimal DifficultyCoefficient
        {
            set { _difficultycoefficient = value; }
            get { return _difficultycoefficient; }
        }
        private string _mouldid;
        /// <summary>
        ///磨具编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///磨具名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldcavitycount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldCavityCount
        {
            set { _mouldcavitycount = value; }
            get { return _mouldcavitycount; }
        }
        private string _orderid;
        /// <summary>
        ///工单单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private string _productname;
        /// <summary>
        ///产品品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productspecification;
        /// <summary>
        ///产品规格
        /// </summary>
        public string ProductSpecification
        {
            set { _productspecification = value; }
            get { return _productspecification; }
        }
        private int _productflowsign;
        /// <summary>
        ///工艺标示（0起始,1中间,2结尾）
        /// </summary>
        public int ProductFlowSign
        {
            set { _productflowsign = value; }
            get { return _productflowsign; }
        }
        private string _productflowid;
        /// <summary>
        ///工序编号
        /// </summary>
        public string ProductFlowID
        {
            set { _productflowid = value; }
            get { return _productflowid; }
        }
        private string _productflowname;
        /// <summary>
        ///工序名称
        /// </summary>
        public string ProductFlowName
        {
            set { _productflowname = value; }
            get { return _productflowname; }
        }
        private int _standardhourstype;
        /// <summary>
        ///标准工时类别(1m,2s,3pcs/h)
        /// </summary>
        public int StandardHoursType
        {
            set { _standardhourstype = value; }
            get { return _standardhourstype; }
        }
        private decimal _standardhours;
        /// <summary>
        ///标准工时
        /// </summary>
        public decimal StandardHours
        {
            set { _standardhours = value; }
            get { return _standardhours; }
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
        private string _userworkerid;
        /// <summary>
        ///操作员工号
        /// </summary>
        public string UserWorkerId
        {
            set { _userworkerid = value; }
            get { return _userworkerid; }
        }
        private string _username;
        /// <summary>
        ///操作员姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        ///师傅姓名
        /// </summary>
        public string MasterName
        {
            set { _mastername = value; }
            get { return _mastername; }
        }
        private decimal _inputstorecount;
        /// <summary>
        ///入库数量
        /// </summary>
        public decimal InputStoreCount
        {
            set { _inputstorecount = value; }
            get { return _inputstorecount; }
        }
        private decimal _qty;
        /// <summary>
        ///总产量
        /// </summary>
        public decimal Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        private decimal _qtygood;
        /// <summary>
        ///良品数量
        /// </summary>
        public decimal QtyGood
        {
            set { _qtygood = value; }
            get { return _qtygood; }
        }
        private decimal _qtybad;
        /// <summary>
        ///不良品数量
        /// </summary>
        public decimal QtyBad
        {
            set { _qtybad = value; }
            get { return _qtybad; }
        }
        private string _failurerate;
        /// <summary>
        ///不良率
        /// </summary>
        public string FailureRate
        {
            set { _failurerate = value; }
            get { return _failurerate; }
        }
        private decimal _sethours;
        /// <summary>
        ///设置时数
        /// </summary>
        public decimal SetHours
        {
            set { _sethours = value; }
            get { return _sethours; }
        }
        private decimal _inputhours;
        /// <summary>
        ///投入时数
        /// </summary>
        public decimal InputHours
        {
            set { _inputhours = value; }
            get { return _inputhours; }
        }
        private decimal _productionhours;
        /// <summary>
        ///生成时数
        /// </summary>
        public decimal ProductionHours
        {
            set { _productionhours = value; }
            get { return _productionhours; }
        }
        private decimal _attendancehours;
        /// <summary>
        ///出勤时数
        /// </summary>
        public decimal AttendanceHours
        {
            set { _attendancehours = value; }
            get { return _attendancehours; }
        }
        private decimal _nonproductionhours;
        /// <summary>
        ///非生产时数
        /// </summary>
        public decimal NonProductionHours
        {
            set { _nonproductionhours = value; }
            get { return _nonproductionhours; }
        }
        private decimal _receivehours;
        /// <summary>
        ///得到工时
        /// </summary>
        public decimal ReceiveHours
        {
            set { _receivehours = value; }
            get { return _receivehours; }
        }
        private decimal _manhours;
        /// <summary>
        ///人工工时
        /// </summary>
        public decimal ManHours
        {
            set { _manhours = value; }
            get { return _manhours; }
        }
        private string _productionefficiency;
        /// <summary>
        ///生产效率
        /// </summary>
        public string ProductionEfficiency
        {
            set { _productionefficiency = value; }
            get { return _productionefficiency; }
        }
        private string _operationefficiency;
        /// <summary>
        ///作业效率
        /// </summary>
        public string OperationEfficiency
        {
            set { _operationefficiency = value; }
            get { return _operationefficiency; }
        }
        private string _paramenterkey;
        /// <summary>
        ///组合键
        /// </summary>
        public string ParamenterKey
        {
            set { _paramenterkey = value; }
            get { return _paramenterkey; }
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
        ///操作标示
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

    #region  日报实体模型 临时表  
    /// <summary>
    ///日报实体模型 临时表
    ///DailyReportsTemp
    /// </summary>
    [Serializable]
    public partial class DailyReportTempModel : ICloneable
    {
        public DailyReportTempModel()
        { }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public DailyReportTempModel Clone()
        {
            return (DailyReportTempModel)this.MemberwiseClone();
        }

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
        private DateTime _dailyreportdate;
        /// <summary>
        ///日报日期
        /// </summary>
        public DateTime DailyReportDate
        {
            set { _dailyreportdate = value; }
            get { return _dailyreportdate; }
        }
        private string _dailyreportmonth;
        /// <summary>
        ///日报月份
        /// </summary>
        public string DailyReportMonth
        {
            set { _dailyreportmonth = value; }
            get { return _dailyreportmonth; }
        }
        private DateTime _inputtime;
        /// <summary>
        ///录入时间
        /// </summary>
        public DateTime InputTime
        {
            set { _inputtime = value; }
            get { return _inputtime; }
        }
        private string _machineid;
        /// <summary>
        ///机台编号
        /// </summary>
        public string MachineId
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        private string _equipmenteifficiency;
        /// <summary>
        ///稼动率（机台效率）
        /// </summary>
        public string EquipmentEifficiency
        {
            set { _equipmenteifficiency = value; }
            get { return _equipmenteifficiency; }
        }
        private decimal _difficultycoefficient;
        /// <summary>
        ///难度系数
        /// </summary>
        public decimal DifficultyCoefficient
        {
            set { _difficultycoefficient = value; }
            get { return _difficultycoefficient; }
        }
        private string _mouldid;
        /// <summary>
        ///磨具编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///磨具名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldcavitycount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldCavityCount
        {
            set { _mouldcavitycount = value; }
            get { return _mouldcavitycount; }
        }
        private string _orderid;
        /// <summary>
        ///工单单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private string _productname;
        /// <summary>
        ///产品品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productspecification;
        /// <summary>
        ///产品规格
        /// </summary>
        public string ProductSpecification
        {
            set { _productspecification = value; }
            get { return _productspecification; }
        }
        private int _productflowsign;
        /// <summary>
        ///工艺标示（0起始,1中间,2结尾）
        /// </summary>
        public int ProductFlowSign
        {
            set { _productflowsign = value; }
            get { return _productflowsign; }
        }
        private string _productflowid;
        /// <summary>
        ///工序编号
        /// </summary>
        public string ProductFlowID
        {
            set { _productflowid = value; }
            get { return _productflowid; }
        }
        private string _productflowname;
        /// <summary>
        ///工序名称
        /// </summary>
        public string ProductFlowName
        {
            set { _productflowname = value; }
            get { return _productflowname; }
        }
        private int _standardhourstype;
        /// <summary>
        ///标准工时类别(1m,2s,3pcs/h)
        /// </summary>
        public int StandardHoursType
        {
            set { _standardhourstype = value; }
            get { return _standardhourstype; }
        }
        private decimal _standardhours;
        /// <summary>
        ///标准工时
        /// </summary>
        public decimal StandardHours
        {
            set { _standardhours = value; }
            get { return _standardhours; }
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
        private string _userworkerid;
        /// <summary>
        ///操作员工号
        /// </summary>
        public string UserWorkerId
        {
            set { _userworkerid = value; }
            get { return _userworkerid; }
        }
        private string _username;
        /// <summary>
        ///操作员姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        ///师傅姓名
        /// </summary>
        public string MasterName
        {
            set { _mastername = value; }
            get { return _mastername; }
        }
        private decimal _inputstorecount;
        /// <summary>
        ///入库数量
        /// </summary>
        public decimal InputStoreCount
        {
            set { _inputstorecount = value; }
            get { return _inputstorecount; }
        }
        private decimal _qty;
        /// <summary>
        ///总产量
        /// </summary>
        public decimal Qty
        {
            set { _qty = value; }
            get { return _qty; }
        }
        private decimal _qtygood;
        /// <summary>
        ///良品数量
        /// </summary>
        public decimal QtyGood
        {
            set { _qtygood = value; }
            get { return _qtygood; }
        }
        private decimal _qtybad;
        /// <summary>
        ///不良品数量
        /// </summary>
        public decimal QtyBad
        {
            set { _qtybad = value; }
            get { return _qtybad; }
        }
        private string _failurerate;
        /// <summary>
        ///不良率
        /// </summary>
        public string FailureRate
        {
            set { _failurerate = value; }
            get { return _failurerate; }
        }
        private decimal _sethours;
        /// <summary>
        ///设置时数
        /// </summary>
        public decimal SetHours
        {
            set { _sethours = value; }
            get { return _sethours; }
        }
        private decimal _inputhours;
        /// <summary>
        ///投入时数
        /// </summary>
        public decimal InputHours
        {
            set { _inputhours = value; }
            get { return _inputhours; }
        }
        private decimal _productionhours;
        /// <summary>
        ///生成时数
        /// </summary>
        public decimal ProductionHours
        {
            set { _productionhours = value; }
            get { return _productionhours; }
        }
        private decimal _attendancehours;
        /// <summary>
        ///出勤时数
        /// </summary>
        public decimal AttendanceHours
        {
            set { _attendancehours = value; }
            get { return _attendancehours; }
        }
        private decimal _nonproductionhours;
        /// <summary>
        ///非生产时数
        /// </summary>
        public decimal NonProductionHours
        {
            set { _nonproductionhours = value; }
            get { return _nonproductionhours; }
        }
        private string _nonproductionreasoncode;
        /// <summary>
        ///非生产原因代码
        /// </summary>
        public string NonProductionReasonCode
        {
            set { _nonproductionreasoncode = value; }
            get { return _nonproductionreasoncode; }
        }
        private string _nonproductionreason;
        /// <summary>
        ///非生产原因
        /// </summary>
        public string NonProductionReason
        {
            set { _nonproductionreason = value; }
            get { return _nonproductionreason; }
        }
        private decimal _receivehours;
        /// <summary>
        ///得到工时
        /// </summary>
        public decimal ReceiveHours
        {
            set { _receivehours = value; }
            get { return _receivehours; }
        }
        private decimal _manhours;
        /// <summary>
        ///人工工时
        /// </summary>
        public decimal ManHours
        {
            set { _manhours = value; }
            get { return _manhours; }
        }
        private string _productionefficiency;
        /// <summary>
        ///生产效率
        /// </summary>
        public string ProductionEfficiency
        {
            set { _productionefficiency = value; }
            get { return _productionefficiency; }
        }
        private string _operationefficiency;
        /// <summary>
        ///作业效率
        /// </summary>
        public string OperationEfficiency
        {
            set { _operationefficiency = value; }
            get { return _operationefficiency; }
        }
        private string _remarks;
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        private int _inputid;
        /// <summary>
        ///输入顺序
        /// </summary>
        public int InPutId
        {
            set { _inputid = value; }
            get { return _inputid; }
        }
        private string _ismachine;
        /// <summary>
        ///是否机器
        /// </summary>
        public string IsMachine
        {
            set { _ismachine = value; }
            get { return _ismachine; }
        }
        private string _checksign;
        /// <summary>
        ///是否核对
        /// </summary>
        public string CheckSign
        {
            set { _checksign = value; }
            get { return _checksign; }
        }
        private string _paramenterkey;
        /// <summary>
        ///组合键
        /// </summary>
        public string ParamenterKey
        {
            set { _paramenterkey = value; }
            get { return _paramenterkey; }
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
        ///操作标示
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
    public interface ICloneable
    {
        object Clone();
    }
    #endregion


    /// <summary>
    ///产品工艺模型
    ///DReportsProductFlow
    /// </summary>
    [Serializable]
    public partial class ProductFlowModel
    {
        public ProductFlowModel()
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
        private string _productname;
        /// <summary>
        ///产品品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private int _productflowsign;
        /// <summary>
        ///工艺标示（0起始,1中间,2结尾）
        /// </summary>
        public int ProductFlowSign
        {
            set { _productflowsign = value; }
            get { return _productflowsign; }
        }
        private string _productflowpdegree;
        /// <summary>
        ///工艺主次关系(Primary / Secondary)
        /// </summary>
        public string ProductFlowpDegree
        {
            set { _productflowpdegree = value; }
            get { return _productflowpdegree; }
        }
        private string _productflowid;
        /// <summary>
        ///工艺编号
        /// </summary>
        public string ProductFlowId
        {
            set { _productflowid = value; }
            get { return _productflowid; }
        }
        private string _productflowname;
        /// <summary>
        ///工艺名称
        /// </summary>
        public string ProductFlowName
        {
            set { _productflowname = value; }
            get { return _productflowname; }
        }
        private int _standardhourstype;
        /// <summary>
        ///标准工时类别(1s,2m,3pcs/h)
        /// </summary>
        public int StandardHoursType
        {
            set { _standardhourstype = value; }
            get { return _standardhourstype; }
        }
        private decimal _standardhours;
        /// <summary>
        ///标准工时
        /// </summary>
        public decimal StandardHours
        {
            set { _standardhours = value; }
            get { return _standardhours; }
        }
        private decimal _relaxcoefficient;
        /// <summary>
        ///宽放系数
        /// </summary>
        public decimal RelaxCoefficient
        {
            set { _relaxcoefficient = value; }
            get { return _relaxcoefficient; }
        }
        private int _minmachinecount;
        /// <summary>
        ///最小机台数
        /// </summary>
        public int MinMachineCount
        {
            set { _minmachinecount = value; }
            get { return _minmachinecount; }
        }
        private int _maxmachinecount;
        /// <summary>
        ///最大机台数
        /// </summary>
        public int MaxMachineCount
        {
            set { _maxmachinecount = value; }
            get { return _maxmachinecount; }
        }
        private decimal _difficultycoefficient;
        /// <summary>
        ///难度系数
        /// </summary>
        public decimal DifficultyCoefficient
        {
            set { _difficultycoefficient = value; }
            get { return _difficultycoefficient; }
        }
        private string _mouldid;
        /// <summary>
        ///模板编号
        /// </summary>
        public string MouldId
        {
            set { _mouldid = value; }
            get { return _mouldid; }
        }
        private string _mouldname;
        /// <summary>
        ///模板名称
        /// </summary>
        public string MouldName
        {
            set { _mouldname = value; }
            get { return _mouldname; }
        }
        private int _mouldcavitycount;
        /// <summary>
        ///模穴数
        /// </summary>
        public int MouldCavityCount
        {
            set { _mouldcavitycount = value; }
            get { return _mouldcavitycount; }
        }
        private string _parameterkey;
        /// <summary>
        ///标识键
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
        private string _opsign;
        /// <summary>
        ///操作标示
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
    ///机台实体模型
    ///DReportsMachine
    /// </summary>
    [Serializable]
    public partial class MachineModel
    {
        public MachineModel()
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
        ///机台编号
        /// </summary>
        public string MachineId
        {
            set { _machineid = value; }
            get { return _machineid; }
        }
        private string _machineCode;
        /// <summary>
        /// 机台编码
        /// </summary>
        public string MachineCode
        {
            set { _machineCode = value; }
            get { return _machineCode; }
        }

        private string _machinename;
        /// <summary>
        ///机台名称
        /// </summary>
        public string MachineName
        {
            set { _machinename = value; }
            get { return _machinename; }
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
        ///操作标示
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
    ///非生产原因实体模型
    ///DReportsNonProduction
    /// </summary>
    [Serializable]
    public partial class NonProductionReasonModel
    {
        public NonProductionReasonModel()
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
        private string _nonproductionreasoncode;
        /// <summary>
        ///非生产原因代码
        /// </summary>
        public string NonProductionReasonCode
        {
            set { _nonproductionreasoncode = value; }
            get { return _nonproductionreasoncode; }
        }
        private string _nonproductionreason;
        /// <summary>
        ///非生产原因
        /// </summary>
        public string NonProductionReason
        {
            set { _nonproductionreason = value; }
            get { return _nonproductionreason; }
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
        ///操作标示
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
    /// 日报出勤时数表
    /// </summary>
    public class ReportsAttendenceModel
    {
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
        private string _attendencestation;
        /// <summary>
        ///站别
        /// </summary>
        public string AttendenceStation
        {
            set { _attendencestation = value; }
            get { return _attendencestation; }
        }
        private int _shouldattendenceusercount;
        /// <summary>
        ///应到人数
        /// </summary>
        public int ShouldAttendenceUserCount
        {
            set { _shouldattendenceusercount = value; }
            get { return _shouldattendenceusercount; }
        }
        private double _shouldattendencehours;
        /// <summary>
        ///应到人员时数
        /// </summary>
        public double ShouldAttendenceHours
        {
            set { _shouldattendencehours = value; }
            get { return _shouldattendencehours; }
        }
        private int _askleaveusercount;
        /// <summary>
        ///请假人数
        /// </summary>
        public int AskLeaveUserCount
        {
            set { _askleaveusercount = value; }
            get { return _askleaveusercount; }
        }
        private double _askleavehours;
        /// <summary>
        ///请假人员时数
        /// </summary>
        public double AskLeaveHours
        {
            set { _askleavehours = value; }
            get { return _askleavehours; }
        }
        private int _haveleaveusercount;
        /// <summary>
        ///旷工人数
        /// </summary>
        public int HaveLeaveUserCount
        {
            set { _haveleaveusercount = value; }
            get { return _haveleaveusercount; }
        }
        private double _haveleavehours;
        /// <summary>
        ///旷工人员时数
        /// </summary>
        public double HaveLeaveHours
        {
            set { _haveleavehours = value; }
            get { return _haveleavehours; }
        }
        private int _supportoutusercount;
        /// <summary>
        ///援出人数
        /// </summary>
        public int SupportOutUserCount
        {
            set { _supportoutusercount = value; }
            get { return _supportoutusercount; }
        }
        private double _supportouthours;
        /// <summary>
        ///援出人员时数
        /// </summary>
        public double SupportOutHours
        {
            set { _supportouthours = value; }
            get { return _supportouthours; }
        }
        private int _realityworkingusercount;
        /// <summary>
        ///实到人数
        /// </summary>
        public int RealityWorkingUserCount
        {
            set { _realityworkingusercount = value; }
            get { return _realityworkingusercount; }
        }
        private double _realityworkinghours;
        /// <summary>
        ///实到人员时数
        /// </summary>
        public double RealityWorkingHours
        {
            set { _realityworkinghours = value; }
            get { return _realityworkinghours; }
        }
        private int _innewworkercount;
        /// <summary>
        ///新进人数
        /// </summary>
        public int InNewWorkerCount
        {
            set { _innewworkercount = value; }
            get { return _innewworkercount; }
        }
        private double _innewworkerhours;
        /// <summary>
        ///新时人员时数
        /// </summary>
        public double InNewWorkerHours
        {
            set { _innewworkerhours = value; }
            get { return _innewworkerhours; }
        }
        private int _supportinshoutabsentcount;
        /// <summary>
        ///援入应到人数
        /// </summary>
        public int SupportInShoutAbsentCount
        {
            set { _supportinshoutabsentcount = value; }
            get { return _supportinshoutabsentcount; }
        }
        private double _supportinshoutabsenthours;
        /// <summary>
        ///援入应到时数
        /// </summary>
        public double SupportInShoutAbsentHours
        {
            set { _supportinshoutabsenthours = value; }
            get { return _supportinshoutabsenthours; }
        }
        private int _supportinrealityworkingcount;
        /// <summary>
        ///援入实到人数
        /// </summary>
        public int SupportInRealityWorkingCount
        {
            set { _supportinrealityworkingcount = value; }
            get { return _supportinrealityworkingcount; }
        }
        private double _supportinrealityworkinghours;
        /// <summary>
        ///援入实到时数
        /// </summary>
        public double SupportInRealityWorkingHours
        {
            set { _supportinrealityworkinghours = value; }
            get { return _supportinrealityworkinghours; }
        }
        private int _supportinaskleavecount;
        /// <summary>
        ///援入请假人数
        /// </summary>
        public int SupportInAskLeaveCount
        {
            set { _supportinaskleavecount = value; }
            get { return _supportinaskleavecount; }
        }
        private double _supportinaskleavehours;
        /// <summary>
        ///援入请假时数
        /// </summary>
        public double SupportInAskLeaveHours
        {
            set { _supportinaskleavehours = value; }
            get { return _supportinaskleavehours; }
        }
        private int _supportinhaveleavecount;
        /// <summary>
        ///援入旷工人数
        /// </summary>
        public int SupportInHaveLeaveCount
        {
            set { _supportinhaveleavecount = value; }
            get { return _supportinhaveleavecount; }
        }
        private double _supportinhaveleavehours;
        /// <summary>
        ///援入旷工时数
        /// </summary>
        public double SupportInHaveLeaveHours
        {
            set { _supportinhaveleavehours = value; }
            get { return _supportinhaveleavehours; }
        }
        private int _overworkusercount;
        /// <summary>
        ///加班人数
        /// </summary>
        public int OverWorkUserCount
        {
            set { _overworkusercount = value; }
            get { return _overworkusercount; }
        }
        private double _overworkhours;
        /// <summary>
        ///加班时数
        /// </summary>
        public double OverWorkHours
        {
            set { _overworkhours = value; }
            get { return _overworkhours; }
        }
        private int _attendencetotalcount;
        /// <summary>
        ///出勤总人数
        /// </summary>
        public int AttendenceTotalCount
        {
            set { _attendencetotalcount = value; }
            get { return _attendencetotalcount; }
        }
        private double _attendencetotalhours;
        /// <summary>
        ///出勤总时数
        /// </summary>
        public double AttendenceTotalHours
        {
            set { _attendencetotalhours = value; }
            get { return _attendencetotalhours; }
        }
        private DateTime _reportdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
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
        public decimal Id_key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model

    }

    #endregion


    
    #region  用来展示的模型

    /// <summary>
    /// 产品工艺概述模型
    /// </summary>
    public class ProductFlowOverviewModel
    {
        /// <summary>
        /// 产品品名
        /// </summary>
        public string ProductName { get; set; }
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
    ///工单信息实体模型
    /// </summary>
    [Serializable]
    public partial class DReportsOrderModel
    {
        public DReportsOrderModel()
        { }
        #region Model
        private string _orderid;
        /// <summary>
        ///工单单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        private string _productid;
        /// <summary>
        ///产品品号
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        private string _productname;
        /// <summary>
        ///产品品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productspecify;
        /// <summary>
        ///产品规格
        /// </summary>
        public string ProductSpecify
        {
            set { _productspecify = value; }
            get { return _productspecify; }
        }
        private int _count;
        /// <summary>
        ///批量
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        private DateTime _instockdate;
        /// <summary>
        ///入库日期
        /// </summary>
        public DateTime InStockDate
        {
            set { _instockdate = value; }
            get { return _instockdate; }
        }
        private DateTime _orderfinishdate;
        /// <summary>
        ///工单完工日期
        /// </summary>
        public DateTime OrderFinishDate
        {
            set { _orderfinishdate = value; }
            get { return _orderfinishdate; }
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
        ///操作标示
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

    #endregion
}
