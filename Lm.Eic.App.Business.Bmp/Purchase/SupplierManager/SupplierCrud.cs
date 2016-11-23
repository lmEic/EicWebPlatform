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
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement;


namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
 internal class SupplierCrudFactory
    {
       /// <summary>
       /// 合格供应商清册CRUD
       /// </summary>
       public static QualifiedSupplierCrud QualifiedSupplierCrud
        {
            get { return OBulider.BuildInstance<QualifiedSupplierCrud>(); }
        }     
       /// <summary>
       /// 供应商合格文件CRUD
       /// </summary>
      public static SupplierEligibleCrud SupplierEligibleCrud
      {
          get { return OBulider.BuildInstance<SupplierEligibleCrud>(); }
      }
       /// <summary>
       /// 供应商信息
       /// </summary>
       public static SuppliersInfoCrud SuppliersInfoCrud
      {
          get { return OBulider.BuildInstance<SuppliersInfoCrud>();}
      }
      /// <summary>
       /// 供应商季度审计考核表
      /// </summary>
       public static SuppliersSeasonAuditCrud  SuppliersSeasonAuditCrud
       {
           get { return OBulider.BuildInstance<SuppliersSeasonAuditCrud>(); }
       }
    }

 /// <summary>
 /// 合格供应商清册CRUD
 /// </summary>
 public class QualifiedSupplierCrud : CrudBase<QualifiedSupplierModel, IQualifiedSupplierRepository>
 {
     public QualifiedSupplierCrud()
         : base(new QualifiedSupplierRepository(), "合格供应商录入")
     { }

     /// 
     /// </summary>
     /// <param name="model"></param>
     /// <returns></returns>
     protected override void AddCrudOpItems()
     { 

     }
     /// <summary>
     /// / 添加一条合格供应商的记录
     /// </summary>
     /// <param name="model"></param>
     /// <returns></returns>
     public OpResult SavaQualifiedSupplier(QualifiedSupplierModel model)
     {
         ///判断产品品号是否存在
         try
         {
             model.OpSign = OpMode.Add;
             SetFixFieldValue(model);
             return irep.Insert(model).ToOpResult_Add(OpContext);
         }
         catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

     }
    /// <summary>
     /// 批量保存供应商的记录
    /// </summary>
    /// <param name="modelList"></param>
    /// <returns></returns>
    public OpResult SavaQualifiedSupplierInfoList(List<QualifiedSupplierModel> modelList)
    {
        try
        {
            DateTime date = DateTime.Now.ToDate();
            SetFixFieldValue(modelList, OpMode.Add, m =>
            {
                m.OpDate = date;
                //需要添加附加答条件
            });

            if (!modelList.IsNullOrEmpty())
                return OpResult.SetResult("供应商的列表不能为空！ 保存失败");
            return irep.Insert(modelList).ToOpResult_Add(OpContext);
        }
        catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
    }
     /// <summary>
     /// 获取供应商合格的列表
     /// </summary>
    /// <param name="supplierId">供应商ID</param>
     /// <returns></returns>
     public List<QualifiedSupplierModel> GetQualifiedSupplierListBy(string supplierId)
     {
         try
         {
             return irep.Entities.Where(m => m.SupplierId == supplierId).ToList();
         }
         catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
     }


 }
  /// <summary>
 /// 供应商合格证书CRUD
  /// </summary>
 public class SupplierEligibleCrud:CrudBase <SupplierEligibleCertificateModel,ISupplierEligibleRepository >
  {
      public SupplierEligibleCrud():base(new SupplierEligibleRepository() ,"供应商合格文件录入")
      {}
      /// 
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      protected override void AddCrudOpItems()
      { }
      /// <summary>
      /// / 添加一条供应商的合格文件记录
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public OpResult SavaSupplierEligible(SupplierEligibleCertificateModel model)
      {
          try
          {
              model.OpSign = OpMode.Add;
              SetFixFieldValue(model);
              return irep.Insert(model).ToOpResult_Add(OpContext);
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

      }
     /// <summary>
      /// 批量保存供应商的合格文件记录
     /// </summary>
     /// <param name="modelList"></param>
     /// <returns></returns>
      public OpResult SavaSupplierEligibleList(List<SupplierEligibleCertificateModel> modelList)
      {
      
          try
          {
              DateTime date = DateTime.Now.ToDate();
              SetFixFieldValue(modelList, OpMode.Add, m =>
              {
                  m.OpDate = date;
                  //需要添加附加答条件
              });

              if (!modelList.IsNullOrEmpty())
                  return OpResult.SetResult("合格文件记录列表不能为空！ 保存失败");
              return irep.Insert(modelList).ToOpResult_Add(OpContext);
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

      }

      
    
     /// <summary>
     /// 获得供应商合格文件项目
     /// </summary>
     /// <param name="supplierId"></param>
     /// <returns></returns>
     public List<SupplierEligibleCertificateModel> GetEligibleItemsBy(string supplierId)
      {
          try
          {
              return irep.Entities.Where(m => m.SupplierId  == supplierId).ToList();
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
      }
  }


    /// <summary>
    /// 供应商信息
    /// </summary>
    public class SuppliersInfoCrud : CrudBase<SupplierInfoModel, ISupplierInfoRepository>
    {

        public SuppliersInfoCrud()
            : base(new SupplierInfoRepository(), "供应商信息")
        { }

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierInfo);
            this.AddOpItem(OpMode.Edit, EidtSupplierInfo);
            this.AddOpItem(OpMode.Delete, DeleteSupplierInfo);
        }

        public bool IsExistSupperid(string supplierId, out decimal findId_key)
        {
            if (irep.IsExist(e => e.SupplierId == supplierId))
            {
                findId_key = irep.Entities .Where (e=>e.SupplierId ==supplierId ).ToList ().FirstOrDefault().Id_key;
                return true;
            }
            else
            { findId_key = 0;  return false; }
        }
       
      /// <summary>
      /// 批量保存供应商信息
      /// </summary>
      /// <param name="modelList"></param>
      /// <returns></returns>
      public OpResult SavaSupplierInfoList(List<SupplierInfoModel> modelList)
      {
          try
          {
              DateTime date = DateTime.Now.ToDate();
              SetFixFieldValue(modelList, OpMode.Add, m =>
              {
                  m.OpDate = date;
                  //需要添加附加答条件
              });
              ///如查SupplierID号存在  
              if (!modelList.IsNullOrEmpty())
                  return OpResult.SetResult("列表不能为空！ 保存失败");
          
            //return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("修改完成");
             
              return irep.Insert(modelList).ToOpResult_Add(OpContext);
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
      }
      #region  Store
      /// <summary>
       /// 添加供应商信息
      /// </summary>
      /// <param name="model></param>
      /// <returns></returns>
      OpResult AddSupplierInfo(SupplierInfoModel model)
      {

          ///判断产品品号是否存在
          try
          {
              if (irep.IsExist(m => m.Id_key == model.Id_key))
              {
                  return OpResult.SetResult("此数据已存在！");
              }
              SetFixFieldValue(model);
              return irep.Insert(model).ToOpResult_Add(OpContext);
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

      }

      OpResult EidtSupplierInfo(SupplierInfoModel model)
      {
          if (irep.IsExist(m => m.Id_key  == model.Id_key ))
          {
              return irep.Update(m => m.Id_key == model.Id_key, model).ToOpResult_Add("修改成功", model.Id_key);
             
          }
          else  return OpResult.SetResult("此数据不存在！无法修改");
         

      }

      OpResult DeleteSupplierInfo(SupplierInfoModel model)
      {
          return irep.Delete (model).ToOpResult_Add("删除成功", model.Id_key);
      }
      #endregion
      /// <summary>
      /// 获取供应商信息
      /// </summary>
      /// <param name="supplierId">供应商ID</param>
      /// <returns></returns>
      public SupplierInfoModel GetSupplierInfoBy(string supplierId)
      {
          try
          {
              return irep.Entities.Where(m => m.SupplierId == supplierId).ToList().FirstOrDefault ();
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
      }
}

/// <summary>
 /// 供应商季度审查表
/// </summary>
 public class SuppliersSeasonAuditCrud:CrudBase <SupplieSeasonAuditModel ,ISupplierSeasonAuditRepository>
 {
     public SuppliersSeasonAuditCrud()
         : base(new SupplierSeasonAuditRepository(), "供应商季度审计考核表")
     { }
     /// </summary>
     /// <param name="model"></param>
     /// <returns></returns>
     protected override void AddCrudOpItems()
     {
        
     }
     
 }

}
