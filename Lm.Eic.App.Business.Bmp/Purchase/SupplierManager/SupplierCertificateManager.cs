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
        public List<SuppliersSumInfoVM> GetQualifiedSupplierList(string endYearMonth)
        {
            string startYearMonth = (int.Parse(endYearMonth) - 100).ToString();
            return GetQualifiedSupplierDates(  startYearMonth, endYearMonth);
        }

        public List<SuppliersSumInfoVM> GetQualifiedSupplierDates( string startYearMonth,string endYearMonth)
        {
            SuppliersSumInfoVM modelVm = null;
            List<SuppliersSumInfoVM> SupplierInfoVmDatas = new List<SuppliersSumInfoVM>();
            //获取供应商信息
            var supplierInfoDatas = GetSupplierInformationDatasBy(startYearMonth, endYearMonth);
            if (supplierInfoDatas == null || supplierInfoDatas.Count == 0) return SupplierInfoVmDatas;
            supplierInfoDatas.ForEach(supplierInfo =>
            {
                if (supplierInfo.IsCooperate.ToString() == "True")
                    ///供应商信息加载最后二次采购信息
                    modelVm = GetSuppliersInfoAddrLatestTwoPurchaseInfo(supplierInfo);
                if (modelVm != null && !SupplierInfoVmDatas.Contains(modelVm))
                    SupplierInfoVmDatas.Add(modelVm);
            });
            return SupplierInfoVmDatas.OrderBy(e => e.SupplierId).ToList();
        }

        public SuppliersSumInfoVM GetSupplierBaseInfoBy(string supplierId)
        {
            var supplierInfo = GetSuppplierInfoBy(supplierId);
            if (supplierInfo!=null && supplierInfo.IsCooperate.ToString() == "True")
                    return  GetSuppliersInfoAddrLatestTwoPurchaseInfo(supplierInfo);
            return null;
        }
        /// <summary>
        /// 获取供应商信息
        /// <param name="supplierId"></param>
        /// <returns></returns>
        /// </summary>
        public  SupplierInfoModel GetSuppplierInfoBy(string supplierId)
        {
            try
            {
                //先从已存的数据信息中找
                var data = SupplierCrudFactory.SuppliersInfoCrud.GetSupplierInfoBy(supplierId);
                if (data != null) return data;
                    //没有找到再从ERP中找
                SupplierInfoModel supplierInfo = GetSuppplierInfoFromErpBy(supplierId);
               if(supplierInfo != null&&supplierInfo.IsCooperate== "True")
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
                /// 
                return SupplierCrudFactory.SupplierQualifiedCertificateCrud.UpdateQualifiedCertificateFile(model, siteRootPath);

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

            return dlfm.CreateInstance
                (siteRootPath.GetDownLoadFilePath(model.FilePath),
                model.CertificateFileName.GetDownLoadContentType(),
                model.CertificateFileName);
        }
        /// <summary>
        /// 生成合格供应商清单
        /// </summary>
        /// <returns></returns>
        public DownLoadFileModel BuildQualifiedSupplierInfoList(List<SuppliersSumInfoVM> datas)
        {
            try
            {
                datas = AddQualifiedCertificateDate(datas);
                if (datas == null || datas.Count == 0) return new DownLoadFileModel().Default();
                var dataGroupping = datas.GetGroupList<SuppliersSumInfoVM>();
                return dataGroupping.ExportToExcelMultiSheets<SuppliersSumInfoVM>(CreateFieldMapping()).CreateDownLoadExcelFileModel("供应商证书信息数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 加载证书日期
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private List<SuppliersSumInfoVM> AddQualifiedCertificateDate(List<SuppliersSumInfoVM> datas)
        {
            List<SuppliersSumInfoVM> retrundatas = new List<SuppliersSumInfoVM>();
            if (datas == null || datas.Count == 0)
            return null;
            datas.ForEach(e =>
            {
                var dd = CertificateDictionary(e.SupplierId,e.QualifiedCertificateDatas);
                e.EnvironmentalInvestigation = dd[certificateName.EnvironmentalInvestigation];
                e.HonestCommitment = dd[certificateName.HonestCommitment];
                e.QualityAssuranceProtocol = dd[certificateName.QualityAssuranceProtocol];
                e.SupplierBaseDocument = dd[certificateName.SupplierBaseDocument];
                e.SupplierComment = dd[certificateName.SupplierComment];
                e.NotUseChildLabor = dd[certificateName.NotUseChildLabor];
                e.PCN_Protocol = dd[certificateName.PCN_Protocol];
                e.HSF_Guarantee = dd[certificateName.HSF_Guarantee];
                e.REACH_Guarantee = dd[certificateName.REACH_Guarantee];
                e.SVHC_Guarantee = dd[certificateName.SVHC_Guarantee];
                e.ISO14001 = dd[certificateName.ISO14001];
                e.ISO9001 = dd[certificateName.ISO9001];
                retrundatas.Add(e);
            });
            return retrundatas;
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
                new FileFieldMapping ("ISO9001",certificateName.ISO9001) ,
                new FileFieldMapping ("ISO14001",certificateName.ISO14001) ,
                new FileFieldMapping ("SupplierBaseDocument",certificateName.SupplierBaseDocument ) ,
                new FileFieldMapping ("SupplierComment",certificateName.SupplierComment) ,
                new FileFieldMapping ("NotUseChildLabor",certificateName.NotUseChildLabor ) ,
                new FileFieldMapping ("EnvironmentalInvestigation",certificateName.EnvironmentalInvestigation ) ,
                new FileFieldMapping ("HonestCommitment",certificateName.HonestCommitment ) ,
                new FileFieldMapping ("PCN_Protocol",certificateName.PCN_Protocol) ,
                new FileFieldMapping ("QualityAssuranceProtocol",certificateName.QualityAssuranceProtocol ) ,
                new FileFieldMapping ("HSF_Guarantee",certificateName.HSF_Guarantee) ,
                new FileFieldMapping ("REACH_Guarantee",certificateName.REACH_Guarantee) ,
                new FileFieldMapping ("SVHC_Guarantee",certificateName.SVHC_Guarantee )

            };
            return fieldmappping;
        }
        /// <summary>
        /// 获取供应商信息表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
       public  List<SupplierInfoModel> GetSupplierInformationDatasBy(string startYearMonth, string endYearMonth)
        {
            List<SupplierInfoModel> SupplierInfoDatas = new List<SupplierInfoModel>();
            SupplierInfoModel m = null;
            ///从ERP中得到此年中最新 所有供应商Id号
            var supplierListFromErp = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(startYearMonth, endYearMonth);
            ///对每一个供应商收集信息
            if (supplierListFromErp == null || supplierListFromErp.Count <= 0) return null;
            supplierListFromErp.ForEach(supplierId =>
            {
                m = GetSuppplierInfoBy(supplierId);
                if (m != null && !SupplierInfoDatas.Contains(m))
                    SupplierInfoDatas.Add(m);
            });
            return SupplierInfoDatas;
        }

        /// <summary>
        /// 从ERP中获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SupplierInfoModel GetSuppplierInfoFromErpBy(string supplierId)
        {
            var erpSupplierInfo = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);
            string purchaseUser =  PurchaseDbManager.PurchaseDb.PurchaseUserBy(supplierId);
            if (erpSupplierInfo == null) return null;
                string principal = erpSupplierInfo.Principal.Trim() == string.Empty ? erpSupplierInfo.Contact : erpSupplierInfo.Principal;
                return  new SupplierInfoModel
                {
                    SupplierId = supplierId.Trim(),
                    SupplierEmail = erpSupplierInfo.Email,
                    SupplierAddress = erpSupplierInfo.Address,
                    //采购人员
                    PurchaseUser= purchaseUser,
                    // 负责人
                    SupplierPrincipal = principal.Trim(),
                    SupplierFaxNo = erpSupplierInfo.FaxNo,
                    SupplierName = erpSupplierInfo.SupplierName,
                    SupplierShortName = erpSupplierInfo.SupplierShortName,
                    SupplierUser = erpSupplierInfo.Contact,
                    SupplierTel = erpSupplierInfo.Tel,
                    PayCondition = erpSupplierInfo.PayCondition,
                    IsCooperate = erpSupplierInfo.IsCooperate
                };
        }

        /// <summary>
        /// 得到所需的证书字典
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private Dictionary<string, string> CertificateDictionary(string supplierId, List<SupplierQualifiedCertificateModel> QualifiedCertificateDatas)
        {
            Dictionary<string, string> certificateDictionary = new Dictionary<string, string>();
            certificateDictionary.Add(certificateName.EnvironmentalInvestigation, string.Empty);
            certificateDictionary.Add(certificateName.QualityAssuranceProtocol, string.Empty);
            certificateDictionary.Add(certificateName.SupplierBaseDocument, string.Empty);
            certificateDictionary.Add(certificateName.SupplierComment, string.Empty);
            certificateDictionary.Add(certificateName.NotUseChildLabor, string.Empty);
            certificateDictionary.Add(certificateName.PCN_Protocol, string.Empty);
            certificateDictionary.Add(certificateName.HonestCommitment, string.Empty);
            certificateDictionary.Add(certificateName.HSF_Guarantee, string.Empty);
            certificateDictionary.Add(certificateName.REACH_Guarantee, string.Empty);
            certificateDictionary.Add(certificateName.SVHC_Guarantee, string.Empty);
            certificateDictionary.Add(certificateName.ISO14001, string.Empty);
            certificateDictionary.Add(certificateName.ISO9001, string.Empty);
            if (QualifiedCertificateDatas != null || QualifiedCertificateDatas.Count > 0)
            {
                QualifiedCertificateDatas.ForEach(e =>
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
        private SuppliersSumInfoVM GetSuppliersInfoAddrLatestTwoPurchaseInfo(SupplierInfoModel supplierInfo)
        {
            //// 获取供应商证书字典
            // var certificateDictionary = CertificateDictionary(supplierInfo.SupplierId);
            SuppliersSumInfoVM returnData = new SuppliersSumInfoVM();
            OOMaper.Mapper<SupplierInfoModel, SuppliersSumInfoVM>(supplierInfo, returnData);
            returnData.QualifiedCertificateDatas = GetSupplierQualifiedCertificateListBy(supplierInfo.SupplierId);
            var  LatestTwoPurchaseModel = LatestTwoPurchaseData(supplierInfo.SupplierId);
            OOMaper.Mapper<SupplierLatestTwoPurchaseCell, SuppliersSumInfoVM>(LatestTwoPurchaseModel, returnData);
            return returnData;
        }
        /// <summary>
        /// 得到最后二次采购的日期和采购人员
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private SupplierLatestTwoPurchaseCell LatestTwoPurchaseData(string supplierId)
        {
            SupplierLatestTwoPurchaseCell LatestTwoPurchaseModel = new SupplierLatestTwoPurchaseCell();
            //从ERP中得到最新二次采购信息
            var supplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierId);
            if (supplierLatestTwoPurchase != null)
            {
                LatestTwoPurchaseModel.LastPurchaseDate = supplierLatestTwoPurchase[0].PurchaseDate.Trim().ToDate();
                LatestTwoPurchaseModel.UpperPurchaseDate = supplierLatestTwoPurchase[1].PurchaseDate.Trim().ToDate();
                LatestTwoPurchaseModel.PurchaseUser = supplierLatestTwoPurchase[0].PurchasePerson.Trim();
            }
            return LatestTwoPurchaseModel;
        }
        #endregion
    }

    public class certificateName
    {
        /// <summary>
        /// 供应商环境调查表
        /// </summary>
        public const string EnvironmentalInvestigation = "供应商环境调查表";
        /// <summary>
        /// 供应商基本资料表
        /// </summary>
        public const string SupplierBaseDocument = "供应商基本资料表";
        /// <summary>
        /// 供应商评鉴表
        /// </summary>
        public const string SupplierComment = "供应商评鉴表";
        /// <summary>
        /// 不使用童工申明
        /// </summary>
        public const string NotUseChildLabor = "不使用童工申明";
        /// <summary>
        /// PCN协议
        /// </summary>
        public const string PCN_Protocol = "PCN协议";
        /// <summary>
        /// 廉洁承诺书
        /// </summary>
        public const string HonestCommitment = "廉洁承诺书";
        /// <summary>
        /// 质量保证协议
        /// </summary>
        public const string QualityAssuranceProtocol = "质量保证协议";
        /// <summary>
        /// HSF保证书
        /// </summary>
        public const string HSF_Guarantee = "HSF保证书";
        /// <summary>
        /// REACH保证书
        /// </summary>
        public const string REACH_Guarantee = "REACH保证书";
        /// <summary>
        /// SVHC调查表
        /// </summary>
        public const string SVHC_Guarantee = "SVHC调查表";
        /// <summary>
        /// ISO14001
        /// </summary>
        public const string ISO9001 = "ISO14001";
        /// <summary>
        /// ISO9001
        /// </summary>
        public const string ISO14001 = "ISO9001";
    }
}

