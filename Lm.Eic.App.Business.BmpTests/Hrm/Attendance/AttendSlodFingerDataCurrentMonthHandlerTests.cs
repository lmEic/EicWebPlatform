using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.Business.Bmp.Hrm.Attendance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance.Tests
{
    [TestClass()]
    public class AttendSlodFingerDataCurrentMonthHandlerTests
    {
        AttendSlodFingerDataCurrentMonthHandler handler = new AttendSlodFingerDataCurrentMonthHandler();
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
