using Lm.Eic.App.DomainModel.Mes.Mes_Nbosa;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.App.DbAccess.Mes.Mes_NBOSA.Mapping
{
 
    public class HosingSn_ContrllerMapping : EntityTypeConfiguration<HousingSN_ControllerModel>
    {
        public HosingSn_ContrllerMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("HousingSN_Controller");
        }
    }

}
