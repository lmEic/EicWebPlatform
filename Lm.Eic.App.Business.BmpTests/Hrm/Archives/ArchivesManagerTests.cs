using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System.Globalization;
using System.Collections.Generic;


namespace Lm.Eic.App.Business.Bmp.Hrm.Archives.Tests
{
    [TestClass()]
    public class ArchivesManagerTests
    {
        [TestMethod()]
        public void StoreTest()
        {
            string dt = "2011年12月27日--2031年12月27日";
            int pos = dt.LastIndexOf("--");
            if (pos > 0)
            {
                string et = dt.Substring(pos + 2, dt.Length - pos - 2);
                DateTime b = DateTime.Parse(et);
            }
            Assert.Fail();
        }
        [TestMethod()]
        public void testLeaveOffManager()
        {
            var model = new ArLeaveOfficeModel
            {
                ID = "42092319811109247X",
                WorkerId = "001359",
                WorkerName = "万晓桥",
                Department = "Eic",
                LeaveDate = DateTime.Now,
                LeaveReason = "漫漫流量监测",
                OpPerson = "万晓桥",
                Post = "操作工",
                Memo = "测试",
                OpSign = "add"

            };
            var Result = ArchiveService.ArchivesManager.LeaveOffManager.StoreLeaveOffInfo(model);
        }

        public void testWorkerIdChange()
        {

            var model = new WorkerChangedModel
            {
                OldWorkerId = "881359",
                NewWorkerId = "001359",
                WorkerName = "万晓桥",
                OpSign = "add",
                OpPerson = "万晓桥"



            };
            var resulst = ArchiveService.ArchivesManager.WorkerIdChangeManager.StoreWorkerIdChangeInfo(model);
        }


        public void test()
        {

            
            DateTime ddd = GetDateFromLunarDate(2018, 1,1);
            ChineseLunisolarCalendar cls = new ChineseLunisolarCalendar();
            DateTime dt = cls.ToDateTime(2014 ,1, 1, 0, 0, 0, 0).Date;

            var mm = ArchiveService.ArCalendarManger.GetMonthCalendar(2018,2);
            if (mm == null )
            {
                Assert.Fail();
            }
        }

    
        public void testReadText()
        {
            //直接读取出字符串
            string text = System.IO.File.ReadAllText(@"C:\test.txt", System.Text.Encoding.UTF8);
        }





        private static ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();
        /// <summary>  
        /// 阴历转阳历  
        /// </summary>  
        /// <param name="year">阴历年</param>  
        /// <param name="month">阴历月</param>  
        /// <param name="day">阴历日</param>  
        private DateTime GetDateFromLunarDate(int year, int month, int day)
        {
            if (year < 1902 || year > 2100)
                throw new Exception("只支持1902～2100期间的农历年");
            if (month < 1 || month > 12)
                throw new Exception("表示月份的数字必须在1～12之间");

            if (day < 1 || day > calendar.GetDaysInMonth(year, month))
                throw new Exception("农历日期输入有误");

            int num1 = 0, num2 = 0;
            //当年闰月的月份
            int leapMonth = calendar.GetLeapMonth(year);

            if (((leapMonth == month) ) || (leapMonth > 0 && leapMonth <= month))
                num2 = month;
            else
                num2 = month - 1;
            while (num2 > 0)
            {
                num1 = calendar.GetDaysInMonth(year, num2--);
            }

            DateTime dt = GetLunarNewYearDate(year);
            return dt.AddDays(num1 + day - 1);
        }

        /// <summary>  
        /// 获取指定年份春节当日（正月初一）的阳历日期  
        /// </summary>  
        /// <param name="year">指定的年份</param>  
        private static DateTime GetLunarNewYearDate(int year)
        {
            DateTime dt = new DateTime(year, 1, 1);
            int cnYear = calendar.GetYear(dt);
            int cnMonth = calendar.GetMonth(dt);

            int num1 = 0;
            int num2 = calendar.IsLeapYear(cnYear) ? 13 : 12;

            while (num2 >= cnMonth)
            {
                num1 = calendar.GetDaysInMonth(cnYear, num2--);
            }

            num1 = num1 - calendar.GetDayOfMonth(dt);
            return dt.AddDays(num1);
        }
    }
       
 }
