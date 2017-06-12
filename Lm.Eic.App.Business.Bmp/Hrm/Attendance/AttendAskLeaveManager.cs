using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    public class AttendAskLeaveManager
    {
        ///// <summary>
        ///// 同步请假数据信息
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //internal int SyncAskLeaveData(List<AttendAskLeaveModel> entities)
        //{
        //    int record = 0;
        //    if (entities == null || entities.Count == 0) return record;
        //    entities.ForEach(e =>
        //    {
        //        int days = (e.EndLeaveDate - e.StartLeaveDate).Days;
        //        if (days > 0)
        //        {
        //            DateTime dnow = e.StartLeaveDate;
        //            //循环遍历请假的每一天，如果存在考勤数据，则进行更改，不存在，则直接添加考勤数据
        //            for (int dayIndex = 0; dayIndex <= days; dayIndex++)
        //            {
        //                if (!this.irep.IsExist(w => w.WorkerId == e.WorkerId && w.AttendanceDate == dnow))
        //                {
        //                    record += this.irep.Insert(new AttendSlodFingerDataCurrentMonthModel()
        //                    {
        //                        AttendanceDate = dnow,
        //                        Department = e.Department,
        //                        WorkerId = e.WorkerId,
        //                        WorkerName = e.WorkerName,
        //                        ClassType = e.ClassType,
        //                        YearMonth = dnow.ToString("yyyyMM"),
        //                        WeekDay = dnow.DayOfWeek.ToString().ToChineseWeekDay(),
        //                        LeaveHours = e.LeaveHours,
        //                        LeaveMark = 1,
        //                        LeaveType = e.LeaveType,
        //                        LeaveTimeRegion = e.LeaveTimeRegionStart + "--" + e.LeaveTimeRegionEnd,
        //                        LeaveDescription = "请假处理,假别：" + e.LeaveType,
        //                        LeaveMemo = e.LeaveMemo,
        //                        HandleSlotExceptionStatus = 0,
        //                        SlotExceptionMark = 0,
        //                        OpSign = "init",
        //                    });
        //                }
        //                else
        //                {
        //                    record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == dnow,
        //                         u => new AttendSlodFingerDataCurrentMonthModel
        //                         {
        //                             LeaveHours = e.LeaveHours,
        //                             LeaveMark = 1,
        //                             LeaveType = e.LeaveType,
        //                             LeaveTimeRegion = e.LeaveTimeRegionStart + "--" + e.LeaveTimeRegionEnd,
        //                             LeaveDescription = "请假处理,假别：" + e.LeaveType,
        //                             LeaveMemo = e.LeaveMemo
        //                         });
        //                }
        //                dnow = dnow.AddDays(1);
        //            }
        //        }
        //    });
        //    return record;
        //}

        ///// <summary>
        ///// 更改请假信息
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //internal int UpdateAskLeaveData(List<AttendSlodFingerDataCurrentMonthModel> entities)
        //{
        //    int record = 0;
        //    if (entities == null || entities.Count == 0) return record;
        //    try
        //    {
        //        entities.ForEach(e =>
        //        {
        //            if (e.OpSign == "handleEdit")
        //            {
        //                record += this.irep.Update(w => w.WorkerId == e.WorkerId && w.AttendanceDate == e.AttendanceDate,
        //                               u => new AttendSlodFingerDataCurrentMonthModel
        //                               {
        //                                   LeaveHours = e.LeaveHours,
        //                                   LeaveMark = e.LeaveHours < 0.01 ? 0 : 1,
        //                                   LeaveType = e.LeaveType,
        //                                   LeaveTimeRegion = e.LeaveTimeRegion,
        //                                   LeaveDescription = "请假处理,假别：" + e.LeaveType,
        //                                   LeaveMemo = e.LeaveMemo
        //                               });
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return record;
        //}

        ///// <summary>
        ///// 获取某作业人员的请假信息
        ///// </summary>
        ///// <param name="workerId"></param>
        ///// <returns></returns>
        //internal List<AttendSlodFingerDataCurrentMonthModel> GetAskLeaveDataAbout(string workerId, string qryMonth)
        //{
        //    return this.irep.Entities.Where(e => e.WorkerId == workerId && e.YearMonth == qryMonth && e.LeaveMark == 1).ToList();
        //}

        public OpResult HandleAskForLeave(List<AttendAskLeaveModel> askForLeaves)
        {
            if (askForLeaves == null) return OpResult.SetErrorResult("askForLeaves 不能为null");
            bool result = true;
            askForLeaves.ForEach(m =>
            {
                if (AttendCrudFactory.AskLeaveCrud.Store(m).Result)
                {

                }
            });
            return OpResult.SetSuccessResult("存储请假数据成功！");
        }
    }
}
