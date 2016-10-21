using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Bussiness.CopManage;
namespace Lm.Eic.App.Business.Bmp.Ast.Tests
{
    [TestClass()]
    public class EquipmentManagerTests
    {
        [TestMethod()]
        public void BuildAssetNumberTest()
        {
            EquipmentManager tem2 = new EquipmentManager();

            Lm.Eic.App.DomainModel.Bpm.Ast.EquipmentModel model = new DomainModel.Bpm.Ast.EquipmentModel();
            model.AssetNumber = "I169001";
            model.EquipmentName = "Test1";
            // model.Id_Key = 4;

            //修改
            //var ttt = tem.ChangeStorage(model, 1);

            string i = tem2.BuildAssetNumber("生产设备", "低值易耗品", "保税");
            string i2 = tem2.BuildAssetNumber("生产设备", "低值易耗品", "非保税");
            string i3 = tem2.BuildAssetNumber("生产设备", "固定资产", "保税");

              Assert.Fail();
        }

        [TestMethod()]
        public void StoreTest()
        {
            DomainModel.Bpm.Ast.EquipmentModel model = new DomainModel.Bpm.Ast.EquipmentModel();
            model.AssetNumber = AstService.EquipmentManager.BuildAssetNumber("生产设备", "低值易耗品", "保税");
            model.EquipmentType = "生产设备";
            model.AssetType = "低值易耗品";
            model.TaxType = "保税";
            model.EquipmentName = "Test";
            model.MaintenanceDate = DateTime.Now.ToDate();
            model.MaintenanceInterval = 10;
            model.CheckDate = DateTime.Now.ToDate();
            model.CheckInterval = 6;
            model.OpSign = "add";
            var tem = AstService.EquipmentManager.Store(model);
            if (!tem.Result) { Assert.Fail(); }
        }
    }


    public class EquipmentDiscardTests
    {
        public void StoreEquipmentDiscardTest()
        {
            DomainModel.Bpm.Ast.EquipmentDiscardRecordModel model = new DomainModel.Bpm.Ast.EquipmentDiscardRecordModel();
            model.AssetNumber = "Z160001";
            model.DiscardDate = DateTime.Now;
            model.DiscardType = "无法修复";
            model.DiscardCause = "无法修复了";
            model.DocumentId = "BF20160822";
            model.OpSign = "add";
            var tem = AstService.EquipmentManager.DiscardManager.Store(model);
            if (!tem.Result) { Assert.Fail(); }
        }

        public void GetEquipmentDiscardRecord()
        {
            var temList = AstService.EquipmentManager.DiscardManager.GetEquipmentDiscardRecord("Z160001");
            if (temList == null) { Assert.Fail(); }
        }
    }

    public class test
    {
       public void getstss()
       {
           var mmm= CopService.OrderManageManager .GetMS589ProductTypeMonitor();
       }

       public void Test1212112()
       {
           var tem = CopService.OrderManageManager.BuildProductTypeMonitoList();
           if (tem==null ) { Assert.Fail(); }
           #region 输出到Excel
           string path = @"E:\\IQC.xls";
           using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
           {
               byte[] bArr = tem.ToArray();
               fs.Write(bArr, 0, bArr.Length);
               fs.Flush();
           }

           #endregion
       }
    }
}