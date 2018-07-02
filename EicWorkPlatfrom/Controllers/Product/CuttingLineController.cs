using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class CuttingLineController : EicBaseController
    {
        //
        // GET: /CuttingLine/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CLMOCManager()
        {
            return View();
        }

    }
}
