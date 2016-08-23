using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    /// <summary>
    /// 生产看板管理器
    /// </summary>
    public class ProBoardController : EicBaseController
    {
        //
        // GET: /ProBoard/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 线材看板控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult JumperWireBoard()
        {
            return View();
        }
    }
}
