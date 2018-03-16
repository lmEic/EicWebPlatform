using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Quality.RmaManage;
using System;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;

namespace Lm.Eic.App.Business.BmpTests.Quantity
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void QsTestMethod()
        {
            for (int i=1;i<=31;i++)
            {
                string days = i.ToString("00");
                InspectionService.DataGatherManager.FqcDataGather.AddonlyDataDatialDatas("2018-01-"+ days);
            }
             
            //  /// 测工单从ERP中得到物料信息


            //DateTime statDate = DateTime.Now .Date ;
            //DateTime endDate = DateTime.Now.Date;
            // var m= InspectionService.DataGatherManager.IqcDataGather.GetOrderIdList(statDate, endDate);
            //var mm =QuantityServices. SampleManger.SampleItemsIqcRecordManager.GetPringSampleItemBy("591-1607032", "32AAP00001200RM");
            // var ms = QuantityServices.SampleManger.MaterialSampleItemsManager.GetMaterilalSampleItemBy("32AAP00001200RM");
            // System.IO.MemoryStream stream= QuantityServices. SampleManger.SampleItemsIqcRecordManager.ExportPrintToExcel(mm);
            //#region 输出到Excel
            //string path = @"E:\\IQC.xls";
            // using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            // {
            //     byte[] bArr = stream.ToArray();
            //     fs.Write(bArr, 0, bArr.Length);
            //     fs.Flush();

            // }
            // #endregion
            var listDatas = RmaService.RmaManager.BusinessManageProcessor.GetErpBusinessInfoDatasBy("241-160909001");
            if (listDatas == null)
                Assert.Fail();
        }




    }


}
