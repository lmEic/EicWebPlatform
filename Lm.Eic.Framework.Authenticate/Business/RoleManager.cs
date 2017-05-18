using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.Framework.Authenticate.Business
{
    /// <summary>
    /// 角色管理器
    /// </summary>
    public class RoleManager
    {
        private IRoleRepository irep = null;

        public RoleManager()
        {
            irep = new RoleRepository();
        }

        /// <summary>
        /// 所有的角色数据
        /// </summary>
        public List<RoleModel> Roles
        {
            get { return irep.Entities.ToList(); }
        }

        /// <summary>
        /// 用户匹配处理器
        /// </summary>
        public UserMatchRolesHandler MatchRolesHandler
        {
            get { return OBulider.BuildInstance<UserMatchRolesHandler>(); }
        }

        public RoleMatchModuleHandler MatchModuleHandler
        {
            get { return OBulider.BuildInstance<RoleMatchModuleHandler>(); }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public OpResult AddRole(RoleModel role)
        {
            int record = 0;
            if (role.OpSign == "add")
            {
                if (!irep.IsExist(r => r.RoleId == role.RoleId))
                    record = irep.Insert(role);
            }
            else if (role.OpSign == "edit")
            {
                record = irep.Update(f => f.RoleId == role.RoleId, role);
            }
            return OpResult.SetSuccessResult("加入角色成功!", record > 0, role.Id_Key);
        }

        /// <summary>
        /// 获取该用户所有的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RoleModel> GetUserMatchRoles(string userId)
        {
            return irep.GetUserMatchRoles(userId);
        }

        /// <summary>
        /// 获取该用户匹配的模块信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ModuleNavigationModel> GetMatchModuleNavs(string userId)
        {
            List<ModuleNavigationModel> navs = this.irep.GetRoleMatchModuleNavs(userId);
            return navs;
        }
    }

    /// <summary>
    /// 用户与角色匹配处理器
    /// </summary>
    public class UserMatchRolesHandler
    {
        private IUserMatchRoleRepository irep = null;

        public UserMatchRolesHandler()
        {
            irep = new UserMatchRoleRepository();
        }

        public List<UserMatchRoleModel> GetMatchRoles(string userId)
        {
            return irep.Entities.Where(e => e.UserId == userId).ToList();
        }

        /// <summary>
        /// 管理用户角色
        /// </summary>
        /// <param name="matchRoles"></param>
        /// <returns></returns>
        public OpResult StoreMatchRoles(List<UserMatchRoleModel> matchRoles)
        {
            if (matchRoles == null || matchRoles.Count == 0)
                return OpResult.SetSuccessResult("没有要添加的数据", false);
            int record = 0;
            matchRoles = matchRoles.FindAll(e => e.OpSign != "init");
            matchRoles.ForEach(role =>
            {
                if (role.OpSign == "add")
                {
                    if (!irep.IsExist(r => r.RoleID == role.RoleID && r.UserId == role.UserId))
                        record += irep.Insert(role);
                }
                else if (role.OpSign == "delete")
                {
                    var entity = irep.Entities.FirstOrDefault(e => e.RoleID == role.RoleID && e.UserId == role.UserId);
                    if (entity != null)
                        record += irep.Delete(entity);
                }
            });
            return OpResult.SetSuccessResult("保存用户添加角色数据成功!", record > 0);
        }

        /// <summary>
        /// 查找该用户的所有角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserMatchRoleModel> FindMatchRolesBy(string userId)
        {
            return irep.Entities.Where(e => e.UserId == userId).ToList();
        }
    }

    /// <summary>
    /// 角色与模块匹配处理器
    /// </summary>
    public class RoleMatchModuleHandler
    {
        private IRoleMatchModuleRepository irep = null;

        public RoleMatchModuleHandler()
        {
            this.irep = new RoleMatchModuleRepository();
        }

        private void SetPrimaryPropertyValue(RoleMatchModuleModel mdl)
        {
            mdl.PrimaryKey = string.Format("{0}&{1}&{2}&{3}&{4}", mdl.AssemblyName, mdl.ModuleName, mdl.CtrlName, mdl.ActionName, mdl.RoleId);
            mdl.ModuleNavPrimaryKey = string.Format("{0}&{1}&{2}&{3}", mdl.AssemblyName, mdl.ModuleName, mdl.CtrlName, mdl.ActionName);
        }

        /// <summary>
        /// 存储角色匹配模块信息
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public OpResult Store(List<RoleMatchModuleModel> entities)
        {
            if (entities == null || entities.Count == 0) return OpResult.SetSuccessResult("entities can't be null", false);
            int record = 0;
            entities = entities.FindAll(e => e.OpSign != "init");
            entities.ForEach(mdl =>
            {
                if (mdl.OpSign == "add")
                {
                    SetPrimaryPropertyValue(mdl);
                    if (!irep.IsExist(m => m.PrimaryKey == mdl.PrimaryKey))
                    {
                        record += irep.Insert(mdl);
                    }
                }
                else if (mdl.OpSign == "delete")
                {
                    SetPrimaryPropertyValue(mdl);
                    record += irep.Delete(d => d.PrimaryKey == mdl.PrimaryKey);
                }
            });
            return OpResult.SetSuccessResult("保存数据成功", record > 0);
        }

        /// <summary>
        /// 删除记录，在模块管理中删除模块时进行调用此函数
        /// 删除此模块对应的角色匹配信息
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        internal int Delete(string primaryName)
        {
            return this.irep.Delete(e => e.ModuleNavPrimaryKey == primaryName);
        }

        /// <summary>
        /// 更新记录，在模块管理中修改模块节点时进行调用此函数
        /// 以更新对应的角色匹配节点信息
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        internal int Update(ModuleNavigationModel oldEntity, ModuleNavigationModel newEntity)
        {
            List<RoleMatchModuleModel> mdls = this.irep.Entities.Where(e => e.ModuleNavPrimaryKey == oldEntity.PrimaryKey).ToList();
            int record = UpdateData(newEntity, mdls);
            if (record == 0)
            {
                mdls = this.irep.Entities.Where(e => e.AssemblyName == oldEntity.AssemblyName && e.ModuleName == oldEntity.ModuleName && e.CtrlName == oldEntity.CtrlName).ToList();
                record = UpdateData(newEntity, mdls);
            }
            return record;
        }

        private int UpdateData(ModuleNavigationModel newEntity, List<RoleMatchModuleModel> mdls)
        {
            int record = 0;
            if (mdls != null && mdls.Count > 0)
            {
                mdls.ForEach(m =>
                {
                    m.ModuleName = newEntity.ModuleName;
                    m.ModuleText = newEntity.ModuleText;
                    m.ActionName = newEntity.ActionName;
                    m.CtrlName = newEntity.CtrlName;
                    m.AssemblyName = newEntity.AssemblyName;
                    SetPrimaryPropertyValue(m);
                    record += irep.Update(u => u.Id_Key == m.Id_Key, m);
                });
            }

            return record;
        }

        public List<RoleMatchModuleModel> FindBy(string assemblyName, string moduleName, string ctrlName)
        {
            return irep.Entities.Where(e => e.AssemblyName == assemblyName && e.ModuleName == moduleName && e.CtrlName == ctrlName).ToList();
        }

        public List<RoleMatchModuleModel> FindBy(string roleId)
        {
            return irep.Entities.Where(e => e.RoleId == roleId).ToList();
        }
    }
}