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

            step data1 = new step { isCheck = false, StepDescription = "1111", StepId = "1", StepLevel = 2 };
            step data2 = new step { isCheck = false, StepDescription = "2222", StepId = "2", StepLevel = 1 };
            step data3 = new step { isCheck = false, StepDescription = "3333", StepId = "3", StepLevel = 5 };
            step data4 = new step { isCheck = false, StepDescription = "4444", StepId = "4", StepLevel = 6 };
            step data5 = new step { isCheck = false, StepDescription = "5555", StepId = "5", StepLevel = 4 };
            step data6 = new step { isCheck = false, StepDescription = "7666", StepId = "6", StepLevel = 6 };
            step data7 = new step { isCheck = false, StepDescription = "7777", StepId = "7", StepLevel = 7 };
            step data8 = new step { isCheck = false, StepDescription = "8888", StepId = "8", StepLevel = 8 };
            List<step> steps = new List<step>();
            steps.Add(data1);
            steps.Add(data2);
            steps.Add(data3);
            steps.Add(data4);
            steps.Add(data5);
            steps.Add(data6);
            steps.Add(data7);
            steps.Add(data8);
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
