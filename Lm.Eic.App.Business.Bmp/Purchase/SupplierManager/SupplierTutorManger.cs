
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
 
    /// <summary>
    /// 供应商辅导\计划管理
    /// </summary>
public     class SupplierTutorManger
    {

        /// <summary>
        /// 得到考核分数低80分的供应商信息
        /// </summary>
        /// <param name="seasonDateNum"></param>
        /// <returns></returns>
        public List<SupplierSeasonTutorModel> GetWaittingTourSupplier(string seasonDateNum)
        {
            List<SupplierSeasonTutorModel> waittingTourSupplierList = new List<SupplierSeasonTutorModel>();
            List<SupplierSeasonAuditModel> auditNewModelLsit = new List<SupplierSeasonAuditModel>();
            //得到低于80分的所以供应商
            var auditModelLsit = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetlimitScoreSupplierAuditInfo(seasonDateNum, 80);
            if (auditModelLsit!=null&&auditModelLsit .Count >0)
            {
                auditModelLsit.ForEach(m =>
                {
                   if( SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(m.ParameterKey))
                    { waittingTourSupplierList.Add(SupplierCrudFactory.SuppliersSeasonTutorCrud.GetSupplierSeasonTutorModelBy(m.ParameterKey));}
                   else
                    { auditNewModelLsit.Add(m); }
                });
            }
            return waittingTourSupplierList.Union(OOMaper.Mapper<SupplierSeasonAuditModel, SupplierSeasonTutorModel>(auditNewModelLsit).ToList()).ToList ();
        }
        /// <summary>
        /// 保存供应商辅导\计划管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreSupplierTutorModel(SupplierSeasonTutorModel model)
        {
            if (SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(model.ParameterKey))
                model.OpSign = "edit";
            else model.OpSign = "add";
            return SupplierCrudFactory.SuppliersSeasonTutorCrud.Store(model);
        }

        


    }
}
