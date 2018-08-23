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
        /// <summary>
        /// 研发文档
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult DocumentInputRecord()
        {
            return View();
        }
        /// <summary>
        ///开发文档
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult SponsorDocumentInputRecord()
        {
            return View();
        }
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult StoreDisgnDveData(DesignDevelopInputModel model)
        {
            var opResult = DevelopSerivce.DesignDevManager.StoreDesignModel(model);
            return Json(opResult);
        }
    }
}