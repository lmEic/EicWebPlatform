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
    public class EquipmentMaintenanceManagerTests
    {
        [TestMethod()]
        public void GetWaitingMaintenanceListByTest()
        {
            string planMaintenanceDate = "2016-07";
            var waitingMainteanceList = AstService.EquipmentManager.MaintenanceManager.GetWaitingMaintenanceListBy(planMaintenanceDate);
            if (waitingMainteanceList.Count() > 0) { } else { Assert.Fail(); }
         
        }

        [TestMethod()]
        public void BuildWaitingMaintenanceListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StoreTest()
        {
            Assert.Fail();
        }
    }
}