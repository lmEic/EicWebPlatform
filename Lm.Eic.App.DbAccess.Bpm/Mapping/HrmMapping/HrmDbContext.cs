using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{
    /// <summary>
    ///
    /// </summary>
    public class HrmDbContext : DbContext
    {
        public HrmDbContext(string databaseName = "HrmDbContext")
        {
            Database.SetInitializer<HrmDbContext>(null);
        }

        #region dbset

        /// <summary>
        /// 身份证数据集
        /// </summary>
        public DbSet<ArchivesIdentityModel> ArchiveIdentity { get; set; }

        /// <summary>
        /// 生产作业人员信息
        /// </summary>
        public DbSet<ProWorkerInfo> ProWorkerInfo { get; set; }

        /// <summary>
        /// 学习信息
        /// </summary>
        public DbSet<ArStudyModel> ArchiveStudy { get; set; }

        /// <summary>
        /// 联系方式信息
        /// </summary>
        public DbSet<ArTelModel> ArchiveTel { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        public DbSet<ArDepartmentChangeLibModel> ArchiveDepartment { get; set; }

        /// <summary>
        /// 岗位方式信息
        /// </summary>
        public DbSet<ArPostChangeLibModel> ArchivePost { get; set; }

        /// <summary>
        /// 班别设置信息
        /// </summary>
        public DbSet<AttendClassTypeDetailModel> AttendClassType { get; set; }

        /// <summary>
        /// 实时刷卡数据
        /// </summary>
        public DbSet<AttendFingerPrintDataInTimeModel> AttendFingerPrintDataInTime { get; set; }

        /// <summary>
        /// 当月刷卡数据模型
        /// </summary>
        public DbSet<AttendSlodFingerDataCurrentMonthModel> AttendSlodFingerDataCurrentMonth { get; set; }

        public DbSet<WorkClothesManageModel> WorkClothesManage { get; set; }
        /// <summary>
        /// 离职人员信息
        /// </summary>
        public DbSet<ArLeaveOfficeModel> ArWorkerLeaveOfficeInfo { get; set; }
        /// <summary>
        /// 行事历列表
        /// </summary>
        public DbSet<CalendarModel> Calendarlist { set; get; }

        #endregion dbset

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ArchivesIdentityModelMapping());
            modelBuilder.Configurations.Add(new ArCertificateModelMapping());
            modelBuilder.Configurations.Add(new ArDepartmentChangeLibModelMapping());
            modelBuilder.Configurations.Add(new ArPostChangeLibModelMapping());
            modelBuilder.Configurations.Add(new ArStudyModelMapping());
            modelBuilder.Configurations.Add(new ArTelModelMapping());
            modelBuilder.Configurations.Add(new ArchivesEmployeeIdentityModelMapping());

            modelBuilder.Configurations.Add(new ProWorkerInfoMapping());
            //离职人员信息
            modelBuilder.Configurations.Add(new ArWorkerLeaveOfficeInfoMapping());

            //考勤模块
            modelBuilder.Configurations.Add(new AttendClassTypeModelMapping());
            modelBuilder.Configurations.Add(new AttendClassTypeDetailModelMapping());
            modelBuilder.Configurations.Add(new AttendSlodFingerDataCurrentMonthModelMapping());
            modelBuilder.Configurations.Add(new AttendFingerPrintDataInTimeModelMapping());
            modelBuilder.Configurations.Add(new AttendAskLeaveModelMapping());

            //总务
            modelBuilder.Configurations.Add(new WorkClothesManageModelMapping());
            //工号变更
            modelBuilder.Configurations.Add(new ArWorkerIdChangedMapping());
            //
            modelBuilder.Configurations.Add(new CalendarsMapping());
        }
    }

    #region Repository Framework

    /// <summary>
    ///
    /// </summary>
    public class HrmUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new HrmDbContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class HrmRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : class, new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new HrmUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    #endregion Repository Framework
}