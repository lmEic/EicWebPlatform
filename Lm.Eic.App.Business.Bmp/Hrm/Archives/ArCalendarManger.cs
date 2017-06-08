using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArCalendarManger
    {
        /// <summary>
        ///
        /// </summary>
        ArcalendarCurd curd
        {
            get { return OBulider.BuildInstance<ArcalendarCurd>(); }
        }


        public List<CalendarModel> GetDateDictionary(int nowYear, int nowMonth)
        {
            List<CalendarModel> returnDateDictionary = new List<CalendarModel>();
            var ListModel = curd.FindCalendarDateListBy(nowYear, nowMonth);
            if (ListModel == null || ListModel.Count <= 0) return returnDateDictionary;
            //得到当月所有日期周次
            var nowMonthWeeksList = ListModel.Select(e => e.NowMonthWeekNumber).Distinct().ToList();
            if (nowMonthWeeksList == null || nowMonthWeeksList.Count <= 0) return returnDateDictionary;
            nowMonthWeeksList.ForEach(W =>
            {
                var models = ListModel.Where(e => e.NowMonthWeekNumber == W).ToList();
                int modelsCount = models.Count;
                if (0 < modelsCount && modelsCount < 7)
                {
                    int InsertIndex = (W == 1) ? 0 : modelsCount;
                    int yearWeek = models.FirstOrDefault().YearWeekNumber;
                    for (int n = 1; n <= 7 - modelsCount; n++)
                    { models.Insert(InsertIndex, new CalendarModel() { YearWeekNumber = yearWeek, CalendarDay = string.Empty }); }
                }
                models.ForEach(e =>
                {
                    if (e.CalendarDay != string.Empty)
                    { e.ChineseCalendar = new ChineseCalendar(e.CalendarDate).ChineseDayString; }
                    returnDateDictionary.Add(e);
                });
            });
            return returnDateDictionary;
        }
        /// <summary>
        /// 获取该月日历模型
        /// </summary>
        /// <param name="qryYear"></param>
        /// <param name="qryMonth"></param>
        /// <returns></returns>
        public MonthCalendarModel GetMonthCalendar(int qryYear, int qryMonth)
        {
            return this.curd.GetMonthCalendar(qryYear, qryMonth);
        }
        public OpResult store(CalendarModel model)
        {
            return curd.Store(model);
        }
    }

    internal class ArcalendarCurd : CrudBase<CalendarModel, ICalendarsRepository>
    {
        public ArcalendarCurd()
            : base(new CalendarsRepository(), "行事历")
        { }
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddReportAttendence);
            AddOpItem(OpMode.Edit, EditReportAttendece);
        }

        private OpResult EditReportAttendece(CalendarModel model)
        {
            model.DateColor = CalendarColor(model.DateProperty);
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        private string CalendarColor(string CalendarProty)
        {
            switch (CalendarProty)
            {
                case "法定假日":
                    return "#29B8CB";
                case "补班":
                    return "yellow";
                case "休假":
                    return "violet";
                case "星期六日":
                    return "red";
                case "正常":
                    return "white";
                default:
                    return "white";
            }
        }
        /// <summary>
        /// 添加一年行事历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddReportAttendence(CalendarModel model)
        {
            try
            {
                int i = 0;
                string year = model.CalendarYear.ToString("0000");
                if (irep.IsExist(e => e.CalendarYear == model.CalendarYear))
                    return OpResult.SetErrorResult(year + "年行事历已经存在");
                DateTime beginDate = DateTime.Parse(year + "-01-01");
                DateTime endDate = DateTime.Parse(year + "-12-31");
                List<DateTime> adlldate = new List<DateTime>();
                while (beginDate <= endDate)
                {
                    adlldate.Add(beginDate);
                    beginDate = beginDate.AddDays(1);
                }
                adlldate.ForEach(d =>
                {
                    string dateProperty = GetDateProperty((int)d.DayOfWeek);
                    var newModel = new CalendarModel()
                    {
                        CalendarDate = d,
                        CalendarDay = d.Day.ToString(),
                        CalendarMonth = d.Month,
                        CalendarYear = d.Year,
                        CalendarWeek = (int)d.DayOfWeek,
                        NowMonthWeekNumber = GetDateWeekBy(d),
                        ChineseCalendar = GetchineseCalendar(d),
                        Title = "",
                        DateProperty = dateProperty,
                        DateColor = CalendarColor(dateProperty),
                        YearWeekNumber = GetWeekOfYear(d, true),
                        OpDate = DateTime.Now.Date,
                        OpSign = "add",
                        OpTime = DateTime.Now
                    };
                    i += irep.Insert(newModel);
                });

                if (i == 365)
                    return i.ToOpResult(OpContext + "一年日行事历操作成功");
                else return i.ToOpResult(OpContext + (365 - i) + "天" + "操作失败");
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string GetDateProperty(int CalendarWeek)
        {
            if (CalendarWeek == 0 | CalendarWeek == 6)
                return "星期六日";
            else return "正常";
        }

        public List<CalendarModel> FindCalendarDateListBy(int nowYear, int nowMonth)
        {
            return irep.Entities.Where(e => e.CalendarYear == nowYear && e.CalendarMonth == nowMonth).ToList();
        }
        /// <summary>
        /// 获取月日历模型
        /// </summary>
        /// <param name="qryYear"></param>
        /// <param name="qryMonth"></param>
        /// <returns></returns>
        public MonthCalendarModel GetMonthCalendar(int qryYear, int qryMonth)
        {
            MonthCalendarModel monthCalendar = new MonthCalendarModel() { QryYear = qryYear, QryMonth = qryMonth };
            WeekCalendarModel weekCalendar = null;
            var datas = FindCalendarDateListBy(qryYear, qryMonth);
            if (datas == null || datas.Count == 0) return monthCalendar;
            //该月份周次列表
            List<int> weekList = datas.OrderBy(o => o.YearWeekNumber).Select(s => s.YearWeekNumber).Distinct().ToList();
            int weekCount = weekList.Count;
            for (int i = 0; i < weekCount; i++)
            {
                int week = weekList[i];
                weekCalendar = new WeekCalendarModel() { Week = week, WeekDays = new List<WeekDayModel>() };
                WeekDayModel day = null;
                for (int w = 0; w <= 6; w++)
                {
                    day = new WeekDayModel() { Id = w };
                    var m = datas.FirstOrDefault(e => e.CalendarWeek == w);
                    if (m != null)
                    {
                        day.ChineseCalendar = m.ChineseCalendar;
                        day.Day = m.CalendarDay;

                    }
                    else
                    {
                        day.ChineseCalendar = "";
                        day.Day = "";
                    }
                    weekCalendar.WeekDays.Add(day);
                }
            }
            return monthCalendar;
        }
        /// <summary>
        /// 获取日期是当月中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int GetDateWeekBy(DateTime dt)
        {
            bool sundayStart = true;
            //如果要判断的日期为1号，则肯定是第一周了
            if (dt.Day == 1)
                return 1;
            else
            {
                //得到本月第一天
                DateTime dtStart = new DateTime(dt.Year, dt.Month, 1);
                //得到本月第一天是周几
                int dayofweek = (int)dtStart.DayOfWeek;
                //如果不是以周日开始，需要重新计算一下dayofweek，详细DayOfWeek枚举的定义
                if (!sundayStart)
                {
                    dayofweek = dayofweek - 1;
                    if (dayofweek < 0)
                        dayofweek = 7;
                }
                //得到本月的第一周一共有几天
                int startWeekDays = 7 - dayofweek;
                //如果要判断的日期在第一周范围内，返回1
                if (dt.Day <= startWeekDays)
                    return 1;
                else
                {
                    int aday = dt.Day + 7 - startWeekDays;
                    return aday / 7 + (aday % 7 > 0 ? 1 : 0);
                }
            }
        }
        /// <summary>
        /// 获取日期是全年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sundayFirstDay">星期天是否本周第一天</param>
        /// <returns></returns>

        public int GetWeekOfYear(DateTime dt, bool sundayFirstDay)
        {
            try
            {
                GregorianCalendar gc = new GregorianCalendar();
                if (sundayFirstDay)
                    return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                else return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
            catch (Exception)
            {

                throw;
            }


        }

        private static string GetchineseCalendar(DateTime date)
        {
            return new ChineseCalendar(date).ChineseDayString;
        }
    }
    /// <summary>
    /// 月日历模型
    /// </summary>
    public class MonthCalendarModel
    {
        /// <summary>
        /// 查询年份
        /// </summary>
        public int QryYear { get; set; }
        /// <summary>
        /// 查询月份
        /// </summary>
        public int QryMonth { get; set; }
        /// <summary>
        /// 一周天数
        /// </summary>
        public List<WeekDayModel> WeekDays
        {
            get
            {
                return new List<WeekDayModel>() {
                    new WeekDayModel() { Id=-1, Day="Week" },
                    new WeekDayModel() { Id=0,Day="Sun" },
                    new WeekDayModel() { Id=1,Day="Mon" },
                    new WeekDayModel() { Id=2,Day="Tue" },
                    new WeekDayModel() { Id=3,Day="Wed" },
                    new WeekDayModel() { Id=4,Day="Thu" },
                    new WeekDayModel() { Id=5,Day="Fri" },
                    new WeekDayModel() { Id=6,Day="Sat" }
                };
            }
        }
        /// <summary>
        /// 周历列表
        /// </summary>
        public List<WeekCalendarModel> WeekCalendars { get; set; }
    }
    /// <summary>
    /// 周历模型
    /// </summary>
    public class WeekCalendarModel
    {
        /// <summary>
        /// 周次
        /// </summary>
        public int Week { get; set; }
        public List<WeekDayModel> WeekDays { get; set; }

    }

    public class WeekDayModel
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string ChineseCalendar { get; set; }
    }
}

