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
        /// <summary>
        /// 业务管理常规配置
        /// </summary>
        /// <returns></returns>
        public ActionResult BusiCommonDataSet()
        {
            return View();
        }

        /// <summary>
        /// 品保常规配置管理
        /// </summary>
        /// <returns></returns>
        public ActionResult QualityCommonDataSet()
        {
            return View();
        }

        #region config dictionary data operate
        [NoAuthenCheck]
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
        /// <summary>
        /// 获取某一模块类别的配置字典数据
        /// </summary>
        /// <param name="treeModuleKey"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetConfigDicDataAbout(string treeModuleKey, string moduleName)
        {
            var modules = PmConfigService.DataDicManager.FindConfigDatasBy(treeModuleKey, moduleName);
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}