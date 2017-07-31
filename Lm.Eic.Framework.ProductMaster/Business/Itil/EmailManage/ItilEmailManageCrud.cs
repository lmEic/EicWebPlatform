using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
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

    internal class ItilEmailManageCrud : CrudBase<Model.ITIL.ItilEmailManageModel, IItilEmailManageRepository>
    {
        public ItilEmailManageCrud() : base(new ItilEmailManageRepository(), "邮箱管理")
        {

        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Edit);
            this.AddOpItem(OpMode.Delete,DeleteRecord);
        }

        private OpResult DeleteRecord(Model.ITIL.ItilEmailManageModel model)
        {
            DicDatas.Remove(Ckey);
            return irep.Delete(e => e.Email == model.Email).ToOpResult_Delete(OpContext);
        }

        private OpResult Edit(Model.ITIL.ItilEmailManageModel model)
        {
            return irep.Update(m=>m.Id_Key==model.Id_Key,model).ToOpResult_Eidt(OpContext);
        }

        private OpResult Add(Model.ITIL.ItilEmailManageModel model)
        {
            if(irep.IsExist(m=>m.Email.Equals(model.Email)))
            {
                return OpResult.SetErrorResult("亲，邮箱帐号己存在！无法新增");

            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
              
        }
        /// <summary>
        /// 键值
        /// </summary>
        string Ckey = "email";
        public static Dictionary<string, List<Model.ITIL.ItilEmailManageModel>> DicDatas = new Dictionary<string, List<Model.ITIL.ItilEmailManageModel>>();    
        /// <summary>
        /// 1、按工号查询 2、按帐号查询 3、按等级查询 4、按部门查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public List<Model.ITIL.ItilEmailManageModel> FindBy(ItilEmailManageModelDto dto)
        {
            if (dto== null) return new List<Model.ITIL.ItilEmailManageModel>();

            if (!DicDatas.ContainsKey(Ckey))
            {
                DicDatas[Ckey] = irep.Entities.ToList();
            }
            try
            {
                switch (dto.SearchMode)
                {
                    case 1:
                        return irep.Entities.Where(m => m.WorkerId == dto.WorkerId).ToList();
                        
                    case 2:
                        return irep.Entities.Where(m => m.Email == dto.Email ).ToList();
                    case 3:
                        // return irep.Entities.Where(m => m.ReceiveGrade == dto.ReceiveGrade).ToList();
                        return DicDatas[Ckey].Where(m => m.ReceiveGrade == dto.ReceiveGrade).ToList();
                    case 4:
                        // return irep.Entities.Where(m => m.Department == dto.Department).ToList();
                        return DicDatas[Ckey].Where(m => m.Department == dto.Department).ToList();
                    default:
                        return new List<Model.ITIL.ItilEmailManageModel>();
                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
