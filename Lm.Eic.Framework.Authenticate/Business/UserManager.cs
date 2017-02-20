using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System.Linq;

namespace Lm.Eic.Framework.Authenticate.Business
{
    public class UserManager
    {
        /// <summary>
        /// 用户注册器
        /// </summary>
        public RegistManager UserRegister
        {
            get { return OBulider.BuildInstance<RegistManager>(); }
        }
    }

    /// <summary>
    /// 注册管理器
    /// </summary>
    public class RegistManager
    {
        private IRegistUserRepository registerRep = null;

        public RegistManager()
        {
            this.registerRep = new RegistUserRepository();
        }

        public IdentityInfo LoginCheck(LoginModel loginMdl)
        {
            IdentityInfo identity = new IdentityInfo();
            LoginStatus status = new LoginStatus();
            var loginUser = registerRep.Entities.FirstOrDefault(e => e.UserId == loginMdl.UserId);
            if (loginUser != null)
            {
                if (loginUser.Password == loginMdl.Password)
                {
                    status.StatusCode = 0;
                    identity = LoginHandler.CollectUserInfoWhenLogined(identity,loginMdl.UserId);
                }
                else
                {
                    status.StatusCode = 2;
                }
            }
            else
            {
                status.StatusCode = 1;
            }
            identity.LoginStatus = status;
            return identity;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public OpResult RegistUser(RegistUserModel user)
        {
            int record = 0;
            if (registerRep.IsExist(e => e.UserId == user.UserId))
            {
                return OpResult.SetResult("该用户已经存在！", false);
            }
            var UserOrganizeInfo =UserInfoHandler.GetLoginedUserInfo(user.UserId);
            if (UserOrganizeInfo != null)
            {
                user.CurrentStatus = "可使用";
                user.UserName = UserOrganizeInfo.UserName;
                record = registerRep.Insert(user);
                return OpResult.SetResult("添加用户成功！", record > 0);
            }
            else
            {
                return OpResult.SetResult("人事系统中没有此用户信息！", false);
            }
        }

        /// <summary>
        /// 根据用户工号查找用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RegistUserModel FindUserBy(string userId)
        {
            return registerRep.Entities.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// 获取用户身份信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IdentityInfo GetUserIdentity(string userId)
        {
            IdentityInfo identity = null;
            var user = FindUserBy(userId);
            if (user != null)
            {
                LoginModel lm = new LoginModel() { UserId = user.UserId, Password = user.Password };
                return LoginCheck(lm);
            }
            return identity;
        }
    }

    /// <summary>
    /// 登录处理器
    /// </summary>
    internal class LoginHandler
    {
        /// <summary>
        /// 当用户登录成功时，搜集整理用户信息
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        internal static IdentityInfo CollectUserInfoWhenLogined(IdentityInfo waitHandlerModel,string userId)
        {
            var user = UserInfoHandler.GetLoginedUserInfo(userId);
            waitHandlerModel.LoginedUser = user;
            RoleManager rm = new RoleManager();
            var matchRoles = rm.GetUserMatchRoles(user.UserId);
            if (matchRoles != null)
            {
                waitHandlerModel.MatchRoleList = matchRoles;
            }
            var matchRoleModules = rm.GetMatchModuleNavs(user.UserId);
            if (matchRoleModules != null)
            {
                waitHandlerModel.MatchModulePowerList = matchRoleModules;
            }
            return waitHandlerModel;
        }
    }
}