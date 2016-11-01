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
                return OpResult.SetResult("日报列表不能为空！ 保存失败");
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
 /// 供应商合格文件CRUD
  /// </summary>
 public class SupplierEligibleCrud:CrudBase <SupplierEligibleModel,ISupplierEligibleRepository >
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
      public OpResult SavaSupplierEligible(SupplierEligibleModel model)
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
     /// 获得供应商合格文件项目
     /// </summary>
     /// <param name="supplierId"></param>
     /// <returns></returns>
     public List<SupplierEligibleModel> GetEligibleItemsBy(string supplierId)
      {
          try
          {
              return irep.Entities.Where(m => m.SuppliersID == supplierId).ToList();
          }
          catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
      }
  }


/// <summary>
 /// 供应商信息
/// </summary>
 public class SuppliersInfoCrud:CrudBase <SupplierInfoModel,ISupplierInfoRepository >
{

    public SuppliersInfoCrud()
        : base(new SupplierInfoRepository(), "供应商信息")
      {}
 
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      protected override void AddCrudOpItems()
      { }

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
       /// <summary>
      /// 批量更新供应商信息
     /// </summary>
     /// <param name="modelList"></param>
     /// <returns></returns>
      public OpResult UpdateSupplierInfoList(List<SupplierInfoModel> modelList)
      { 
          int i=0;
          if (!modelList.IsNullOrEmpty())
              return OpResult.SetResult("列表不能为空！ 保存失败");
           modelList .ForEach (m=>{
               if (irep.IsExist(e => e.SupplierId == m.SupplierId))
               {
                   i += irep.Update(e => e.SupplierId == m.SupplierId, m);
               }
               else i += irep.Insert(m);
           });
          return i.ToOpResult_Eidt(OpContext);

      }
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
/// 
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
     { }
 }

}
