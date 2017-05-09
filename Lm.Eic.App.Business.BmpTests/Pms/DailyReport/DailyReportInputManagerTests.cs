using Microsoft.VisualStudio.TestTools.UnitTesting;

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