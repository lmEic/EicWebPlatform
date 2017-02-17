using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class HomeController : EicBaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult EditHomeCalendarTpl()
        {
            return View();
        }
        /// <summary>
        /// 获取模块导航列表
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetModuleNavList()
        {
            List<ModuleNavigationModel> datas = GetChildrenNavModules("系统集成平台", "Home", 1);
            if (datas != null)
            {
                var mdl = datas.FirstOrDefault(e => e.ModuleText == "Home");
                if (mdl != null)
                    datas.Remove(mdl);
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nowYear"></param>
        /// <param name="nowMonth"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetCalendarDatas(int  nowYear,int  nowMonth)
        {
            var datas =  ArchiveService.ArCalendarManger.GetDateDictionary(nowYear,nowMonth);;
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取模块导航列表
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetSubModuleNavList(string moduleText, string cacheKey)
        {
            var datas = GetMenuNavModules(moduleText, cacheKey);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存行事历
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveCalendarDatas(CalendarModel vm)
        {
            var resultstring = ArchiveService.ArCalendarManger.store(vm);
            return null;
        }


    }
}