using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives.Tests
{
    [TestClass()]
    public class ArWorkerIdChangeCurdTests
    {
        [TestMethod()]
        public void TestUpdateTest()
        {
            ArWorkerIdChangeCurd c = new ArWorkerIdChangeCurd();
            c.TestUpdate("604558", "014488");
            Assert.Fail();
        }
    }
}