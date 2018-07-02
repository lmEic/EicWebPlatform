using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class DevelopmentController : EicBaseController
    {
        //
        // GET: /Development/
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult DocumentInputRecord()
        {
            return View();
        }
    }
}