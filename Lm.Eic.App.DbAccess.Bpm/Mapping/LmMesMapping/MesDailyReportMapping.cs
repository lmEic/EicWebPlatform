using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
namespace Lm.Eic.App.DbAccess.Bpm.Mapping.LmMapping
{
    public class ProductCompleteInputDataMapping : EntityTypeConfiguration<WipProductCompleteInputDataModel>
    {

        public ProductCompleteInputDataMapping()
        {
           
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Wip_ProductCompleteInputData");
        }
    }
}
