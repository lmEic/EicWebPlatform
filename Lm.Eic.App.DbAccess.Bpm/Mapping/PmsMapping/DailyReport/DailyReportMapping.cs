using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.DailyReport
{
    /// <summary>
    /// 生产日报表
    ///DailyReportModelMapping
    /// </summary>
    public class DailyReportModelMapping : EntityTypeConfiguration<DailyReportModel>
    {
        public DailyReportModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyReports");
        }
    }

    /// <summary>
    /// 生产日报临时表
    ///DailyReportTempModelMapping
    /// </summary>
    public class DailyReportTempModelMapping : EntityTypeConfiguration<DailyReportTempModel>
    {
        public DailyReportTempModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyReportsTemp");
        }
    }

    /// <summary>
    /// 生产工艺流程
    ///ProductFlowModelMapping
    /// </summary>
    public class ProductFlowModelMapping : EntityTypeConfiguration<ProductFlowModel>
    {
        public ProductFlowModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DReportsProductFlow");
        }
    }

    /// <summary>
    /// 机器资料
    ///MachineModel
    /// </summary>
    public class MachineModelMapping : EntityTypeConfiguration<MachineModel>
    {
        public MachineModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DReportsMachine");
        }
    }

    /// <summary>
    /// 非生产定义表
    ///NonProductionModel
    /// </summary>
    public class NonProductionModelMapping : EntityTypeConfiguration<NonProductionReasonModel>
    {
        public NonProductionModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DReportsNonProduction");
        }
    }


    /// <summary>
    /// 物料工单（针对非生产工单所做的表单）
    ///DReportsOrderModel
    /// </summary>
    public class DReportsOrderModelMapping : EntityTypeConfiguration<DReportsOrderModel>
    {
        public DReportsOrderModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DReportsOrder");
        }
    }
    /// <summary>
    /// 日报表出勤统计
    /// </summary>

    public class ReportsAttendenceModelMapping : EntityTypeConfiguration<ReportsAttendenceModel>
    {
        public ReportsAttendenceModelMapping()
        {
            this.HasKey(t => t.Id_key );
            this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DReportsAttendence");
        }
    }


}
