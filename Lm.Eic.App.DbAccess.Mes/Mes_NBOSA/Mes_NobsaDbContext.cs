using Lm.Eic.App.DbAccess.Mes.Mes_NBOSA.Mapping;
using Lm.Eic.App.DbAccess.Mes.Optical.Mapping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;


namespace Lm.Eic.App.DbAccess.Mes.Mes_NBOSA
{
    /// <summary>
    /// 光主动数据库
    /// </summary>
    public class OpticalMes_NBOSA_DbContext : DbContext
    {
        public OpticalMes_NBOSA_DbContext(string databaseName = "OpticalMes_Nobsa_DbContext")
        {
            Database.SetInitializer<OpticalMes_NBOSA_DbContext>(null);
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
            modelBuilder.Configurations.Add(new HosingSn_ContrllerMapping());
        }

        #endregion OnModelCreating
    }


    /// <summary>
    /// 光主动数据库工作单元
    /// </summary>
    public class OpticalMesUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new OpticalMes_NBOSA_DbContext();
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
    public class OpticalMes_NBOSA_RepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
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
}
