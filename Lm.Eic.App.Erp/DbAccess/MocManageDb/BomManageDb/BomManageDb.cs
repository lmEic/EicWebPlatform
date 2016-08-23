using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.BomManageBb
{
    /// <summary>
    /// Bom管理Crud工厂
    /// </summary>
    internal class BomCrudFactory
    {
        /// <summary>
        /// Bom管理Crud
        /// </summary>
        public static BomManageDb BomManageDb
        {
            get { return OBulider.BuildInstance<BomManageDb>(); }
        }
    }

    /// <summary>
    /// Bom管理Db
    /// </summary>
    public class BomManageDb
    {
        /// <summary>
        /// Sql
        /// </summary>
        private string SqlFields
        {
            get { return " "; }
        }
        /// <summary>
        /// 获取Bom物料列表
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<MaterialModel> GetBomMaterialListBy(string productId)
        {
            return null;
        }
    }
}
