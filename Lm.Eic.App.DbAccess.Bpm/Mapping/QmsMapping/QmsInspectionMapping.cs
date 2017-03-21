using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using  Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QmsMapping
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionModeConfigMapping : EntityTypeConfiguration<InspectionModeConfigModel>
    {
        public InspectionModeConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_InspectionModeConfig");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InspectionModeSwitchConfigMapping : EntityTypeConfiguration<InspectionModeSwitchConfigModel>
    {
        public InspectionModeSwitchConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_InspectionModeSwitchConfig");
        }
    }

    #region  IQC
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionItemConfigMapping : EntityTypeConfiguration<InspectionIqcItemConfigModel>
    {
        public IqcInspectionItemConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionItemConfig");
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionMasterMapping : EntityTypeConfiguration<InspectionIqcMasterModel>
    {
        public IqcInspectionMasterMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionMaster");
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionDetailMapping : EntityTypeConfiguration<InspectionIqcDetailModel>
    {
        public IqcInspectionDetailMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionDetail");
        }

    }
    #endregion



    #region  FQC
    public class FqcInspectionItemConfigMapping :  EntityTypeConfiguration<InspectionFqcItemConfigModel>
    {
        public FqcInspectionItemConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_FqcInspectionItemConfig");
        }
    }

    public class FqcInspectionDetailMapping : EntityTypeConfiguration<InspectionFqcDetailModel>
    {
        public FqcInspectionDetailMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_FqcInspectionDetail");
        }

    }


    public class FqcInspectionMasterMapping : EntityTypeConfiguration<InspectionFqcMasterModel>
    {
        public FqcInspectionMasterMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_FqcInspectionMaster");
        }

    }
    #endregion
}
