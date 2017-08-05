using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Framework.ProductMaster.Model.MessageNotify;
using Lm.Eic.Framework.ProductMaster.Business.MessageNotify;

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
        #region ItilProjectDevelopManage
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
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetProjectDevelopModuleBy(List<string> progressStatuses)
        {
            var result = ItilService.ItilDevelopModuleManager.GetDevelopModuleManageListBy(new ItilDto() { ProgressSignList = progressStatuses, SearchMode = 1 });
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
        #endregion

        #region ItilMessageNotifyManage
        public ActionResult ItilMessageNotifyManage()
        {
            return View();
        }
        #endregion


        #region ItilEmailManage
        public ActionResult ItilEmailManage()
        {
            return View();
        }
        [HttpPost]
        [NoAuthenCheck]
        //邮箱登记存储
        public JsonResult StoreEmailManageRecord(ItilEmailManageModel model)
        {
            try
            {
                var opresult = ItilService.EmailManager.StoreItilEmailManage(model);
                return Json(opresult);
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }

        }

        [HttpGet]
        [NoAuthenCheck]
        //邮箱查询
        public ContentResult GetEmailManageRecord(string workerId, string email, int receiveGrade, string department, int mode)
        {
            var datas = ItilService.EmailManager.GetEmails(new ItilEmailManageModelDto()
            {
                WorkerId = workerId,
                Email = email,
                ReceiveGrade = receiveGrade,
                Department = department,
                SearchMode = mode

            });
            return DateJsonResult(datas);

        }
        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SendMail()
        {
            var result = 0;/*ItilService.ItilDevelopModuleManager.SendMail();*/
            return Json(result);
        }
        #endregion

        #region  NotifyAddress  通知地址

        public ActionResult FormNotifyManage()
        {
            return View();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreitilNotifyAddress(ConfigNotifyAddressModel entity)
        {
            var result = NotifyService.NotifyManager.StoreNotifyInfo(entity);
            return Json(result);
        }
        #endregion 
    }
}