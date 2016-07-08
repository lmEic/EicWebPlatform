using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quantity;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Bussiness.PurchaseManage;

namespace Lm.Eic.App.Business.BmpTests.Quantity
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void QsTestMethod()
        {

            int i = QuantityDBManager.QuantityPurchseDb.FindMaterialBy("110-1410001").Count;
           
            Assert.Fail();
        }
    }
}
