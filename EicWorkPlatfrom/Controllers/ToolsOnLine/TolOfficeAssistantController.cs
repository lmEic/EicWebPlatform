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
            department = "品保部";
            QueryContactDto queryDto = new QueryContactDto()
            {
                SearchMode = searchMode,
                Department = department,
                QueryContent = queryContent,
                IsExactQuery = false,
            };
            var datas = 0; /*ToolOnlineService.ContactManager.GetContactLibDatasBy(queryDto);*/
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
        public ContentResult GetWorkTaskManageDatas(string department, int searchMode, string queryContent)
        {
            // var datas = ToolOnlineMockDatas.WorkTaskManageDataSet;
           department = "EIC";
           QueryWorkTaskManageDto queryDto = new QueryWorkTaskManageDto()
            {
                SearchMode = searchMode,
                Department = department,
                QueryContent = queryContent,

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
