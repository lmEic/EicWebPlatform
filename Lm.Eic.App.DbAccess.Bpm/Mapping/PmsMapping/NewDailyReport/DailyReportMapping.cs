using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.NewDailyReport
{
    /// <summary>
    ///StandardProductionFlow 产品标准工艺生产流程
    /// </summary>
    public class StandardProductionFlowMapping : EntityTypeConfiguration<StandardProductionFlowModel>
    {
        public StandardProductionFlowMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_StandardProductionFlow");
        }
    }


    /// <summary>
    ///ReportsOrderDispatchhModel 生产订单分配
    /// </summary>
    public class ProductOrderDispatchMapping : EntityTypeConfiguration<ProductOrderDispatchModel>
    {
        public ProductOrderDispatchMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyReportsOrderDispatch");
        }
    }


    /// <summary>
    ///DailyProductionReport 每日生产报表
    /// </summary>
    public class DailyProductionReportMapping : EntityTypeConfiguration<DailyProductionReportModel>
    {
        public DailyProductionReportMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyProductionReport");
        }
    }
    /// <summary>
    ///DailyProductionReport 日报表生产编码配置
    /// </summary>
    public class ProductionCodeConfigMapping : EntityTypeConfiguration<ProductionCodeConfigModel>
    {
        public ProductionCodeConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyProductionCodeConfig");
        }
    }
}



