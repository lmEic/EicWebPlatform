using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement;
namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{

    public class QualifiedSupplierManager
    {

        /// <summary>
        /// 从ERP中获取年份合格供应商信息
        /// </summary>
        /// <param name="year">年份格式yyyy</param>
        /// <returns></returns>
       public  List<QualifiedSupplierModel> FindQualifiedSupplierList(string year)
        {
            List<QualifiedSupplierModel> QualifiedSupplierInfo = new List<QualifiedSupplierModel>();
            var supplierList = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(year);
            if (supplierList == null || supplierList.Count <= 0) return null;
            supplierList.ForEach(supplierId =>
            {
                var SupplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierId);
                var mm = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);
                QualifiedSupplierInfo.Add(new QualifiedSupplierModel
                {
                    LastPurchaseDate =  SupplierLatestTwoPurchase.FirstOrDefault().PurchaseDate.Trim().ToDate(),
                    UpperPurchaseDate = SupplierLatestTwoPurchase.LastOrDefault().PurchaseDate.Trim().ToDate(),
                    PurchaseUser= SupplierLatestTwoPurchase.FirstOrDefault().PurchasePerson,
                    SuppliersId = supplierId,
                    SupplierAddress = mm.Address,
                    BillAddress =mm.BillAddress ,
                    SupplierFaxNo = mm.FaxNo,
                    SupplierName = mm.SupplierName,
                    SupplierShortName = mm.SupplierShortName,
                    SupplierUser  = mm.Contact,
                    SupplierTel = mm.Tel
                });
            });


            return QualifiedSupplierInfo;
        }

      /// <summary>
       /// 批量保存供应商信息
      /// </summary>
      /// <param name="modelList"></param>
      /// <returns></returns>
       public OpResult SavaQualifiedSupplierInfoS(List<QualifiedSupplierModel> modelList)
       {
           return QualifiedSupplierCrudFactory.QualifiedSupplierCrud.SavaQualifiedSupplierInfoList(modelList);
       }
    }
   
}
