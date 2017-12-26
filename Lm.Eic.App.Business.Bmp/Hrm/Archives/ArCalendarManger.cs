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
            var ListModel = curd.FindCalendarDateDatasBy(nowYear, nowMonth);
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
        private string CalendarColor(string calendarProty)
        {
            switch (calendarProty)
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
                if (irep.IsExist(e => e.CalendarDate == model.CalendarDate))
                    return EditReportAttendece(model);
              return  irep.Insert(model).ToOpResult_Add("行事历操作成功");
          
            }
            catch (Exception)
            {

                throw;
            }

        }


        public List<CalendarModel> FindCalendarDateDatasBy(int nowYear, int nowMonth)
        {
            int vMax = DateTime.DaysInMonth(nowYear, nowMonth);
            List<CalendarModel> datas = irep.Entities.Where(e => e.CalendarYear == nowYear && e.CalendarMonth == nowMonth).ToList();
            if(datas!=null&& datas.Count>0) return datas;
            for (int i = 1; i < vMax; i++)
            {
                DateTime dt = new DateTime(nowYear,  nowMonth, i);
                var model = CreatNewCalendarModel(dt);
                if (!datas.Contains(model))
                {
                    datas.Add(model);
                    //并存诸
                    Store(model);
                }
            }
            return datas;
        }
        public CalendarModel CreatNewCalendarModel(DateTime day)
        {
            var getCalendar = new ChineseCalendar(day);
         
            CalendarModel carendarModel = new CalendarModel()
            {
                CalendarDate = day,
                CalendarMonth = day.Month,
                CalendarYear = day.Year,
                CalendarDay = day.Day.ToString(),
                CalendarWeek = getCalendar.WeekDayInt,
                DateColor = getCalendar.DateColor,
                DateProperty = getCalendar.DateProperty,
                ChineseCalendar = getCalendar.ChineseDayString,
                NowMonthWeekNumber = getCalendar.NowMonthWeekNumber,
                YearWeekNumber = getCalendar.YearWeekNumber,
                Title = "",
                OpSign=OpMode.Add,
                OpDate=DateTime.Now.Date,
                OpTime=DateTime.Now,
                OpPerson="初始化",
                
            };
            return carendarModel;
        }
        /// <summary>
        /// 获取月日历模型
        /// </summary>
        /// <param name="qryYear"></param>
        /// <param name="qryMonth"></param>
        /// <returns></returns>
        public MonthCalendarModel GetMonthCalendar(int qryYear, int qryMonth)
        {
            MonthCalendarModel monthCalendar = new MonthCalendarModel()
            { QryYear = qryYear, QryMonth = qryMonth, WeekCalendars = new List<WeekCalendarModel>() };
            WeekCalendarModel weekCalendar = null;
            var datas = FindCalendarDateDatasBy(qryYear, qryMonth);
            if (datas == null || datas.Count == 0) return monthCalendar;
            //该月份周次列表
            List<int> weekList = datas.OrderBy(o => o.YearWeekNumber).Select(s => s.YearWeekNumber).Distinct().ToList();
            int weekCount = weekList.Count;
            for (int i = 0; i < weekCount; i++)
            {
                int week = weekList[i];
                weekCalendar = new WeekCalendarModel() { Week = week, WeekDays = new List<WeekDayModel>() };
                var weekDatas = datas.FindAll(e => e.YearWeekNumber == week);
                WeekDayModel day = null;
                for (int w = 0; w <= 6; w++)
                {
                    day = new WeekDayModel() { Id = w, ChineseDayOfWeek = monthCalendar.WeekDays.FirstOrDefault(e => e.Id == w).ChineseDayOfWeek };
                    var m = weekDatas.FirstOrDefault(e => e.CalendarWeek == w);
                    if (m != null)
                    {
                        day.ChineseCalendar = m.ChineseCalendar;
                        day.Day = m.CalendarDay;
                        day.DateColor = m.DateColor;
                        day.Title = m.Title;
                    }
                    else
                    {
                        day.ChineseCalendar = "";
                        day.Day = "";
                    }
                    weekCalendar.WeekDays.Add(day);
                }
                monthCalendar.WeekCalendars.Add(weekCalendar);
            }
            return monthCalendar;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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
                    new WeekDayModel() { Id=-1, Day="Week",ChineseDayOfWeek="周" },
                    new WeekDayModel() { Id=0,Day="Sun",ChineseDayOfWeek="日" },
                    new WeekDayModel() { Id=1,Day="Mon",ChineseDayOfWeek="一" },
                    new WeekDayModel() { Id=2,Day="Tue",ChineseDayOfWeek="二"},
                    new WeekDayModel() { Id=3,Day="Wed",ChineseDayOfWeek="三" },
                    new WeekDayModel() { Id=4,Day="Thu",ChineseDayOfWeek="四"},
                    new WeekDayModel() { Id=5,Day="Fri",ChineseDayOfWeek="五"},
                    new WeekDayModel() { Id=6,Day="Sat",ChineseDayOfWeek="六"}
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
        /// <summary>
        /// 中国文化的星期几
        /// </summary>
        public string ChineseDayOfWeek { get; set; }
        /// <summary>
        /// 中国文化的天数
        /// </summary>
        public string ChineseCalendar { get; set; }
        /// <summary>
        /// 日期背景色
        /// </summary>
        public string DateColor { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}

