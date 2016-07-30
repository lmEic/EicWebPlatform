using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Mapping
{
    public class LmProMasterDbContext : DbContext
    {
        public LmProMasterDbContext(string databaseName = "LmProMasterDbContext")
        {
            Database.SetInitializer<LmProMasterDbContext>(null);
        }

        public DbSet<ConfigDataDictionaryModel> ConfigDataDictionary { get; set; }

        public DbSet<ConfigFilePathModel> ConfigFilePathInfo { get; set; }

        public DbSet<ItilDevelopModuleManageModel> ItilDevelopModuleManage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ConfigDataDictionaryModelMapping());
            modelBuilder.Configurations.Add(new ConfigFilePathModelMapping());

            modelBuilder.Configurations.Add(new ItilDevelopModuleManageModelMapping());
        }
    }

    #region Repository Framework

    /// <summary>
    /// 产品主数据库工作单元
    /// </summary>
    public class LmProMasterUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new LmProMasterDbContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    /// <summary>
    /// 产品主数据库持久化基类/父类/超类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LmProMasterRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
         where TEntity : class, new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new LmProMasterUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    #endregion Repository Framework
}