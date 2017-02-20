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
    ///班次数据持久化层
    /// </summary>
    public interface IAttendClassTypeDetailRepository : IRepository<AttendClassTypeDetailModel> { }
    /// <summary>
    ///班次数据持久化层
    /// </summary>
    public class AttendClassTypeDetailRepository : HrmRepositoryBase<AttendClassTypeDetailModel>, IAttendClassTypeDetailRepository
    { }

  

    /// <summary>
    ///实时刷卡数据持久化层
    /// </summary>
    public interface IAttendFingerPrintDataInTimeRepository : IRepository<AttendFingerPrintDataInTimeModel> {
        int backupData(AttendFingerPrintDataInTimeModel entity);

        int StoreNoIdentityWorkerInfo(AttendFingerPrintDataInTimeModel entity);

      
        List<AttendFingerPrintDataInTimeModel> loaddatas();

        int deleteLibData(DateTime t, string workerId);
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

        public List<AttendFingerPrintDataInTimeModel> loaddatas()
        {
            List<AttendFingerPrintDataInTimeModel> datas = DbHelper.Hrm.LoadEntities<AttendFingerPrintDataInTimeModel>("SELECT   WorkerId, WorkerName, CardID, CardType, SlodCardTime, SlodCardDate  FROM Attendance_FingerPrintDataInTimeLib");
            return datas;
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


        public int deleteLibData(DateTime t, string workerId)
        {
            return DbHelper.Hrm.ExecuteNonQuery(string.Format("delete from Attendance_FingerPrintDataInTimeLib where WorkerId='{0}' AND SlodCardTime='{1}'", workerId, t));
        }
    }

    /// <summary>
    ///当月刷卡数据持久化
    /// </summary>
    public interface IAttendSlodFingerDataCurrentMonthRepository : IRepository<AttendSlodFingerDataCurrentMonthModel>
    {
        /// <summary>
        /// searchMode:
        /// 0:按考勤日期查询
        /// 1:按考勤日期与部门查询
        /// 2:按工号查询
        /// 3:按年月份查询
        /// </summary>
        /// <param name="qryDto"></param>
        /// <returns></returns>
        List<AttendanceDataModel> LoadAttendanceDatasBy(AttendanceDataQueryDto qryDto);
        /// <summary>
        /// 修改班次信息
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="slodCardDate"></param>
        /// <returns></returns>
        int UpdateClassTypeInfo(string classType, DateTime slodCardDate);
    }

    /// <summary>
    ///当月刷卡数据持久化
    /// </summary>
    public class AttendSlodFingerDataCurrentMonthRepository : HrmRepositoryBase<AttendSlodFingerDataCurrentMonthModel>, IAttendSlodFingerDataCurrentMonthRepository
    {
        private const string loadAttendDataSql = "SELECT WorkerId, WorkerName, Department, ClassType, AttendanceDate, CardID, CardType,WeekDay,SlotCardTime1,SlotCardTime2,SlotCardTime from Attendance_SlodFingerDataCurrentMonth";
        public List<AttendanceDataModel> LoadAttendanceDatasBy(AttendanceDataQueryDto qryDto)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append(loadAttendDataSql);
            if (qryDto.SearchMode == 0)
            {
                sqlText.AppendFormat(" where AttendanceDate='{0}'", qryDto.AttendanceDate);
            }
            else if (qryDto.SearchMode == 1)
            {
                sqlText.AppendFormat(" where AttendanceDate='{0}' And Department='{1}'", qryDto.AttendanceDate,qryDto.Department);
            }
            else if (qryDto.SearchMode == 2)
            {
                sqlText.AppendFormat(" where WorkerId='{0}'", qryDto.WorkerId);
            }
            else if (qryDto.SearchMode == 3)
            {
                sqlText.AppendFormat(" where YearMonth='{0}'", qryDto.YearMonth);
            }
            sqlText.Append(" order by AttendanceDate");
            return DbHelper.Hrm.LoadEntities<AttendanceDataModel>(sqlText.ToString());
        }


        public int UpdateClassTypeInfo(string classType, DateTime slodCardDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Update Attendance_SlodFingerDataCurrentMonth set ClassType='{0}' where AttendanceDate='{1}'", classType, slodCardDate);
            return DbHelper.Hrm.ExecuteNonQuery(sb.ToString());
        }
    }
}