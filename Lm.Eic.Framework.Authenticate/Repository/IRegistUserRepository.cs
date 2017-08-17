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
        internal static LoginedUserInfo GetLoginedUserInfo(string userId)
        {
            string sql = string.Format("Select WorkerId as UserId,Name as UserName,Post, PostNature,Organizetion, Department,ClassType,PersonalPicture from Archives_EmployeeIdentityInfo where WorkerId='{0}' And WorkingStatus='在职'", userId);
            var loginUser = DbHelper.Hrm.LoadEntities<LoginedUserInfo>(sql).FirstOrDefault();
            if (loginUser != null)
            {
                if (loginUser.PersonalPicture != null)
                    loginUser.HeadPortrait = "data:image/jpg;base64," + Convert.ToBase64String(loginUser.PersonalPicture);
                var department = GetDepartment(loginUser.Department);
                if (department != null)
                    loginUser.DepartmentText = department.DepartmentText;
            }
            return loginUser;
        }


        private static DepartmentModel GetDepartment(string departmentCode)
        {
            string sql = string.Format("Select Top 1 DataNodeName As DepartmentNode,DataNodeText As DepartmentText from  Config_DataDictionary where DataNodeName='{0}'", departmentCode);
            return DbHelper.LmProductMaster.LoadEntities<DepartmentModel>(sql).FirstOrDefault();
        }
    }
    /// <summary>
    /// 部门信息模型
    /// </summary>
    public class DepartmentModel
    {
        public string DepartmentNode { get; set; }

        public string DepartmentText { get; set; }
    }
}