using Lm.Eic.Framework.Authenticate.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.Framework.Authenticate.Repository.Mapping
{
    /// <summary>
    /// 注册的用户
    /// </summary>
    public class RegistUserModelMapping : EntityTypeConfiguration<RegistUserModel>
    {
        public RegistUserModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_User");
        }
    }
    /// <summary>
    /// 用户角色
    /// </summary>
    public class RoleModelMapping : EntityTypeConfiguration<RoleModel>
    {
        public RoleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_Role");
        }
    }
    /// <summary>
    /// 平台集合
    /// </summary>
    public class AssemblyModelMapping : EntityTypeConfiguration<AssemblyModel>
    {
        public AssemblyModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_Assembly");
        }
    }
    /// <summary>
    /// 用户角色配置
    /// </summary>
    public class UserMatchRoleModelMapping : EntityTypeConfiguration<UserMatchRoleModel>
    {
        public UserMatchRoleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_UserMatchRole");
        }
    }
    /// <summary>
    /// 角色配置模块
    /// </summary>
    public class RoleMatchModuleModelMapping : EntityTypeConfiguration<RoleMatchModuleModel>
    {
        public RoleMatchModuleModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Authen_RoleMatchModules");
        }
    }
    /// <summary>
    /// 数据模块导航
    /// </summary>
    public class ModuleNavigationModelMapping : EntityTypeConfiguration<ModuleNavigationModel>
    {
        public ModuleNavigationModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Authen_ModuleNavigation");
        }
    }

    /// <summary>
    /// 模块申查表
    /// </summary>
    public class AuditModelMapping : EntityTypeConfiguration<AuditModel>
    {
        public AuditModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Authen_Audit");
        }
    }

}