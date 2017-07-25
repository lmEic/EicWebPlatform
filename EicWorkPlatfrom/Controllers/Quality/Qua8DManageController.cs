using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage;

namespace EicWorkPlatfrom.Controllers
{
    public class Qua8DManageController : Controller
    {
        //
        // GET: /Qua8DManage/

        public ActionResult Index()
        {
            return View();
        }

        #region Create8DForm
        public ActionResult Create8DForm()
        {
            return View();
        }
        #endregion

        #region Handle8DFolw
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
            List<step> steps = new List<step>();
            var HanldeStepInfodatas = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy("M1707004-2");
            HanldeStepInfodatas.ForEach(m =>
            {
                step data = new step { isCheck = false, StepDescription = m.DescribeType, StepId = "第" + m.StepId.ToString() + "歩", StepLevel = m.StepId };
                if (!steps.Contains(data))
                {
                    steps.Add(data);
                }
            });
            var datas = steps;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetRua8DReportStepData(string reportId, int stepId)
        {
            var data = Qua8DService.Qua8DManager.Qua8DDatail.GetQua8DDetailDatasBy(reportId, stepId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Close8DForm
        public ActionResult Close8DForm()
        {
            return View();
        }
        #endregion

    }

    public class step
    {
        public bool isCheck
        { set; get; }
        public string StepId
        { set; get; }

        public string StepDescription
        {
            set; get;
        }
        public int StepLevel
        {
            set; get;
        }
    }


}
