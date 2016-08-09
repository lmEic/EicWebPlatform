using Lm.Eic.App.DbAccess.Bpm.Mapping.AstMapping;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.BoardManager;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping
{
    /// <summary>
    /// 设备管理数据库
    /// </summary>
    public class BpmDbContext : DbContext
    {
        public BpmDbContext(string databaseName = "BpmDbContext")
        {
            Database.SetInitializer<BpmDbContext>(null);
        }

        public DbSet<EquipmentModel> Equipment { get; set; }
        public DbSet<EquipmentCheckRecordModel> EquipmentCheck { get; set; }

        public DbSet<EquipmentMaintenanceRecordModel> EquipmentMaintenance { get; set; }

        public DbSet<MaterialSpecBoardModel> MaterialSpecBoard { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EquipmentModelMapping());
            modelBuilder.Configurations.Add(new EquipmentCheckModelMapping());
            modelBuilder.Configurations.Add(new EquipmentMaintenanceModelMapping());
            modelBuilder.Configurations.Add(new MaterialSpecBoardModelMapping());
        }
    }

    /// <summary>
    /// 设备管理中心数据库工作单元
    /// </summary>
    public class BpmUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new BpmDbContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    /// <summary>
    /// 数据库持久化基类/父类/超类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BpmRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity> where TEntity : class, new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new BpmUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}