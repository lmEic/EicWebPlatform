using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport
{
    /// <summary>
    ///日报模型
    /// </summary>
    [Serializable]
    public partial class DailyReportModel
    {
        public DailyReportModel()
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
        ///录入日期
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
        private decimal _equipmenteifficiency;
        /// <summary>
        ///稼动率（设备效率）
        /// </summary>
        public decimal EquipmentEifficiency
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
        ///工艺编号
        /// </summary>
        public string ProductFlowID
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
        ///员工工号
        /// </summary>
        public string UserWorkerId
        {
            set { _userworkerid = value; }
            get { return _userworkerid; }
        }
        private string _username;
        /// <summary>
        ///员工姓名
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
        ///生成总数
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
        private decimal _failurerate;
        /// <summary>
        ///不良率
        /// </summary>
        public decimal FailureRate
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
        ///生产时数
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
        private decimal _productionefficiency;
        /// <summary>
        ///生产效率
        /// </summary>
        public decimal ProductionEfficiency
        {
            set { _productionefficiency = value; }
            get { return _productionefficiency; }
        }
        private decimal _operationefficiency;
        /// <summary>
        ///作业效率
        /// </summary>
        public decimal OperationEfficiency
        {
            set { _operationefficiency = value; }
            get { return _operationefficiency; }
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
    ///日报模板模型
    /// </summary>
    [Serializable]
    public partial class DailyReportTemplateModel
    {
        public DailyReportTemplateModel()
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
        ///工艺标示
        /// </summary>
        public int ProductFlowSign
        {
            set { _productflowsign = value; }
            get { return _productflowsign; }
        }
        private string _productflowid;
        /// <summary>
        ///工艺编号
        /// </summary>
        public string ProductFlowID
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
        ///标准工时类别
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
        ///员工工号
        /// </summary>
        public string UserWorkerId
        {
            set { _userworkerid = value; }
            get { return _userworkerid; }
        }
        private string _username;
        /// <summary>
        ///员工姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        private string _masterworkerid;
        /// <summary>
        ///师傅编号
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

}
