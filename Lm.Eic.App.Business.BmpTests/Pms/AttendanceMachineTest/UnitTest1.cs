using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Lm.Eic.App.Business.Attendance;
using System.Text;
using System.Globalization;

namespace Lm.Eic.App.Business.BmpTests.Pms.AttendanceTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]


        public void TestMethod1()
        {
            DateTime ddd = Convert.ToDateTime("2017-01-2");
            int ii = GetWeekOfYear(ddd);
            // AttendanceBusiness attendance = new AttendanceBusiness();
            // string msg = string.Empty;
            // attendance.OpenConnent("192.168.0.24", 1, out msg);
            //var dd= attendance.GetNewAddEnrollSourceData(1);
            // if (msg == string.Empty) { Assert.Fail(); }
        }

        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            DateTime NewYearsDay = new DateTime(dt.Year, 1, 1);
            //如果元旦那天刚好是星期天 没全年周次减1
            int dayofweek = (int)NewYearsDay.DayOfWeek;
            if (dayofweek == 0)
            {
                weekOfYear = weekOfYear - 1;
            }

            return weekOfYear;
        }


    }







    #region ChineseCalendarException
    /// <summary>
    /// 中国日历异常处理
    /// </summary>
    public class ChineseCalendarException : System.Exception
    {
        public ChineseCalendarException(string msg)
            : base(msg)
        {
        }
    }

    #endregion

}
