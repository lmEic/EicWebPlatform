using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Linq;

namespace Lm.Eic.Framework.Authenticate.Repository
{
    public interface IRegistUserRepository : IRepository<RegistUserModel>
    {
        
    }

    public class RegistUserRepository : AuthenRepositoryBase<RegistUserModel>, IRegistUserRepository
    {
      
    }


    internal class UserInfoHandler
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static LoginedUserInfo GetLoginedUserInfo(string userId)
        {
            string sql = string.Format("Select WorkerId as UserId,Name as UserName,Post, PostNature,Organizetion, Department,ClassType,PersonalPicture from Archives_EmployeeIdentityInfo where WorkerId='{0}' And WorkingStatus='在职'", userId);
            var loginUser = DbHelper.Hrm.LoadEntities<LoginedUserInfo>(sql).FirstOrDefault();
            if (loginUser != null)
            {
                loginUser.HeadPortrait = "data:image/jpg;base64," + Convert.ToBase64String(loginUser.PersonalPicture);
            }
            return loginUser;
        }
    }
}