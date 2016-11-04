using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Purchase;
namespace Lm.Eic.App.Business.BmpTests.Purchase
{
    [TestClass]
    public class QualifiedSupperlierUnitTest
    {
        [TestMethod]
        public void QualifiedSupplierTest()
        {
            var mm = PurchaseService.PurSupplierManager.InPutManage.FindQualifiedSupplierList("2016");
           
           if (mm!=null ||mm.Count >0)
            {
               var tem=  PurchaseService.PurSupplierManager.InPutManage.SavaQualifiedSupplierInfoS(mm);
               if (!tem.Result) { Assert.Fail(); }
            }
      
        }


        public void SupplierInfoTest()
        {
            var supplierInfos = PurchaseService.PurSupplierManager.InPutManage.FindSupplierInformationList("201601");
            if(supplierInfos!=null ||supplierInfos.Count >0)
            {
                var tem = PurchaseService.PurSupplierManager.InPutManage.SaveSupplierInfos(supplierInfos);
                if (!tem.Result) { Assert.Fail(); }
            }
        }
    }
}
