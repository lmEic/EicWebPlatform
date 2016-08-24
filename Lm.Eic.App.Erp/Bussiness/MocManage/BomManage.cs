using Lm.Eic.App.Erp.DbAccess.MocManageDb.BomManageBb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Bussiness.MocManage
{
    /// <summary>
    /// Bom管理
    /// </summary>
    public class BomManage
    {
        public BomManage() { }
        /// <summary>
        /// 获取Bom物料列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialModel> GetBomMaterialList(string productId)
        {
          return  BomCrudFactory.BomManageDb.GetBomMaterialListBy(productId);
        }

    }
}
