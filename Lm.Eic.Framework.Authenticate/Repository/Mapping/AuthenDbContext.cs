using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lm.Eic.Framework.Authenticate.Model;


namespace Lm.Eic.Framework.Authenticate.Repository.Mapping
{
    /// <summary>
    /// 用户验证数据库
    /// </summary>
    public class AuthenDbContext : DbContext
    {
        public AuthenDbContext(string databaseName = "AuthenDbContext")
        {
            Database.SetInitializer<AuthenDbContext>(null);
        }
        /// <summary>
        /// 注册的用户信息
        /// </summary>
        public DbSet<RegistUserModel> RegistUsers { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public DbSet<RoleModel> Roles { get; set; }
        /// <summary>
        /// 程序集信息
        /// </summary>
        public DbSet<AssemblyModel> Assemblys { get; set; }
        /// <summary>
        /// 匹配的角色信息
        /// </summary>
        public DbSet<UserMatchRoleModel> MatchRoles { get; set; }
        /// <summary>
        /// 模块导航数据
        /// </summary>
        public DbSet<ModuleNavigationModel> ModuleNavigations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RegistUserModelMapping());
            modelBuilder.Configurations.Add(new RoleModelMapping());
            modelBuilder.Configurations.Add(new AssemblyModelMapping());

            modelBuilder.Configurations.Add(new UserMatchRoleModelMapping());
            modelBuilder.Configurations.Add(new RoleMatchModuleModelMapping());

            modelBuilder.Configurations.Add(new ModuleNavigationModelMapping());
        }
        #region dbset

        #endregion
    }

    #region Repository Framework
    /// <summary>
    /// 用户验证中心数据库工作单元
    /// </summary>
    public class AuthenUnitOfWorkContext : UnitOfWorkContextBase, IUnitOfWorkContext
    {
        protected override void SetDbContext()
        {
            try
            {
                this.Context = new AuthenDbContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    /// <summary>
    /// 光主动数据库持久化基类/父类/超类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class AuthenRepositoryBase<TEntity> : EFRepositoryBase<TEntity>, IRepository<TEntity>
         where TEntity : class,new()
    {
        protected override void SetUnitOfWorkContext()
        {
            try
            {
                this.EFContext = new AuthenUnitOfWorkContext();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
    #endregion
}
