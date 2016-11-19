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
    }
    public class ArWorkerLeaveOfficeRepository : HrmRepositoryBase<ArLeaveOfficeModel>, IArWorkerLeaveOfficeRepository
    {
        public int ChangeWorkingStatus(string workingStatus, string workerId)
        {
            string sqlText = string.Format("UPDATE  Archives_EmployeeIdentityInfo SET  WorkingStatus ='{0}' WHERE   (WorkerId = '{1}')", workingStatus, workerId);
            return DbHelper.Hrm.ExecuteNonQuery(sqlText);
        }
    }

    public interface IArWorkerIdChangedRepository:IRepository <WorkerChangedModel>
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
                string wheresqlText = string.Format(" WHERE (workerid='{0}')", oldWorkerId);

                string setSqlText = string.Format(" set (workerid='{0}') ", newWorkreId);
                List<string> updateTable = new List<string>()
                {
                    "Archives_Study",
                    "Archives_TelInfo",
                    "Archives_WorkerInfo",
                    "Attendance_CardSet",
                    "Attendance_ClassType",
                    "Archives_EmployeeIdentityInfo",
                    "Archives_IdentitySumerize"
                };
                updateTable.ForEach(e =>
                {

                    updateInt += DbHelper.Hrm.ExecuteNonQuery("Upadte " + e + setSqlText + wheresqlText);
                });

                return updateInt;

            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}