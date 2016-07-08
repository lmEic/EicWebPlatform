using Lm.Eic.App.DomainModel.Mes.Optical.Authen;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

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