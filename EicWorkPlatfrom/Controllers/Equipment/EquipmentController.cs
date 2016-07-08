using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Ast;
using Lm.Eic.App.DomainModel.Bpm.Ast;


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
            var d = 0;
            return Json(d);
        }

        [NoAuthenCheck]
        public FileResult ExportToExcel()
        {
            List<DDD> datas = new List<DDD>() { 
               new DDD(){ Name="aaa", Age =20},
               new DDD(){ Name="bbb", Age =25},
            };

            return this.ExportToExcel<DDD>(datas, "aaa", "AAA");
        }
    }

    public class DDD
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
