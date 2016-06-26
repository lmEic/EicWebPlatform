using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;

namespace EicWorkPlatfrom.Controllers.Hr
{
    public class HrBaseInfoManageController : EicBaseController
    {
        //
        // GET: /HrBaseInfoManage/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HrDepartmentSet()
        {
            return View();
        }

        public ActionResult HrCommonDataSet()
        {
            return View();
        }
        public JsonResult SaveConfigDicData(ConfigDataDictionaryModel model,ConfigDataDictionaryModel oldModel, string opType)
        {
            var result = PmConfigService.DataDicManager.Store(model,oldModel,opType);
            return Json(result);
        }
       
        [NoAuthenCheck]
        public JsonResult GetConfigDicData(string treeModuleKey)
        {
            var modules = PmConfigService.DataDicManager.FindConfigDatasBy(treeModuleKey);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult LoadConfigDicData(string moduleName,string aboutCategory)
        {
            var modules = PmConfigService.DataDicManager.LoadConfigDatasBy(moduleName, aboutCategory);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
    }
}
