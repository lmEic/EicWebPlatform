using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    public class AttendAskLeaveManager
    {
        public List<AttendAskLeaveModel> GetAskLeaveDatas(string workerId, string yearMonth)
        {
            return AttendCrudFactory.AskLeaveCrud.GetAskLeaveDatas(workerId, yearMonth);
        }
        /// <summary>
        /// 建立请假报表汇总数据
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public DownLoadFileModel BuildAskLeaveSumerizeReportFile(string yearMonth)
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                  new FileFieldMapping ("WorkerId","工号") ,
                  new FileFieldMapping ("WorkerName","姓名") ,
                  new FileFieldMapping ("Department","部门") ,
                  new FileFieldMapping ("PostType","直/间接") ,

                  new FileFieldMapping ("SJ","事假") ,
                  new FileFieldMapping ("BJ","病假") ,
                  new FileFieldMapping ("YXSJ","有薪事假") ,
                  new FileFieldMapping ("NXJ","年休假") ,
                  new FileFieldMapping ("GSJ","工伤") ,
                  new FileFieldMapping ("HJ","婚假") ,
                  new FileFieldMapping ("ShangJ","丧假") ,
                  new FileFieldMapping ("CJ","产假") ,
                   new FileFieldMapping ("PCJ","陪产假") ,
                  new FileFieldMapping ("KGJ","旷工") ,
                  new FileFieldMapping ("TXJ","调休") ,
                };
            var datas = GetAskLeaveSumerizeDatas(yearMonth);
            if (datas == null || datas.Count == 0) return new DownLoadFileModel().Default();
            var dataGrouping = datas.GetGroupList<AttendAskLeaveSumerizeItem>("请假数据汇总");
            return dataGrouping.ExportToExcelMultiSheets<AttendAskLeaveSumerizeItem>(fieldmappping).CreateDownLoadExcelFileModel(string.Format("{0}月份请假汇总", yearMonth));
        }
        /// <summary>
        /// 获取给定月份的请假汇总信息
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        private List<AttendAskLeaveSumerizeItem> GetAskLeaveSumerizeDatas(string yearMonth)
        {
            var sumerizeDatas = new List<AttendAskLeaveSumerizeItem>();
            var askLeaveDatas = AttendCrudFactory.AskLeaveCrud.GetAskLeaveDatas(yearMonth);
            if (askLeaveDatas == null || askLeaveDatas.Count == 0) return sumerizeDatas;
            List<string> workers = askLeaveDatas.Select(s => s.WorkerId).Distinct().ToList<string>();
            var workerManager = new ArchivesManager();
            workers.ForEach(w =>
            {
                var askLeaveWorker = askLeaveDatas.FindAll(e => e.WorkerId == w);
                var worker = askLeaveWorker[0];
                AttendAskLeaveSumerizeItem item = AttendAskLeaveSumerizeItem.Create(worker.WorkerId, worker.WorkerName, worker.Department);
                SetPostType(item, workerManager);
                askLeaveWorker.ForEach(alw =>
                {
                    item = SetAskLeaveHour(item, alw);
                });
                sumerizeDatas.Add(item);
            });
            return sumerizeDatas;
        }
        /// <summary>
        /// 设定假期数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="askLeave"></param>
        /// <returns></returns>
        private AttendAskLeaveSumerizeItem SetAskLeaveHour(AttendAskLeaveSumerizeItem item, AttendAskLeaveModel askLeave)
        {
            switch (askLeave.LeaveType.Trim())
            {
                case "事假":
                    item.SJ += askLeave.LeaveHours;
                    break;
                case "病假":
                    item.BJ += askLeave.LeaveHours;
                    break;
                case "有薪事假":
                    item.YXSJ += askLeave.LeaveHours;
                    break;
                case "有薪病假":
                    item.YXBJ += askLeave.LeaveHours;
                    break;
                case "年休假":
                    item.NXJ += askLeave.LeaveHours;
                    break;
                case "工伤假":
                    item.GSJ += askLeave.LeaveHours;
                    break;

                case "婚假":
                    item.HJ += askLeave.LeaveHours;
                    break;
                case "丧假":
                    item.ShangJ += askLeave.LeaveHours;
                    break;
                case "产假":
                    item.CJ += askLeave.LeaveHours;
                    break;
                case "陪产假":
                    item.PCJ += askLeave.LeaveHours;
                    break;
                case "调休":
                    item.TXJ += askLeave.LeaveHours;
                    break;
                default:
                    break;
            }
            return item;
        }
        private void SetPostType(AttendAskLeaveSumerizeItem item, ArchivesManager manager)
        {
            var worker = manager.FindWorkers(new QueryWorkersDto() { WorkerId = item.WorkerId }, 2).FirstOrDefault();
            item.PostType = worker.PostNature;
        }
        public OpResult HandleAskForLeave(List<AttendAskLeaveModel> askForLeaves)
        {
            if (askForLeaves == null) return OpResult.SetErrorResult("askForLeaves 不能为null");
            bool result = true;
            try
            {
                AttendClassTypeDetailModel classTypeMdl = null;
                AttendSlodFingerDataCurrentMonthModel attendMdl = null;
                askForLeaves.ForEach(m =>
                {
                    if (m.OpSign == OpMode.None)
                        result = result && true;
                    else
                    {
                        m = EncodeAskLeaveDateData(m);
                        attendMdl = AttendCrudFactory.CurrentMonthAttendDataCrud.GetAttendanceDataBy(m.WorkerId, m.AttendanceDate);
                        classTypeMdl = attendMdl == null ? AttendCrudFactory.ClassTypeDetailCrud.GetClassTypeDetailModel(m.WorkerId, m.AttendanceDate) : null;
                        //同步考勤数据
                        int record = AttendCrudFactory.CurrentMonthAttendDataCrud.SyncAskLeaveDataToAttendData(m, classTypeMdl, ref attendMdl);
                        if (record > 0)
                        {
                            m.SlotCardTime = attendMdl.SlotCardTime;
                            result = result && AttendCrudFactory.AskLeaveCrud.Store(m).Result;
                        }
                    }
                });
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }
            return OpResult.SetResult("存储请假数据成功！", result);
        }
        /// <summary>
        /// 编码请假日期数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private AttendAskLeaveModel EncodeAskLeaveDateData(AttendAskLeaveModel model)
        {
            string dateStr = model.YearMonth.Substring(0, 4) + "-" + model.YearMonth.Substring(4, 2) + "-" + model.Day.ToString().PadLeft(2, '0');
            DateTime attendDate = DateTime.Parse(dateStr);
            model.AttendanceDate = attendDate.ToDate();
            if (string.IsNullOrEmpty(model.LeaveTimeRegion)) model.LeaveTimeRegion = "";
            string[] timeRegions = model.LeaveTimeRegion.Split('-');
            if (timeRegions.Length == 2)
            {
                string timeStart = timeRegions[0].Trim();
                string timeEnd = timeRegions[1].Trim();
                model.LeaveTimeRegionStart = DateTime.Parse(dateStr + " " + timeStart);
                model.LeaveTimeRegionEnd = DateTime.Parse(dateStr + " " + timeEnd);
            }
            return model;
        }
    }
}
