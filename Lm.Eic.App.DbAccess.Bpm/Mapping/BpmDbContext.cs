using Lm.Eic.App.DbAccess.Bpm.Mapping.AstMapping;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.BoardManagment;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.DailyReport;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PurchaseMapping;
using Lm.Eic.App.DbAccess.Bpm.Mapping.WorkFlowMapping;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Data.Entity;
using Lm.Eic.App.DbAccess.Bpm.Mapping.QmsMapping;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.LeaveAsk;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping
{

    /// <summary>
    /// 管理数据库
    /// </summary>
    public class BpmDbContext : DbContext
    {
        public BpmDbContext(string databaseName = "BpmDbContext")
        {
            Database.SetInitializer<BpmDbContext>(null);
        }


        #region 设备管理
        public DbSet<EquipmentModel> Equipment { get; set; }
        public DbSet<EquipmentCheckRecordModel> EquipmentCheck { get; set; }

        public DbSet<EquipmentMaintenanceRecordModel> EquipmentMaintenance { get; set; }

        public DbSet<EquipmentDiscardRecordModel> EquipmentDiscard { get; set; }

        public DbSet<EquipmentRepairedRecordModel> EquipmentRepaired { get; set; }
        #endregion


        #region 日报管理
        //public DbSet<DailyReportModel> DailyReports { get; set; }
        //public DbSet<DailyReportTempModel> DailyReportsTemp { get; set; }
        //public DbSet<MachineModel> Machine { get; set; }
        //public DbSet<NonProductionReasonModel> NonProduction { get; set; }
        //public DbSet<DReportsOrderModel> DReportOrder { get; set; }
        //public DbSet<ProductFlowModel> ProductFlow { get; set; }
        //public DbSet<ReportsAttendenceModel> ReportsAttendence { get; set; }

        public DbSet<ProductOrderDispatchModel> ProductOrderDispatch { get; set; }
        public DbSet<StandardProductionFlowModel> StandardProductionFlow { get; set; }
        public DbSet<DailyProductionReportModel> DailyProductionRepor { get; set; }


        #endregion


        #region  物料看板
        public DbSet<MaterialSpecBoardModel> MaterialSpecBoard { get; set; }

        #endregion


        #region  供应商管理

        //供应商信息
        public DbSet<SupplierInfoModel> SupplierInfo { set; get; }
        //供应商证书信息
        public DbSet<SupplierQualifiedCertificateModel> SupplierEligible { set; get; }

        //季度考核总览表
        public DbSet<SupplierSeasonAuditModel> SupplieSeasonAudit { set; get; }
        //季度考核 实地辅导计划/执行表
        public DbSet<SupplierSeasonTutorModel> SupplierSeasonAuditTutor { set; get; }
        //供应商自评复评明细表
        public DbSet<SupplierGradeInfoModel> SupplierGradeInfo { set; get; }




        #endregion


        #region  质理管理
        public DbSet<InspectionModeConfigModel> InspectionModeConfig { set; get; }
        public DbSet<InspectionModeSwitchConfigModel> InspectionModeSwithConfig { set; get; }

        #region Iqc
        public DbSet<InspectionIqcItemConfigModel> IqcInspectionItemConfig { set; get; }
        public DbSet<InspectionIqcMasterModel> IqcInspectionMaster { set; get; }
        public DbSet<InspectionIqcDetailModel> IqcInspectionDetail { set; get; }

        #endregion

        #region  Fqc
        public DbSet<InspectionFqcItemConfigModel> FqcInspectionItemConfig { set; get; }
        public DbSet<InspectionFqcMasterModel> FqcInspectionMaster { set; get; }
        public DbSet<InspectionFqcDetailModel> FqcInspectionDetail { set; get; }

        #endregion

        #region  RMA

        public DbSet<RmaBusinessDescriptionModel> RmaBussesDescription { set; get; }
        public DbSet<RmaInspectionManageModel> RmaInspectionManage { set; get; }
        public DbSet<RmaReportInitiateModel> RmaReportInitiate { set; get; }
        #endregion
        #endregion

        #region 电子签核管理
        public DbSet<InternalContactFormModel> InternalContactForm { get; set; }
        public DbSet<WfFormCheckFlowModel> FormCheckFlow { get; set; }
        #endregion

        #region 请假管理
        public DbSet<LeaveAskManagerModels> LeaveAsk { get; set; }
      
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region 设备管理
            modelBuilder.Configurations.Add(new EquipmentModelMapping());
            modelBuilder.Configurations.Add(new EquipmentCheckModelMapping());
            modelBuilder.Configurations.Add(new EquipmentMaintenanceModelMapping());
            modelBuilder.Configurations.Add(new EquipmentDiscardRecordModelMapping());
            modelBuilder.Configurations.Add(new EquipmentRepairedRecordModelMapping());

            #endregion

            #region 日报管理
            ////日报表
            //modelBuilder.Configurations.Add(new DailyReportModelMapping());
            ////临时日报表
            //modelBuilder.Configurations.Add(new DailyReportTempModelMapping());
            ////工艺流程表
            //modelBuilder.Configurations.Add(new ProductFlowModelMapping());
            ////机台信息
            //modelBuilder.Configurations.Add(new MachineModelMapping());
            ////非生产工时表
            //modelBuilder.Configurations.Add(new NonProductionModelMapping());
            ////非生产工时工单表
            //modelBuilder.Configurations.Add(new DReportsOrderModelMapping());
            ////出勤人员表
            //modelBuilder.Configurations.Add(new ReportsAttendenceModelMapping());
            #endregion

            #region 新日报管理
            //工艺流程表
            modelBuilder.Configurations.Add(new StandardProductionFlowMapping());
            //生产订单分配
            modelBuilder.Configurations.Add(new ProductOrderDispatchMapping());
            //每天生产日报表
            modelBuilder.Configurations.Add(new DailyProductionReportMapping());
            //非生产代码
            modelBuilder.Configurations.Add(new ProductionCodeConfigMapping());
            //不良制程处理单
            modelBuilder.Configurations.Add(new DailyProductionDefectiveTreatmentModelMapping());

            #endregion

            #region  物料看板
            modelBuilder.Configurations.Add(new MaterialSpecBoardModelMapping());
            #endregion

            #region  供应商管理
            //供应商信息
            modelBuilder.Configurations.Add(new SupplierInfoMapping());
            //供应商证书信息
            modelBuilder.Configurations.Add(new SupplierQualifiedCertificateMapping());
            //季度考核总览表
            modelBuilder.Configurations.Add(new SupplierSeasonAuditMapping());
            //季度考核 实地辅导计划/执行表
            modelBuilder.Configurations.Add(new SupplierSeasonTutorMapping());
            //供应商自评复评明细表
            modelBuilder.Configurations.Add(new SupplierGradeInfoMapping());
            #endregion

            #region 质量管理


            modelBuilder.Configurations.Add(new InspectionModeConfigMapping());
            modelBuilder.Configurations.Add(new InspectionModeSwitchConfigMapping());
            //IQC
            modelBuilder.Configurations.Add(new IqcInspectionItemConfigMapping());
            modelBuilder.Configurations.Add(new IqcInspectionMasterMapping());
            modelBuilder.Configurations.Add(new IqcInspectionDetailMapping());
            //FQC
            modelBuilder.Configurations.Add(new FqcInspectionItemConfigMapping());
            modelBuilder.Configurations.Add(new FqcInspectionMasterMapping());
            modelBuilder.Configurations.Add(new FqcInspectionDetailMapping());
            // ORT
            modelBuilder.Configurations.Add(new OrtMaterialConfigMapping());
            // RMA
            modelBuilder.Configurations.Add(new RmaBussesDescriptionMapping());
            modelBuilder.Configurations.Add(new RmaInspectionManageMapping());
            modelBuilder.Configurations.Add(new RmaReportInitiateMapping());
            // 8D
            modelBuilder.Configurations.Add(new Qua8DReportDetailMapping());
            modelBuilder.Configurations.Add(new Qua8DReportMasterMapping());
            #endregion

            #region 电子签核管理
            modelBuilder.Configurations.Add(new InternalContactFormModelMapping());
            modelBuilder.Configurations.Add(new WfFormCheckFlowModelMapping());
            #endregion

            #region 请假管理
            modelBuilder.Configurations.Add(new LeaveAskMapping());
            #endregion
        }
    }

    /// <summary>
    /// 管理中心数据库工作单元
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