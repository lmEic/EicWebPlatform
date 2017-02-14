using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public   class ArCalendarManger
    {
        /// <summary>
        /// 
        /// </summary>
         ArcalendarCurd ArcalendarCurd
        {
            get { return OBulider.BuildInstance<ArcalendarCurd>(); }
        }
        public Dictionary<int, CalendarModel> GetDateDictionary(string nowYear, string nowMonth)
        {
            Dictionary<int, CalendarModel> retrunDateDictionary = new Dictionary<int, CalendarModel>();
            int year = Convert.ToInt32(nowYear);
            int month = Convert.ToInt32(nowMonth);
            var ListModel = ArcalendarCurd. getDateLiatBy(year, month);
            //得到当月所有日期周次
            List<int> nowMonthWeeksList = ListModel.Select(e => e.NowMothWeekNumber).Distinct().ToList();
            //
            Dictionary<int, List<CalendarModel>> dicCalendarModel = new Dictionary<int, List<CalendarModel>>();
            nowMonthWeeksList.ForEach(W =>
            {
                List<CalendarModel> models = ListModel.Where(e => e.NowMothWeekNumber == W).ToList();
                int ii = models.Count;
                if (ii < 7)
                {
                    if (W == 1)
                    {
                        for (int n = 1; n <= 7 - ii; n++)
                        {
                            models.Insert(0, new CalendarModel());
                        }
                    }
                    else
                    {
                        for (int n = 1; n <= 7 - ii; n++)
                        {
                            models.Insert(ii, new CalendarModel());
                        }
                    }
                }
                dicCalendarModel.Add(W, models);
            });
            int i = 0;
            foreach (var item in dicCalendarModel)
            {
                item.Value.ForEach(e =>
                {
                    retrunDateDictionary.Add(i, e);
                    i++;
                });
            }
            return retrunDateDictionary;
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
        /// <returns></returns>

        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            DateTime NewYearsDay = new DateTime(dt.Year, 1, 1);
            //如果元旦那天刚好是星期天 没全年周次减1
            if ((int)NewYearsDay.DayOfWeek == 0)
            { weekOfYear -= 1; }
            return weekOfYear;
        }
    }

    internal  class ArcalendarCurd : CrudBase<CalendarModel, ICalendarsRepository>
    {
        public ArcalendarCurd ():base (new CalendarsRepository(),"行事历")
        {

        }
        protected override void AddCrudOpItems()
        {
            
        }
        public List<CalendarModel> getDateLiatBy(int nowYear, int  nowMonth)
        {
            return irep.Entities.Where(e => e.CalendarYear == nowYear && e.CalendarMoth == nowMonth).ToList ();
        }
        public Dictionary<int, CalendarModel> GetDateDictionary(string nowYear, string nowMonth)
        {
            Dictionary<int, CalendarModel> retrunDateDictionary = new Dictionary<int, CalendarModel>();
            var ListModel = irep.Entities.Where(e => e.CalendarYear == Convert.ToInt16(nowYear) && e.CalendarMoth == Convert.ToInt16(nowMonth)).ToList ();
            //得到当月所有日期周次
            List<int> nowMonthWeeksList = ListModel.Select(e => e.NowMothWeekNumber).Distinct ().ToList();
            //
            Dictionary<int, List<CalendarModel>> dicCalendarModel = new Dictionary<int, List<CalendarModel>>();
            nowMonthWeeksList.ForEach(W => {
                List<CalendarModel> models = ListModel.Where(e => e.NowMothWeekNumber == W).ToList();
                if(models.Count <7)
                {
                    if (W== 1)
                    {
                        for (int n = 0; n < 7 - models.Count; n++)
                        {
                            models.Insert(0, new CalendarModel());
                        }
                    }
                    else
                    {
                        for (int n = 0; n < 7 - models.Count; n++)
                        {
                            models.Insert(7 - models.Count, new CalendarModel());
                        }
                    }
                }
                dicCalendarModel.Add(W, models);
            });
            int i = 0;
            foreach (var item in dicCalendarModel)
            {
                item.Value.ForEach(e => {
                    retrunDateDictionary.Add(i, e);
                    i++;
                });
            }
            return retrunDateDictionary;
        }


      

    }

       
}
   
