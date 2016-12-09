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
  public class SupplierQualifiedCertificateMapping : EntityTypeConfiguration<SupplierQualifiedCertificateModel>
    {
       public SupplierQualifiedCertificateMapping()
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
  public class SupplierInfoMapping : EntityTypeConfiguration<SupplierInfoModel>
  {
      public SupplierInfoMapping()
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
    public class SupplierSeasonAuditMapping : EntityTypeConfiguration<SupplierSeasonAuditModel>
  {
      public SupplierSeasonAuditMapping()
      {
          this.HasKey(t => t.Id_key);
          this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          this.ToTable("Pur_SuppliersSeasonAuditInfo");
      }
  }

    /// <summary>
    /// 供应商季度审计实地辅导计划/执行表
    /// </summary>

    public class SupplierSeasonAuditTutorMapping : EntityTypeConfiguration<SupplierSeasonAuditTutorModel>
    {
        public SupplierSeasonAuditTutorMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SupplierSeasonToturInfo");
        }
    }

    /// <summary>
    /// 供应商自评复评明细表 
    /// </summary>
    public class SupplierGradeInfoMapping : EntityTypeConfiguration<SupplierGradeInfoModel>
    {
        public SupplierGradeInfoMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SupplierGradeInfo");
        }
    }
}
