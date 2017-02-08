using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Hrm.Attendance;
using System;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance.Tests
{
    [TestClass()]
    public class AttendSlodFingerDataCurrentMonthHandlerTests
    {
        private AttendSlodFingerDataCurrentMonthHandler handler = new AttendSlodFingerDataCurrentMonthHandler();

        [TestMethod()]
        public void TransimitAttendDatasTest()
        {
            handler.TransimitAttendDatas(System.DateTime.Now);
            Assert.Fail();
        }

        [TestMethod()]
        public void AutoHandleExceptionSlotDataTest()
        {
            handler.AutoHandleExceptionSlotData();
            Assert.Fail();
        }

      public void BuildExcelDateTest()
        {
            //DateTime qryDate = Convert.ToDateTime("2017-01-03");
            //var datas= AttendanceService.AttendSlodPrintManager.LoadAttendDataInToday(qryDate);
            //var tem = AttendanceService.AttendSlodPrintManager.BuildAttendanceDataMonitoList(datas);
            //#region 输出到Excel
            //string path = @"E:\\IQC.xls";
            //using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            //{
            //    byte[] bArr = tem.ToArray();
            //    fs.Write(bArr, 0, bArr.Length);
            //    fs.Flush();
            //}


            //#endregion

            //if (tem == null)
            //{ Assert.Fail(); }
        }

      
           
    }
}