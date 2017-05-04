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
using System.IO;

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
    public class SupplierAuditManager
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
            //处理季度数  统计一年的 向后腿一年
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

        public MemoryStream SupplierSeasonDataStream(List<SupplierSeasonAuditModel> datas)
        {
            try
            {
                if (datas == null || datas.Count < 0) return null;
                //
                var dataGroupping = datas.GetGroupList<SupplierSeasonAuditModel>("");
                return dataGroupping.ExportToExcelMultiSheets<SupplierSeasonAuditModel>(CreateFieldMapping());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        private List<FileFieldMapping> CreateFieldMapping()
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                new FileFieldMapping ("Number","项次") ,
                new FileFieldMapping ("SupplierId","供应商Id") ,
                new FileFieldMapping ("SupplierShortName","供应商简称") ,
                new FileFieldMapping ("SupplierName","供应商名称名") ,
                new FileFieldMapping ("QualityCheck","质量考核分") ,
                new FileFieldMapping ("AuditPrice","价格考核分") ,
                new FileFieldMapping ("DeliveryDate","交期考核分") ,
                new FileFieldMapping ("ActionLiven","配合度考核分") ,
                new FileFieldMapping ("HSFGrade","HSF能力考核等级分") ,
                new FileFieldMapping ("TotalCheckScore","考核总分") ,
                new FileFieldMapping ("CheckLevel","考核级别") ,
                new FileFieldMapping ("RewardsWay","奖惩方式") ,
                new FileFieldMapping ("MaterialGrade","供应商风险等级") ,
                new FileFieldMapping ("ManagerRisk","供应商管理风险") ,
                new FileFieldMapping ("SubstitutionSupplierId","替代厂商") ,
                new FileFieldMapping ("SeasonDateNum","第几季度") ,
                new FileFieldMapping ("Remark","备注")
            };
            return fieldmappping;
        }

        #endregion
    }
}
