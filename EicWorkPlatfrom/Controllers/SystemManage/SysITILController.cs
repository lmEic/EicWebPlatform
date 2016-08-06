using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
namespace EicWorkPlatfrom.Controllers
{
    public class SysITILController : EicBaseController
    {
        //
        // GET: /ITIL/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItilSupTelManage()
        {
            return View();
        }

        public ActionResult ItilProjectDevelopManage()
        {
            return View();
        }
        /// <summary>
        /// 变更开发模块进度状态模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult ChangeDevelopModuleProgressStatusTpl()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult StoreProjectDevelopRecord(ItilDevelopModuleManageModel entity)
        {
            var result = ItilService.ItilDevelopModuleManager.Store(entity);
            return Json(result);
        }
        /// <summary>
        /// 根据开发进度状态查找开发模块
        /// </summary>
        /// <param name="progressStatuses"></param>
        /// <param name="functionName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetProjectDevelopModuleBy(List<string> progressStatuses,string functionName,int mode)
        {
            var result = ItilService.ItilDevelopModuleManager.GetDevelopModuleManageListBy(new ItilDto()
            {
                ProgressSignList = progressStatuses,
                FunctionName = functionName,
                SearchMode = mode
            });
            return DateJsonResult(result);
        }
        /// <summary>
        /// 改变模块开发进度状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ChangeDevelopModuleProgressStatus(ItilDevelopModuleManageModel entity)
        {
            var result = ItilService.ItilDevelopModuleManager.ChangeProgressStatus(entity);
            return Json(result);
        }
        /// <summary>
        /// 查看模块开发明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult ViewDevelopModuleDetails(ItilDevelopModuleManageModel entity)
        {
            var datas = ItilService.ItilDevelopModuleManager.GetChangeRecordListBy(entity);
            return DateJsonResult(datas);            
        }
    }
}