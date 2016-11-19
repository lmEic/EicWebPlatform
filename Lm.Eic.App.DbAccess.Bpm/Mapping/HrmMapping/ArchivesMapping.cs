using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{
    /// <summary>
    ///身份证实体模型映射
    /// </summary>
    public class ArchivesIdentityModelMapping : EntityTypeConfiguration<ArchivesIdentityModel>
    {
        public ArchivesIdentityModelMapping()
        {
            this.HasKey(t => t.IdentityID);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_IdentityInfo");
        }
    }

    /// <summary>
    ///ArchivesEmployeeIdentityModel
    /// </summary>
    public class ArchivesEmployeeIdentityModelMapping : EntityTypeConfiguration<ArchivesEmployeeIdentityModel>
    {
        public ArchivesEmployeeIdentityModelMapping()
        {
            this.HasKey(t => t.IdentityID).HasKey(t => t.WorkerId);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_EmployeeIdentityInfo");
        }
    }

    /// <summary>
    ///ArPostChangeLibModel
    /// </summary>
    public class ArPostChangeLibModelMapping : EntityTypeConfiguration<ArPostChangeLibModel>
    {
        public ArPostChangeLibModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_PostChangeLib");
        }
    }

    /// <summary>
    ///ArDepartmentChangeLibModel
    /// </summary>
    public class ArDepartmentChangeLibModelMapping : EntityTypeConfiguration<ArDepartmentChangeLibModel>
    {
        public ArDepartmentChangeLibModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_DepartmentChangeLib");
        }
    }

    /// <summary>
    ///ArCertificateModel
    /// </summary>
    public class ArCertificateModelMapping : EntityTypeConfiguration<ArCertificateModel>
    {
        public ArCertificateModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_Certificate");
        }
    }

    /// <summary>
    ///ArTelModel
    /// </summary>
    public class ArTelModelMapping : EntityTypeConfiguration<ArTelModel>
    {
        public ArTelModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_TelInfo");
        }
    }

    /// <summary>
    ///ArStudyModel
    /// </summary>
    public class ArStudyModelMapping : EntityTypeConfiguration<ArStudyModel>
    {
        public ArStudyModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_Study");
        }
    }

    /// <summary>
    ///ProWorkerInfo
    /// </summary>
    public class ProWorkerInfoMapping : EntityTypeConfiguration<ProWorkerInfo>
    {
        public ProWorkerInfoMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_WorkerInfo");
        }
    }

    public class ArWorkerLeaveOfficeInfoMapping: EntityTypeConfiguration<ArLeaveOfficeModel>
    {
        public ArWorkerLeaveOfficeInfoMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_LeaveOffice");
        }
    }
   
    public class ArWorkerIdChangedMapping:EntityTypeConfiguration <WorkerChangedModel>
    {
        public ArWorkerIdChangedMapping ()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Archives_WorkerIdChanged");
        }
    }
}