using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Newtonsoft.Json;
using System.IO;

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
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetCollaborateContactDatas(string department, int searchMode, string queryContent)
        {
            QueryContactDto queryDto = new QueryContactDto()
            {
                SearchMode = searchMode,
                Department = department,
                QueryContent = queryContent,
                IsExactQuery = false,
            };
            var datas = ToolOnlineService.ContactManager.GetContactLibDatas(queryDto);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreCollaborateContactDatas(CollaborateContactLibModel model)
        {
            var opResult = ToolOnlineService.ContactManager.StoreData(model);
            return Json(opResult);
        }
        #endregion

        #region WorkTaskManage
        public ActionResult WorkTaskManage()
        {
            return View();
        }
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetWorkTaskManageDatas(string systemName, string moduleName, string progressStatus, int mode)
        {

            QueryWorkTaskManageDto queryDto = new QueryWorkTaskManageDto()
            {
                SystemName = systemName,
                ModuleName = moduleName,
                ProgressStatus = progressStatus,
                SearchMode = mode
            };
            var datas = ToolOnlineService.WorkTaskManage.GetWorkTaskDatasBy(queryDto);
            return DateJsonResult(datas);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreWorkTaskManageDatas(WorkTaskManageModel model)
        {
            var opResult = ToolOnlineService.WorkTaskManage.StoreTaskData(model);
            return Json(opResult);
        }


        #endregion

        #region ReportImproveProblem    
        public ActionResult ReportImproveProblem()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult StoreReportImproveProblemDatas(ReportImproveProblemModels model)
        {
            var opresult = ToolOnlineService.ReportImproveProblemManager.StoreReportImproveProblem(model);
            return Json(opresult);
        }
        /// <summary>
        /// 自动生成编号
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]       
        public JsonResult AutoCreateCaseId()
        {
            var data = ToolOnlineService.ReportImproveProblemManager.AutoCreateCaseIdBuild();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [NoAuthenCheck]
        public ContentResult GetReportImproveProbleDatas(string problemSolve,int mode)
        {
            ReportImproveProblemModelsDto queryDto = new ReportImproveProblemModelsDto()
            {
                ProblemSolve = problemSolve,
                SearchMode = mode
            };
            var datas = ToolOnlineService.ReportImproveProblemManager.GetReportImproveProbleDataBy(queryDto);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult UploadReportProblemFile(HttpPostedFileBase file)
        {

            string addchangeFileName = DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.ReportProblemDataFile, DateTime.Now.ToString("yyyyMM"));
            this.SaveFileToServer(file, filePath, addchangeFileName);
            return Json("OK");
           

        }
        public class FileAttatchData
        {
            public string CaseId { get; set; }
            public string CaseIdNumber { get; set; }
        }


       
        #endregion
    }
}
