using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Ast.Tests
{
    [TestClass()]
    public class EquipmentCheckManagerTests
    {
        /// <summary>
        /// 测试 获取待校验设备列表
        /// </summary>
        [TestMethod()]
        public void GetEquipmentNotCheckTest()
        {
            //
            DateTime tem = DateTime.Parse("2017-01-01");
            var equipmentNoetChekcList = AstService.EquipmentManager.CheckManager.GetWithoutCheckEquipmentListBy(tem);
            if (equipmentNoetChekcList.Count() > 0) { } else { Assert.Fail(); }
        }

        [TestMethod()]
        public void ExportEquipmentNotCheckToExcleTest()
        {
            var tem = AstService.EquipmentManager.CheckManager.ExportWithoutCheckEquipmentListToExcle();
            Assert.Fail();
        }

        [TestMethod()]
        public void StoreTest()
        {
            EquipmentCheckModel model = new EquipmentCheckModel();
            model.AssetNumber = "I169001";
            model.CheckDate = DateTime.Now.ToDate();
            model.CheckResult = "";
            model.OpSign = "add";
           var tem = AstService.EquipmentManager.CheckManager.Store(model);

            Assert.Fail();
        }
    }
}