using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Newtonsoft.Json;
using System.IO;
using Lm.Eic.Uti.Common.YleeOOMapper;

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
        [NoAuthenCheck]
        public JsonResult UploadReportProblemFile(HttpPostedFileBase file)
        {
            var FailResult = new { Result = "FAIL" };
            if(file!=null)
            {
                if(file.ContentLength>0)
                {
                    FileAttatchData data = JsonConvert.DeserializeObject<FileAttatchData>(Request.Params["fileAttachData"]);
                    if (data == null) return Json(FailResult);
                    string extensionName = Path.GetExtension(file.FileName);
                    string fileName = string.Format("{0}{1}", data.CaseId,extensionName);
                    string fullFileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.ReportProblemDataFile), fileName);
                    file.DeleteExistFile(fullFileName).SaveAs(fullFileName);
                    return Json(new { Result = "OK", FullFileName = fullFileName, FileName = fileName });
                }
            }
            return Json(FailResult);
        }
        public class FileAttatchData
        {
            public string CaseId { get; set; }
           
        }


       
        #endregion
    }
}
