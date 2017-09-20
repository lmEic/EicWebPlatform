﻿using System;
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
    public class TolCooperateWithHwController : Controller
    {
        //
        // GET: /TolCooperateWithHw/

        public ActionResult Index()
        {
            return View();
        }


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

        #region HwInventoryDetail
        public ActionResult HwInventoryDetail()
        {
            return View();
        }
        #endregion

        #region HwMakingVoDetail
        public ActionResult HwMakingVoDetail()
        {
            return View();
        }
        #endregion

        #region HwShipmentVoDetail
        public ActionResult HwShipmentVoDetail()
        {
            return View();
        }
        #endregion

        #region HwPurchaseOnWay
        public ActionResult HwPurchaseOnWay()
        {
            return View();
        }
        #endregion

    }
}
