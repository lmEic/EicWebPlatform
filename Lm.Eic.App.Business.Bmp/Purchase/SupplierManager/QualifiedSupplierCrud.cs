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
  internal class QualifiedSupplierCrudFactory
    {
        public static QualifiedSupplierCrud QualifiedSupplierCrud
        {
            get { return OBulider.BuildInstance<QualifiedSupplierCrud>(); }
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
    public  OpResult Add(QualifiedSupplierModel model)
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
    public OpResult SavaQualifiedSupplierInfoList(List<QualifiedSupplierModel> modelList)
    {
        try
        {
            DateTime date = DateTime.Now.ToDate();
            SetFixFieldValue(modelList, OpMode.Add, m =>
            {
                m.Opdate = date;
                m.SuperlierEligibleprojectsDate = "imyyyyy";
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
     /// <param name="department">部门</param>
     /// <returns></returns>
     public List<QualifiedSupplierModel> GetQualifiedSupplierListBy(string suppliersId)
     {
         try
         {
             return irep.Entities.Where(m => m.SuppliersId == suppliersId).ToList();
         }
         catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
     }


 }
}
