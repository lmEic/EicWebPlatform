using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Bussiness.CopManage;

namespace Lm.Eic.App.Business.BmpTests.Erp
{
      [TestClass()]
 public   class ProductTypeMonitorTest
      {

        public void MS589ProductTypeMonitorTest()
             {
                //var mmm= CopService.OrderManageManager .GetMS589ProductTypeMonitor();
            var mmmmm = CopService.OrderManageManager.GetProductTypeMonitorInfoBy("138155-475-01");
             }
         public void TestExportExcel()
            {
                var tem = CopService.OrderManageManager.BuildProductTypeMonitoList();
                if (tem == null) { Assert.Fail(); }

                string path = @"E:\\IQC.xls";
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    byte[] bArr = tem.ToArray();
                    fs.Write(bArr, 0, bArr.Length);
                    fs.Flush();
                }

            }
      }

     


}
