using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;

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
            HwDataEntity entity = new HwDataEntity()
            {
                Dto = HwMockDatas.ManPowerDto,
                OpLog = HwMockDatas.OpLog
            };
            var result = HwCollaborationService.ManPowerManager.SynchronizeDatas(entity);
            return Json(result.Message, JsonRequestBehavior.AllowGet);
        }
    }
}
