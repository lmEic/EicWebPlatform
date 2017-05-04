﻿using System;
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
    internal class CertificateManagerFactory
    {
        /// <summary>
        /// 供应商考核管理
        /// </summary>
        public static SupplierCertificateManager SupplierCertificateManager
        {
            get { return OBulider.BuildInstance<SupplierCertificateManager>(); }
        }
    }
    /// <summary>
    /// 供应商证书管理
    /// </summary>
    public class SupplierCertificateManager
    {

        /// <summary>
        /// 从ERP中获取年份合格供应商清册表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        public List<EligibleSuppliersModel> GetQualifiedSupplierList(string endYearMonth)
        {
            var QualifiedSupplierInfo = new List<EligibleSuppliersModel>();
            EligibleSuppliersModel model = null;
            string startYearMonth = (int.Parse(endYearMonth) - 100).ToString();
            //获取供应商信息
            var supplierInfoList = GetSupplierInformationListBy(startYearMonth, endYearMonth);
            if (supplierInfoList == null || supplierInfoList.Count <= 0) return QualifiedSupplierInfo;
            supplierInfoList.ForEach(supplierInfo =>
            {
                model = getEligibleSuppliersModel(supplierInfo);
                if (model != null && model.Remark == "True")
                    QualifiedSupplierInfo.Add(model);
            });
            return QualifiedSupplierInfo.OrderBy(e => e.SupplierId).ToList();
        }



        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SupplierInfoModel GetSuppplierInfoBy(string supplierId)
        {
            try
            {

                SupplierInfoModel SupplierInfo = SupplierCrudFactory.SuppliersInfoCrud.GetSupplierInfoBy(supplierId);
                if (SupplierInfo == null)
                {
                    SupplierInfo = GetErpSuppplierInfoBy(supplierId);
                    //添加至供应商信息表中
                    SupplierCrudFactory.SuppliersInfoCrud.InitSupplierInfo(SupplierInfo);
                }
                return SupplierInfo;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }


        /// <summary>
        /// 保存编辑的供应商证书信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SaveSpplierCertificateData(InPutSupplieCertificateInfoModel model, string siteRootPath)
        {
            try
            {
                //判断列表是否为空
                OpResult reOpresult = OpResult.SetResult("没有进任何操作");
                if (model == null) return OpResult.SetResult("数据列表不能为空");
                //通过SupplierId得到供应商信息
                // var supplierInfoModel = GetSuppplierInfoBy(model.SupplierId);
                var supplierInfoModel = GetErpSuppplierInfoBy(model.SupplierId);
                //判断是否为空
                if (supplierInfoModel == null) return OpResult.SetResult(string.Format("没有{0}供应商编号", model.SupplierId), true);
                //赋值 供应商属性和采购性质
                supplierInfoModel.PurchaseType = model.PurchaseType;
                supplierInfoModel.SupplierProperty = model.SupplierProperty;
                if (model.OpSign == "editPurchaseType")//修改证书类别信息
                    SupplierCrudFactory.SuppliersInfoCrud.UpSupplierInfo(supplierInfoModel);


                if (model.CertificateFileName == null || model.CertificateFileName == string.Empty) return OpResult.SetResult("证书名称不能为空");
                SupplierQualifiedCertificateModel savemodel = new SupplierQualifiedCertificateModel()
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
                return StoreSpplierCertificateData(savemodel, siteRootPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
        /// <summary>
        /// 删除供应商证书
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="rootPath">根路经</param>
        /// <returns></returns>
        public OpResult StoreSpplierCertificateData(SupplierQualifiedCertificateModel model, string siteRootPath)
        {
            try
            {
                if (model != null && model.OpSign == OpMode.DeleteFile)//如果删除文件则启文件相应的删除处理
                    return SupplierCrudFactory.SupplierQualifiedCertificateCrud.DeleteFileSupplierQualifiedCertificate(model, siteRootPath);

                return SupplierCrudFactory.SupplierQualifiedCertificateCrud.StoreSupplierQualifiedCertificate(model);

            }
            catch (Exception ex)
            {
                return new OpResult("操作出现异常", false);
                throw new Exception(ex.InnerException.Message);
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
        /// 生成合格供应商清单
        /// </summary>
        /// <returns></returns>
        public MemoryStream BuildQualifiedSupplierInfoList(List<EligibleSuppliersModel> datas)
        {
            try
            {
                if (datas == null || datas.Count < 0) return null;
                var dataGroupping = datas.GetGroupList<EligibleSuppliersModel>("");
                return dataGroupping.ExportToExcelMultiSheets<EligibleSuppliersModel>(CreateFieldMapping());
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
                new FileFieldMapping ("SupplierName","供应商全称") ,
                new FileFieldMapping ("SupplierProperty","供应商属性") ,
                new FileFieldMapping ("SupplierTel","供应商电话") ,
                new FileFieldMapping ("SupplierFaxNo","供应商传真") ,
                new FileFieldMapping ("SupplierEmail","供应商邮箱") ,
                new FileFieldMapping ("SupplierAddress","供应商地址") ,
                new FileFieldMapping ("BillAddress","供应商联系人") ,
                new FileFieldMapping ("PurchaseUser","采购人员") ,
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


        #region   internal


        /// <summary>
        /// 获取供应商信息表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        List<SupplierInfoModel> GetSupplierInformationListBy(string startYearMonth, string endYearMonth)
        {
            List<SupplierInfoModel> SupplierInfoList = new List<SupplierInfoModel>();
            //从ERP中得到此年中所有供应商Id号
            var supplierList = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(startYearMonth, endYearMonth);

            if (supplierList == null || supplierList.Count <= 0) return null;
            supplierList.ForEach(supplierId =>
            {
                var model = GetSuppplierInfoBy(supplierId);
                if (model != null && model.Remark == "True")
                    SupplierInfoList.Add(GetSuppplierInfoBy(supplierId));
            });
            return SupplierInfoList;
        }
        /// <summary>
        /// <summary>
        /// 批量保存供应商信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        OpResult SaveSupplierInfoList(List<SupplierInfoModel> modelList)
        {
            return SupplierCrudFactory.SuppliersInfoCrud.SavaSupplierInfoList(modelList);
        }

        /// <summary>
        /// 更新并保存供应商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OpResult SaveSupplierInfoModel(SupplierInfoModel model)
        {

            try
            {
                decimal findId_key = 0;
                if (SupplierCrudFactory.SuppliersInfoCrud.IsExistSupperid(model.SupplierId, out findId_key))
                {
                    model.OpSign = "edit";
                    model.Id_Key = findId_key;
                }
                else model.OpSign = "add";

                return SupplierCrudFactory.SuppliersInfoCrud.Store(model);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }



        }
        /// <summary>
        /// 从ERP中得到供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        SupplierInfoModel GetErpSuppplierInfoBy(string supplierId)
        {
            var erpSupplierInfo = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);

            if (erpSupplierInfo == null) return null;
            return new SupplierInfoModel
            {
                SupplierId = supplierId,
                SupplierEmail = erpSupplierInfo.Email,
                SupplierAddress = erpSupplierInfo.Address,
                ///交货地址不需要，可以填充 供应商联系人
                BillAddress = erpSupplierInfo.Contact,
                SupplierFaxNo = erpSupplierInfo.FaxNo,
                SupplierName = erpSupplierInfo.SupplierName,
                SupplierShortName = erpSupplierInfo.SupplierShortName,

                ///供应商联系人 就是ERP中的负责人
                SupplierUser = erpSupplierInfo.Principal,
                ///采购人员 占时为空
                ///PurchaseUser= erpSupplierInfo.Contact,
                SupplierTel = erpSupplierInfo.Tel,
                PayCondition = erpSupplierInfo.PayCondition,
                /// 备注 作为是不是有效的记录
                Remark = erpSupplierInfo.IsValidity.ToString()
            };
        }

        /// <summary>
        /// 得到所需的证书字典
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Dictionary<string, string> CertificateDictionary(string supplierId)
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

            var SuppliersQualifiedCertificate = GetSupplierQualifiedCertificateListBy(supplierId);
            if (SuppliersQualifiedCertificate == null || SuppliersQualifiedCertificate.Count > 0)
            {
                SuppliersQualifiedCertificate.ForEach(e =>
               {
                   if (certificateDictionary.ContainsKey(e.EligibleCertificate))
                   {
                       certificateDictionary[e.EligibleCertificate] = e.DateOfCertificate.ToShortDateString();
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
        EligibleSuppliersModel getEligibleSuppliersModel(SupplierInfoModel supplierInfo)
        {
            //从ERP中得到最新二次采购信息
            var SupplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierInfo.SupplierId);
            // 获取供应商证书字典
            var certificateDictionary = CertificateDictionary(supplierInfo.SupplierId);
            return new EligibleSuppliersModel
            {
                LastPurchaseDate = SupplierLatestTwoPurchase[0].PurchaseDate.Trim().ToDate(),
                UpperPurchaseDate = SupplierLatestTwoPurchase[1].PurchaseDate.Trim().ToDate(),
                PurchaseUser = SupplierLatestTwoPurchase[0].PurchasePerson,
                SupplierId = supplierInfo.SupplierId,
                SupplierProperty = supplierInfo.SupplierProperty,
                PurchaseType = supplierInfo.PurchaseType,
                SupplierEmail = supplierInfo.SupplierEmail,
                SupplierAddress = supplierInfo.SupplierAddress,
                BillAddress = supplierInfo.BillAddress,
                SupplierFaxNo = supplierInfo.SupplierFaxNo,
                SupplierName = supplierInfo.SupplierName,
                Remark = supplierInfo.Remark,
                SupplierShortName = supplierInfo.SupplierShortName,
                SupplierUser = supplierInfo.SupplierUser,
                SupplierTel = supplierInfo.SupplierTel,
                EnvironmentalInvestigation = certificateDictionary["供应商环境调查表"],
                HonestCommitment = certificateDictionary["廉洁承诺书"],
                HSF_Guarantee = certificateDictionary["HSF保证书"],
                ISO14001 = certificateDictionary["ISO14001"],
                ISO9001 = certificateDictionary["ISO9001"],
                NotUseChildLabor = certificateDictionary["不使用童工申明"],
                PCN_Protocol = certificateDictionary["PCN协议"],
                QualityAssuranceProtocol = certificateDictionary["质量保证协议"],
                REACH_Guarantee = certificateDictionary["REACH保证书"],
                SupplierBaseDocument = certificateDictionary["供应商基本资料表"],
                SupplierComment = certificateDictionary["供应商评鉴表"],
                SVHC_Guarantee = certificateDictionary["SVHC调查表"],
            };
        }


        #endregion
    }

}

