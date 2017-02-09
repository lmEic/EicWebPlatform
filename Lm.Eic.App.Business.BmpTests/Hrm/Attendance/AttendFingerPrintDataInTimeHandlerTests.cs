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
    public class AttendFingerPrintDataInTimeHandlerTests
    {
        [TestMethod()]
        public void tuTest()
        {
            AttendFingerPrintDataInTimeHandler handler = new AttendFingerPrintDataInTimeHandler();
            handler.tu();
            Assert.Fail();
        }
    }
}
