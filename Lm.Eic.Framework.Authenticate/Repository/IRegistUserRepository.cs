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
                var departmentDataSource = GetDepartments();
                var department = departmentDataSource.FirstOrDefault(e => e.DepartmentNode == loginUser.Department);
                if (department != null)
                {
                    loginUser.DepartmentText = department.DepartmentText;
                    loginUser.ParentDepartmentText = department.ParentDepartmentText;
                    loginUser.OrganizationUnits = GetOrganizationUnitOfSameDepartment(department, departmentDataSource);
                }

            }
            return loginUser;
        }

        private static List<DepartmentModel> GetDepartments()
        {
            string sql = "Select DataNodeName As DepartmentNode,DataNodeText As DepartmentText,ParentDataNodeText As ParentDepartmentText from  Config_DataDictionary where TreeModuleKey='Organization' order by DisplayOrder";
            return DbHelper.LmProductMaster.LoadEntities<DepartmentModel>(sql);
        }
        /// <summary>
        /// 获取同一个部门的所有单位
        /// </summary>
        /// <param name="parentDepartmentText"></param>
        /// <returns></returns>
        private static List<DepartmentModel> GetOrganizationUnitOfSameDepartment(DepartmentModel departmentModel, List<DepartmentModel> dataSource)
        {
            List<DepartmentModel> organizationUnits = new List<DepartmentModel>();
            string parentNodeText = string.Empty;
            List<string> parentNodes = new List<string>() { "研发处", "行政处", "制造处" };
            if (parentNodes.Contains(departmentModel.ParentDepartmentText))
            {
                parentNodeText = departmentModel.DepartmentText;
                departmentModel.IsSelf = true;
                organizationUnits.Add(departmentModel);
            }
            else
            {
                parentNodeText = departmentModel.ParentDepartmentText;
                var m = dataSource.FirstOrDefault(e => e.DepartmentText == departmentModel.ParentDepartmentText);
                m.IsSelf = true;
                organizationUnits.Add(m);
            }
            var datas = dataSource.FindAll(f => f.ParentDepartmentText == parentNodeText);
            if (datas != null && datas.Count > 0)
                datas.ForEach(d =>
                {
                    d.IsSelf = d.DepartmentText == departmentModel.DepartmentText ? true : false;
                    organizationUnits.Add(d);
                });
            return organizationUnits;
        }
    }
}