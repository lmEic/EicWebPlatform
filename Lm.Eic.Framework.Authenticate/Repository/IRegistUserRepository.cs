using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System.Linq;

namespace Lm.Eic.Framework.Authenticate.Repository
{
    public interface IRegistUserRepository : IRepository<RegistUserModel>
    {
        UserOrganizeModel GetUserOrganiseInfo(string workerId);
    }

    public class RegistUserRepository : AuthenRepositoryBase<RegistUserModel>, IRegistUserRepository
    {
        public UserOrganizeModel GetUserOrganiseInfo(string workerId)
        {
            string sql = string.Format("Select  WorkerId,Name,Department,Post,PostNature,PersonalPicture  from  Archives_IdentitySumerize where WorkerId='{0}'", workerId);
            var userOrganize = DbHelper.Hrm.LoadEntities<UserOrganizeModel>(sql).FirstOrDefault();
            if (userOrganize != null)
            {
                string department = userOrganize.Department;
                string[] deps = department.Split(',');
                if (deps.Length > 0)
                {
                    userOrganize.Department = deps[deps.Length - 1];
                }
            }
            return userOrganize;
        }
    }
}