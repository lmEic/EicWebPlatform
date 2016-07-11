using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Ast;
using Lm.Eic.App.DomainModel.Bpm.Ast;


using Lm.Eic.App.Business.Bmp.Quantity;
using Lm.Eic.App.DomainModel.Bpm.Quanity;



namespace EicWorkPlatfrom.Controllers
{
    public class EquipmentController : EicBaseController
    {
        //
        // GET: /Equipment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AstArchiveInput()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult GetAstInputConfigDatas()
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var configData = new { departments = departments };
            return Json(configData, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        /// <summary>
        /// 获取设备编号
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="assetType"></param>
        /// <param name="taxType"></param>
        /// <returns></returns>
        public JsonResult GetEquipmentID(string equipmentType, string assetType, string taxType)
        {
            string id = AstService.EquipmentManager.BuildAssetNumber(equipmentType, assetType, taxType);
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult SaveEquipmentRecord(EquipmentModel equipment)
        {
            var result = AstService.EquipmentManager.Store(equipment);
            return Json(result);
        }

        [NoAuthenCheck]
        public FileResult ExportToExcel()
        {
            var ds=QuantityService.IQCSampleItemsRecordManager.GetSamplePrintItemBy("591-1607032");

            var ms= QuantityService.IQCSampleItemsRecordManager.ExportPrintToExcel(ds, "SmapleDatas");

            return this.ExportToExcel(ms, "aaa", "AAA");
        }
    }

    public class DDD
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
