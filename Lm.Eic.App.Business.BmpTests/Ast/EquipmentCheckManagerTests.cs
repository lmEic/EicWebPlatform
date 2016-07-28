using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.Business.Bmp.Ast.Tests
{
    [TestClass()]
    public class EquipmentCheckManagerTests
    {
        [TestMethod()]
        public void GetEquipmentNotCheckTest()
        {
            AstService.EquipmentCheckManager.GetEquipmentNotCheck(DateTime.Now);
            Assert.Fail();
        }
    }
}