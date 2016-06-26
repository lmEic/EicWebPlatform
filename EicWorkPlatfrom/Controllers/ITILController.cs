using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class ITILController:EicBaseController
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

    }
}
