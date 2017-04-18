using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Quality
{
    public class QuaRmaManageController : EicBaseController
    {
        //
        // GET: /QuaRma/

        public ActionResult Index()
        {
            return View();
        }

        #region CreateRmaForm

        public ActionResult CreateRmaForm()
        {
            return View();
        }

        #endregion

        #region RmaInputDescription
        public ActionResult RmaInputDescription()
        {
            return View();
        }
        #endregion

        #region RmaInspectionHandle
        public ActionResult RmaInspectionHandle()
        {
            return View();
        }
        #endregion
    }
}
