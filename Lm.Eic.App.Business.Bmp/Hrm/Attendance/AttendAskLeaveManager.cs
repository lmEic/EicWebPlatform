using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    public class AttendAskLeaveManager
    {
        public List<AttendAskLeaveModel> GetAskLeaveDatas(string workerId, string yearMonth)
        {
            return AttendCrudFactory.AskLeaveCrud.GetAskLeaveDatas(workerId, yearMonth);
        }
        public OpResult HandleAskForLeave(List<AttendAskLeaveModel> askForLeaves)
        {
            if (askForLeaves == null) return OpResult.SetErrorResult("askForLeaves 不能为null");
            bool result = true;
            AttendSlodFingerDataCurrentMonthModel attendMdl = null;
            try
            {
                askForLeaves.ForEach(m =>
                {

                    m = EncodeAskLeaveDateData(m);
                    attendMdl = AttendCrudFactory.CurrentMonthAttendDataCrud.GetAttendanceDataBy(m.WorkerId, m.AttendanceDate);
                    var classTypeMdl = AttendCrudFactory.ClassTypeDetailCrud.GetClassTypeDetailModel(m.WorkerId, m.AttendanceDate);
                    //同步考勤数据
                    int record = AttendCrudFactory.CurrentMonthAttendDataCrud.SyncAskLeaveDataToAttendData(m, classTypeMdl, ref attendMdl);
                    if (record > 0)
                    {
                        m.SlotCardTime = attendMdl.SlotCardTime;
                        result = result && AttendCrudFactory.AskLeaveCrud.Store(m).Result;
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
