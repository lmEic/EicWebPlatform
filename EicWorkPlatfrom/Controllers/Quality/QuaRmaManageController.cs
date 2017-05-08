using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.RmaManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace EicWorkPlatfrom.Controllers.Quality
{
    public class QuaRmaManageController : EicBaseController
    {
        //
        // GET: /QuaRma/

        public ActionResult Index()
        {
            return View();
        }

        #region CreateRmaForm
        [NoAuthenCheck]
        public ActionResult CreateRmaForm()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult CreateRmaId()
        {
            var data = RmaService.RmaManager.RmaReportBuilding.AutoBuildingRmaID();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult StoreinitiateDataData(RmaReportInitiateModel initiateData)
        {
            var result = RmaService.RmaManager.RmaReportBuilding.StoreRamReortInitiate(initiateData);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult GetRmaReportMaster(string rmaId)
        {
            var datas = RmaService.RmaManager.RmaReportBuilding.GetRemPeortInitiateData(rmaId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RmaInputDescription
        [NoAuthenCheck]
        public ActionResult RmaInputDescription()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult GetReturnOrderInfo(string orderId)
        {
            var datas = 0;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRmaDescriptionDatas(string rmaId)
        {
            var datas = 0;//RmaService.RmaManger.GetBussesDescriptiondatas(rmaId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreRmaInputDescriptionData(RmaBussesDescriptionModel model)
        {
            var opResult = 0;
            return Json(opResult);
        }
        #endregion

        #region RmaInspectionHandle
        [NoAuthenCheck]
        public ActionResult RmaInspectionHandle()
        {
            return View();
        }
        #endregion
    }
}
