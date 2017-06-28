using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
   internal class ItilEmailFactory
    {
        public static ItilEmailManageCrud ItilEmailManageCrud
        {
            get { return OBulider.BuildInstance<ItilEmailManageCrud>(); }
        }
    }

    internal class ItilEmailManageCrud:CrudBase<ItilEmailManageModel, IItilEmailManageRepository>
    {
      public ItilEmailManageCrud():base(new ItilEmailManageRepository(),"邮箱管理")
        {

        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add,Add);
            this.AddOpItem(OpMode.Edit, Edit);
            this.AddOpItem(OpMode.Delete, DeleteEmailRecord);
        }

        private OpResult DeleteEmailRecord(ItilEmailManageModel model)
        {
            return irep.Update(m => m.Id_Key == model.Id_Key,
                f => new ItilEmailManageModel
                {
                    OpDate = model.OpDate,
                    OpTime = model.OpTime,
                    OpPerson = model.OpPerson,
                    OpSign = model.OpSign
                }).ToOpResult_Delete(OpContext);
        }

        private OpResult Edit(ItilEmailManageModel model)
        {
            return irep.Update(m=>m.Id_Key==model.Id_Key,model).ToOpResult_Eidt(OpContext);
        }

        private OpResult Add(ItilEmailManageModel model)
        {
            if(irep.IsExist(m=>m.Email.Equals(model.Email)))
            {
                return OpResult.SetErrorResult("亲，邮箱帐号己存在！无法新增");

            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
              
        }

        public List<ItilEmailManageModel>FindBy(ItilEmailManageModelDto dto)
        {
            if (dto== null) return new List<ItilEmailManageModel>();
            try
            {
                switch (dto.SearchMode)
                {
                    case 1:
                        return irep.Entities.Where(m => m.WorkerId == dto.WorkerId).ToList();
                        
                    case 2:
                        return irep.Entities.Where(m => m.Email == dto.Email ).ToList();
                       
                    default:
                        return new List<ItilEmailManageModel>();
                        
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
