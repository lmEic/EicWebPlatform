using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance
{
    /// <summary>
    ///班别设置持久化层
    /// </summary>
    public interface IAttendClassTypeRepository : IRepository<AttendClassTypeModel> { }
    /// <summary>
    ///班别设置持久化层
    /// </summary>
    public class AttendClassTypeRepository : HrmRepositoryBase<AttendClassTypeModel>, IAttendClassTypeRepository
    { }

    /// <summary>
    ///实时刷卡数据持久化层
    /// </summary>
    public interface IAttendFingerPrintDataInTimeRepository : IRepository<AttendFingerPrintDataInTimeModel> { }
    /// <summary>
    ///实时刷卡数据持久化层
    /// </summary>
    public class AttendFingerPrintDataInTimeRepository : HrmRepositoryBase<AttendFingerPrintDataInTimeModel>, IAttendFingerPrintDataInTimeRepository
    { }

    /// <summary>
    ///当月刷卡数据持久化
    /// </summary>
    public interface IAttendSlodFingerDataCurrentMonthRepository : IRepository<AttendSlodFingerDataCurrentMonthModel> 
    {
        List<AttendanceDataModel> LoadAttendDataOfToday(string department);
    }
    /// <summary>
    ///当月刷卡数据持久化
    /// </summary>
    public class AttendSlodFingerDataCurrentMonthRepository : HrmRepositoryBase<AttendSlodFingerDataCurrentMonthModel>, IAttendSlodFingerDataCurrentMonthRepository
    {
        /// <summary>
        /// 载入当天考勤数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<AttendanceDataModel> LoadAttendDataOfToday(string department)
        {
            string sqlText = string.Format("SELECT WorkerId, WorkerName, Department, ClassType, AttendanceDate, CardID, CardType,WeekDay,SlotCardTime1,SlotCardTime2,SlotCardTime from Attendance_SlodFingerDataCurrentMonth where Department='{0}'  And AttendanceDate='{1}'", department,DateTime.Now.ToDate());
            return DbHelper.Hrm.LoadEntities<AttendanceDataModel>(sqlText); 
        }
    }
}
