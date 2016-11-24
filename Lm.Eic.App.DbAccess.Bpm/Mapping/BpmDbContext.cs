using Lm.Eic.App.DbAccess.Bpm.Mapping.AstMapping;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.BoardManagment;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.DailyReport;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PurchaseMapping;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
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

        public DbSet<EquipmentDiscardRecordModel> EquipmentDiscard { get; set; }

        public DbSet<EquipmentRepairedRecordModel> EquipmentRepaired { get; set; }

        public DbSet<DailyReportModel> DailyReports { get; set; }

        public DbSet<DailyReportTempModel> DailyReportsTemp { get; set; }

        public DbSet<MachineModel> Machine { get; set; }

        public DbSet<NonProductionReasonModel> NonProduction { get; set; }

        public DbSet<ProductFlowModel> ProductFlow { get; set; }

        public DbSet<MaterialSpecBoardModel> MaterialSpecBoard { get; set; }



        public DbSet<DReportsOrderModel> DReportOrder { get; set; }
        //合格供应商清册
        public DbSet<EligibleSuppliersModel> QualifiedSupplier { set; get; }
        //供应商证书信息
        public DbSet<SuppliersQualifiedCertificateModel> SupplierEligible { set; get; }
        //供应商信息
        public DbSet<SuppliersInfoModel> SupplierInfo { set; get; }
        //季度考核总览表
        public DbSet<SupplieSeasonAuditModel> SupplieSeasonAudit { set; get; }
       
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EquipmentModelMapping());
            modelBuilder.Configurations.Add(new EquipmentCheckModelMapping());
            modelBuilder.Configurations.Add(new EquipmentMaintenanceModelMapping());
            modelBuilder.Configurations.Add(new EquipmentDiscardRecordModelMapping());
            modelBuilder.Configurations.Add(new EquipmentRepairedRecordModelMapping());

            modelBuilder.Configurations.Add(new DailyReportModelMapping());
            modelBuilder.Configurations.Add(new DailyReportTempModelMapping());
            modelBuilder.Configurations.Add(new ProductFlowModelMapping());
            modelBuilder.Configurations.Add(new MachineModelMapping());
            modelBuilder.Configurations.Add(new NonProductionModelMapping());

            modelBuilder.Configurations.Add(new MaterialSpecBoardModelMapping());

            modelBuilder.Configurations.Add(new DReportsOrderModelMapping());
            
            //合格供应商清册
            modelBuilder.Configurations.Add(new EligibleSuppliersModelMapping());
            //供应商证书信息
            modelBuilder.Configurations.Add(new SuppliersQualifiedCertificateMapping());
            //供应商信息
            modelBuilder.Configurations.Add(new SuppliersInfoMapping());
            //季度考核总览表
            modelBuilder.Configurations.Add(new SuppliersSeasonAuditMapping());
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