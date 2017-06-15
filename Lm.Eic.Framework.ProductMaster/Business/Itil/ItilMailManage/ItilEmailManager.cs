using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using EmailCurdFactory = Lm.Eic.Framework.ProductMaster.Business.Itil.ItilMailManage.ItilEmailManageFactory;




namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{

   public class ItilEmailManager
    {
        List<ItilEmailManageModel> _EmailManageList = new List<ItilEmailManageModel>();
       
        /// <summary>
        /// 保存邮箱
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreEmailManage(ItilEmailManageModel model)
        {
            return EmailCurdFactory.ItilEmailCrud.Store(model);
        }
        /// <summary>
        /// 邮箱查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<ItilEmailManageModel> FindEmailRecords(ItilEmailManageDto dto)
        {
            _EmailManageList = EmailCurdFactory.ItilEmailCrud.FindBy(dto);
            return _EmailManageList;
        }

    }

   internal class ItilEmailCrud : CrudBase<ItilEmailManageModel, IItilEmailManageRepository>
    {
        public ItilEmailCrud() : base(new ItilEmailManageRepository(), "邮箱管理") { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddEmailManageRecord);
            this.AddOpItem(OpMode.Edit, EditEmailManageRecord);
            this.AddOpItem(OpMode.UpDate, UpDateEmailManageRecord);;
        }

        private OpResult UpDateEmailManageRecord(ItilEmailManageModel model)
        {
            OpResult result = OpResult.SetErrorResult("亲，未执行任何修改");
            if (model == null) return result;
            return result;
        }

        private OpResult EditEmailManageRecord(ItilEmailManageModel model)
        {
           return irep.Update(u=>u.Id_Key==model.Id_Key,model).ToOpResult_Eidt(model.Email.ToString());
        }

        private OpResult AddEmailManageRecord(ItilEmailManageModel model)
        {
               if (irep.IsExist(m => m.Email == model.Email))
                {
                  return OpResult.SetErrorResult("亲，添加邮箱帐号己存在！");
                }
                  return irep.Insert(model).ToOpResult_Add("邮箱用户",model.Id_Key);
        }
        public List<ItilEmailManageModel> FindBy(ItilEmailManageDto dto)
        {
            if (dto == null) return new List<ItilEmailManageModel>();
            try
            {
                switch (dto.SearchMode)
                {
                    case 1:
                        return irep.Entities.Where(m => m.WorkerId == dto.WorkerId).ToList();
                    case 2:
                        return irep.Entities.Where(m => m.Email == dto.Email).ToList();
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
