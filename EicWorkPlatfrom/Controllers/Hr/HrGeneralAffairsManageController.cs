using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs;


namespace EicWorkPlatfrom.Controllers.Hr
{
    //总务管理控制器
    public class HrGeneralAffairsManageController : EicBaseController
    {
        //
        // GET: /HrGeneralAffairsManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GaWorkerClothesManage()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult StoreWorkerClothesReceiveRecord()
        {
            var result = 0;
            return Json(result);
        }
    }
}
