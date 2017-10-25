using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 华为协同控制器
    /// </summary>
    public class TolCooperateWithHwController : EicBaseController
    {
        //
        // GET: /TolCooperateWithHw/

        public ActionResult Index()
        {
            return View();
        }

        #region HwMaterialBaseConfig method
        public ActionResult HwMaterialBaseConfig()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetMaterialBaseConfigDatas()
        {
            var datas = HwCollaborationService.MaterialManager.BaseBomManager.MaterialBaseDictionary;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult SaveMaterialBaseConfigDatas(HwCollaborationMaterialBaseConfigModel entity)
        {
            var opResult = HwCollaborationService.MaterialManager.BaseBomManager.Store(entity);
            return Json(opResult);
        }
        #endregion

        #region HwManpowerInput
        public ActionResult HwManpowerInput()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetManPower()
        {
            var entity = HwCollaborationService.ManPowerManager.GetLatestEntity();
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var data = new { entity, departments };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult SaveManPower(HwCollaborationDataTransferModel entity)
        {
            var result = HwCollaborationService.ManPowerManager.SynchronizeDatas(entity);
            return Json(result);
        }

        #endregion

        #region HwLogisticDeliveryInput
        public ActionResult HwLogisticDeliveryInput()
        {
            return View();
        }

        #endregion

        #region Hw Material  Board Detail
        public ActionResult HwInventoryDetail()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetMaterialDetails()
        {
            var dto = HwCollaborationService.MaterialManager.AutoLoadDataFromErp();
            var entity = new
            {
                inventoryEntity = dto.InvertoryDto,
                makingEntity = dto.MakingDto,
                shippingEntity = dto.ShippmentDto,
                purchaseEntity = dto.PurchaseDto
            };
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult SaveMaterialDetailData(MaterialComposeEntity entity)
        {
            var result = HwCollaborationService.MaterialManager.SaveMaterialDetail(entity);
            return Json(result);
        }
        #endregion
    }
}
