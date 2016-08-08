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
        
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult CanChangeOldForNew(string workerId, string productName, string dealwithType)
        {
            bool canChange = GeneralAffairsService.WorkerClothesManager.CanOldChangeNew(workerId, productName, dealwithType);
            return Json(canChange, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取领取厂服记录
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="department"></param>
        /// <param name="receiveMonth"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetWorkerClothesReceiveRecords(string workerId, string department, string receiveMonth,int mode)
        {
            var datas = GeneralAffairsService.WorkerClothesManager.FindReceiveRecordBy(new QueryGeneralAffairsDto()
            {
                Department = department,
                WorkerId = workerId,
                ReceiveMonth = receiveMonth,
                SearchMode = mode
            });
            return DateJsonResult(datas);
        }
    }
}
