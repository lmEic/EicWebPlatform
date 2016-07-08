using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class SysConfigController : EicBaseController
    {
        //
        // GET: /SysConfig/

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

        public JsonResult SaveConfigDicData(ConfigDataDictionaryModel model, ConfigDataDictionaryModel oldModel, string opType)
        {
            var result = PmConfigService.DataDicManager.Store(model, oldModel, opType);
            return Json(result);
        }

        [NoAuthenCheck]
        public JsonResult GetConfigDicData(string treeModuleKey)
        {
            var modules = PmConfigService.DataDicManager.FindConfigDatasBy(treeModuleKey);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult LoadConfigDicData(string moduleName, string aboutCategory)
        {
            var modules = PmConfigService.DataDicManager.LoadConfigDatasBy(moduleName, aboutCategory);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
    }
}