using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;

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
            //HwDataEntity entity = HwCollaborationService.ManPowerManager.GetLatestEntity();
            HwDataEntity entity = new HwDataEntity()
            {
                Dto = HwMockDatas.ManPowerDto,
                OpLog = HwMockDatas.OpLog
            };
            var result = HwCollaborationService.ManPowerManager.SynchronizeDatas(entity);
            return Json(entity, JsonRequestBehavior.AllowGet);
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
