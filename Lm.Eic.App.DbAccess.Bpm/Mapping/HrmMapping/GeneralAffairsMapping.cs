using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
