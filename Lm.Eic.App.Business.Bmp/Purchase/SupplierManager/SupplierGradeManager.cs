using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
    /// <summary>
    /// 供应商稽核评分管理
    /// </summary>
    public class SupplierGradeManager
    {
        #region  property
        /// <summary>
        /// 具有证书的供应商管理
        /// </summary>
        private SupplierCertificateManager HaveCertificateSupplierManager
        { get { return OBulider.BuildInstance<SupplierCertificateManager>(); } }
        #endregion

        #region   method
        public List<SupplierGradeInfoVm> GetPurSupGradeInfoBy(string yearQuarter)
        {
            List<SupplierGradeInfoVm> returnDatas = new List<SupplierGradeInfoVm>();
            SupplierGradeInfoVm model = null;
            //格式是yyyyMM
            string gradeYear = yearQuarter.Substring(0, 4);
            ///  加供应商信息
            List<SuppliersSumInfoVM> SupplierInfoDatas = HaveCertificateSupplierManager.GetQualifiedSupplierList(yearQuarter);
            if (SupplierInfoDatas == null || SupplierInfoDatas.Count == 0) return returnDatas;
            SupplierInfoDatas.ForEach(m =>
            {
                List<string> supGradeTypes = GetPurSupGradeInfoDataBy(m.SupplierId, gradeYear).Select(e => e.SupGradeType).ToList();
                model = new SupplierGradeInfoVm
                {
                    SupplierId = m.SupplierId,
                    SupplierName = m.SupplierShortName,
                    PurchaseType = m.PurchaseType,
                    SupplierProperty = m.SupplierProperty,
                    LastPurchaseDate = m.LastPurchaseDate,
                    SupGradeInfoContent = ConvertStringBy(supGradeTypes)
                };
                if (!returnDatas.Contains(model))
                    returnDatas.Add(model);
            });
            return returnDatas;
        }
        public List<SupplierGradeInfoModel> GetPurSupGradeInfoDataBy(string supplierId, string yearQuarter)
        {
            //格式是yyyyMM
            string gradeYear = yearQuarter.Substring(0, 4);
            return SupplierCrudFactory.SupplierGradeInfoCrud.GetPurSupGradeInfoBy(supplierId, gradeYear);
        }
        public OpResult SavePurSupGradeData(SupplierGradeInfoModel entity)
        {
            ///操作符在界面没有确定
            return SupplierCrudFactory.SupplierGradeInfoCrud.Store(entity);
        }
        private string ConvertStringBy(List<string> listDatas)
        {
            string convertString = string.Empty;
            if (listDatas == null || listDatas.Count <= 0)  return convertString;
            listDatas.ForEach(s =>{convertString += s + ","; });
            return convertString;
        }
        #endregion
    }
}
