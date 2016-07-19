using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QuantityMapping
{
  public  class QuantityDbContext:DbContext 
    {
      public QuantityDbContext(string connectionstring = "QuantityDbContext")
      {
          Database.SetInitializer<QuantityDbContext>(null);
      }
      #region    baseSet
      /// <summary>
      /// IQC抽样记录信息
      /// </summary>
      public DbSet<IQCSampleRecordModel> IQCSampleRecord { set; get; }
      /// <summary>
      ///  IQC抽样项次打印记录信息
      /// </summary>
      public DbSet<IQCSampleItemRecordModel> IQCSamplePrintItemRecord { set; get; }
      /// <summary>
      ///  抽样不合格记录信息
      /// </summary>
      public DbSet<ProductNgRecordModel> ProductNgRecord { set; get; }
      /// <summary>
      /// 抽样物料项目设置信息
      /// </summary>
      public DbSet<MaterialSampleSetModel> MaterialSampleSet { set; get; }
      /// <summary>
      /// 物料抽样规则表
      /// </summary>
      public DbSet<SamplePlanTableModel> SamplePlanTable { set; get; }
      /// <summary>
      /// / 抽样放宽加严限制控制
      /// </summary>
      public DbSet<SampleContorlLimitModel> SampleContorlLimit { set; get; }

       #endregion
      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          base.OnModelCreating(modelBuilder);
          modelBuilder.Configurations.Add(new IQCSampleRecordMapping());
          modelBuilder.Configurations.Add(new IQCSamplePrintItemsRecordMapping());
          modelBuilder.Configurations.Add(new SampleProductNgRecordMapping());
          modelBuilder.Configurations.Add(new MaterialSampleItemMapping());
          modelBuilder.Configurations.Add(new SamplePlanMapping());
          modelBuilder.Configurations.Add(new SampleContorlLimitMapping());

      }
   
    }
  /// <summary>
  //质量验证中心数据库工作单元
  /// </summary>
  public class QuantityUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
  {
      protected override void SetDbContext()
      {
          try
          {
              this.Context = new QuantityDbContext();
          }
          catch (Exception ex)
          {
              throw new Exception(ex.ToString());
          }
      }
  }
  public class QuantityRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
   where TEntity : class,new()
  {
      protected override void SetUnitOfWorkContext()
      {
          try
          {
              this.EFContext = new QuantityUnitOfWorkContext();
          }
          catch (Exception ex)
          {
              throw new Exception(ex.ToString());
          }
      }
  } 
}
