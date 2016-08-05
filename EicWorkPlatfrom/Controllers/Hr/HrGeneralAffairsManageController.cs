using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;


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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreWorkerClothesReceiveRecord(WorkClothesManageModel model)
        {
            var result = GeneralAffairsService.WorkerClothesManager.StoreReceiveWorkClothes(model);
            return Json(result);
        }
    }
}
