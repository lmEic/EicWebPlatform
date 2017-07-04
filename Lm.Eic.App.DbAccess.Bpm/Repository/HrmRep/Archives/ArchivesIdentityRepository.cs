using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System.Collections.Generic;
using System.Data;
using System;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives
{
    /// <summary>
    ///员工档案数据持久化
    /// </summary>
    public interface IArchivesEmployeeIdentityRepository : IRepository<ArchivesEmployeeIdentityModel>
    {
        string CreateWorkerId(string workerIdNumType);

        List<ArWorkerInfo> GetWorkerInfos(string whereAppend = "");

        List<LeaveOfficeMapEntity> GetAttendWorkers();

    }

    /// <summary>
    ///员工档案数据持久化
    /// </summary>
    public class ArchivesEmployeeIdentityRepository : HrmRepositoryBase<ArchivesEmployeeIdentityModel>, IArchivesEmployeeIdentityRepository
    {
        public string CreateWorkerId(string workerIdNumType)
        {
            string workerId = string.Empty;
            string sqlText = string.Format("SELECT TOP (1) WorkerId from Archives_EmployeeIdentityInfo where WorkerIdNumType='{0}' order By Id_Key Desc", workerIdNumType);
            DataTable dt = DbHelper.Hrm.LoadTable(sqlText);
            if (dt != null && dt.Rows.Count > 0)
            {
                workerId = dt.Rows[0][0].ToString().Trim();
                int id = int.Parse(workerId) + 1;
                workerId = id.ToString().PadLeft(6, '0');
                //判定生成工号是否在更变的工号中
                string SqlFindChangeWorkerId = string.Format("SELECT OldWorkerId FROM Archives_WorkerIdChanged WHERE (OldWorkerId = '{0}')", workerId);
                if (DbHelper.Hrm.IsExist(SqlFindChangeWorkerId))
                {
                    int newid = int.Parse(workerId) + 1;
                    workerId = newid.ToString().PadLeft(6, '0');
                }
            }
            return workerId;
        }

        public List<ArWorkerInfo> GetWorkerInfos(string whereAppend = "")
        {
            string sql = "Select IdentityID, WorkerId,Name,Post, PostNature,Organizetion, Department,ClassType,PersonalPicture from Archives_EmployeeIdentityInfo ";
            if (whereAppend != "")
                sql = sql + " where " + whereAppend;
            return DbHelper.Hrm.LoadEntities<ArWorkerInfo>(sql);
        }
        /// <summary>
        /// 获取出勤人员的信息
        /// </summary>
        /// <returns></returns>
        public List<LeaveOfficeMapEntity> GetAttendWorkers()
        {
            return DbHelper.Hrm.LoadEntities<LeaveOfficeMapEntity>("Select WorkerId,Name as WorkerName,Department,RegistedDate as LeaveDate from Archives_EmployeeIdentityInfo where WorkingStatus='在职'");
        }

    }

    /// <summary>
    ///身份证数据持久化
    /// </summary>
    public interface IArchivesIdentityRepository : IRepository<ArchivesIdentityModel> { }

    /// <summary>
    ///身份证数据持久化
    /// </summary>
    public class ArchivesIdentityRepository : HrmRepositoryBase<ArchivesIdentityModel>, IArchivesIdentityRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IArDepartmentChangeLibRepository : IRepository<ArDepartmentChangeLibModel> { }

    /// <summary>
    ///
    /// </summary>
    public class ArDepartmentChangeLibRepository : HrmRepositoryBase<ArDepartmentChangeLibModel>, IArDepartmentChangeLibRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IArPostChangeLibRepository : IRepository<ArPostChangeLibModel> { }

    /// <summary>
    ///
    /// </summary>
    public class ArPostChangeLibRepository : HrmRepositoryBase<ArPostChangeLibModel>, IArPostChangeLibRepository
    { }

    /// <summary>
    ///证书持久化
    /// </summary>
    public interface IArCertificateRepository : IRepository<ArCertificateModel> { }

    /// <summary>
    ///证书持久化
    /// </summary>
    public class ArCertificateRepository : HrmRepositoryBase<ArCertificateModel>, IArCertificateRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IArTelRepository : IRepository<ArTelModel> { }

    /// <summary>
    ///
    /// </summary>
    public class ArTelRepository : HrmRepositoryBase<ArTelModel>, IArTelRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IArStudyRepository : IRepository<ArStudyModel> { }

    /// <summary>
    ///
    /// </summary>
    public class ArStudyRepository : HrmRepositoryBase<ArStudyModel>, IArStudyRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IProWorkerInfoRepository : IRepository<ProWorkerInfo> { }

    /// <summary>
    ///
    /// </summary>
    public class ProWorkerInfoRepository : HrmRepositoryBase<ProWorkerInfo>, IProWorkerInfoRepository
    { }
    /// <summary>
    /// 离职人员
    /// </summary>
    public interface IArWorkerLeaveOfficeRepository : IRepository<ArLeaveOfficeModel>
    {
        int ChangeWorkingStatus(string workingStatus, string workerId);
        /// <summary>
        /// 载入离职人员数据，用来考勤处理用
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        List<LeaveOfficeMapEntity> GetLeavedWorkers(DateTime dtStart, DateTime dtEnd);
    }
    public class ArWorkerLeaveOfficeRepository : HrmRepositoryBase<ArLeaveOfficeModel>, IArWorkerLeaveOfficeRepository
    {
        public int ChangeWorkingStatus(string workingStatus, string workerId)
        {
            string sqlText = string.Format("UPDATE  Archives_EmployeeIdentityInfo SET  WorkingStatus ='{0}' WHERE   (WorkerId = '{1}')", workingStatus, workerId);
            return DbHelper.Hrm.ExecuteNonQuery(sqlText);
        }
        public List<LeaveOfficeMapEntity> GetLeavedWorkers(DateTime dtStart, DateTime dtEnd)
        {
            return DbHelper.Hrm.LoadEntities<LeaveOfficeMapEntity>(
                string.Format("Select WorkerId,WorkerName, Department,LeaveDate from Archives_LeaveOffice where LeaveDate>='{0}' and LeaveDate<='{1}'", dtStart, dtEnd));
        }
    }

    public interface IArWorkerIdChangedRepository : IRepository<WorkerChangedModel>
    {
        int UpdateAllTableWorkerId(string oldWorkerId, string newWorkreId);

    }
    public class ArworkerIdChangedRepository : HrmRepositoryBase<WorkerChangedModel>, IArWorkerIdChangedRepository
    {
        public int UpdateAllTableWorkerId(string oldWorkerId, string newWorkreId)
        {

            try
            {
                int updateInt = 0;
                string setSqlText = string.Format("  set workerid ='{0}' ", newWorkreId);

                string wheresqlText = string.Format(" WHERE (workerid='{0}')", oldWorkerId);
                List<string> updateTable = new List<string>()
                {
                    "Archives_Study",
                    "Archives_TelInfo",
                    "Archives_WorkerInfo",
                    "Attendance_ClassType",
                    "Archives_IdentitySumerize",
                    "Archives_Department",
                    "Archives_Employee"

                };
                updateTable.ForEach(e =>
                {

                    string UpdateSQlstring = "UPDATE  " + e + setSqlText + wheresqlText;
                    updateInt = DbHelper.Hrm.ExecuteNonQuery(UpdateSQlstring);
                });
                return updateInt;

            }
            catch (Exception)
            {

                throw;
            }

        }
    }

    /// <summary>
    ///忘记录入员工持久化模型
    /// </summary>
    public interface IArchivesForgetInputWorkerRepositoryRepository : IRepository<ArchivesForgetInputWorkerModel> { }
    /// <summary>
    ///忘记录入员工持久化模型
    /// </summary>
    public class ArchivesForgetInputWorkerRepositoryRepository : HrmRepositoryBase<ArchivesForgetInputWorkerModel>, IArchivesForgetInputWorkerRepositoryRepository
    { }
}