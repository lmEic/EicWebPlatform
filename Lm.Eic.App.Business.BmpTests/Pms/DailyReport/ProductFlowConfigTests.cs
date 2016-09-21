using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;

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
        public void GetProductFlowListByTest()
        {

            var tem = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(new DomainModel.Bpm.Pms.DailyReport.QueryDailyReportDto()
            {
                Department = "生技部",
                SearchMode = 5,
                OrderId = "512-1608092"
            });
            if (tem == null) { Assert.Fail(); }

          
        }

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
            model.MachineId = "M90T-1";
            model.MachineName = "测试机台";

            var tem = DailyReportService.ConfigManager.MachineSetter.GetMachineListBy(model.Department);

            if (tem == null) { Assert.Fail(); }
        }
    }
}