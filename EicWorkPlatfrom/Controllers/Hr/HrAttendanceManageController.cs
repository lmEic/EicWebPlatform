using System;
using Lm.Eic.App.Business.Bmp.Hrm.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours;
using System.Web;
using System.IO;

namespace EicWorkPlatfrom.Controllers.Hr
{
    public class HrAttendanceManageController : EicBaseController
    {
        #region 班别管理
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
        public ContentResult GetClassTypeDatas(string department, string workerId, string classType)
        {
            var datas = AttendanceService.ClassTypeSetter.LoadDatasBy(department, workerId, classType);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult SaveClassTypeDatas(List<AttendClassTypeModel> classTypes)
        {
            var result = AttendanceService.ClassTypeSetter.SetClassType(classTypes, OnLineUser.UserName);
            return Json(result);
        }
        #endregion

        #region 考勤数据汇总管理
        /// <summary>
        /// 今日考勤
        /// </summary>
        /// <returns></returns>
        public ActionResult HrSumerizeAttendanceData()
        {
            return View();
        }
        /// <summary>
        /// 获取今日的考勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetAttendanceDatas(DateTime qryDate, DateTime dateFrom, DateTime dateTo, string department, string workerId, int mode)
        {
            List<AttendanceDataModel> datas = new List<AttendanceDataModel>();
            if (mode == 0)
                datas = AttendanceService.AttendSlodPrintManager.LoadAttendDataInToday(qryDate);
            else if (mode == 1)
                datas = AttendanceService.AttendSlodPrintManager.LoadAttendDataInToday(dateFrom, dateTo, department);
            else if (mode == 2)
                datas = AttendanceService.AttendSlodPrintManager.LoadAttendDatasBy(workerId, dateFrom, dateTo);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 导出当前日期考勤记录
        /// </summary>
        /// <param name="qryDate"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult ExoportAttendanceDatasToExcel(DateTime qryDate)
        {
            ///excel
            var dlfm = AttendanceService.AttendSlodPrintManager.BuildAttendanceDataBy(qryDate);
            return this.DownLoadFile(dlfm);
        }
        [NoAuthenCheck]
        public FileResult ExoportAttendanceMonthDatasToExcel(string yearMonth)
        {
            ///excel
            var dlfm = AttendanceService.AttendSlodPrintManager.BuildAttendanceDataBy(yearMonth);
            return this.DownLoadFile(dlfm);
        }
        #endregion

        #region 请假管理
        /// <summary>
        /// 请假管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrAskLeaveManage()
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
            return Json(leaveConfigTypes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 处理请假数据
        /// </summary>
        /// <param name="askForLeaves"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult HandleAskForLeave(List<AttendAskLeaveModel> askForLeaves)
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
            var datas = AttendanceService.AttendAskLeaveManager.GetAskLeaveDatas(workerId, yearMonth);
            return DateJsonResult(datas);
        }

        [NoAuthenCheck]
        public ContentResult GetAskLeaveDataAbout1(string workerId, string yearMonth)
        {
            var datas = AttendanceService.AttendAskLeaveManager.GetAskLeaveDatas(workerId, yearMonth);
            return DateJsonResult(datas);
        }
        #endregion

        #region 考勤异常检测管理
        /// <summary>
        /// 自动检测考勤异常数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ContentResult AutoCheckExceptionSlotData(string yearMonth)
        {
            var datas = AttendanceService.AttendSlodPrintManager.AutoCheckExceptionSlotData(yearMonth);
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
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <returns></returns>
        public ActionResult HrHandleException()
        {
            return View();
        }
        #endregion

        #region 加班管理
        public ActionResult HrWorkOverHoursManage()
        {
            return View();
        }
        [NoAuthenCheck]
       
        public ContentResult GetWorkOverHoursData(DateTime workDate,string departmentText,int mode)
        {
            WorkOverHoursDto dto = new WorkOverHoursDto()
            {
                WorkDate = workDate,
                DepartmentText=departmentText,
                SearchMode = mode

            };
            var datas = WorkOverHoursService.WorkOverHoursManager.FindRecordBy(dto);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        /// <summary>
        /// 载入模板
        /// </summary>
        /// <param name="workDate"></param>
        /// <param name="departmentText"></param>
        /// <returns></returns>
        public ContentResult GetWorkOverHoursMode(string departmentText,DateTime workDate)
        {
            var datas = WorkOverHoursService.WorkOverHoursManager.FindRecordByModel(departmentText,workDate);
            TempData["WorkOverHoursDatas"] = datas;
            return DateJsonResult(datas);
        }
        [HttpPost]
        [NoAuthenCheck]
        
        public JsonResult HandlWorkOverHoursDt(List<WorkOverHoursMangeModels> workOverHours)
        {
            var result = WorkOverHoursService.WorkOverHoursManager.HandleWorkOverHoursDatas(workOverHours);
            return Json(result);

        }
        /// <summary>
        /// 导入EXCEL数据到DataSets
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult ImportWorkOverHoursDatas(HttpPostedFileBase file)
        {
            List<WorkOverHoursMangeModels> datas = null;
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp);
            SaveFileToServer(file, filePath, () =>
            {
                string fileName = Path.Combine(filePath, file.FileName);
                datas = WorkOverHoursService.WorkOverHoursManager.ImportWorkOverHoursListBy(fileName);
                //System.IO.File.Delete(fileName);
            });
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public FileResult WorkOverHoursDatasToExcel()
        {
            var datas = TempData["WorkOverHoursDatas"] as List<WorkOverHoursMangeModels>;
            var dlfm = WorkOverHoursService.WorkOverHoursManager.WorkOverHoursDatasDLFM(datas);
            return this.DownLoadFile(dlfm);

        }

        #endregion
    }
}