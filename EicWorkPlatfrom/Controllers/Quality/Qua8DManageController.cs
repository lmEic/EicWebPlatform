using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;


namespace EicWorkPlatfrom.Controllers
{
    public class Qua8DManageController : EicBaseController
    {
        //
        // GET: /Qua8DManage/

        public ActionResult Index()
        {
            return View();
        }

        #region Create8DForm
        [NoAuthenCheck]
        public ActionResult Create8DForm()
        {
            return View();
        }
        [NoAuthenCheck]
        public ContentResult GetQueryDatas(string searchModel, string orderId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.MasterDatasGather.GetIqcMasterContainDatasBy(orderId);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 保存 初始8Dp初始数据
        /// </summary>
        /// <param name="initiateData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreCraet8DInitialData(Qua8DReportMasterModel initialData)
        {
            var result = Qua8DService.Qua8DManager.Qua8DMaster.StoreQua8DMaster(initialData);
            return Json(result);
        }
        #endregion

        #region Handle8DFolw
        [NoAuthenCheck]
        public ActionResult Handle8DFolw()
        {
            return View();
        }
        /// <summary>
        /// 得到Rma单的数据
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRmaReportDatas(string reportId)
        {
            List<ShowStepViewModel> steps = new List<ShowStepViewModel>();
            ShowStepViewModel data = null;
            var HanldeStepInfodatas = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy("M1707004-2");
            HanldeStepInfodatas.ForEach(m =>
            {
                data = new ShowStepViewModel { isCheck = false, HandelQua8DStepDatas = m };
                if (!steps.Contains(data)) steps.Add(data);
            });
            var datas = steps;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过单号 序号得到设置模板
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetRua8DReportStepData(string reportId, int stepId)
        {
            var data = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy(reportId, stepId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult StoreQua8DCreateDatas(Qua8DReportMasterModel vUmdoelData)
        {
            var data = "";
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region Close8DForm
        [NoAuthenCheck]
        public ActionResult Close8DForm()
        {
            return View();
        }
        #endregion

    }
}
