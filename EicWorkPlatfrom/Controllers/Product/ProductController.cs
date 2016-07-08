using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProductController : EicBaseController
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }
    }
}