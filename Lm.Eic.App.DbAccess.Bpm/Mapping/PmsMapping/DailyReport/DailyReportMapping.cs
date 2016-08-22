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
    ///DailyReportModel
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
    ///DailyReportsTemplateModel
    /// </summary>
    public class DailyReportsTemplateModelMapping : EntityTypeConfiguration<DailyReportTemplateModel>
    {
        public DailyReportsTemplateModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_DailyReportsTemplate");
        }
    }

    /// <summary>
    ///ProductFlowModel
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
