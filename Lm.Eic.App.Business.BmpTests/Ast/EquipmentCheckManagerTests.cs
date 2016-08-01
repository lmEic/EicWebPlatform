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

        [TestMethod]
        public void AutoTest()
        {
            GetEquipmentNotCheckTest();
            ExportEquipmentNotCheckToExcleTest();
        }

        /// <summary>
        /// 测试 获取待校验设备列表
        /// </summary>
        [TestMethod()]
        public void GetEquipmentNotCheckTest()
        {
            //
            DateTime tem = DateTime.Parse("2017-01-01");
            var equipmentNoetChekcList = AstService.EquipmentManager.CheckManager.GetWaitingCheckListBy(tem);
            if (equipmentNoetChekcList.Count() > 0) { } else { Assert.Fail(); }
        }

        [TestMethod()]
        public void ExportEquipmentNotCheckToExcleTest()
        {
           
            var tem = AstService.EquipmentManager.CheckManager.BuildWaitingCheckList();


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
        public void EquipmentCheckStoreTest()
        {
            EquipmentCheckRecordModel model = new EquipmentCheckRecordModel();
            model.AssetNumber = "Z169001";
            model.CheckDate = DateTime.Now.ToDate();
            model.CheckResult = "";
            model.OpSign = "add";
           var result = AstService.EquipmentManager.CheckManager.Store(model);
            if (!result.Result) { Assert.Fail(); }
           
        }
    }
}