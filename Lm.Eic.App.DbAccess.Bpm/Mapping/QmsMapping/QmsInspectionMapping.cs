using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

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
    public class FqcInspectionItemConfigMapping : EntityTypeConfiguration<InspectionFqcItemConfigModel>
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
    public class OrtMaterialConfigMapping : EntityTypeConfiguration<MaterialOrtConfigModel>
    {
        public OrtMaterialConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_ORT_MaterialConfig");

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


    #region Ipqc
    public class IpqcInspectionDetailMapping : EntityTypeConfiguration<InspectionIpqcDetailModel>
    {
        public IpqcInspectionDetailMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IpqcInspectionDetail");
        }

    }
    public class IpqcConfigMapping : EntityTypeConfiguration<InspectionIpqcConfigModel>
    {
        public IpqcConfigMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IpqcInspectionItemConfig");
        }

    }
    public class IpqcInspectionReportMapping : EntityTypeConfiguration<InspectionIpqcReportModel>
    {
        public IpqcInspectionReportMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_IpqcDailyReportOrderInfo");
        }

    }
    #endregion
}
