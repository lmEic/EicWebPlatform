using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class TolWorkFlowController : EicBaseController
    {
        //
        // GET: /TolWorkFlow/

        public ActionResult Index()
        {
            return View();
        }


        #region WFInternalContactForm
        public ActionResult WFInternalContactForm()
        {
            return View();
        }
        #endregion

    }
}
