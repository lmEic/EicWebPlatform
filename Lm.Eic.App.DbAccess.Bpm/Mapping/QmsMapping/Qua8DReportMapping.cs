using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QmsMapping
{


    /// <summary>
    ///EightReportMaster
    /// </summary>
    public class Qua8DReportMasterMapping : EntityTypeConfiguration<Qua8DReportMasterModel>
    {
        public Qua8DReportMasterMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_8DReportMaster");
        }
    }

    /// <summary>
    ///EightDReportDetail
    /// </summary>
    public class Qua8DReportDetailMapping : EntityTypeConfiguration<Qua8DReportDetailModel>
    {
        public Qua8DReportDetailMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_8DReportDetail");
        }
    }
}
