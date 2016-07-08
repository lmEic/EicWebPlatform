using Lm.Eic.App.Business.Bmp.Hrm.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Hr
{
    public class HrAttendanceManageController : EicBaseController
    {
        //
        // GET: /HrAttendanceManage/
        /// <summary>
        /// 班别管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrClassTypeManage()
        {
            return View();
        }

        [NoAuthenCheck]
        public ContentResult GetClassTypeDatas(string department)
        {
            var datas = AttendanceService.ClassTypeSetter.LoadDatasBy(department);
            return DateJsonResult(datas);
        }

        [HttpPost]
        public JsonResult SaveClassTypeDatas(List<AttendClassTypeModel> classTypes)
        {
            var result = AttendanceService.ClassTypeSetter.SetClassType(classTypes, OnLineUser.UserName);
            return Json(result);
        }

        /// <summary>
        /// 今日考勤
        /// </summary>
        /// <returns></returns>
        public ActionResult HrAttendInToday()
        {
            return View();
        }

        /// <summary>
        /// 获取今日的考勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAttendanceDatasOfToday(string department)
        {
            var datas = AttendanceService.AttendSlodPrintManager.LoadAttendDataInToday(department);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 请假管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrAskLeaveManage()
        {
            return View();
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrHandleException()
        {
            return View();
        }

        /// <summary>
        /// 载入请假类别，同时载入部门信息
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetLeaveTypesConfigs()
        {
            List<ConfigDataDictionaryModel> leaveConfigTypes = PmConfigService.DataDicManager.LoadConfigDatasBy("AttendanceConfig", "AskForLeaveType");
            List<ConfigDataDictionaryModel> departments = PmConfigService.DataDicManager.FindConfigDatasBy("Organization");
            departments.AddRange(leaveConfigTypes);
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 处理请假数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult HandleAskForLeave(List<AttendAskLeaveModel> askForLeaves)
        {
            var result = AttendanceService.AttendAskLeaveManager.HandleAskForLeave(askForLeaves);
            return Json(result);
        }

        /// <summary>
        /// 处理请假数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateAskForLeave(List<AttendSlodFingerDataCurrentMonthModel> askForLeaves)
        {
            var result = AttendanceService.AttendAskLeaveManager.HandleAskForLeave(askForLeaves);
            return Json(result);
        }

        /// <summary>
        /// 获取某人的当月请假数据
        /// </summary>
        /// <param name="workerId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAskLeaveDataAbout(string workerId, string yearMonth)
        {
            var datas = AttendanceService.AttendAskLeaveManager.GetAskLeaveDataAbout(workerId, yearMonth);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 自动检测考勤异常数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult AutoCheckExceptionSlotData(string yearMonth)
        {
            var datas = AttendanceService.AttendSlodPrintManager.AutoCheckExceptionSlotData();
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 处理考勤异常数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult HandleExceptionSlotData(List<AttendSlodFingerDataCurrentMonthModel> exceptionDatas)
        {
            var datas = AttendanceService.AttendSlodPrintManager.HandleExceptionSlotCardData(exceptionDatas);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 载入考勤异常数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetExceptionSlotData()
        {
            var datas = AttendanceService.AttendSlodPrintManager.LoadExceptionSlotData();
            return DateJsonResult(datas);
        }
    }
}