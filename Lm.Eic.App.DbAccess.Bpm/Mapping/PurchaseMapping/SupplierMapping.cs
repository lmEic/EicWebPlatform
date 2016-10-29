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
  ///QualifiedSupplierModelMapping   
  /// </summary>
  public class QualifiedSupplierModelMapping : EntityTypeConfiguration<QualifiedSupplierModel>
{
    public QualifiedSupplierModelMapping()
    { 
        this.HasKey(t => t.Id_key);
        this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        this.ToTable("Pur_QualifiedSupplier");
    }
}
 
    /// <summary>
  /// SupplierEligibleMapping
  /// </summary>
  public class SupplierEligibleMapping : EntityTypeConfiguration<SupplierEligibleModel>
    {
       public SupplierEligibleMapping()
        {
            this.HasKey(t => t.Id_key );
            this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pur_SupplierEligible");
        }
    }
}
