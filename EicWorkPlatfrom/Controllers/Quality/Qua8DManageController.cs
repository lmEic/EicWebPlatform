using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult GgetRmaReportDatas(string reportId)
        {
            step data = new step { isCheck = true, StepDescription = "4545454", StepId = "1222212", StepLevel = 4 };
            List<step> steps = new List<step>();
            steps.Add(data);
            steps.Add(data);
            steps.Add(data);
            steps.Add(data);
            steps.Add(data);
            steps.Add(data);
            steps.Add(data);
            var datas = steps;
            return Json(datas, JsonRequestBehavior.AllowGet);
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
