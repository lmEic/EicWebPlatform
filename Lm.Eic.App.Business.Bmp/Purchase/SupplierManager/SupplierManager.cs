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
        /// <param name="year">年份(格式yy)</param>
        /// <returns></returns>
        List<QualifiedSupplierModel> FindQualifiedSupplierList(string year)
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
                    LastPurchaseDate = SupplierLatestTwoPurchase.LastOrDefault().PurchaseDate.Trim().ToDate(),
                    LatestPurchaseDate = SupplierLatestTwoPurchase.FirstOrDefault().PurchaseDate.Trim().ToDate(),
                    PurchaseClass = SupplierLatestTwoPurchase.FirstOrDefault().PurchasePerson,
                    PurchasePeople = SupplierLatestTwoPurchase.FirstOrDefault().PurchasePerson,
                    SuppliersId = supplierId,
                    SupplierAddress = mm.Address,
                    BillAddress =mm.BillAddress ,
                    SupplierFaxNo = mm.FaxNo,
                    SupplierName = mm.SupplierName,
                    SupplierShortName = mm.SupplierShortName,
                    SupplierPeople = mm.Contact,
                    SupplierTel = mm.Tel,
                    SuperlierEligibleprojectsDate ="测试",
                    SupplierEmail="ww@163.com",
                    SupplierEligibleprojects="111111",
                    SupplierParty="1212122",
                    OpPersom="fff",
                    Remark="454454",
                    OpTime=DateTime.Now.ToDate(),
                    Opdate=DateTime.Now.ToDate(),
                    OpSign="add"
                });
            });


            return QualifiedSupplierInfo;
        }


        public void store(string year)
        {
            var qualifiedSupplierList = FindQualifiedSupplierList(year);
            if (qualifiedSupplierList != null || qualifiedSupplierList.Count >= 0)
            {
                QualifiedSupplierCrudFactory.QualifiedSupplierCrud.SavaQualifiedSupplierInfoList(qualifiedSupplierList);
            }
        }
    }
   
}
