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
        public JsonResult GetCollaborateContactDatas(string department, int searchMode, string contactPerson, string telPhone)
        {
            var datas = ToolOnlineMockDatas.CollaborateContactDataSet;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreCollaborateContactDatas(CollaborateContactLibModel model)
        {
            var opResult = 1;
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
        public JsonResult GetWorkTaskManageDatas(string department, int searchMode, string systemName, string moduleName)
        {
            var datas = ToolOnlineMockDatas.WorkTaskManageDataSet;
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreWorkTaskManageDatas(WorkTaskManageModel model)
        {
            var opResult =1;
            return Json(opResult);
        }


        #endregion
    }
}
