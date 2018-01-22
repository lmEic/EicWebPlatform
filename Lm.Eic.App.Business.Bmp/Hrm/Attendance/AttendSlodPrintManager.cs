using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    /// <summary>
    /// 指纹刷卡数据管理器
    /// </summary>
    public class AttendSlodPrintManager
    {
        #region member

        private AttendSlodFingerDataCurrentMonthCurd currentMonthAttendDataHandler = null;

        #endregion member

        #region constructure

        public AttendSlodPrintManager()
        {
            this.currentMonthAttendDataHandler = new AttendSlodFingerDataCurrentMonthCurd();
        }

        #endregion constructure

        #region method
        /// <summary>
        /// 载入某部门的当天的数据
        /// </summary>
        /// <param name="qryDate"></param>
        /// <returns></returns>
        public List<AttendanceDataModel> LoadAttendDataInToday(DateTime qryDate)
        {
            var qdate = qryDate.ToDate();
            return this.currentMonthAttendDataHandler.LoadAttendDataInToday(qdate);
        }
        /// <summary>
        /// 按考勤日期导出数据
        /// </summary>
        /// <param name="qryDate"></param>
        /// <returns></returns>
        public DownLoadFileModel BuildAttendanceDataBy(DateTime qryDate)
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping ("Number","项次") ,
                  new FileFieldMapping ("WorkerId","工号") ,
                  new FileFieldMapping ("WorkerName","姓名") ,
                  new FileFieldMapping ("Department","部门") ,
                  new FileFieldMapping ("ClassType","班别") ,
                  new FileFieldMapping ("AttendanceDate","刷卡日期") ,
                  new FileFieldMapping ("SlotCardTime1","第一次时间") ,
                  new FileFieldMapping ("SlotCardTime2","第一次时间") ,
                  new FileFieldMapping ("SlotCardTime","刷卡时间") ,
                };
            var datas = LoadAttendDataInToday(qryDate);
            if (datas == null || datas.Count < 0) return new DownLoadFileModel(2).Default();
            var dataGrouping = datas.GetGroupList<AttendanceDataModel>("考勤数据");
            return dataGrouping.ExportToExcelMultiSheets<AttendanceDataModel>(fieldmappping).CreateDownLoadExcelFileModel(qryDate.ToShortDateString() + "考勤数据-" + qryDate.ToShortDateString());
        }
        /// <summary>
        /// 按月份导出数据
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public DownLoadFileModel BuildAttendanceDataBy(string yearMonth)
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                 new FileFieldMapping ("Number","项次") ,
                  new FileFieldMapping ("WorkerId","工号") ,
                  new FileFieldMapping ("WorkerName","姓名") ,
                  new FileFieldMapping ("Department","部门") ,
                  new FileFieldMapping ("ClassType","班别") ,
                  new FileFieldMapping ("LeaveType","请假类别") ,
                  new FileFieldMapping ("LeaveHours","请假时数") ,
                  new FileFieldMapping ("LeaveTimeRegion","请假时段") ,
                  new FileFieldMapping ("LeaveDescription","请假描述") ,
                  new FileFieldMapping ("AttendanceDate","刷卡日期") ,
                  new FileFieldMapping ("SlotCardTime1","第一次时间") ,
                  new FileFieldMapping ("SlotCardTime2","第一次时间") ,
                  new FileFieldMapping ("SlotCardTime","刷卡时间") ,
                };
            var datas = this.currentMonthAttendDataHandler.LoadAttendanceDatasBy(new AttendanceDataQueryDto() { SearchMode = 3, YearMonth = yearMonth });
            if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
            BindingAskLeaveDataToAttendance(datas);
            var dataGrouping = datas.GetGroupList<AttendanceDataModel>("考勤数据");
            return dataGrouping.ExportToExcelMultiSheets<AttendanceDataModel>(fieldmappping).CreateDownLoadExcelFileModel("考勤数据-" + yearMonth);
        }
        private void BindingAskLeaveDataToAttendance(List<AttendanceDataModel> datas)
        {
            datas.ForEach(d =>
            {
                if (d.LeaveHours > 0)
                {
                    AttendAskLeaveEntry item = AttendCrudFactory.AskLeaveCrud.GetAskLeaveDatasOfWorker(d.WorkerId, d.AttendanceDate);
                    if (item != null)
                    {
                        d.LeaveHours = item.AskLeaveHours;
                        d.LeaveTimeRegion = item.AskLeaveRegion;
                        d.LeaveType = item.AskLeaveType;
                        d.LeaveDescription = item.AskLeaveDescription;
                    }
                }
            });
        }
        public List<AttendanceDataModel> LoadAttendDataInToday(DateTime dateFrom, DateTime dateTo, string department)
        {
            var dfrom = dateFrom.ToDate();
            var dTo = dateTo.ToDate();
            return this.currentMonthAttendDataHandler.LoadAttendDataInToday(dateFrom, dateTo, department);
        }
        public List<AttendanceDataModel> LoadAttendDatasBy(string workerId, DateTime dateFrom, DateTime dateTo)
        {
            var dFrom = dateFrom.ToDate();
            var dTo = dateTo.ToDate();
            return this.currentMonthAttendDataHandler.LoadAttendanceDatasBy(new AttendanceDataQueryDto() { SearchMode = 2, WorkerId = workerId, DateFrom = dFrom, DateTo = dTo });

        }
        /// <summary>
        /// 自动处理异常数据
        /// </summary>
        public List<AttendSlodFingerDataCurrentMonthModel> AutoCheckExceptionSlotData(string yearMonth)
        {
            return this.currentMonthAttendDataHandler.AutoHandleExceptionSlotData(yearMonth);
        }
        /// <summary>
        /// 载入异常考勤数据
        /// </summary>
        /// <returns></returns>
        public List<AttendSlodFingerDataCurrentMonthModel> LoadExceptionSlotData()
        {
            return this.currentMonthAttendDataHandler.LoadExceptionSlotData();
        }
        /// <summary>
        /// 处理异常刷卡数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult HandleExceptionSlotCardData(List<AttendSlodFingerDataCurrentMonthModel> entities)
        {
            return OpResult.SetSuccessResult("处理异常刷卡数据成功！", this.currentMonthAttendDataHandler.HandleExceptionSlotCardData(entities) > 0);
        }

        #endregion method
    }



    /// <summary>
    /// 实时数据处理器
    /// </summary>
    public class AttendFingerPrintDataInTimeHandler
    {
        #region member

        private IAttendFingerPrintDataInTimeRepository irep = null;

        #endregion member

        #region constructure

        public AttendFingerPrintDataInTimeHandler()
        {
            this.irep = new AttendFingerPrintDataInTimeRepository();
        }

        #endregion constructure

        #region property

        /// <summary>
        /// 实时的刷卡数据
        /// </summary>
        public List<AttendFingerPrintDataInTimeModel> FingPrintDatas
        {
            get
            {
                return this.irep.Entities.ToList();
            }
        }

        #endregion property

        #region method
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public void BackupData(List<AttendFingerPrintDataInTimeModel> entities, int targetRecord)
        {
            int record = 0;
            entities.ForEach(entity => { record += this.irep.backupData(entity); });
            if (record == targetRecord)
            {
                var mdl = entities[0];
                this.irep.Delete(e => e.WorkerId == mdl.WorkerId && e.SlodCardDate == mdl.SlodCardDate);
            }
        }
        public int StoreNoIdentityWorkerInfo(AttendFingerPrintDataInTimeModel entity)
        {
            return this.irep.StoreNoIdentityWorkerInfo(entity);
        }
        /// <summary>
        /// 实时考勤数据表中是否有考勤数据
        /// </summary>
        public bool IsExsitAttendData
        {
            get
            {
                var datas = this.irep.Entities;
                return datas != null && datas.Count() > 0;
            }
        }

        #endregion method

        public void tu()
        {
            var datas = this.irep.loaddatas();
            datas.ForEach(d =>
            {
                if (!this.irep.IsExist(e => e.WorkerId == d.WorkerId && e.SlodCardDate == d.SlodCardDate && e.SlodCardTime == d.SlodCardTime))
                {
                    if (this.irep.Insert(d) == 1)
                    {
                        this.irep.deleteLibData(d.SlodCardTime, d.WorkerId);
                    }
                }
                else
                {
                    this.irep.deleteLibData(d.SlodCardTime, d.WorkerId);
                }
            });
        }
    }
    /// <summary>
    /// 作业员出勤刷卡模型
    /// </summary>
    public class AttendSlodWorkerModel
    {
        public string WorkerId { get; set; }

        public string WorkerName { get; set; }

        public string Department { get; set; }

        public DateTime AttendDateStart { get; set; }

        public DateTime AttendDateEnd { get; set; }
    }
}