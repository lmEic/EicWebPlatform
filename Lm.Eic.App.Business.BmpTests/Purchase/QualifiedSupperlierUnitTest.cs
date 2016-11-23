using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Purchase;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using System.Collections.Generic;

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



        public void EditsupplierInfoTest()
        {
            PutIntSupplieInfoModel model = new PutIntSupplieInfoModel()
            {
                DateOfCertificate = DateTime.Now,
                EligibleCertificate = "学习证书",
                FilePath = @"d:\ee\test",
                IsEfficacy = "是",
                SupplierId = "D04005",
                PurchaseType = "光纤主、被动元件散件",
                SupplierProperty = "关键供应商"
            };
            var modellist = new List<PutIntSupplieInfoModel>();
            modellist.Add(model);
            var tem = PurchaseService.PurSupplierManager.InPutManage.SavaEditSpplierPutInt(modellist);
            if (!tem.Result) { Assert.Fail(); }

        }
    }
}
