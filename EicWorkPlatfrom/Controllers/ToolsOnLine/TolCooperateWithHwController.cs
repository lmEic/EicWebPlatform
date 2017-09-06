using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.HwCollaboration.Business;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 华为协同控制器
    /// </summary>
    public class TolCooperateWithHwController : Controller
    {
        //
        // GET: /TolCooperateWithHw/

        public ActionResult Index()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetManPower()
        {
            string result = HwCollaborationService.ManPowerManager.SynchronizeDatas();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
