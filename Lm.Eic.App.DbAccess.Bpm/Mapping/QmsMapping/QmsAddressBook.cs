using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QmsMapping
{
    public class QmsAddressBook
    {
    }

    public class AddressBookMapping : EntityTypeConfiguration<AddressBookModel>
    {
        public AddressBookMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Qms_RmaReportInitiate");
        }
    }
}
