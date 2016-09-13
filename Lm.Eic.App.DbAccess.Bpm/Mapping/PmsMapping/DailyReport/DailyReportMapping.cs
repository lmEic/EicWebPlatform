using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.DailyReport
{
    /// <summary>
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
    ///ProductFlowModelMapping
    /// </summary>
    public class ProductFlowModelMapping : EntityTypeConfiguration<ProductFlowModel>
    {
        public ProductFlowModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_ProductFlow");
        }
    }
}
