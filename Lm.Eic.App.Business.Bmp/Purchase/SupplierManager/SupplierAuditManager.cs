using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Validation;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
 internal class SupplierAuditManagerFactory
    {
        /// <summary>
        /// 供应商证书管理
        /// </summary>
        public static SupplierCertificateManager SupplierCertificateManager
        {
            get { return OBulider.BuildInstance<SupplierCertificateManager>(); }
        }
    }


   /// <summary>
   /// 供应商考核管理
   /// </summary>
public     class SupplierAuditManager
    {
        #region 季度考核表

        /// <summary>
        /// 加载ERP厂商季度考核列表
        /// </summary>
        /// <param name="seasonDateNum">Year-Season</param>
        /// <returns></returns>
        public List<SupplierSeasonAuditModel> GetSeasonSupplierList(string seasonDateNum)
        {
            string starDate = string.Empty, endDate = string.Empty;
            //处理季度数
            seasonDateNum.SeasonNumConvertStartDateAndEndDate(out starDate, out endDate);
            List<SupplierSeasonAuditModel> supplierSeasonAuditModelList = new List<SupplierSeasonAuditModel>();
            //从ERP中得到季度进货厂商ID
            var getSeasonSupplierList = PurchaseDbManager.StockDb.GetStockSupplierId(starDate, endDate);
            if (getSeasonSupplierList == null || getSeasonSupplierList.Count <= 0) return supplierSeasonAuditModelList;
            getSeasonSupplierList.ForEach(e =>
            {
                supplierSeasonAuditModelList.Add(getSupplierSeasonAuditModel(e, seasonDateNum));
            });
         supplierSeasonAuditModelList.OrderBy(e => e.SupplierId);
            return supplierSeasonAuditModelList;

        }


        /// <summary>
        /// 获得厂商季度考核信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="seasonDateNum"></param>
        /// <returns></returns>
        public SupplierSeasonAuditModel getSupplierSeasonAuditModel(string supplierId, string seasonDateNum)
        {
            SupplierSeasonAuditModel supplierSeasonAuditInfo = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetSupplierSeasonAuditInfo(supplierId.Trim() + "&&" + seasonDateNum);
            if (supplierSeasonAuditInfo != null) return supplierSeasonAuditInfo;
            var supplierInfo = CertificateManagerFactory.SupplierCertificateManager.GetSuppplierInfoBy(supplierId);
            supplierSeasonAuditInfo = new SupplierSeasonAuditModel()
            {
                SupplierId = supplierInfo.SupplierId,
                SupplierShortName = supplierInfo.SupplierShortName,
                SupplierName = supplierInfo.SupplierName,
                SeasonDateNum = seasonDateNum
            };
            return supplierSeasonAuditInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SaveAuditSupplierInfo(SupplierSeasonAuditModel model)
        {
            if (SupplierCrudFactory.SuppliersSeasonAuditCrud.IsExist(model.ParameterKey))
                model.OpSign = "edit";
            else model.OpSign = "add";
            return SupplierCrudFactory.SuppliersSeasonAuditCrud.Store(model);
        }



        #endregion
    }
}
