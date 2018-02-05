using System;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance
{
    /// <summary>
    ///班别设置实体模型
    /// </summary>
    [Serializable]
    public partial class AttendClassTypeDetailModel
    {
        public AttendClassTypeDetailModel()
        { }
        #region Model
        private DateTime _dateat;
        /// <summary>
        ///班别日期
        /// </summary>
        public DateTime DateAt
        {
            set { _dateat = value; }
            get { return _dateat; }
        }
        private string _workerid;
        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        private string _isalwaysday;
        /// <summary>
        ///常白班
        /// </summary>
        public string IsAlwaysDay
        {
            set { _isalwaysday = value; }
            get { return _isalwaysday; }
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
        ///操作标志
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
    ///班别设置实体模型
    /// </summary>
    [Serializable]
    public partial class AttendClassTypeModel
    {
        public AttendClassTypeModel()
        { }
        #region Model
        private DateTime _datefrom;
        /// <summary>
        ///班别起始日期
        /// </summary>
        public DateTime DateFrom
        {
            set { _datefrom = value; }
            get { return _datefrom; }
        }
        private DateTime _dateto;
        /// <summary>
        ///班别截止日期
        /// </summary>
        public DateTime DateTo
        {
            set { _dateto = value; }
            get { return _dateto; }
        }
        private string _workerid;
        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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
        private string _classtype;
        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        private string _isalwaysday;
        /// <summary>
        ///常白班
        /// </summary>
        public string IsAlwaysDay
        {
            set { _isalwaysday = value; }
            get { return _isalwaysday; }
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
        ///操作标志
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
    ///当月刷卡数据模型
    /// </summary>
    [Serializable]
    public partial class AttendSlodFingerDataCurrentMonthModel
    {
        public AttendSlodFingerDataCurrentMonthModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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

        private string _classtype;

        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }

        private DateTime _attendancedate;

        /// <summary>
        ///出勤日期
        /// </summary>
        public DateTime AttendanceDate
        {
            set { _attendancedate = value; }
            get { return _attendancedate; }
        }

        private string _cardid;

        /// <summary>
        ///卡号
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        private string _weekday;

        /// <summary>
        ///周几
        /// </summary>
        public string WeekDay
        {
            set { _weekday = value; }
            get { return _weekday; }
        }

        private string _cardtype;

        /// <summary>
        ///卡类型
        /// </summary>
        public string CardType
        {
            set { _cardtype = value; }
            get { return _cardtype; }
        }

        private string _yearmonth;

        /// <summary>
        ///考勤年月分
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
        }

        private string _slotcardtime1;

        /// <summary>
        ///刷卡时间1
        /// </summary>
        public string SlotCardTime1
        {
            set { _slotcardtime1 = value; }
            get { return _slotcardtime1; }
        }

        private string _slotcardtime2;

        /// <summary>
        ///刷卡时间2
        /// </summary>
        public string SlotCardTime2
        {
            set { _slotcardtime2 = value; }
            get { return _slotcardtime2; }
        }

        private string _slotcardtime;

        /// <summary>
        ///刷卡时间
        /// </summary>
        public string SlotCardTime
        {
            set { _slotcardtime = value; }
            get { return _slotcardtime; }
        }

        private string _leavetype;

        /// <summary>
        ///假别名称
        /// </summary>
        public string LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }

        private double _leavehours;

        /// <summary>
        ///请假时数
        /// </summary>
        public double LeaveHours
        {
            set { _leavehours = value; }
            get { return _leavehours; }
        }

        private string _leavetimeregion;

        /// <summary>
        ///请假时段
        /// </summary>
        public string LeaveTimeRegion
        {
            set { _leavetimeregion = value; }
            get { return _leavetimeregion; }
        }

        private string _leavedescription;

        /// <summary>
        ///请假详述
        /// </summary>
        public string LeaveDescription
        {
            set { _leavedescription = value; }
            get { return _leavedescription; }
        }

        private string _leavememo;

        /// <summary>
        ///请假备注
        /// </summary>
        public string LeaveMemo
        {
            set { _leavememo = value; }
            get { return _leavememo; }
        }

        private int _leavemark;

        /// <summary>
        ///请假标识
        /// </summary>
        public int LeaveMark
        {
            set { _leavemark = value; }
            get { return _leavemark; }
        }

        private int _slotexceptionmark;

        /// <summary>
        ///刷卡异常标识
        /// </summary>
        public int SlotExceptionMark
        {
            set { _slotexceptionmark = value; }
            get { return _slotexceptionmark; }
        }

        private string _slotexceptiontype;

        /// <summary>
        ///刷卡异常类型
        /// </summary>
        public string SlotExceptionType
        {
            set { _slotexceptiontype = value; }
            get { return _slotexceptiontype; }
        }

        private string _slotexceptionmemo;

        /// <summary>
        ///刷卡异常备注
        /// </summary>
        public string SlotExceptionMemo
        {
            set { _slotexceptionmemo = value; }
            get { return _slotexceptionmemo; }
        }

        private int _handleslotexceptionstatus;

        /// <summary>
        ///处理刷卡异常状态
        /// </summary>
        public int HandleSlotExceptionStatus
        {
            set { _handleslotexceptionstatus = value; }
            get { return _handleslotexceptionstatus; }
        }

        private string _forgetslotreason;

        /// <summary>
        ///漏刷卡原因
        /// </summary>
        public string ForgetSlotReason
        {
            set { _forgetslotreason = value; }
            get { return _forgetslotreason; }
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
        ///操作标志
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
    /// 考勤数据查询Dto
    /// </summary>
    public partial class AttendanceDataQueryDto
    {
        public int SearchMode { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Department { get; set; }

        public string WorkerId { get; set; }

        public string YearMonth { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }

    /// <summary>
    ///实时刷卡数据模型
    /// </summary>
    [Serializable]
    public partial class AttendFingerPrintDataInTimeModel
    {
        public AttendFingerPrintDataInTimeModel()
        { }

        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
        }

        private string _cardid;

        /// <summary>
        ///登记卡号
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        private string _cardtype;

        /// <summary>
        ///刷卡类型
        /// </summary>
        public string CardType
        {
            set { _cardtype = value; }
            get { return _cardtype; }
        }

        private DateTime _slodcardtime;

        /// <summary>
        ///刷卡时间
        /// </summary>
        public DateTime SlodCardTime
        {
            set { _slodcardtime = value; }
            get { return _slodcardtime; }
        }

        private DateTime _slodcarddate;

        /// <summary>
        ///刷卡日期
        /// </summary>
        public DateTime SlodCardDate
        {
            set { _slodcarddate = value; }
            get { return _slodcarddate; }
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
    /// 考勤数据模型
    /// </summary>
    public class AttendanceDataModel
    {
        #region Model

        private string _workerid;

        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }

        private string _workername;

        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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

        private string _classtype;

        /// <summary>
        ///班别
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }

        private DateTime _attendancedate;

        /// <summary>
        ///出勤日期
        /// </summary>
        public DateTime AttendanceDate
        {
            set { _attendancedate = value; }
            get { return _attendancedate; }
        }

        private string _cardid;

        /// <summary>
        ///卡号
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        private string _cardtype;

        /// <summary>
        ///卡类型
        /// </summary>
        public string CardType
        {
            set { _cardtype = value; }
            get { return _cardtype; }
        }

        private string _week;

        /// <summary>
        ///周天次
        /// </summary>
        public string Week
        {
            set { _week = value; }
            get { return _week; }
        }

        private string _slotcardtime1;

        /// <summary>
        ///刷卡时间1
        /// </summary>
        public string SlotCardTime1
        {
            set { _slotcardtime1 = value; }
            get { return _slotcardtime1; }
        }

        private string _slotcardtime2;

        /// <summary>
        ///刷卡时间2
        /// </summary>
        public string SlotCardTime2
        {
            set { _slotcardtime2 = value; }
            get { return _slotcardtime2; }
        }

        private string _slotcardtime3;

        /// <summary>
        ///刷卡时间3
        /// </summary>
        public string SlotCardTime3
        {
            set { _slotcardtime3 = value; }
            get { return _slotcardtime3; }
        }

        private string _slotcardtime4;

        /// <summary>
        ///刷卡时间4
        /// </summary>
        public string SlotCardTime4
        {
            set { _slotcardtime4 = value; }
            get { return _slotcardtime4; }
        }

        private string _slotcardtime;

        /// <summary>
        ///刷卡时间
        /// </summary>
        public string SlotCardTime
        {
            set { _slotcardtime = value; }
            get { return _slotcardtime; }
        }

        private string _LeaveType;

        /// <summary>
        ///请假类别
        /// </summary>
        public string LeaveType
        {
            set { _LeaveType = value; }
            get { return _LeaveType; }
        }

        private double _LeaveHours;

        /// <summary>
        ///请假时数
        /// </summary>
        public double LeaveHours
        {
            set { _LeaveHours = value; }
            get { return _LeaveHours; }
        }

        private string _LeaveTimeRegion;

        /// <summary>
        ///请假时段
        /// </summary>
        public string LeaveTimeRegion
        {
            set { _LeaveTimeRegion = value; }
            get { return _LeaveTimeRegion; }
        }

        private string _LeaveDescription;

        /// <summary>
        ///请假描述
        /// </summary>
        public string LeaveDescription
        {
            set { _LeaveDescription = value; }
            get { return _LeaveDescription; }
        }
        #endregion Model
    }

    /// <summary>
    ///请假领域模型
    /// </summary>
    [Serializable]
    public class AttendAskLeaveModel
    {
        public AttendAskLeaveModel()
        { }
        #region Model
        private string _workerid;
        /// <summary>
        ///作业工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _workername;
        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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
        private DateTime _attendancedate;
        /// <summary>
        ///出勤日期
        /// </summary>
        public DateTime AttendanceDate
        {
            set { _attendancedate = value; }
            get { return _attendancedate; }
        }
        private string _slotcardtime;
        /// <summary>
        ///刷卡时间
        /// </summary>
        public string SlotCardTime
        {
            set { _slotcardtime = value; }
            get { return _slotcardtime; }
        }
        private string _leavetype;
        /// <summary>
        ///请假类别
        /// </summary>
        public string LeaveType
        {
            set { _leavetype = value; }
            get { return _leavetype; }
        }
        private double _leavehours;
        /// <summary>
        ///请假时数
        /// </summary>
        public double LeaveHours
        {
            set { _leavehours = value; }
            get { return _leavehours; }
        }
        private string _leavetimeregion;
        /// <summary>
        ///时间段
        /// </summary>
        public string LeaveTimeRegion
        {
            set { _leavetimeregion = value; }
            get { return _leavetimeregion; }
        }
        private DateTime _leavetimeregionstart;
        /// <summary>
        ///开始时间
        /// </summary>
        public DateTime LeaveTimeRegionStart
        {
            set { _leavetimeregionstart = value; }
            get { return _leavetimeregionstart; }
        }
        private DateTime _leavetimeregionend;
        /// <summary>
        ///结束时间
        /// </summary>
        public DateTime LeaveTimeRegionEnd
        {
            set { _leavetimeregionend = value; }
            get { return _leavetimeregionend; }
        }
        private string _leavememo;
        /// <summary>
        ///备注
        /// </summary>
        public string LeaveMemo
        {
            set { _leavememo = value; }
            get { return _leavememo; }
        }
        private int _day;
        /// <summary>
        ///天
        /// </summary>
        public int Day
        {
            set { _day = value; }
            get { return _day; }
        }
        private string _yearmonth;
        /// <summary>
        ///请假年月
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
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
        private string _field1;
        /// <summary>
        ///预留字段1
        /// </summary>
        public string Field1
        {
            set { _field1 = value; }
            get { return _field1; }
        }
        private string _field2;
        /// <summary>
        ///预留字段2
        /// </summary>
        public string Field2
        {
            set { _field2 = value; }
            get { return _field2; }
        }
        private string _field3;
        /// <summary>
        ///预留字段3
        /// </summary>
        public string Field3
        {
            set { _field3 = value; }
            get { return _field3; }
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
    /// 考勤请假条目
    /// </summary>
    public class AttendAskLeaveEntry
    {


        /// <summary>
        /// 请假时数
        /// </summary>
        public double AskLeaveHours { get; set; }
        /// <summary>
        /// 请假类型
        /// </summary>
        public string AskLeaveType { get; set; }
        /// <summary>
        /// 请假时段
        /// </summary>
        public string AskLeaveRegion { get; set; }
        /// <summary>
        /// 请假描述信息
        /// </summary>
        public string AskLeaveDescription { get; set; }
    }

}