using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

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
    public interface IAttendFingerPrintDataInTimeRepository : IRepository<AttendFingerPrintDataInTimeModel> {
        int backupData(AttendFingerPrintDataInTimeModel entity);

        int StoreNoIdentityWorkerInfo(AttendFingerPrintDataInTimeModel entity);

    }

    /// <summary>
    ///实时刷卡数据持久化层
    /// </summary>
    public class AttendFingerPrintDataInTimeRepository : HrmRepositoryBase<AttendFingerPrintDataInTimeModel>, IAttendFingerPrintDataInTimeRepository
    {
        public int backupData(AttendFingerPrintDataInTimeModel entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Attendance_FingerPrintDataInTimeLib  (WorkerId,WorkerName,CardID,CardType,SlodCardTime,SlodCardDate)");
            sb.AppendFormat(" values ('{0}',",entity.WorkerId);
            sb.AppendFormat("'{0}',", entity.WorkerName);
            sb.AppendFormat("'{0}',", entity.CardID);
            sb.AppendFormat("'{0}',", entity.CardType);
            sb.AppendFormat("'{0}',", entity.SlodCardTime);
            sb.AppendFormat("'{0}')", entity.SlodCardDate);
            return DbHelper.Hrm.ExecuteNonQuery(sb.ToString());
        }

        /// <summary>
        /// 存储没有档案信息人员数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int StoreNoIdentityWorkerInfo(AttendFingerPrintDataInTimeModel entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO Archives_ForgetInputWorkerInfo  (WorkerId,WorkerName)");
            sb.AppendFormat(" values ('{0}',", entity.WorkerId);
            sb.AppendFormat("'{0}')", entity.WorkerName);
            return DbHelper.Hrm.ExecuteNonQuery(sb.ToString());
        }
    }

    /// <summary>
    ///当月刷卡数据持久化
    /// </summary>
    public interface IAttendSlodFingerDataCurrentMonthRepository : IRepository<AttendSlodFingerDataCurrentMonthModel>
    {
        List<AttendanceDataModel> LoadAttendDataOfToday(DateTime qryDate);
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
        public List<AttendanceDataModel> LoadAttendDataOfToday(DateTime qryDate)
        {
            string sqlText = string.Format("SELECT WorkerId, WorkerName, Department, ClassType, AttendanceDate, CardID, CardType,WeekDay,SlotCardTime1,SlotCardTime2,SlotCardTime from Attendance_SlodFingerDataCurrentMonth where AttendanceDate='{0}'",qryDate);
            return DbHelper.Hrm.LoadEntities<AttendanceDataModel>(sqlText);
        }
    }
}