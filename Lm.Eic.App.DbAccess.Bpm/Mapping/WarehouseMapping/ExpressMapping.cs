using Lm.Eic.App.DomainModel.Bpm.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.WarehouseMapping
{
   
    /// <summary>
    ///ExpressModel
    /// </summary>
    public class ExpressModelMapping : EntityTypeConfiguration<ExpressModel>
    {
        public ExpressModelMapping()
        { 
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Wms_GoodsDelivery");
        }
    }

}
