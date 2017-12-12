using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.PmsMapping.LeaveAsk
{
   public class LeaveAskMapping:EntityTypeConfiguration<LeaveAskManagerModels>
    {
        public LeaveAskMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendace_ProAskLeaveManager");
        }


    }
}
