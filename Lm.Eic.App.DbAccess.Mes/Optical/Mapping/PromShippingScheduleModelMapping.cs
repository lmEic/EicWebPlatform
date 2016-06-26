using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductShipping;
using Lm.Eic.App.DomainModel.Mes.Optical.ProductWip;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lm.Eic.App.DbAccess.Mes.Optical.Mapping
{
    public class PromShippingScheduleModelMapping:EntityTypeConfiguration<PromShippingScheduleModel>
    {
        public PromShippingScheduleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Prom_ShippingSchedule");
        }
    }

    public class ProductWipModelMapping : EntityTypeConfiguration<ProductedWipDataModel>
    {
        public ProductWipModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Wip_ProductedWipData");
        }
    }

    public class WipNormalFlowStationSetModelMapping : EntityTypeConfiguration<WipNormalFlowStationSetModel>
    {
        public WipNormalFlowStationSetModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Wip_NormalFlowStationSet");
        }
    }
}
