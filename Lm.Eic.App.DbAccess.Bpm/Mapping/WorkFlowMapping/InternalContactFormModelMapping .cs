using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.WorkFlowMapping
{
    /// <summary>
    ///InternalContactFormModel
    /// </summary>
    public class InternalContactFormModelMapping : EntityTypeConfiguration<InternalContactFormModel>
    {
        public InternalContactFormModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Wf_InternalContactForm");
        }
    }

}
