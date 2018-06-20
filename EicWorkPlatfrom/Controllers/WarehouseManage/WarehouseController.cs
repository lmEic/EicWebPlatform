using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.WarehouseManage
{
    public class WarehouseController : EicBaseController
    {
        //
        // GET: /Warehouse/
        [NoAuthenCheck]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
