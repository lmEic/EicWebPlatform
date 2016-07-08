using Lm.Eic.Framework.Authenticate.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.Framework.Authenticate.Repository.Mapping
{
    public class RegistUserModelMapping : EntityTypeConfiguration<RegistUserModel>
    {
        public RegistUserModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_User");
        }
    }

    public class RoleModelMapping : EntityTypeConfiguration<RoleModel>
    {
        public RoleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_Role");
        }
    }

    public class AssemblyModelMapping : EntityTypeConfiguration<AssemblyModel>
    {
        public AssemblyModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_Assembly");
        }
    }

    public class UserMatchRoleModelMapping : EntityTypeConfiguration<UserMatchRoleModel>
    {
        public UserMatchRoleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_UserMatchRole");
        }
    }

    public class RoleMatchModuleModelMapping : EntityTypeConfiguration<RoleMatchModuleModel>
    {
        public RoleMatchModuleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_RoleMatchModules");
        }
    }

    public class ModuleNavigationModelMapping : EntityTypeConfiguration<ModuleNavigationModel>
    {
        public ModuleNavigationModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Authen_ModuleNavigation");
        }
    }
}