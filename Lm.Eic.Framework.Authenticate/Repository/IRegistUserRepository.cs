using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
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
                {
                    loginUser.DepartmentText = department.DepartmentText;
                    loginUser.ParentDepartmentText = department.ParentDepartmentText;
                    loginUser.OrganizationUnits = GetOrganizationUnitOfSameDepartment(department);
                }

            }
            return loginUser;
        }


        private static DepartmentModel GetDepartment(string departmentCode)
        {
            string sql = string.Format("Select Top 1 DataNodeName As DepartmentNode,DataNodeText As DepartmentText,ParentDataNodeText As ParentDepartmentText from  Config_DataDictionary where DataNodeName='{0}'", departmentCode);
            return DbHelper.LmProductMaster.LoadEntities<DepartmentModel>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取同一个部门的所有单位
        /// </summary>
        /// <param name="parentDepartmentText"></param>
        /// <returns></returns>
        private static List<DepartmentModel> GetOrganizationUnitOfSameDepartment(DepartmentModel departmentModel)
        {
            List<DepartmentModel> organizationUnits = new List<DepartmentModel>() { departmentModel };
            string parentNodeText = string.Empty;
            List<string> parentNodes = new List<string>() { "研发处", "行政处", "制造处" };
            if (parentNodes.Contains(departmentModel.ParentDepartmentText))
            {
                parentNodeText = departmentModel.DepartmentText;
            }
            else
            {
                parentNodeText = departmentModel.ParentDepartmentText;
            }

            string sql = string.Format("Select DataNodeName As DepartmentNode,DataNodeText As DepartmentText,ParentDataNodeText As ParentDepartmentText from  Config_DataDictionary where ParentDataNodeText='{0}' Order By DisplayOrder", parentNodeText);
            var datas = DbHelper.LmProductMaster.LoadEntities<DepartmentModel>(sql);
            if (datas != null && datas.Count > 0)
                organizationUnits.AddRange(datas);
            return organizationUnits;
        }
    }
}