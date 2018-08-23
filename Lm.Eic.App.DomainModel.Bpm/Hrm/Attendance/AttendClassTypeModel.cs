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
        /// <summary>
        /// 星期
        /// </summary>
        public string  WeekDay { set; get; }
        private string _slotcardtime1=string.Empty;

        /// <summary>
        ///刷卡时间1
        /// </summary>
        public string SlotCardTime1
        {
            set { _slotcardtime1 = value; }
            get { return _slotcardtime1; }
        }

        private string _slotcardtime2 = string.Empty;

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

        private string _slotcardtime4=string.Empty;
        /// <summary>
        ///刷卡时间4
        /// </summary>
        public string SlotCardTime4
        { 
            get
            {
                if (SlotCardTime1!=null&&  SlotCardTime1 != string.Empty&& SlotCardTime1.Length>=16)
                {
                    if (SlotCardTime2 != null && SlotCardTime2 != string.Empty && SlotCardTime2.Length >= 16)
                    {
                        _slotcardtime4 = SlotCardTime1.Substring(11, 5)+"," + SlotCardTime2.Substring(11, 5);
                    }
                    else  _slotcardtime4 = SlotCardTime1.Substring(11, 5);
                }
               else
                {
                    if (SlotCardTime2 != null && SlotCardTime2 != string.Empty && SlotCardTime2.Length >= 16)
                    {
                        _slotcardtime4 = SlotCardTime2.Substring(11, 5);
                    }
                }
                return _slotcardtime4;
            }
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
        DateTime _dayGoWorkTimePoint = Convert.ToDateTime("07:50");
        /// <summary>
        /// 白班上班时间点
        /// </summary>
        public DateTime DayGoWorkTimePoint
        {
            set { _dayGoWorkTimePoint = value; }
            get { return _dayGoWorkTimePoint; }
        }
        DateTime _dayLeaveWorkTimePoint;
        /// <summary>
        /// 白班下班时间点
        /// </summary>
        public  DateTime DayLeaveWorkTimePoint
        {
            set { _dayLeaveWorkTimePoint = value; }

            get
            {
                if (_dayGoWorkTimePoint != default(DateTime) && _dayLeaveWorkTimePoint == default(DateTime) )
                    _dayLeaveWorkTimePoint = _dayGoWorkTimePoint.AddHours(9).AddMinutes(20);
                return _dayLeaveWorkTimePoint;
            }
        }



        DateTime _nightGoWorkTimePoint = Convert.ToDateTime("19:50") ;
        /// <summary>
        /// 晚班上班时间点
        /// </summary>
        public DateTime NightGoWorkTimePoint
        {
            set { _nightGoWorkTimePoint = value; }
            get
            {
                return _nightGoWorkTimePoint;
            }
        }
        DateTime _nightLeaveWorkTimePoint ;
        /// <summary>
        /// 晚班下班时间点
        /// </summary>
        public DateTime NightLeaveWorkTimePoint
        {
            set { _dayLeaveWorkTimePoint = value; }
            get
            {
                if (_nightGoWorkTimePoint != default(DateTime) && _nightLeaveWorkTimePoint == default(DateTime))
                    _dayLeaveWorkTimePoint = _nightGoWorkTimePoint.AddHours(9).AddMinutes(20); 
                return _dayLeaveWorkTimePoint;
            }
        }

        /// <summary>
        /// 异常原因
        /// </summary>
        string _specialCause;
        public string SpecialCause
        {
            set { _specialCause=value;}
            get
            {
                return HandleSpecialCause(this.LeaveType,this.WeekDay,this.SlotCardTime1,this.SlotCardTime2);
            }
        }
        /// <summary>
        /// 处理异常原因
        /// </summary>
        /// <param name="leaveType">请假类型</param>
        /// <param name="weekDay">星期</param>
        /// <param name="slotCardTime1">上班时间</param>
        /// <param name="slotCardTime2">下班时间</param>
        /// <returns></returns>
        private string HandleSpecialCause(string leaveType,string weekDay,string slotCardTime1,string slotCardTime2)
        {
            if (leaveType != null && leaveType != string.Empty) return " ";
            if (weekDay == "星期日" || weekDay == "星期六" || weekDay == "法定假日" || weekDay == "星期六日")
            {
                ///都为空
                if ((slotCardTime1 == null || slotCardTime1 == string.Empty || slotCardTime1.Length < 16) &&
                       (slotCardTime2 == null || slotCardTime2 == string.Empty || slotCardTime2.Length < 16))
                { return " "; }
                ///都不空
                if (slotCardTime1 != null && slotCardTime1 != string.Empty &&
                     slotCardTime2 != null && slotCardTime2 != string.Empty)
                { return " "; }
                else return "漏刷卡";
            }
            if (ClassType == "白班")
            {
                if (slotCardTime1 != null && slotCardTime1 != string.Empty && slotCardTime1.Length >= 16)
                {
                    //上班时间
                   DateTime doTime = Convert.ToDateTime(SlotCardTime1.Substring(11, 5));
                    if (DayGoWorkTimePoint.AddMinutes(1) <= doTime && doTime <= DayGoWorkTimePoint.AddMinutes(6))
                    {
                        if (slotCardTime2 == null || slotCardTime2 == string.Empty) return "旷工";
                        return "迟到";
                    }
                    if (doTime > DayGoWorkTimePoint.AddMinutes(6)) return "旷工";
                    //下班时间
                    if (slotCardTime2 == null || slotCardTime2 == string.Empty || slotCardTime2.Length < 16) return "漏刷卡";
                    else
                    {
                        doTime = Convert.ToDateTime(slotCardTime2.Substring(11, 5));
                        if (doTime < DayLeaveWorkTimePoint) return "旷工";
                    }

                }
                else
                {
                    if (slotCardTime2 != null && slotCardTime2 != string.Empty) return "漏刷卡";
                    else return "旷工";
                }
            }
            if (ClassType == "晚班")
            {
                if (SlotCardTime1 != null && SlotCardTime1 != string.Empty && SlotCardTime1.Length >= 16)
                {
                    #region 上班时间
              
                    DateTime  doTime = Convert.ToDateTime(SlotCardTime1.Substring(11, 5));
                    if (NightGoWorkTimePoint.AddMinutes(1) <= doTime && doTime <= NightGoWorkTimePoint.AddMinutes(6))
                    {
                        ///没有下班数据
                        if (slotCardTime2 == null || slotCardTime2 == string.Empty) return "旷工";
                        return "迟到";
                    }
                    ///上班时间超出 迟到充许的范围 
                    if (doTime > NightGoWorkTimePoint.AddMinutes(6)) return "旷工";
                    #endregion
                    #region  下班时间
                    ///没有下班时间
                    if (slotCardTime2 == null || slotCardTime2 == string.Empty || slotCardTime2.Length < 16) return "漏刷卡";
                    ///下班 上班时间 都有
                    else
                    {
                        /// 先判断是不是 上班的第二天时间  
                       if( Convert.ToDateTime( slotCardTime2).Date== Convert.ToDateTime(slotCardTime1).Date.AddDays(1))
                        {
                           /// 判定时间点是不是正确
                           /// 上班时间日期  加要实际下班时间点   
                            doTime = Convert.ToDateTime(NightLeaveWorkTimePoint.ToString("yyyy-MM-dd")+" "+slotCardTime2.Substring(11, 5));
                            if (doTime < NightLeaveWorkTimePoint) return "旷工";
                            else return " ";
                        }
                        else return "晚班时间点不对";
                    }
                    #endregion
                }
                else
                {
                    if (slotCardTime2 != null && slotCardTime2 != string.Empty) return "漏刷卡";
                    else return "旷工";
                }
            }
            return " ";
        }
        #endregion Model
    }
    /// <summary>
    /// 考勤数据汇总所需人员信息
    /// </summary>
    public class AttendanceWrkInfo
    {
        public AttendanceWrkInfo()
        {
           
        }
        public string workerid { set; get; }
        public string workerName { set; get; }
        /// <summary>
        /// 本月工作结束日期 WorkerName, LeaveDate
        /// </summary>
        public string LeaveDate { set; get; }
        public string  sDepartment { set; get; }
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            
            get { return Departmentchange(sDepartment); }
        }
        string Departmentchange(string department)
        {
            if (department == null) return string.Empty;
            switch (department.Trim())
            {
                case "XZC": return "行政处";
                case "FN": return "财务部";
                case "AD": return "管理部";
                case "HR": return "人事课";
                case "GA": return "总务课";
                case "PR": return "采购部";
                case "SD": return "业务部";
                case "JYQHS": return "经营企划室";
                case "YFC": return "研发处";
                case "RD": return "开发部";
                case "DC1": return "设计一课";
                case "DC2": return "设计二课";
                case "DC3": return "设计三课";
                case "ED": return "设备课";
                case "IC": return "仪器课";
                case "PT": return "生技部";
                case "MC": return "机加工课";
                case "MD": return "成型课";
                case "QA": return "品保部";
                case "RF": return "RF品保课";
                case "OP": return "OP品保课";
                case "ZZC": return "制造处";
                case "PD": return "生管部";
                case "PM": return "生管课";
                case "PMC": return "资材课";
                case "MD1": return "制一部";
                case "MS1": return "制一课";
                case "MS6": return "制六课";
                case "MD3": return "制三部";
                case "MS5": return "制五课";
                case "MS8": return "制八课";
                case "MS9": return "制九课";
                case "EC": return "工程课";
                case "MD5": return "制五部";
                case "MS3": return "制三课";
                case "MS7": return "制七课";
                case "MD6": return "制六部";
                case "MS2": return "制二课";
                case "MS10": return "制十课";
                case "EIC": return "企业讯息中心";
                case "EAC": return "企业自动化中心";
                case "PZYEHS": return "品质与EHS委员会";
                case "HN": return "环安课";
                case "IQC": return "IQC品保课";
                case "FA": return "会计课";
                case "CA": return "关务课";
                default: 
                    return department.Trim ();
            }
        }
    }

    public class CalenderListInfo
    {
        public DateTime CalendarDate { set; get; }
        public string  CalendarYear { set; get; }
        public string CalendarMonth { set; get; }
        public string CalendarDay { set; get; }
        public string CalendarWeek { set; get; }
        public string  DateProperty { set; get; }
    }
    public class AttendanceClassTypeInfo
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string workerid { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string workerName { set; get; }
        /// <summary>
        /// 班别
        /// </summary>
        public string ClassType { set; get; }
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

    /// <summary>
    /// 考勤请假汇总项
    /// </summary>
    public class AttendAskLeaveSumerizeItem
    {
        public string WorkerId { get; private set; }

        public string WorkerName { get; private set; }

        public string Department { get; private set; }
        /// <summary>
        /// 直接/间接
        /// </summary>
        public string PostType { get; set; }
        /// <summary>
        /// 事假
        /// </summary>
        public double SJ { get; set; }
        /// <summary>
        /// 病假
        /// </summary>
        public double BJ { get; set; }
        /// <summary>
        /// 有薪事假
        /// </summary>
        public double YXSJ { get; set; }
        /// <summary>
        /// 有薪病假
        /// </summary>
        public double YXBJ { get; set; }
        /// <summary>
        /// 年休假
        /// </summary>
        public double NXJ { get; set; }

        /// <summary>
        /// 工伤假
        /// </summary>
        public double GSJ { get; set; }
        /// <summary>
        /// 婚假
        /// </summary>
        public double HJ { get; set; }
        /// <summary>
        /// 丧假
        /// </summary>
        public double ShangJ { get; set; }
        /// <summary>
        /// 产假
        /// </summary>
        public double CJ { get; set; }
        /// <summary>
        /// 陪产假
        /// </summary>
        public double PCJ { get; set; }
        /// <summary>
        /// 旷工
        /// </summary>
        public double KGJ { get; set; }

        /// <summary>
        /// 调休假
        /// </summary>
        public double TXJ { get; set; }

        public AttendAskLeaveSumerizeItem()
        { }

        public AttendAskLeaveSumerizeItem(string workerId, string workerName, string department)
        {
            this.WorkerId = workerId;
            this.WorkerName = workerName;
            this.Department = department;
        }
        public static AttendAskLeaveSumerizeItem Create(string workerId, string workerName, string department)
        {
            return new AttendAskLeaveSumerizeItem(workerId, workerName, department);
        }
    }

}