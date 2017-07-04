using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline;
using Lm.Eic.Framework.ProductMaster.Model.Tools;

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
        public ContentResult GetWorkTaskManageDatas( string systemName,string moduleName,string progressStatus,int mode)
        {

            QueryWorkTaskManageDto queryDto = new QueryWorkTaskManageDto()
            {
                SystemName=systemName,
                ModuleName=moduleName,
                ProgressStatus=progressStatus,
                SearchMode=mode    
            };
            var datas = ToolOnlineService.WorkTaskManage.GetWorkTaskDatasBy(queryDto);
            return DateJsonResult(datas);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreWorkTaskManageDatas(WorkTaskManageModel model)
        {
            var opResult =ToolOnlineService.WorkTaskManage.StoreTaskData(model);
            return Json(opResult);
        }


        #endregion
    }
}
