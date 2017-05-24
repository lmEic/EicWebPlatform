using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    public class CollaborateContactManager
    {
        /// <summary>
        ///存储数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreData(CollaborateContactLibModel model)
        {
            return CollaborateCrudFactorty.ContatCrud.Store(model);
        }
       
        /// <summary>
        /// 部门得到所有的信息
        /// 从数据直接查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public List<CollaborateContactLibModel> QueryContactLibDatasBy(string department,QueryContactDto queryDto=null)
        {
            try
            {
                //部门不能为空
                if (department == null || department == string.Empty)
                    return new List<CollaborateContactLibModel>();
                if (queryDto == null) return CollaborateCrudFactorty.ContatCrud.GetContactLibDatasBy(department);
                if (queryDto.IsExactQuery)
                    return CollaborateCrudFactorty.ContatCrud.ExactFind(queryDto.Department,queryDto);
                else
                    return CollaborateCrudFactorty.ContatCrud.ContainsFind(queryDto.Department, queryDto);
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
        }
    }
  
}
