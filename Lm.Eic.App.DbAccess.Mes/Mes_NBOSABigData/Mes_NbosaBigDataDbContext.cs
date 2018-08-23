
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;


namespace Lm.Eic.App.DbAccess.Mes.Mes_NBOSABigData
{
    /// <summary>
    /// 光主动数据库
    /// </summary>
    public class OpticalMes_NBOSABigData_DbContext : DbContext
    {
        public OpticalMes_NBOSABigData_DbContext(string databaseName = "OpticalMes_NBOSABigData_DbContext")
        {
            Database.SetInitializer<OpticalMes_NBOSABigData_DbContext>(null);
        }

        #region DbSet
        /// <summary>
        /// WIP正常站别设置模型
        /// </summary>
        public DbSet<WipNormalFlowStationSetModel> WipNormalFlowStationSets { get; set; }

        #endregion DbSet

        #region OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Configurations.Add(new HosingSn_ContrllerMapping());
        }

        #endregion OnModelCreating
    }

    #region Repository Framework

    /// <summary>
    /// 光主动数据库工作单元
    /// </summary>
    public class OpticalMesUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new OpticalMes_NBOSABigData_DbContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    /// <summary>
    /// 光主动数据库持久化基类/父类/超类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OpticalMes_NBOSABigData_RepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
         where TEntity : class, new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new OpticalMesUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    #endregion Repository Framework
}
