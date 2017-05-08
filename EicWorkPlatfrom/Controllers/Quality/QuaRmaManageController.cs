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
        public JsonResult AutoBuildingRmaId()
        {
            var data = RmaService.RmaManager.RmaReportBuilding.AutoBuildingRmaId();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult StoreinitiateDataData(RmaReportInitiateModel initiateData)
        {
            var result = RmaService.RmaManager.RmaReportBuilding.StoreRamReortInitiate(initiateData);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 通过 
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
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
        /// <summary>
        ///得到ERP退换单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetReturnOrderInfo(string orderId)
        {
            var datas = RmaService.RmaManager.GetErpBussesInfoDatasBy(orderId); ;
            return Json(datas, JsonRequestBehavior.AllowGet);
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
