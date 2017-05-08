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
        /// <summary>
        ///自动生成RmaId单号
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AutoBuildingRmaId()
        {
            var data = RmaService.RmaManager.RmaReportBuilding.AutoBuildingRmaId();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存 初始RMA单数据
        /// </summary>
        /// <param name="initiateData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreinitiateDataData(RmaReportInitiateModel initiateData)
        {
            var result = RmaService.RmaManager.RmaReportBuilding.StoreRamReortInitiate(initiateData);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 得到Rma单的数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRmaReportMaster(string rmaId)
        {
            var datas = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(rmaId);
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
            var datas = RmaService.RmaManager.BussesManageProcessor.GetErpBussesInfoDatasBy(orderId); ;
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
            /// Rma 初始表的数据
            var RmaInitiateDatas = RmaService.RmaManager.RmaReportBuilding.GetInitiateDatas(rmaId);
            /// 业务部处理的数据
            var BussesDescriptionDatas = RmaService.RmaManager.BussesManageProcessor.GetRmaBussesDescriptionDatasBy(rmaId);

            var datas = new { RmaInitiateDatas, BussesDescriptionDatas };
            
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreRmaInputDescriptionData(RmaBussesDescriptionModel model)
        {
            var opResult = RmaService.RmaManager.BussesManageProcessor.StoreRmaBussesDescriptionData(model);
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
