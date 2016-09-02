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
            ProductFlowManager md = new ProductFlowManager();
            string path = @"E:\日报数据表.xls";
                var tem=md.ImportProductFlowListBy (path);
                if (tem ==null ) { Assert.Fail(); }
        }
    }
}
