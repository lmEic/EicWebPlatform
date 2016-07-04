using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Framework.Authenticate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers
{
    public class AccountController :EicBaseController
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        #region login
        [NoAuthenCheck]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult LoginCheck(LoginModel user)
        {
            var loginUser = AuthenService.UserManager.UserRegister.LoginCheck(user);
            if (loginUser.LoginStatus.StatusCode == 0)
            {
                Session[EicConstKeys.UserAccount] = loginUser;
            }
            
            return Json(loginUser, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult Logout()
        {
            Session.RemoveAll();
            
            var opResult = "ok";
            return Json(opResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region user manage
        public ActionResult RegistUser()
        {
            return View();
        }

        [NoAuthenCheck]
        public JsonResult GetUserIdentityByUserId(string userId)
        {
            IdentityInfo userIdentity = AuthenService.UserManager.UserRegister.GetUserIdentity(userId);

            return Json(userIdentity, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetUserById(string userId)
        {
            var user = AuthenService.UserManager.UserRegister.FindUserBy(userId);

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户匹配的角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
         [NoAuthenCheck]
        public JsonResult FindUserMatchRolesByUserId(string userId)
        {
            RegistUserModel user = AuthenService.UserManager.UserRegister.FindUserBy(userId);
            List<RoleModel> userRoles = AuthenService.RoleManager.GetUserMatchRoles(userId);

            var matchRoles = new { user = user, userRoles = userRoles };
            return Json(matchRoles, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult AddUser(RegistUserModel user)
        {
            var result = AuthenService.UserManager.UserRegister.RegistUser(user);
            return Json(result);
        }
        [NoAuthenCheck]
        public JsonResult GetRoles()
        {
            var roles = AuthenService.RoleManager.Roles;
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult AssignRoleToUser()
        {
            return View();
        }
        #endregion

        #region role manage
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AddRole(RoleModel role)
        {
            var result = AuthenService.RoleManager.AddRole(role);
            return Json(result);
        }
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SysRoleManage()
        {
            return View();
        }
        #endregion

        #region assembly
        /// <summary>
        /// 程序集管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SysAssemblyEdit()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetAssemblies()
        {
            var data = AuthenService.AssemblyManager.AssemblyList;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [NoAuthenCheck]
        public JsonResult AddAssembly(AssemblyModel assembly)
        {
            var result = AuthenService.AssemblyManager.AddAssembly(assembly);
            return Json(result);
        }
        #endregion

        #region module nav
        /// <summary>
        /// 模块管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SysModuleEdit()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult SaveModuleNavData(ModuleNavigationModel moduleNav, ModuleNavigationModel oldModuleNav, string opType)
        {
            var result = AuthenService.ModuleManager.Store(moduleNav,oldModuleNav,opType);
            return Json(result);
        }
        [NoAuthenCheck]
        public JsonResult GetNavMenus()
        {
            var modules = AuthenService.ModuleManager.NavMneus;
            return Json(modules, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region user match roles
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult AddMatchRoles(List<UserMatchRoleModel> matchRoles)
        {
            var result = AuthenService.RoleManager.MatchRolesHandler.StoreMatchRoles(matchRoles);
            return Json(result);
        }
        #endregion

        #region assign power
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignPowerToRole()
        {
            return View();
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignModuleToRole()
        {
            return View();
        }

        [HttpPost]
        [NoAuthenCheck]
        public JsonResult SaveRoleMatchModuleData(List<RoleMatchModuleModel> mdls)
        {
            var result = AuthenService.RoleManager.MatchModuleHandler.Store(mdls);
            return Json(result);
        }
        [NoAuthenCheck]
        public JsonResult FindRoleMatchModules(string assemblyName,string moduleName,string ctrlName)
        {
            List<RoleMatchModuleModel> matchModuleRoles = AuthenService.RoleManager.MatchModuleHandler.FindBy(assemblyName, moduleName,ctrlName);
            return Json(matchModuleRoles, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult FindRoleMatchModulesBy(string roleId)
        {
            List<RoleMatchModuleModel> matchModuleRoles = AuthenService.RoleManager.MatchModuleHandler.FindBy(roleId);
            return Json(matchModuleRoles, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult GetNavsAndRoles()
        {
            var modules = AuthenService.ModuleManager.NavMneus;
            var roles = AuthenService.RoleManager.Roles;
            var datas =new { modules=modules,roles=roles };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
