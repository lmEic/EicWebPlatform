using Lm.Eic.App.DomainModel.Bpm.Dev;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Dev;

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
        [NoAuthenCheck]
        public JsonResult StoreDisgnDveData(DesignDevelopInputModel model)
        {
            var opResult = DevelopSerivce.DesignDevManager.StoreDesignModel(model);
            return Json(opResult);
        }
    }
}