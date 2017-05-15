using System;
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System.IO;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{

    /// <summary>
    ///有证书的供应商管理
    /// </summary>
    public class SupplierCertificateManager
    {

        /// <summary>
        /// 从ERP中获取截止到给定月份的合格供应商清册列表
        /// </summary>
        /// <param name="endYearMonth">年份格式yyyyMM</param>
        /// <returns></returns>
        public List<EligibleSuppliersVM> GetQualifiedSupplierList(string endYearMonth)
        {
            List<EligibleSuppliersVM> QualifiedSupplierInfoList = new List<EligibleSuppliersVM>();
            EligibleSuppliersVM model = null;
            string startYearMonth = (int.Parse(endYearMonth) - 100).ToString();
            //获取供应商信息
            var supplierInfoList = GetSupplierInformationListBy(startYearMonth, endYearMonth);
            if (supplierInfoList == null || supplierInfoList.Count == 0) return QualifiedSupplierInfoList;
            supplierInfoList.ForEach(supplierInfo =>
            {
                model = GetEligibleSuppliersModel(supplierInfo);
                if (model != null && model.Remark == "True")
                    QualifiedSupplierInfoList.Add(model);
            });
            return QualifiedSupplierInfoList.OrderBy(e => e.SupplierId).ToList();
        }
        /// <summary>
        /// 获取供应商信息
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// </summary>
        public SupplierInfoModel GetSuppplierInfoBy(string supplierId)
        {
            try
            {
                //先从已存的数据信息中找
                SupplierInfoModel supplierInfo = SupplierCrudFactory.SuppliersInfoCrud.GetSupplierInfoBy(supplierId);
                if (supplierInfo != null) return supplierInfo;
                //没有找到再从ERP中找
                supplierInfo = GetSuppplierInfoFromErpBy(supplierId);
                if (supplierInfo != null && supplierInfo.Remark == "True")//待修改
                    //添加至供应商信息表中  上传到数据库中
                    SupplierCrudFactory.SuppliersInfoCrud.Init(supplierInfo);
                return supplierInfo;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// 同步供应商信息
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SupplierInfoModel SysnSupplierInfo(InPutSupplieCertificateInfoModel supplier, string supplierId)
        {
            ///从ERP中得到相应的SupplierId供应商信息 便与下面保存时更新数据库信息
            var supplierfromErp = GetSuppplierInfoFromErpBy(supplierId);
            if (supplierfromErp == null) return supplierfromErp;
            supplierfromErp.PurchaseType = supplier.PurchaseType;
            supplierfromErp.SupplierProperty = supplier.SupplierProperty;
            supplierfromErp.OpSign = supplier.OpSign;
            supplierfromErp.OpPerson = supplier.OpPerson;

            return supplierfromErp;
        }
        private SupplierQualifiedCertificateModel CreateSupplierCertificateModel(InPutSupplieCertificateInfoModel model)
        {
            SupplierQualifiedCertificateModel m = new SupplierQualifiedCertificateModel()
            {
                SupplierId = model.SupplierId,
                EligibleCertificate = model.EligibleCertificate,
                CertificateFileName = model.CertificateFileName,
                FilePath = model.FilePath,
                DateOfCertificate = model.DateOfCertificate.ToDate(),
                IsEfficacy = "是",
                OpSign = model.OpSign,
                OpPerson = model.OpPerson,
            };
            return m;
        }

        /// <summary>
        /// 保存编辑的供应商证书信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SaveSupplierCertificateData(InPutSupplieCertificateInfoModel model, string siteRootPath)
        {
            try
            {
                ///如果修改证书类别信息
                if (model.OpSign == "editPurchaseType")//修改证书类别信息
                {
                    var supplierModel = SysnSupplierInfo(model, model.SupplierId);
                    if (supplierModel == null)
                        return OpResult.SetErrorResult(string.Format("ERP中没有{0}供应商编号", model.SupplierId));
                    return SupplierCrudFactory.SuppliersInfoCrud.Update(supplierModel);
                }
                if (model.CertificateFileName == null || model.CertificateFileName == string.Empty)
                    return OpResult.SetErrorResult("证书名称不能为空");
                return SaveSupplierCertificateData(CreateSupplierCertificateModel(model), siteRootPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 保存供应商证书信息
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="rootPath">根路经</param>
        /// <returns></returns>
        public OpResult SaveSupplierCertificateData(SupplierQualifiedCertificateModel model, string siteRootPath)
        {
            try
            {
                if (model != null && model.OpSign == OpMode.DeleteFile)//如果删除文件则启文件相应的删除处理
                    return SupplierCrudFactory.SupplierQualifiedCertificateCrud.DeleteSupplierQualifiedCertificateFile(model, siteRootPath);
                return SupplierCrudFactory.SupplierQualifiedCertificateCrud.Store(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        ///获取供应商证书列表
        /// </summary>
        /// <param name="suppliersId">供应商Id</param>
        /// <returns></returns>
        public List<SupplierQualifiedCertificateModel> GetSupplierQualifiedCertificateListBy(string supplierId)
        {
            return SupplierCrudFactory.SupplierQualifiedCertificateCrud.GetQualifiedCertificateListBy(supplierId);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="siteRootPath">根路径</param>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="eligibleCertificate">证书名称</param>
        /// <returns></returns>
        public DownLoadFileModel GetSupQuaCertificateDLFM(string siteRootPath, string supplierId, string eligibleCertificate)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            var model = SupplierCrudFactory.SupplierQualifiedCertificateCrud.GetQualifiedCertificateModelBy(supplierId, eligibleCertificate);
            if (model == null || model.CertificateFileName == null || model.FilePath == null)
                return dlfm.Default();

            return dlfm.CreateInstance(model.CertificateFileName,
                siteRootPath.GetDownLoadFilePath(model.FilePath),
                model.CertificateFileName.GetDownLoadContentType());
        }
        /// <summary>
        /// 生成合格供应商清单
        /// </summary>
        /// <returns></returns>
        public DownLoadFileModel BuildQualifiedSupplierInfoList(List<EligibleSuppliersVM> datas)
        {
            try
            {
                if (datas == null || datas.Count == 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<EligibleSuppliersVM>("");
                return dataGroupping.ExportToExcelMultiSheets<EligibleSuppliersVM>(CreateFieldMapping()).CreateDownLoadExcelFileModel("供应商证书信息数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }




        #region   Private Method

        private List<FileFieldMapping> CreateFieldMapping()
        {
            List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                new FileFieldMapping ("Number","项次") ,
                new FileFieldMapping ("SupplierId","供应商Id") ,
                new FileFieldMapping ("SupplierShortName","供应商简称") ,
                new FileFieldMapping ("SupplierName","供应商全称") ,
                new FileFieldMapping ("SupplierProperty","供应商属性") ,
                new FileFieldMapping ("SupplierTel","供应商电话") ,
                new FileFieldMapping ("SupplierFaxNo","供应商传真") ,
                new FileFieldMapping ("SupplierEmail","供应商邮箱") ,
                new FileFieldMapping ("SupplierAddress","供应商地址") ,
                new FileFieldMapping ("SupplierPrincipal","负责人") ,
                new FileFieldMapping ("SupplierUser","联系人") ,
                new FileFieldMapping ("UpperPurchaseDate","上次采购时间") ,
                new FileFieldMapping ("LastPurchaseDate","最近采购时间") ,
                new FileFieldMapping ("PurchaseType","采购类型") ,
                new FileFieldMapping ("ISO9001","ISO9001") ,
                new FileFieldMapping ("ISO14001","ISO14001") ,
                new FileFieldMapping ("SupplierBaseDocument","供应商基本资料表") ,
                new FileFieldMapping ("SupplierComment","供应商评鉴表") ,
                new FileFieldMapping ("NotUseChildLabor","不使用童工申明") ,
                new FileFieldMapping ("EnvironmentalInvestigation","供应商环境调查表") ,
                new FileFieldMapping ("HonestCommitment","廉洁承诺书") ,
                new FileFieldMapping ("PCN_Protocol","PCN协议") ,
                new FileFieldMapping ("QualityAssuranceProtocol","质量保证协议") ,
                new FileFieldMapping ("HSF_Guarantee","HSF保证书") ,
                new FileFieldMapping ("REACH_Guarantee","REACH保证书") ,
                new FileFieldMapping ("SVHC_Guarantee","SVHC调查表")

            };
            return fieldmappping;
        }
        /// <summary>
        /// 获取供应商信息表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        private List<SupplierInfoModel> GetSupplierInformationListBy(string startYearMonth, string endYearMonth)
        {
            List<SupplierInfoModel> supplierInfoList = new List<SupplierInfoModel>();
            //从ERP中得到此年中所有供应商Id号
            var supplierListFromErp = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(startYearMonth, endYearMonth);
            if (supplierListFromErp == null || supplierListFromErp.Count <= 0) return null;
            supplierListFromErp.ForEach(supplierId =>
            {
                supplierInfoList.Add(GetSuppplierInfoBy(supplierId));
            });
            return supplierInfoList;
        }

        /// <summary>
        /// 从ERP中获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SupplierInfoModel GetSuppplierInfoFromErpBy(string supplierId)
        {
            var erpSupplierInfo = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);
            if (erpSupplierInfo == null) return null;
            string Principal = erpSupplierInfo.Principal.Trim() == string.Empty ? erpSupplierInfo.Contact : erpSupplierInfo.Principal;
            return new SupplierInfoModel
            {
                SupplierId = supplierId.Trim(),
                SupplierEmail = erpSupplierInfo.Email,
                SupplierAddress = erpSupplierInfo.Address,
                /// 负责人
                SupplierPrincipal = Principal.Trim(),

                SupplierFaxNo = erpSupplierInfo.FaxNo,
                SupplierName = erpSupplierInfo.SupplierName,
                SupplierShortName = erpSupplierInfo.SupplierShortName,

                SupplierUser = erpSupplierInfo.Contact,

                SupplierTel = erpSupplierInfo.Tel,
                PayCondition = erpSupplierInfo.PayCondition,
                Remark = erpSupplierInfo.IsCooperate

            };
        }

        /// <summary>
        /// 得到所需的证书字典
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private Dictionary<string, string> CertificateDictionary(string supplierId)
        {
            Dictionary<string, string> certificateDictionary = new Dictionary<string, string>();
            certificateDictionary.Add("供应商环境调查表", string.Empty);
            certificateDictionary.Add("供应商基本资料表", string.Empty);
            certificateDictionary.Add("供应商评鉴表", string.Empty);
            certificateDictionary.Add("不使用童工申明", string.Empty);
            certificateDictionary.Add("PCN协议", string.Empty);
            certificateDictionary.Add("廉洁承诺书", string.Empty);
            certificateDictionary.Add("质量保证协议", string.Empty);
            certificateDictionary.Add("HSF保证书", string.Empty);
            certificateDictionary.Add("REACH保证书", string.Empty);
            certificateDictionary.Add("SVHC调查表", string.Empty);
            certificateDictionary.Add("ISO14001", string.Empty);
            certificateDictionary.Add("ISO9001", string.Empty);

            var suppliersQualifiedCertificate = GetSupplierQualifiedCertificateListBy(supplierId);
            if (suppliersQualifiedCertificate != null || suppliersQualifiedCertificate.Count > 0)
            {
                suppliersQualifiedCertificate.ForEach(e =>
               {
                   if (certificateDictionary.ContainsKey(e.EligibleCertificate))
                   {
                       certificateDictionary[e.EligibleCertificate] = e.DateOfCertificate.ToDateStr();
                   }
               });
            }
            return certificateDictionary;
        }

        /// <summary>
        ///通过供应商信息得到证书信息
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <returns></returns>
        private EligibleSuppliersVM GetEligibleSuppliersModel(SupplierInfoModel supplierInfo)
        {
            //从ERP中得到最新二次采购信息
            var supplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierInfo.SupplierId);
            //// 获取供应商证书字典
            //var certificateDictionary = CertificateDictionary(supplierInfo.SupplierId);
            EligibleSuppliersVM m = new EligibleSuppliersVM();
            //return new EligibleSuppliersVM
            //{
            //    LastPurchaseDate = supplierLatestTwoPurchase[0].PurchaseDate.Trim().ToDate(),
            //    UpperPurchaseDate = supplierLatestTwoPurchase[1].PurchaseDate.Trim().ToDate(),
            //    PurchaseUser = supplierLatestTwoPurchase[0].PurchasePerson,
            //    SupplierId = supplierInfo.SupplierId,
            //    SupplierProperty = supplierInfo.SupplierProperty,
            //    PurchaseType = supplierInfo.PurchaseType,
            //    SupplierEmail = supplierInfo.SupplierEmail,
            //    SupplierAddress = supplierInfo.SupplierAddress,
            //    SupplierPrincipal = supplierInfo.SupplierPrincipal,
            //    SupplierFaxNo = supplierInfo.SupplierFaxNo,
            //    SupplierName = supplierInfo.SupplierName,
            //    Remark = supplierInfo.Remark,
            //    SupplierShortName = supplierInfo.SupplierShortName,
            //    SupplierUser = supplierInfo.SupplierUser,
            //    SupplierTel = supplierInfo.SupplierTel,
            //    Id_key = supplierInfo.Id_Key,
            //};
            OOMaper.Mapper<SupplierInfoModel, EligibleSuppliersVM>(supplierInfo, m);

            return m;
        }
        #endregion
    }

}

