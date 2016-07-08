using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance.Tests
{
    [TestClass()]
    public class AttendSlodFingerDataCurrentMonthHandlerTests
    {
        private AttendSlodFingerDataCurrentMonthHandler handler = new AttendSlodFingerDataCurrentMonthHandler();

        [TestMethod()]
        public void TransimitAttendDatasTest()
        {
            handler.TransimitAttendDatas();
            Assert.Fail();
        }

        [TestMethod()]
        public void AutoHandleExceptionSlotDataTest()
        {
            handler.AutoHandleExceptionSlotData();
            Assert.Fail();
        }
    }
}