using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lm.Eic.App.Business.Bmp.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.SystemInit.Commom;

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
            DateTime tem = DateTime.Parse("2017-01-19");
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
            string path = @"E:\\Ast.xls";
            Dictionary <string ,string> dic =new Dictionary<string,string> ();
           dic.Add("AssetNumber","财产编号");
           dic.Add("EquipmentName","设备名称"); 
           dic.Add("EquipmentSpec","设备规格");
           dic.Add("FunctionDescription","功能描述");
           dic.Add("ServiceLife","使用寿命");
           dic.Add("EquipmentPhoto","设备照片") ;
           dic.Add("AssetType","财产类别")  ;
           dic.Add("EquipmentType","设备类别") ;
           dic.Add("TaxType","税务类型") ;
           dic.Add("FreeOrderNumber","免单号");
           dic.Add("DeclarationNumber","报关单号") ;
           dic.Add("Unit","单位")  ;
           dic.Add("Manufacturer","厂商");
           dic.Add("ManufacturingNumber","制造编号") ;
           dic.Add("ManufacturerWebsite","官网");
           dic.Add("ManufacturerTel","官方电话");
           dic.Add("AfterSalesTel","售后电话");
           dic.Add("AddMode","添加方式");
           dic.Add("DeliveryDate","登陆日期");
           dic.Add("DeliveryUser","交付人");
           dic.Add("DeliveryCheckUser","验收人") ;
           dic.Add("SafekeepWorkerID","保管人工号") ;
           dic.Add("SafekeepUser","保管人姓名")  ;
           dic.Add("SafekeepDepartment","保管单位");
           dic.Add("Installationlocation","安装位置");
           dic.Add("IsMaintenance","是否保养") ;
           dic.Add("MaintenanceDate","保养日期") ;
           dic.Add("MaintenanceInterval","保养间隔");
           dic.Add("PlannedMaintenanceDate","计划保养日期") ;
           dic.Add("PlannedMaintenanceMonth","计划保养月") ;
           dic.Add("MaintenanceState","保养状态");
           dic.Add("IsCheck","是否校验");
           dic.Add("CheckType","校验类型");
           dic.Add("CheckDate","校验日期");
           dic.Add("CheckInterval", "校验间隔");                  
           dic.Add("PlannedCheckDate","计划校验日期");
           dic.Add("CheckState","校验状态") ;
           dic.Add("State","设备运行状态");
           dic.Add("IsScrapped","是否已报废") ;
           dic.Add("InputDate","录入日期");
           dic.Add("OpPerson","操作人");
           dic.Add("OpSign","操作标识");
           dic.Add("OpDate","操作日期") ;
           dic.Add("OpTime", "操作时间");
           var m = ExcelHelper.ExcelToEntityList<EquipmentModel>(dic, path, out str);
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