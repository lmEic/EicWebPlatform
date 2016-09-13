using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport.Tests
{
    [TestClass()]
    public class DailyReportInputManagerTests
    {
        [TestMethod()]
        public void GetOrderDetailsTest()
        {

            var tem = DailyReportService.InputManager.DailyReportInputManager.GetOrderDetails("512-1608092");
            if (tem == null) { Assert.Fail(); }

          
        }
    }
}