using Lm.Eic.App.DbAccess.Bpm.Repository.WarehouseRep.ExpressRep;
using Lm.Eic.App.DomainModel.Bpm.Warehouse;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Warehouse
{
  public   class WarehouseCrudFactory
    {
        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        internal static ExpressModelCrud ExpressCrud
        {
            get { return OBulider.BuildInstance<ExpressModelCrud>(); }
        }
        internal class ExpressModelCrud : CrudBase<ExpressModel, IExpressRepository>
        {
            public ExpressModelCrud() : base(new ExpressRepository(), "快递收发系统")
            {
            }
            protected override void AddCrudOpItems()
            {
                this.AddOpItem(OpMode.Add, AddModel);
                this.AddOpItem(OpMode.Edit, EidtModel);
                this.AddOpItem(OpMode.Delete, DeleteModel);
            }
            private OpResult AddModel(ExpressModel model)
            {
                if( irep.IsExist(e => e.ExpressId == model.ExpressId))
                {
                    OpResult returnResult = OpResult.SetErrorResult("此单号已存在"); ;
                     returnResult.Entity = GetEntriyBy(model.ExpressId);
                    return returnResult;
                }
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            internal OpResult IsExsitEntriy(string expressId)
            {
                return (irep.IsExist(e => e.ExpressId == expressId))?
                    OpResult.SetErrorResult("此单号已存在"):
                    OpResult.SetSuccessResult("此单号不存在");
            }
            internal ExpressModel GetEntriyBy(string expressId)
            {
                return irep.FirstOfDefault(e => e.ExpressId == expressId);
            }
            private OpResult EidtModel(ExpressModel model)
            {
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            private OpResult DeleteModel(ExpressModel model)
            {
                return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            }
        }
    }
}
