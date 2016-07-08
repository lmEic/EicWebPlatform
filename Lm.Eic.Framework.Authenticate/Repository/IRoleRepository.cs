using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System.Collections.Generic;
using System.Text;

namespace Lm.Eic.Framework.Authenticate.Repository
{
    public interface IRoleRepository : IRepository<RoleModel>
    {
        /// <summary>
        /// 获取用户匹配的角色模型列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RoleModel> GetUserMatchRoles(string userId);

        List<ModuleNavigationModel> GetRoleMatchModuleNavs(string userId);
    }

    public class RoleRepository : AuthenRepositoryBase<RoleModel>, IRoleRepository
    {
        public List<RoleModel> GetUserMatchRoles(string userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT   RoleID, RoleName, RoleLevel, Memo, Id_Key FROM  Authen_Role ")
              .Append("WHERE   (RoleID IN  (SELECT   RoleId FROM  Authen_UserMatchRole ")
              .AppendFormat(" WHERE  (UserId = '{0}')))", userId);
            string sqltext = sb.ToString();
            return DbHelper.Authen.LoadEntities<RoleModel>(sqltext);
        }

        public List<ModuleNavigationModel> GetRoleMatchModuleNavs(string userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT   AssemblyName, ModuleName, ModuleText, ParentModuleName, AtLevel, CtrlName, ActionName, UiSerf, TemplateID, ")
              .Append("Icon, ClientMode, ModuleType,DisplayOrder, PrimaryKey, Id_Key FROM  Authen_ModuleNavigation ")
              .Append("WHERE   (PrimaryKey IN")
              .Append("(SELECT Distinct ModuleNavPrimaryKey FROM  Authen_RoleMatchModules ")
              .Append("WHERE   (RoleID IN  (SELECT   RoleId FROM  Authen_UserMatchRole ")
              .AppendFormat(" WHERE  (UserId = '{0}')))))", userId);
            string sqltext = sb.ToString();
            return DbHelper.Authen.LoadEntities<ModuleNavigationModel>(sqltext);
        }
    }

    public interface IUserMatchRoleRepository : IRepository<UserMatchRoleModel> { }

    public class UserMatchRoleRepository : AuthenRepositoryBase<UserMatchRoleModel>, IUserMatchRoleRepository
    { }

    public interface IRoleMatchModuleRepository : IRepository<RoleMatchModuleModel>
    {
    }

    public class RoleMatchModuleRepository : AuthenRepositoryBase<RoleMatchModuleModel>, IRoleMatchModuleRepository
    {
    }
}