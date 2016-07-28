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
        [TestMethod]
        public void AutoTest()
        {
            GetWaitingMaintenanceListByTest();
            BuildWaitingMaintenanceListTest();
        }

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
            var tem = AstService.EquipmentManager.MaintenanceManager.BuildWaitingMaintenanceList();


            #region 输出到Excel
            string path = @"E:\\IQC.xls";
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bArr = tem.ToArray();
                fs.Write(bArr, 0, bArr.Length);
                fs.Flush();
            }
            #endregion
            Assert.Fail();
          
        }

        [TestMethod()]
        public void StoreTest()
        {
            Assert.Fail();
        }
    }
}