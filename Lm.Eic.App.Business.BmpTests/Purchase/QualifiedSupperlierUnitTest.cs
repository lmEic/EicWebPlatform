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
            var mm = PurchaseService.QualifiedSupplierManager.FindQualifiedSupplierList("2016");
           
           if (mm!=null ||mm.Count >0)
            {
               var tem=  PurchaseService.QualifiedSupplierManager.SavaQualifiedSupplierInfoS(mm);
               if (!tem.Result) { Assert.Fail(); }
            }
      
        }
    }
}
