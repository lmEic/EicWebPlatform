using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.BoardManager
{

    /// <summary>
    ///MaterialSpecBoardModel
    /// </summary>
    public class MaterialSpecBoardModelMapping : EntityTypeConfiguration<MaterialSpecBoardModel>
    {
        public MaterialSpecBoardModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Pms_MaterialBoard");
        }
    }

}
