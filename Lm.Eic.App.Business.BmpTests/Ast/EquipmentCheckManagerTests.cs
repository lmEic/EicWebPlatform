using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

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
            DateTime tem = DateTime.Parse("2017-06-15");
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
        public void  TestExcelToSql()
        {
            StringBuilder str = new StringBuilder();
            string path = @"E:\\设备系统设备总览表.xls";
            var m = ExcelHelper.ExcelToEntityList<EquipmentModel>(path,out str);
            string FilePath = @"C:\testDir\test.txt";
            int Number = m.Count;
            if (str.ToString() != string.Empty)
            { 
                FilePath.CreateFile(str.ToString());
                Assert.Fail(); 
                return;
            }
            
        
            m.ForEach(e => {
                AstService.EquipmentManager.Store(e);
            });
            if (m.Count < 0) { Assert.Fail(); }

        }


        [TestMethod()]
        public void EquipmentCheckStoreTest()
        {
            EquipmentCheckRecordModel model = new EquipmentCheckRecordModel();
            model.AssetNumber = "Z169002";
            model.CheckDate = DateTime.Now.ToDate();
            model.CheckResult = "";
            model.OpSign = "add";
           var result = AstService.EquipmentManager.CheckManager.Store(model);
            if (!result.Result) { Assert.Fail(); }
           
        }
    }
}