using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{

    /// <summary>
    ///WorkClothesManageModel
    /// </summary>
    public class WorkClothesManageModelMapping : EntityTypeConfiguration<WorkClothesManageModel>
    {
        public WorkClothesManageModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("GA_WorkClothesManage");
        }
    }

    /// <summary>
    ///MealReportManageModel
    /// </summary>
    public class MealReportManageModelMapping : EntityTypeConfiguration<MealReportManageModel>
    {
        public MealReportManageModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("GA_MealReportManage");
        }
    }

}
