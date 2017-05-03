using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.RmaMange;
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
            var data = RmaService.RmaReport.CreateRmaID();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult StoreinitiateDataData(RmaReportInitiateModel initiateData)
        {
            var result = RmaService.RmaReport.StoreRamReortInitiate(initiateData);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetBussesDescriptionDatas(string rmaId)
        {
            var datas = 0;//RmaService.RmaManger.GetBussesDescriptiondatas(rmaId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RmaInputDescription
        [NoAuthenCheck]
        public ActionResult RmaInputDescription()
        {
            return View();
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
