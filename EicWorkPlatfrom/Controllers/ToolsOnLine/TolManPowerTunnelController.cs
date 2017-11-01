using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 人力隧道控制器
    /// </summary>
    public class TolManPowerTunnelController : Controller
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
    }
}
