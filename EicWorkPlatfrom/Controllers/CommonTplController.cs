using System;
using System.Web.Mvc;
using System.IO;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
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

        /// <summary>
        /// 季度按钮模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult QuarterButtonTpl()
        {
            return View();
        }

        [NoAuthenCheck]
        public ActionResult OperateMsgBoardTpl()
        {
            return View();
        }

        /// <summary>
        /// 图片文件查看预览窗口
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult ImageFilePreviewTpl()
        {
            return View();
        }
    }
}
