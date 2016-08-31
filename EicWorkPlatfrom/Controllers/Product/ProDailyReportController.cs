using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProDailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DReportHoursSet()
        {
            return View();
        }

        public ActionResult ProStationConfig()
        {
            return View();
        }
    }
}