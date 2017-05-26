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
        ///// <param name="queryDto"></param>
        ///// <returns></returns>
        public List<CollaborateContactLibModel> GetContactLibDatasBy(QueryContactDto queryDto)
        {
            try
            {
                if (queryDto == null) return new DataList<CollaborateContactLibModel>();
                return CollaborateCrudFactorty.ContatCrud.FindBy(queryDto);
            }
            catch (Exception ex)
            {
                return ex.ExDataList<CollaborateContactLibModel>();
            }
        }
    }

}
