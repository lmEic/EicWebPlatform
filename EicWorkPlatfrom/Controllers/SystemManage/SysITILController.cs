using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
namespace EicWorkPlatfrom.Controllers
{
    public class SysITILController : EicBaseController
    {
        //
        // GET: /ITIL/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItilSupTelManage()
        {
            return View();
        }

        public ActionResult ItilProjectDevelopManage()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult StoreProjectDevelopRecord(ItilDevelopModuleManageModel entity)
        {
            var result = 0;

            return Json(result);
        }
        /// <summary>
        /// 根据开发进度状态查找开发模块
        /// </summary>
        /// <param name="progressStatuses"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProjectDevelopModuleBy(List<string> progressStatuses)
        {
            var result = 0;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}