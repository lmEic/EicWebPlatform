using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Mes.Optical.Authen;
using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProEmployeeController : EicBaseController
    {
        //
        // GET: /ProEmployee/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 人员信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistWorkerInfo()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetWorkers()
        {
            var workers = AuthenManager.User.GetWorkers();
            LoginUser currentUser = OnLineUser;
            var currentWorker = workers.FirstOrDefault(e => e.WorkerId == currentUser.UserId);
            if (currentWorker != null)
                currentUser.Department = currentWorker.Department;
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var roles = AuthenService.RoleManager.Roles.Where(e => e.RoleLevel > currentUser.Role.RoleLevel);
            var datas = new {user=currentUser, workers = workers, departments = departments, roles = roles };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetWorkerBy(string workerId)
        {
            var workers = ArchiveService.ProWorkerManager.GetWorkerBy(workerId);
            return Json(workers, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult RegistWorker(ProWorkerInfo worker)
        {
            var result = ArchiveService.ProWorkerManager.RegistUser(worker);
            return Json(result);
        }
        /// <summary>
        /// 所在站别管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ProStationManage()
        {
            return View();
        }
        /// <summary>
        /// 生产班别管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ProClassManage()
        {
            return View();
        }
        /// <summary>
        /// 出勤管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkHoursManage()
        {
            return View();
        }
    }
}
