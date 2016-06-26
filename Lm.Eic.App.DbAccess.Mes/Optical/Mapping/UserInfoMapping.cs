using Lm.Eic.App.DomainModel.Mes.Optical.Authen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Mes.Optical.Mapping
{
    /// <summary>
    ///UserInfo
    /// </summary>
    public class UserInfoMapping : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMapping()
        {
            this.HasKey(t => new { t.UserID, t.UserName });
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("User_Info");
        }
    }
}
