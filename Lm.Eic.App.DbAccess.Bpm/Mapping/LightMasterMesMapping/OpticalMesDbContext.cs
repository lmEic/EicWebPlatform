
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.LightMaterMapping
{
    public class LightMasterMesDbContext : DbContext
    {

        public LightMasterMesDbContext(string databaseName = "OpticalMesDbContext")
        {
            Database.SetInitializer<LightMasterMesDbContext>(null);
        }

        #region dbset

        /// <summary>
        /// WIP完工录入流程卡模型数据集
        /// </summary>
        public DbSet<WipProductCompleteInputDataModel> WipProductCompleteInputData { get; set; }

        

        #endregion dbset

        protected override void OnModelCreating (DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProductCompleteInputDataMapping());


        }
    }

    #region Repository Framework

    /// <summary>
    ///
    /// </summary>
    public class LightMasterMesUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new LightMasterMesDbContext();
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
    public class LightMasterMesRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : class, new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new LightMasterMesUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }

    #endregion Repository Framework
}
