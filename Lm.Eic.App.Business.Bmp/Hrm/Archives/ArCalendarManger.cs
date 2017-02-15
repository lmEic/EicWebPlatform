﻿using System;
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
        public Dictionary<int, CalendarModel> GetDateDictionary(int  nowYear, int nowMonth)
        {
            Dictionary<int, CalendarModel> returnDateDictionary = new Dictionary<int, CalendarModel>();
            var ListModel = ArcalendarCurd. FindCalendarDateListBy(nowYear, nowMonth);
            if (ListModel == null || ListModel.Count <= 0) return returnDateDictionary;
            //得到当月所有日期周次
            var nowMonthWeeksList = ListModel.Select(e => e.NowMothWeekNumber).Distinct().ToList();
            if (nowMonthWeeksList==null|| nowMonthWeeksList.Count <=0) return returnDateDictionary;
            int i = 0;
            nowMonthWeeksList.ForEach(W =>
            {
                var models = ListModel.Where(e => e.NowMothWeekNumber == W).ToList();
                int modelsCount = models.Count;
                if (0<modelsCount&&modelsCount < 7)
                {
                    int InsertIndex = (W == 1) ? 0 : modelsCount;
                    for (int n = 1; n <= 7 - modelsCount; n++)
                    { models.Insert(InsertIndex, new CalendarModel());}
                }
                models.ForEach(e =>
                {
                    returnDateDictionary.Add(i, e);
                    i++;
                });
            });
            return returnDateDictionary;
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
        {}
        protected override void AddCrudOpItems()
        {
           
        }
        public List<CalendarModel> FindCalendarDateListBy(int nowYear, int  nowMonth)
        {
            return irep.Entities.Where(e => e.CalendarYear == nowYear && e.CalendarMoth == nowMonth).ToList ();
        }

    }

       
}
   
