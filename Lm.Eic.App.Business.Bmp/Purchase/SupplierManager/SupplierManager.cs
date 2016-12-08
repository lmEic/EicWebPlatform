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
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
    public class PurSupplierManager
    {
        List<EligibleSuppliersModel> QualifiedSupplierInfo = null;
        /// <summary>
        /// 从ERP中获取年份合格供应商清册表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        public List<EligibleSuppliersModel> FindQualifiedSupplierList(string endYearMonth)
        {
            QualifiedSupplierInfo = new List<EligibleSuppliersModel>();
           
            string startYearMonth = (int.Parse(endYearMonth) - 100).ToString  ();
            //获取ERP供应商信息
            var supplierInfoList = GetSupplierInformationListBy(startYearMonth, endYearMonth);

            if (supplierInfoList == null || supplierInfoList.Count <= 0) return QualifiedSupplierInfo;

            supplierInfoList.ForEach(supplierInfo =>
            {
                //从ERP中得到最新二次采购信息
                var SupplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierInfo.SupplierId);
                // 获取供应商证书列表
                var SuppliersQualifiedCertificate = GetSupplierQualifiedCertificateListBy(supplierInfo.SupplierId);


                QualifiedSupplierInfo.Add(new EligibleSuppliersModel
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

                    EnvironmentalInvestigation ="",
                    HonestCommitment="",
                    HSF_Guarantee="",
                    ISO14001="",
                    ISO9001="",
                    NotUseChildLabor="",
                    PCN_Protocol="",
                    QualityAssuranceProtocol="",
                    REACH_Guarantee="",
                    SupplierBaseDocument="",
                    SupplierComment="",
                    SVHC_Guarantee="",
                });
            });
            return QualifiedSupplierInfo.ToList();
        }
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
        /// 保存编辑的供应商证书信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaEditSpplierCertificate(List<InPutSupplieCertificateInfoModel> modelList)
        {
            //判断列表是否为空
            if (modelList == null || modelList.Count <= 0) return new OpResult("数据列表不能为空");
            var model = modelList[0];
            //通过SupplierId得到供应商信息
            var supplierInfoModel = GetSuppplierInfoBy(model.SupplierId);
            //判断是否为空
            if (supplierInfoModel == null) return new OpResult(string.Format("没有{0}供应商编号", model.SupplierId), true);
            //赋值 供应商属性和采购性质
            supplierInfoModel.PurchaseType = model.PurchaseType;
            supplierInfoModel.SupplierProperty = model.SupplierProperty;
            string isExistCertificateFileName = string.Empty;
            //更新保存数据
            if (SaveSupplierInfoModel(supplierInfoModel).Result)
            {
                List<SupplierQualifiedCertificateModel> certificateModelList = new List<SupplierQualifiedCertificateModel>();
             
                //保存证书数据
                modelList.ForEach(e =>
                {

                   if(SupplierCrudFactory.SupplierQualifiedCertificateCrud.IsExistCertificateFileName(e.CertificateFileName))
                    {
                        isExistCertificateFileName += e.CertificateFileName + ",";
                    }
                    SupplierQualifiedCertificateModel savemodel = new SupplierQualifiedCertificateModel()
                    {
                        SupplierId = e.SupplierId,
                        EligibleCertificate = e.EligibleCertificate,
                        CertificateFileName =e.CertificateFileName,
                        FilePath = e.FilePath,
                        DateOfCertificate =e.DateOfCertificate.ToDate(),
                        IsEfficacy = "是",
                        OpSign = "add"
                    };
                    certificateModelList.Add(savemodel);
                });
                if (isExistCertificateFileName != String.Empty) return new OpResult("此"+isExistCertificateFileName+"文档已经存在，数据保存失败");
               else  return SupplierCrudFactory.SupplierQualifiedCertificateCrud.SavaSupplierEligibleList(certificateModelList);
            }
            else return new OpResult("数据保存失败");
        }
        /// <summary>
        /// 删除供应商证书
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="rootPath">根路经</param>
        /// <returns></returns>
        public OpResult DelEditSpplierCertificate(SupplierQualifiedCertificateModel model, string rootPath)
        {
            try
            {
                OpResult result = OpResult.SetResult("数据操作失败!");
                if (model == null || model.FilePath == string.Empty) return new  OpResult("此文档实体路经不能空");

                if (rootPath == null || rootPath == string.Empty) return  new OpResult("此根路经发生错误");

                var fileDocumentPath = rootPath + model.FilePath.Replace("/", @"\");
                if(! fileDocumentPath.ExistFile())
                     result= new OpResult("此" + fileDocumentPath + "文档不存在或路经不对");
                else
                {
                    if (fileDocumentPath.DeleteFileDocumentation())
                        result = SupplierCrudFactory.SupplierQualifiedCertificateCrud.DeleteSupplierCertificate(model);
                }
               
                return result;
            }
            catch (Exception ex)
            {
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
                    model.Id_key = findId_key;
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
                BillAddress = erpSupplierInfo.BillAddress,
                SupplierFaxNo = erpSupplierInfo.FaxNo,
                SupplierName = erpSupplierInfo.SupplierName,
                SupplierShortName = erpSupplierInfo.SupplierShortName,
                SupplierUser = erpSupplierInfo.Contact,
                SupplierTel = erpSupplierInfo.Tel,
                PayCondition = erpSupplierInfo.PayCondition
            };
        }



       

        #endregion
    }


    public class PurSuppliersSeasonAuditManger
    {

    }

}
