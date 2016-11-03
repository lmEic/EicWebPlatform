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

    public class PurSupplierManager
    {
       
     public purSupplierInputManager PutInManage
        {
            get { return OBulider.BuildInstance<purSupplierInputManager>(); }
        }
    }
    public class purSupplierInputManager
    {
        /// <summary>
        /// 从ERP中获取年份合格供应商信息
        /// </summary>
        /// <param name="year">年份格式yyyy</param>
        /// <returns></returns>
        public List<QualifiedSupplierModel> FindQualifiedSupplierList(string year)
        {
            List<QualifiedSupplierModel> QualifiedSupplierInfo = new List<QualifiedSupplierModel>();

            var supplierInfoList = ERPFindSupplierInformationList(year);

            if (supplierInfoList == null || supplierInfoList.Count <= 0) return null;

            supplierInfoList.ForEach(supplierInfo =>
            {
                //从ERP中得到最新二次采购信息
                var SupplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierInfo.SupplierId);

                QualifiedSupplierInfo.Add(new QualifiedSupplierModel
                {
                    LastPurchaseDate = SupplierLatestTwoPurchase.FirstOrDefault().PurchaseDate.Trim().ToDate(),
                    UpperPurchaseDate = SupplierLatestTwoPurchase.LastOrDefault().PurchaseDate.Trim().ToDate(),
                    PurchaseUser = SupplierLatestTwoPurchase.FirstOrDefault().PurchasePerson,
                    SupplierId = supplierInfo.SupplierId,
                    SupplierEmail = supplierInfo.SupplierEmail,
                    SupplierAddress = supplierInfo.SupplierAddress,
                    BillAddress = supplierInfo.BillAddress,
                    SupplierFaxNo = supplierInfo.SupplierFaxNo,
                    SupplierName = supplierInfo.SupplierName,
                    SupplierShortName = supplierInfo.SupplierShortName,
                    SupplierUser = supplierInfo.PurchaseUser,
                    SupplierTel = supplierInfo.SupplierTel
                });
            });
            return QualifiedSupplierInfo;
        }
        /// <summary>
        /// 从ERP中获取年份供应商信息
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        public List<SupplierInfoModel> ERPFindSupplierInformationList(string yearMoth)
        {
            List<SupplierInfoModel> SupplierInfoList = new List<SupplierInfoModel>();
            //从ERP中得到此年中所有供应商Id号
            var supplierList = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(yearMoth);
            if (supplierList == null || supplierList.Count <= 0) return null;
            supplierList.ForEach(supplierId =>
            {
                SupplierInfoList.Add(GetSuppplierInfoBy(supplierId));
            });
            return SupplierInfoList;
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SupplierInfoModel GetSuppplierInfoBy(string supplierId)
        {
            try
            {
                //先从已存的数据信息中找 没有找到再从ERP中找
                SupplierInfoModel SupplierInfo = SupplierCrudFactory.SuppliersInfoCrud.GetSupplierInfoBy(supplierId);
                if (SupplierInfo == null)
                { SupplierInfo = GetErpSuppplierInfoBy(supplierId); }
                return SupplierInfo;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
        /// <summary>
        /// 从ERP中得到供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SupplierInfoModel GetErpSuppplierInfoBy(string supplierId)
        {
            var erpSupplierInfo = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);
            if (erpSupplierInfo == null) return null;
            return new SupplierInfoModel
            {
                SupplierId = supplierId,
                SupplierEmail = erpSupplierInfo.Email,
                SupplierAddress = erpSupplierInfo.Address,
                BillAddress = erpSupplierInfo.BillAddress,
                SupplierFaxNo = erpSupplierInfo.FaxNo,
                SupplierName = erpSupplierInfo.SupplierName,
                SupplierShortName = erpSupplierInfo.SupplierShortName,
                SupplierUser = erpSupplierInfo.Contact,
                SupplierTel = erpSupplierInfo.Tel,
                PayCondition = erpSupplierInfo.PayCondition
            };
        }
        /// <summary>
        /// 批量保存供应商信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SaveSupplierInfos(List<SupplierInfoModel> modelList)
        {
            return SupplierCrudFactory.SuppliersInfoCrud.SavaSupplierInfoList(modelList);
        }
        /// <summary>
        /// 批量保存合格供应商信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaQualifiedSupplierInfoS(List<QualifiedSupplierModel> modelList)
        {
            return SupplierCrudFactory.QualifiedSupplierCrud.SavaQualifiedSupplierInfoList(modelList);
        }
    }
   
}
