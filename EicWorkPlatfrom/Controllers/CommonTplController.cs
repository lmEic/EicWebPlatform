using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    /// <summary>
    /// 公共模板控制器
    /// </summary>
    public class CommonTplController : Controller
    {

        // GET: /CommonTpl/
        public ActionResult Index()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult ModuleLayoutIndexTpl()
        {
            return View();
        }
        /// <summary>
        /// 消息提示模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult InfoMsgModalTpl()
        {
            return View();
        }
        /// <summary>
        /// 删除确认模态窗口模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult DeleteModalTpl()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult TreeSelectTpl()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult MonthButtonTpl()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult OperateMsgBoardTpl()
        {
            return View();
        }
    }
}
