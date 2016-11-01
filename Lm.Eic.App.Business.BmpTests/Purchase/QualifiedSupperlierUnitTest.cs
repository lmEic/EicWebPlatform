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
            var mm = PurchaseService.PurSupplierManager.PutInManage.FindQualifiedSupplierList("2016");
           
           if (mm!=null ||mm.Count >0)
            {
               var tem=  PurchaseService.PurSupplierManager.PutInManage.SavaQualifiedSupplierInfoS(mm);
               if (!tem.Result) { Assert.Fail(); }
            }
      
        }


        public void SupplierInfoTest()
        {
            var supplierInfos = PurchaseService.PurSupplierManager.PutInManage.ERPFindSupplierInformationList("201601");
            if(supplierInfos!=null ||supplierInfos.Count >0)
            {
                var tem = PurchaseService.PurSupplierManager.PutInManage.SaveSupplierInfos(supplierInfos);
                if (!tem.Result) { Assert.Fail(); }
            }
        }
    }
}
