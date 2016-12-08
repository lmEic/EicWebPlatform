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
    ///供应商季度审计总览表
    /// Pur_SupplieSeasonAuditTable_Mapping
    /// </summary>
    public class SuppliersSeasonAuditMapping : EntityTypeConfiguration<SupplierSeasonAuditModel>
  {
      public SuppliersSeasonAuditMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SuppliersSeasonAuditTable");
      }
  }

    /// <summary>
    /// 供应商季度审计实地辅导计划/执行表
    /// </summary>

    public class SuppliersSeasonAuditTutorMapping : EntityTypeConfiguration<SupplierSeasonAuditTutorModel>
    {
                                                                   
         
        public SuppliersSeasonAuditTutorMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SupplierSeasonToturInfo");
        }
   
    }
}
