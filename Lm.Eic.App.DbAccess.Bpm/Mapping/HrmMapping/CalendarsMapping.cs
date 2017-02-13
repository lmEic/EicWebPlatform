using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{
    public class CalendarsMapping : EntityTypeConfiguration<CalendarModel>
    {
        public CalendarsMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_CalenderList");
        }
    }
}
