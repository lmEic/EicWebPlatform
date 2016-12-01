using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport.Tests
{
    [TestClass()]
    public class ProductFlowConfigTests
    {
        [TestMethod()]
        public void ImportProductFlowListByTest()
        {
            string path = @"E:\日报数据表.xls";
            var tem = DailyReportService.ConfigManager.ProductFlowSetter.ImportProductFlowListBy(path);
            if (tem == null) { Assert.Fail(); }

            var temp = DailyReportService.ConfigManager.ProductFlowSetter.Store(tem);
            if (temp == null) { Assert.Fail(); }
        }

        [TestMethod()]
        //public void GetProductFlowListByTest()
        //{
        //    var tem = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(new DomainModel.Bpm.Pms.DailyReport.QueryDailyReportDto()
        //    {
        //        Department = "生技部",
        //        SearchMode = 5,
        //        OrderId = "517-1605031"
        //    });
        //    if (tem == null) { Assert.Fail(); }
        //}

        [TestMethod]
        public void AddMachineTest()
        {
            var model = new MachineModel();
            model.Department = "生技部";
            model.MachineId = "M90T-1";
            model.MachineName = "测试机台";

            var tem = DailyReportService.ConfigManager.MachineSetter.AddMachineRecord(model);

            if (tem == null) { Assert.Fail(); }
        }

        [TestMethod]
        public void GetMachineListByTest()
        {
            var model = new MachineModel();
            model.Department = "生技部";
            model.MachineId = "M10T-1";
            model.MachineName = "测试机台";

            var tem = DailyReportService.ConfigManager.MachineSetter.GetMachineListBy(model.Department);

            if (tem == null) { Assert.Fail(); }
        }

        [TestMethod()]
        public void GetNonProductionListByTest()
        {
            var model = new NonProductionReasonModel();
            model.Department = "生技部";
            model.NonProductionReasonCode = "A2";
            model.NonProductionReason = "Test2";
            // var tem = DailyReportService.ConfigManager.NonProductionSetter.AddNonProductionRecord(model);

            var tem2 = DailyReportService.ConfigManager.NonProductionReasonSetter.GetNonProductionReasonListBy("生技部");

            if (tem2 == null) { Assert.Fail(); }
        }
      [TestMethod ()]
        public void getDailyReportedData()
        {
           
            var tem = DailyReportService.InputManager.DailyReportInputManager.BuildDailyReportTempList("成型课", "2016-11-15".ToDate ());
            #region 输出到Excel
            string path = @"E:\\IQC.xls";
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bArr = tem.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();
            }
            #endregion
        }
    }
}