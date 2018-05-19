using Lm.Eic.App.DomainModel.Bpm.Dev;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.DevMapping
{

    /// <summary>
    ///DesignDevelopInputModel
    /// </summary>
    public class DesignDevelopInputModelMapping : EntityTypeConfiguration<DesignDevelopInputModel>
    {
        public DesignDevelopInputModelMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Dev_DesignDevelopInput");
        }
    }
}
