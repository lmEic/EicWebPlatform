using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository.CommonManageRepository
{
    /// <summary>
    ///表单编号管理持久化
    /// </summary>
    public interface IFormIdManageRepository : IRepository<FormIdManageModel>
    {
        /// <summary>
        /// 获取表单编号列表
        /// </summary>
        /// <param name="department"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        List<FormIdManageModel> GetFormIds(string department, string moduleName);
    }
    /// <summary>
    ///表单编号管理持久化
    /// </summary>
    public class FormIdManageRepository : LmProMasterRepositoryBase<FormIdManageModel>, IFormIdManageRepository
    {
        public List<FormIdManageModel> GetFormIds(string department, string moduleName)
        {
            string yearMonth = DateTime.Now.ToString("yyyyMM");
            StringBuilder sql = new StringBuilder();
            sql.Append("Select ModuleName, FormId, YearMonth, Department, CreateDate, FormStatus, SubId, PrimaryKey, OpDate, OpTime,")
               .Append("OpPerson, OpSign from  Common_FormIdManager ")
               .AppendFormat("where Department='{0}' and ModuleName='{1}' And YearMonth='{2}'", department, moduleName, yearMonth);
            return DbHelper.LmProductMaster.LoadEntities<FormIdManageModel>(sql.ToString());
        }
    }

}
