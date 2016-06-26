using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DbAccess.Mes.Optical.Mapping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using Lm.Eic.App.DomainModel.Mes.Optical.Authen;

namespace Lm.Eic.App.DbAccess.Mes.Optical
{
  /// <summary>
  /// 光主动数据库
  /// </summary>
  public class OpticalMesDbContext:DbContext
    {
      public OpticalMesDbContext(string databaseName = "OpticalMesDbContext")
      {
          Database.SetInitializer<OpticalMesDbContext>(null);
      }

      #region DbSet
      /// <summary>
      /// 用户信息
      /// </summary>
      public DbSet<UserInfo> UserInfo { get; set; }
      /// <summary>
      /// 出货排程计划模型
      /// </summary>
      public DbSet<PromShippingScheduleModel> ProShippingSchedule { get; set; }
      /// <summary>
      /// WIP生产数据模型
      /// </summary>
      public DbSet<ProductedWipDataModel> ProductWipDatas { get; set; }

      /// <summary>
      /// WIP正常站别设置模型
      /// </summary>
      public DbSet<WipNormalFlowStationSetModel> WipNormalFlowStationSets { get; set; }
      #endregion

      #region OnModelCreating
      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          base.OnModelCreating(modelBuilder);

          modelBuilder.Configurations.Add(new PromShippingScheduleModelMapping());
          modelBuilder.Configurations.Add(new ProductWipModelMapping());
          modelBuilder.Configurations.Add(new WipNormalFlowStationSetModelMapping());

          modelBuilder.Configurations.Add(new UserInfoMapping());
      }
      #endregion
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
              this.Context = new OpticalMesDbContext();
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
  public class OpticalMesRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
       where TEntity : class,new()
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
 #endregion
}
