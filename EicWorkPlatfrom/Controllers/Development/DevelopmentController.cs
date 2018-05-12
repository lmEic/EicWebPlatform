using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class DevelopmentController : EicBaseController
    {
        //
        // GET: /Development/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DocumentInputRecord()
        {
            return View();
        }
    }
}