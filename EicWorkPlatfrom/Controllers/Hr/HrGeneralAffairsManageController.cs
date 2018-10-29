﻿using System;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Collections.Generic;

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
        #region 厂服管理

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
        public JsonResult CanChangeOldForNew(string workerId, string productName, string dealwithType, string department)
        {
            bool canChange = GeneralAffairsService.WorkerClothesManager.CanOldChangeNew(workerId, productName, dealwithType, department);
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
        public ContentResult GetWorkerClothesReceiveRecords(string workerId, string department, string receiveMonth, int mode)
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
        [NoAuthenCheck]
        public FileResult BuildReceiveWorkClothesList()
        {
            /// excel
            var dlfm = GeneralAffairsService.WorkerClothesManager.DownLaodBuildReceiveWorkClothesFile();
            return this.DownLoadFile(dlfm);
        }
        #endregion

        #region 报餐管理
        public ActionResult GaMealReportManage()
        {
            return View();
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreReportMealDatas(List<MealReportManageModel> reportMealDatas)
        {
            var result = GeneralAffairsService.ReportMealManager.Store(reportMealDatas);
            return Json(result);
        }
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetReportMealDatas(string reportType, string yearMonth, string department = null, string workerId = null)
        {

            var datas = GeneralAffairsService.ReportMealManager.GetReportMealDatas(reportType, yearMonth, department, workerId);
            return DateJsonResult(datas, "yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        #region 报餐汇总
        public ActionResult GaMealReportQuery()
        {
            return View();
        }
        [NoAuthenCheck]
        public ContentResult GetReportMealSumerizeDatas(DateTime reportMealDate)
        {
            var data = GeneralAffairsService.ReportMealManager.GetAnalogReportMealDatas(reportMealDate);
            TempData["ReportMealSumerize"] = data;
            return DateJsonResult(data);
        }
        [NoAuthenCheck]
        public ContentResult GetReportMealDetialDatas(DateTime reportMealDate, string reportMealType, string department)
        {
            var data = GeneralAffairsService.ReportMealManager.GetReportMealDetailDatas(reportMealDate, reportMealType, department);
            return DateJsonResult(data);
        }
        

        [NoAuthenCheck]
        public ContentResult HandleMonthDetialDatas(string reportMealType, string department, MealReportedAnalogModel datas)
        {
            var data = datas != null? GeneralAffairsService.ReportMealManager.DandelMonthReportMealDatas(reportMealType, department, datas.DetailDatas):null;
            return DateJsonResult(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult ExportReportMealSumerizeDatas()
        {
            var data = TempData["ReportMealSumerize"] as MealReportedAnalogModel;

            return this.DownLoadFile(GeneralAffairsService.ReportMealManager.ExportAnalogData(data));
        }
        [NoAuthenCheck]
        public ContentResult GetReportSumerizeMonthDatas(string reportMealYearMonth)
        {
            var data = GeneralAffairsService.ReportMealManager.GetSumerizeMonthDatas(reportMealYearMonth);
            TempData["ReportMealSumerizeMonth"] = data;
            return DateJsonResult(data);
        }
        [NoAuthenCheck]
        public FileResult ExportReportMealYearMonthSumerizeMonthDatas()
        {
            var data = TempData["ReportMealSumerizeMonth"] as MealReportedAnalogModel;

            return this.DownLoadFile(GeneralAffairsService.ReportMealManager.ExportAnalogMonthData(data));
        }
        #endregion
    }
}
