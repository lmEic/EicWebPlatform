using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{
    /// <summary>
    ///AttendClassTypeModel
    /// </summary>
    public class AttendClassTypeModelMapping : EntityTypeConfiguration<AttendClassTypeModel>
    {
        public AttendClassTypeModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_ClassType");
        }
    }

    /// <summary>
    ///AttendFingerPrintDataInTimeModel
    /// </summary>
    public class AttendFingerPrintDataInTimeModelMapping : EntityTypeConfiguration<AttendFingerPrintDataInTimeModel>
    {
        public AttendFingerPrintDataInTimeModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_FingerPrintDataInTime");
        }
    }
    /// <summary>
    ///AttendSlodFingerDataCurrentMonthModel
    /// </summary>
    public class AttendSlodFingerDataCurrentMonthModelMapping : EntityTypeConfiguration<AttendSlodFingerDataCurrentMonthModel>
    {
        public AttendSlodFingerDataCurrentMonthModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_SlodFingerDataCurrentMonth");
        }
    }
}
