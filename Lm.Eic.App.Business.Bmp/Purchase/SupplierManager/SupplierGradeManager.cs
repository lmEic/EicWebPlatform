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
        public List<SupplierGradeInfoModel> GetPurSupGradeInfoBy(string yearQuarter)
        {
            List<SupplierGradeInfoModel> returnDatas = new List<SupplierGradeInfoModel>();
            SupplierGradeInfoModel model = null;
            ///关键字段
            string parameterKey = string.Empty;
            //格式是yyyyMM
            string gradeYear = yearQuarter.Substring(0, 4);
            List<EligibleSuppliersVM> SupplierInfoDatas = HaveCertificateSupplierManager.GetQualifiedSupplierList(yearQuarter);
            if (SupplierInfoDatas == null || SupplierInfoDatas.Count == 0) return returnDatas;
            SupplierInfoDatas.ForEach(m =>
            {
                parameterKey = m.SupplierId + "&" + gradeYear;
                model = SupplierCrudFactory.SupplierGradeInfoCrud.GetPurSupGradeInfoBy(parameterKey);
                if (model == null)
                {
                    model = new SupplierGradeInfoModel
                    {
                        SupplierId = m.SupplierId,
                        SupplierName = m.SupplierShortName,
                        PurchaseType = m.SupplierProperty,
                        LastPurchaseDate = m.LastPurchaseDate,
                        PurchaseMaterial = m.PurchaseType,
                    };
                }
                if (!returnDatas.Contains(model))
                    returnDatas.Add(model);
            });
            return returnDatas;
        }
        public OpResult SavePurSupGradeData(SupplierGradeInfoModel entity)
        {
            ///操作符在界面没有确定
            if (entity == null) return OpResult.SetErrorResult("实体不能为空");
            string ParameterKey = entity.SupplierId + "&" + entity.GradeYear + "&" + entity.SupGradeType;
            if (SupplierCrudFactory.SupplierGradeInfoCrud.IsExist(ParameterKey))
                entity.OpSign = OpMode.Add;
            else entity.OpSign = OpMode.Edit;
            return SupplierCrudFactory.SupplierGradeInfoCrud.Store(entity);
        }
        #endregion
    }
}
