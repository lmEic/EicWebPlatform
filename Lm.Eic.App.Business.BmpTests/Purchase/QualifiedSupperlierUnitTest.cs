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
            var mm = PurchaseService.PurSupplierManager.CertificateManager.GetQualifiedSupplierList("2016");

            if (mm != null || mm.Count > 0)
            {
                //var tem=  PurchaseService.PurSupplierManager.SavaQualifiedSupplierInfoList(mm);
                //if (!tem.Result) { Assert.Fail(); }
            }

        }

        [TestMethod]
        public void SupplierInfoTest()
        {
            //var supplierInfos = PurchaseService.PurSupplierManager.GetSupplierInformationListBy("201511","201611");
            //if(supplierInfos!=null ||supplierInfos.Count >0)
            //{
            //    //var tem = PurchaseService.PurSupplierManager.SaveSupplierInfoList(supplierInfos);
            //    //if (!tem.Result) { Assert.Fail(); }
            //}
        }


        [TestMethod]
        public void EditsupplierInfoTest()
        {
            //InPutSupplieCertificateInfoModel model = new InPutSupplieCertificateInfoModel()
            //{
            //    DateOfCertificate = DateTime.Now,
            //    EligibleCertificate = "学习证书",
            //    FilePath = @"d:\ee\test",
            //    IsEfficacy = "是",
            //    SupplierId = "D048888",
            //    PurchaseType = "光纤主、被动元件散件",
            //    SupplierProperty = "关键供应商"
            //};
            //var modellist = new List<InPutSupplieCertificateInfoModel>();
            //modellist.Add(model);
            //var tem = PurchaseService.PurSupplierManager.SupplierCertificateManager.SavaEditSpplierCertificate(modellist);
            //if (!tem.Result) { Assert.Fail(); }

        }
        [TestMethod]
        public void GetSupplierQualifiedCertificateList()
        {
            var datas = PurchaseService.PurSupplierManager.CertificateManager.GetSupplierQualifiedCertificateListBy("D04004");
            if (datas == null) { Assert.Fail(); }
        }
        public void SuppliersSeasonAuditInfo()
        {
            var datas = PurchaseService.PurSupplierManager.AuditManager.GetSeasonSupplierList("201601");
            if (datas == null) { Assert.Fail(); }
        }

    }
}
