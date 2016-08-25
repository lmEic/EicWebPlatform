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
        /// 
        /// </summary>
        /// <param name="productId"></param>
        public BomManage(string productId)
        {
            this._productId = productId;
        }

        /// <summary>
        /// 设置产品品号
        /// </summary>
        /// <param name="productId"></param>
        public void SetProductId(string productId)
        {
            this._productId = productId;
        }
         
        string _productId;
        /// <summary>
        /// 产品品号
        /// </summary>
        public string ProductId
        {
            get { return _productId; }
        }

        /// <summary>
        /// 获取Bom物料列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialBomModel> GetBomMaterialList(string productId)
        {
          return  BomCrudFactory.BomManageDb.GetBomMaterialListBy(productId);
        }

    }
}
