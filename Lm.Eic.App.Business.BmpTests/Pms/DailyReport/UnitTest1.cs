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

                md.CloneProductStore(tem);
                
        }
    }
    [TestClass]
    public class Unittest
    {
        [TestMethod]
        public void test()
        {
            ProductFlowManager md = new ProductFlowManager();
            string path = @"E:\日报数据表.xls";
            var stream = md.GetProductFlowExcelModel(path);
            #region 输出到Excel
            string path11 = @"E:\\11111.xls";
            using (System.IO.FileStream fs = new System.IO.FileStream(path11, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bArr = stream.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();

            }
            #endregion
        }
    }
}
