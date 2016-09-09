using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;

namespace Lm.Eic.App.Business.BmpTests.Pms.DailyReport
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string path = @"E:\日报数据表.xls";
            var tem = DailyReportService.ConfigManager.ProductFlowSetter.ImportProductFlowListBy(path);
            if (tem == null) { Assert.Fail(); }

            //var temp = DailyReportService.ConfigManager.ProductFlowSetter.
            //if (temp == null) { Assert.Fail(); }
                
        }
    }
    [TestClass]
    public class Unittest
    {
        [TestMethod]
        public void test()
        {
           
            //string path = @"E:\日报数据表.xls";
            //var stream = DailyReportService.MaterialBoardManager.GetProductFlowTemplate(path);
            //#region 输出到Excel
            //string path11 = @"E:\\11111.xls";
            //using (System.IO.FileStream fs = new System.IO.FileStream(path11, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            //{
            //    byte[] bArr = stream.ToArray();
            //    fs.Write(bArr, 0, bArr.Length);
            //    fs.Flush();

            //}
            //#endregion
        }
    }

    [TestClass]
    public class Unittest22
    {
        [TestMethod]
        public void test()
        {
           //var temp = DailyReportService.MaterialBoardManager.GetProductFlowOverviewBy("生技部");
           // if (temp == null) { Assert.Fail(); }
        
        }
    }


}
