using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 人力隧道控制器
    /// </summary>
    public class TolManPowerTunnelController : EicBaseController
    {
        //
        // GET: /TolManPowerTunnel/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TolManpowerDisk()
        {
            return View();
        }

        public JsonResult GetWorkerAnalogDatas()
        {
            var datas = ArchiveService.WorkerQueryManager.GetWorkerAnalogDatas();
            return Json(datas);
        }
    }
}
