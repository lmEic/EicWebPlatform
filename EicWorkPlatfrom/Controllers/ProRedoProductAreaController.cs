using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class ProRedoProductAreaController : Controller
    {
        //
        // GET: /ProRedoProductArea/

        public ActionResult Index()
        {
            return View();
        }

        #region DRRedoInput
        /// <summary>
        /// 重工登记
        /// </summary>
        /// <returns></returns>
        public ActionResult DRRedoInput()
        {
            return View();
        }
        #endregion
    }
}
