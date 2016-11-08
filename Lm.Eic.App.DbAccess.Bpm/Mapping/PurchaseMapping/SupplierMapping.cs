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
    ///Pur_SupplierQualifiedTable_Mapping   
  /// </summary>
  public class QualifiedSupplierModelMapping : EntityTypeConfiguration<QualifiedSupplierModel>
{
    public QualifiedSupplierModelMapping()
    { 
        this.HasKey(t => t.Id_key);
        this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        this.ToTable("Pur_SupplierQualifiedTable");
    }
}
 
    /// <summary>
  /// Pur_SupplierEligibleCertificate_Mapping
  /// </summary>
  public class SupplierEligibleMapping : EntityTypeConfiguration<SupplierEligibleCertificateModel>
    {
       public SupplierEligibleMapping()
        {
            this.HasKey(t => t.Id_key );
            this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SupplierEligibleCertificate");
        }
    }

  /// <summary>
  /// Pur_SuppliersInfo_Mapping
  /// </summary>
  public class SuppliersInfoMapping : EntityTypeConfiguration<SupplierInfoModel>
  {
      public SuppliersInfoMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SuppliersInfo");
      }
  }


  /// <summary>
  /// Pur_SupplieSeasonAuditTable_Mapping
  /// </summary>
  public class SupplieSeasonAuditMapping : EntityTypeConfiguration<SupplieSeasonAuditModel>
  {
      public SupplieSeasonAuditMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SupplieSeasonAuditTable");
      }
  }
}
