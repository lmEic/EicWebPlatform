using Lm.Eic.App.DomainModel.Mes.Optical.Authen;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Mes.Optical.AuthenDb
{
    /// <summary>
    ///
    /// </summary>
    public interface IUserRepository : IRepository<UserInfo> 
    {
        
        List<WorkerCell> GetWorkers(string whereAppend = "");
    }
    /// <summary>
    ///
    /// </summary>
    public class UserRepository : OpticalMesRepositoryBase<UserInfo>, IUserRepository
    {
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="whereAppend"></param>
        /// <returns></returns>
        public List<WorkerCell> GetWorkers(string whereAppend = "")
        {
            string sql = "Select WorkerId,Name,Department from Archives_IdentitySumerize where WorkingStatus='在职'";
            if (whereAppend != "")
                sql = sql + " And " + whereAppend;
            var datas = DbHelper.Hrm.LoadEntities<WorkerCell>(sql);
            if (datas != null && datas.Count > 0)
            {
                datas.ForEach(worker => {
                    string[] d = worker.Department.Split(',');
                    if (d.Length > 0)
                        worker.Department = d[d.Length - 1].Trim();
                });
            }
            return datas;
        }
    }
}
