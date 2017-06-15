using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

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
    ///AttendanceClassTypeDetailModel
    /// </summary>
    public class AttendClassTypeDetailModelMapping : EntityTypeConfiguration<AttendClassTypeDetailModel>
    {
        public AttendClassTypeDetailModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_ClassTypeDetail");
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

    /// <summary>
    ///AttendAskLeaveModel
    /// </summary>
    public class AttendAskLeaveModelMapping : EntityTypeConfiguration<AttendAskLeaveModel>
    {
        public AttendAskLeaveModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_AskLeaveInfo");
        }
    }
}