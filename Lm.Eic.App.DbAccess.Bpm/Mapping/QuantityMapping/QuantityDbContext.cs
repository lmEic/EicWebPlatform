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
      #region 进料检验 制程检验
      /// <summary>
      /// IQC抽样记录信息
      /// </summary>
      public DbSet<SampleIqcRecordModel> IQCSampleRecord { set; get; }
      /// <summary>
      ///  IQC抽样项次打印记录信息
      /// </summary>
      public DbSet<SampleItemsIqcRecordModel> IQCSamplePrintItemRecord { set; get; }
      /// <summary>
      ///  抽样不合格记录信息
      /// </summary>
      public DbSet<ProductNgRecordModel> ProductNgRecord { set; get; }
      /// <summary>
      /// 抽样物料项目设置信息
      /// </summary>
      public DbSet<MaterialSampleItemsModel> MaterialSampleItem { set; get; }
      /// <summary>
      /// 物料抽样规则表
      /// </summary>
      public DbSet<SampleRuleTableModel> SampleRuleTable { set; get; }
      /// <summary>
      /// / 抽样放宽加严限制控制
      /// </summary>
      public DbSet<SampleWayLawModel> SampleWayLaw { set; get; }
      /// <summary>
      /// Ipqc
      /// </summary>
      public DbSet<SampleItemsIpqcDataModel> IPQC_SampleItemData { set; get; }
      #endregion
      #endregion
      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          base.OnModelCreating(modelBuilder);
          modelBuilder.Configurations.Add(new IQCSampleRecordMapping());
          modelBuilder.Configurations.Add(new IQCSampleItemsRecordMapping());
          modelBuilder.Configurations.Add(new SampleProductNgRecordMapping());
          modelBuilder.Configurations.Add(new MaterialSampleItemMapping());
          modelBuilder.Configurations.Add(new SampleRuleTableMapping());
          modelBuilder.Configurations.Add(new SampleWayLawMapping());
          modelBuilder.Configurations.Add(new SampleItemsIpqcDataMapping());

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
