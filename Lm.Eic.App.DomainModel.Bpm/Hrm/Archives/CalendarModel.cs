using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.Archives
{
    public class CalendarModel
    {
        #region Model
        private DateTime _calendardate;
        /// <summary>
        ///日期
        /// </summary>
        public DateTime CalendarDate
        {
            set { _calendardate = value; }
            get { return _calendardate; }
        }
        private int _calendaryear;
        /// <summary>
        ///年份
        /// </summary>
        public int CalendarYear
        {
            set { _calendaryear = value; }
            get { return _calendaryear; }
        }
        private int _calendarmonth;
        /// <summary>
        ///月份
        /// </summary>
        public int CalendarMonth
        {
            set { _calendarmonth = value; }
            get { return _calendarmonth; }
        }
        private string _calendarday;
        /// <summary>
        ///天数
        /// </summary>
        public string  CalendarDay
        {
            set { _calendarday = value; }
            get { return _calendarday; }
        }
        private int _yearweeknumber;
        /// <summary>
        ///全年周次
        /// </summary>
        public int YearWeekNumber
        {
            set { _yearweeknumber = value; }
            get { return _yearweeknumber; }
        }
        private int _nowmonthweeknumber;
        /// <summary>
        ///当月周次
        /// </summary>
        public int NowMonthWeekNumber
        {
            set { _nowmonthweeknumber = value; }
            get { return _nowmonthweeknumber; }
        }
        private int _calendarweek;
        /// <summary>
        ///周
        /// </summary>
        public int CalendarWeek
        {
            set { _calendarweek = value; }
            get { return _calendarweek; }
        }

        //ChineseCalendar
        private string _chineseCalendar;
        /// <summary>
        ///农历/节气/节日
        /// </summary>
        public string ChineseCalendar
        {
            set { _chineseCalendar = value; }
            get { return _chineseCalendar; }
        }

        private string _dateproperty;
        /// <summary>
        ///日期属性
        /// </summary>
        public string DateProperty
        {
            set { _dateproperty = value; }
            get { return _dateproperty; }
        }
        private string _datecolor;
        /// <summary>
        ///日期颜色
        /// </summary>
        public string DateColor
        {
            set { _datecolor = value; }
            get { return _datecolor; }
        }
        private string _title;
        /// <summary>
        ///日志标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
}
