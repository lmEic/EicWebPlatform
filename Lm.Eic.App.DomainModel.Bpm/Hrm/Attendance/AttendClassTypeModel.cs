using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance
{
    /// <summary>
    ///班别设置实体模型
    /// </summary>
    [Serializable]
    public partial class AttendClassTypeModel
    {
        public AttendClassTypeModel()
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
        private string _isalwaysday;
        /// <summary>
        ///常白班
        /// </summary>
        public string IsAlwaysDay
        {
            set { _isalwaysday = value; }
            get { return _isalwaysday; }
        }
        private DateTime _datefrom;
        /// <summary>
        ///开始日期
        /// </summary>
        public DateTime DateFrom
        {
            set { _datefrom = value; }
            get { return _datefrom; }
        }
        private DateTime _dateto;
        /// <summary>
        ///截至日期
        /// </summary>
        public DateTime DateTo
        {
            set { _dateto = value; }
            get { return _dateto; }
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
        #endregion Model
    }
    /// <summary>
    /// 请假信息模型
    /// </summary>
    public class AttendAskLeaveModel
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


        string _LeaveTimeRegionStart;
        public string LeaveTimeRegionStart
        {
            get
            {
                return _LeaveTimeRegionStart;
            }
            set
            {
                if (_LeaveTimeRegionStart != value)
                {
                    _LeaveTimeRegionStart = value;
                }
            }
        }

        string _LeaveTimeRegionEnd;
        public string LeaveTimeRegionEnd
        {
            get
            {
                return _LeaveTimeRegionEnd;
            }
            set
            {
                if (_LeaveTimeRegionEnd != value)
                {
                    _LeaveTimeRegionEnd = value;
                }
            }
        }
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
        private int _leavemark;
        /// <summary>
        ///请假标识
        /// </summary>
        public int LeaveMark
        {
            set { _leavemark = value; }
            get { return _leavemark; }
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
        DateTime  _startLeaveDate;
        public DateTime  StartLeaveDate
        {
            get
            {
                return _startLeaveDate;
            }
            set
            {
                if (_startLeaveDate != value)
                {
                    _startLeaveDate = value;
                }
            }
        }

        DateTime  _endLeaveDate;
        public DateTime  EndLeaveDate
        {
            get
            {
                return _endLeaveDate;
            }
            set
            {
                if (_endLeaveDate != value)
                {
                    _endLeaveDate = value;
                }
            }
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
        #endregion Model
    }
}
