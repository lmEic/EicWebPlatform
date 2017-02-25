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
        public  InspectionModeConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_InspectionModeConfig");
        }
    }

    #region  IQC
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionItemConfigMapping : EntityTypeConfiguration<IqcInspectionItemConfigModel>
    {
        public  IqcInspectionItemConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionItemConfig");
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionMasterMapping : EntityTypeConfiguration<IqcInspectionMasterModel>
    {
        public IqcInspectionMasterMapping()
        {
            this.HasKey(t => t.Id_Key );
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionMaster");
        }
    }
    /// <summary>
    /// 
    /// </summary>

    public class IqcInspectionDetailMapping : EntityTypeConfiguration<IqcInspectionDetailModel>
    {
        public IqcInspectionDetailMapping()
        {
            this.HasKey(t => t.Id_Key );
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IqcInspectionDetail");
        }

    }
    #endregion
}
