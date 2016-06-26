using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class DailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StandardHoursConfig()
        {
            return View();
        }
        public ActionResult ProStationConfig()
        {
            return View();
        }
    }
}
