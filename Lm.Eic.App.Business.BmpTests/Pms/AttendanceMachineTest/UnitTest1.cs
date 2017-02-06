using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Attendance;
using System.Text;

namespace Lm.Eic.App.Business.BmpTests.Pms.AttendanceTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
      
  
        public void TestMethod1()
        {
            AttendanceBusiness attendance = new AttendanceBusiness();
            string msg = string.Empty;
            attendance.OpenConnent("192.168.0.24", 1, out msg);
            if (msg == string.Empty) { Assert.Fail(); }
        }
       
    }
}
