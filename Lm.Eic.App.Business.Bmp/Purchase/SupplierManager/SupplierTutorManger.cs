
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

       
        public List<SupplierSeasonTutorModel> GetWaittingTourSupplier(string seasonDateNum)
        {

            List<SupplierSeasonTutorModel> waittingTourSupplier = new List<SupplierSeasonTutorModel>();
            var auditModelLsit = SupplierCrudFactory.SuppliersSeasonAuditCrud.GetlimitScoreSupplierAuditInfo(seasonDateNum, 80);
            List<SupplierSeasonAuditModel> auditNewModelLsit = new List<SupplierSeasonAuditModel>();
            if (auditModelLsit!=null&&auditModelLsit .Count >0)
            {
                auditModelLsit.ForEach(m =>
                {
                   if( SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(m.ParameterKey))
                    {
                        waittingTourSupplier.Add(SupplierCrudFactory.SuppliersSeasonTutorCrud.GetSupplierSeasonTutorModelBy(m.ParameterKey));
                    }
                   else
                    {
                        auditNewModelLsit.Add(m);
                    }
                });
            }
            return waittingTourSupplier.Union(OOMaper.Mapper<SupplierSeasonAuditModel, SupplierSeasonTutorModel>(auditNewModelLsit).ToList()).ToList ();
        }
        public OpResult StoreSupplierTutorModel(SupplierSeasonTutorModel model)
        {
            if (SupplierCrudFactory.SuppliersSeasonTutorCrud.IsExist(model.ParameterKey))
                model.OpSign = "edit";
            else model.OpSign = "add";
            return SupplierCrudFactory.SuppliersSeasonTutorCrud.Store(model);
            
        }

        


    }
}
