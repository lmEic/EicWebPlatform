using Lm.Eic.App.DomainModel.Bpm.Ast;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.AstMapping
{
    public class EquipmentModelMapping : EntityTypeConfiguration<EquipmentModel>
    {
        public EquipmentModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Ast_Equipment");
        }
    }

}
