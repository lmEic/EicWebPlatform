using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.hr
{
    public class HRController : EicBaseController
    {
        //
        // GET: /HR/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 岗位选择视图模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult PostChangeSelectTpl()
        {
            return View();
        }

        /// <summary>
        /// 学习信息编辑模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult StudyEditSelectTpl()
        {
            return View();
        }

        /// <summary>
        /// 联系方式编辑模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult TelEditSelectTpl()
        {
            return View();
        }

        /// <summary>
        /// 请假编辑模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult AskLeaveEditSelectTpl()
        {
            return View();
        }

        /// <summary>
        /// 增补刷卡时间模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult AddSlotCardTimeSelectTpl()
        {
            return View();
        }

        /// <summary>
        /// 迟到旷工确认窗口模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult AbsentSelectTpl()
        {
            return View();
        }
    }
}