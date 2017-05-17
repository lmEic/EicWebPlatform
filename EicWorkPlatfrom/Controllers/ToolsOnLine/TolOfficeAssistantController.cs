using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class TolOfficeAssistantController : EicBaseController
    {
        //
        // GET: /TolOfficeAssistant/

        public ActionResult Index()
        {
            return View();
        }

        #region CollaborateContactLib
        public ActionResult CollaborateContactLib()
        {
            return View();
        }
        #endregion

        #region WorkTaskManage
        public ActionResult WorkTaskManage()
        {
            return View();
        }
        #endregion
    }
}
