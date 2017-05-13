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


    /// <summary>
    /// 供应商考核管理
    /// </summary>
    public class SupplierAuditManager
    {
        #region 季度考核表

        /// <summary>
        /// 具有供应商证书管理
        /// </summary>
        private SupplierCertificateManager HaveCertificateSupplierManager
        { get { return OBulider.BuildInstance<SupplierCertificateManager>(); } }
        /// <summary>
        /// 加载厂商季度考核列表
        /// </summary>
        /// <param name="seasonDateNum">Year-Season</param>
        /// <returns></returns>
        public List<SupplierSeasonAuditModel> GetSeasonSupplierList(string seasonDateNum)
        {
            string startDate = string.Empty, endDate = string.Empty;
            ///处理季度数
            seasonDateNum.SeasonNumConvertStartDateAndEndDate(out startDate, out endDate);
            List<SupplierSeasonAuditModel> supplierSeasonAuditModelList = new List<SupplierSeasonAuditModel>();
            SupplierSeasonAuditModel model = null;
            ///从ERP中得到时间段所有进货厂商ID
            List<string> getSeasonSupplierList = PurchaseDbManager.StockDb.GetStockSupplierId(startDate, endDate);

            if (getSeasonSupplierList == null || getSeasonSupplierList.Count <= 0) return supplierSeasonAuditModelList;
            ///对每个供应商进行处理
            getSeasonSupplierList.ForEach(e =>
            {
                model = GetSupplierSeasonAuditModelBy(e, seasonDateNum);
                if (model != null)
                    supplierSeasonAuditModelList.Add(model);
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
        private SupplierSeasonAuditModel GetSupplierSeasonAuditModelBy(string supplierId, string seasonDateNum)
        {
            ///如果已存在，直接导出信息 返回
            SupplierSeasonAuditModel supplierSeasonAuditInfo = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetSupplierSeasonAuditInfo(supplierId.Trim() + "&&" + seasonDateNum);
            if (supplierSeasonAuditInfo != null) return supplierSeasonAuditInfo;
            /// 从得到供应商信息
            var supplierInfo = HaveCertificateSupplierManager.GetSuppplierInfoBy(supplierId);

            if (supplierInfo == null || supplierInfo.Remark == "False") return null;
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
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SaveAuditSupplierInfo(SupplierSeasonAuditModel model)
        {
            //if (SupplierCrudFactory.SuppliersSeasonAuditCrud.IsExist(model.ParameterKey))
            //    model.OpSign = OpMode.Edit;
            //else model.OpSign = OpMode.Add;
            return SupplierCrudFactory.SuppliersSeasonAuditCrud.Store(model);
        }

        /// <summary>
        /// 下载 季度审查总览表
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public DownLoadFileModel SupplierSeasonDataDLFM(List<SupplierSeasonAuditModel> datas)
        {
            try
            {
                if (datas == null || datas.Count < 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<SupplierSeasonAuditModel>("");
                return dataGroupping.ExportToExcelMultiSheets<SupplierSeasonAuditModel>(CreateFieldMapping()).CreateDownLoadExcelFileModel("供应商考核清单");
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
