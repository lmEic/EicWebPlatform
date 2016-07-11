using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quantity;


namespace Lm.Eic.App.Business.BmpTests.Quantity
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void QsTestMethod()
        {
            int n = QuantityService.IQCSampleItemsRecordManager.GetSamplePrintItemBy("591-1607032").Count;
              /// 测工单从ERP中得到物料信息
            int m = QuantityService .IQCSampleItemsRecordManager.GetPuroductSupplierInfo("591-1607032").Count;

            int mm = QuantityService.IQCSampleItemsRecordManager.GetPringSampleItemBy("","").Count;
           
            Assert.Fail();
        }
    }
}
