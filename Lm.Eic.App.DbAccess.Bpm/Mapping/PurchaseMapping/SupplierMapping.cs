using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lm.Eic.App.DomainModel.Bpm.Purchase;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PurchaseMapping
{
    /// <summary>
    /// 符合条件的供应商清单表
    /// Pur_EligibleSupplierTable
    /// </summary>
    public class EligibleSuppliersModelMapping : EntityTypeConfiguration<EligibleSuppliersModel>
{
    public EligibleSuppliersModelMapping()
    { 
        this.HasKey(t => t.Id_key);
        this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        this.ToTable("Pur_EligibleSuppliersTable");
    }
}
 
   /// <summary>
  ///供应商合格证书
  /// Pur_SupplierEligibleCertificate_Mapping
  /// </summary>
  public class SuppliersQualifiedCertificateMapping : EntityTypeConfiguration<SuppliersQualifiedCertificateModel>
    {
       public SuppliersQualifiedCertificateMapping()
        {
            this.HasKey(t => t.Id_key );
            this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SuppliersQualifiedCertificate");
        }
    }

  /// <summary>
  /// 供应商信息
  /// Pur_SuppliersInfo_Mapping
  /// </summary>
  public class SuppliersInfoMapping : EntityTypeConfiguration<SuppliersInfoModel>
  {
      public SuppliersInfoMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SuppliersInfo");
      }
  }


  /// <summary>
  ///
  /// Pur_SupplieSeasonAuditTable_Mapping
  /// </summary>
  public class SuppliersSeasonAuditMapping : EntityTypeConfiguration<SupplieSeasonAuditModel>
  {
      public SuppliersSeasonAuditMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SuppliersSeasonAuditTable");
      }
  }
}
