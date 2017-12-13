using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.LeaveAsk;
using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.LeaveAsk
{
   internal class LeaveAskFactory
    {
        /// <summary>
        /// 请假管理工厂
        /// </summary>
        public static LeaveAskCrud LeaveAskCrud
        {
            get { return OBulider.BuildInstance<LeaveAskCrud>(); }
        }


    }
    internal class LeaveAskCrud:CrudBase<LeaveAskManagerModels,ILeaveAskManagerRepository>
    {
        public LeaveAskCrud() : base(new LeaveAskRepository(), "请假管理")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddLeaveAsk);
            this.AddOpItem(OpMode.Edit, EditLeaveAsk);
            this.AddOpItem(OpMode.Delete, DeleteLeaveAsk);
        }

        private OpResult DeleteLeaveAsk(LeaveAskManagerModels model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EditLeaveAsk(LeaveAskManagerModels model)
        {
            return irep.Update(k => k.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddLeaveAsk(LeaveAskManagerModels model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// 1、按部门查询
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        internal List<LeaveAskManagerModels>FindBy(LeaveAskManagerModelDto Dto)
        {
            if (Dto == null) return new List<LeaveAskManagerModels>();
            try
            {
                switch (Dto.SearchMode)
                {
                    case 1:
                    return irep.Entities.Where(k => k.WorkerId == Dto.WorkerId && k.Department==Dto.Department).ToList();
                        
                    default:
                        return new List<LeaveAskManagerModels>();                      
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
