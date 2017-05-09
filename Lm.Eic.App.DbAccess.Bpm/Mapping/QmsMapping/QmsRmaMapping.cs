using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QmsMapping
{


    public class RmaReportInitiateMapping : EntityTypeConfiguration<RmaReportInitiateModel>
    {
        public RmaReportInitiateMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_RmaReportInitiate");
        }
    }
    public class RmaBussesDescriptionMapping : EntityTypeConfiguration<RmaBussesDescriptionModel>
    {
        public RmaBussesDescriptionMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_RmaBussesDescription");
        }
    }
    public class RmaInspectionManageMapping : EntityTypeConfiguration<RmaInspectionManageModel>
    {
        public RmaInspectionManageMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_RmaInspectionManage");
        }
    }
}
