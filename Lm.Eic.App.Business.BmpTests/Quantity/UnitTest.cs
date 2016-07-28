﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quantity;
using Lm.Eic.App.Business.Bmp.Quantity.SampleManger;

namespace Lm.Eic.App.Business.BmpTests.Quantity
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void QsTestMethod()
        {
            //var n = QuantityService.IQCSampleItemsRecordManager.GetSamplePrintItemBy("32AAP00001200RM");
            //  /// 测工单从ERP中得到物料信息
            var m= QuantitySampleService .SampleItemsIqcRecordManager.GetPuroductSupplierInfo("591-1607032");
            var mm = QuantitySampleService.SampleItemsIqcRecordManager.GetPringSampleItemBy("591-1607032", "32AAP00001200RM");
            var ms = QuantitySampleService.MaterialSampleItemsManager.GetMaterilalSampleItemBy("32AAP00001200RM");
            System.IO.MemoryStream stream=  QuantitySampleService.SampleItemsIqcRecordManager.ExportPrintToExcel(mm);
           #region 输出到Excel
           string path = @"E:\\IQC.xls";
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bArr = stream.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();
                
            }
            #endregion

   
           
            Assert.Fail();
        }



     
    }


}