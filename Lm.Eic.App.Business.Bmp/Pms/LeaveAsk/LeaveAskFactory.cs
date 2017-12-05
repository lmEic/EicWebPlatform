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
        internal static LeaveAskCrud LeaveAskCrud
        {
            get { return OBulider.BuildInstance<LeaveAskCrud>(); }
        }


    }
    internal class LeaveAskCrud:CrudBase<LeaveAskModels,ILeaveAskManagerRepository>
    {
        public LeaveAskCrud() : base(new LeaveAskRepository(), "请假管理")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddLeaveAsk);
            this.AddOpItem(OpMode.Edit, EditLeaveAsk);
            this.AddOpItem(OpMode.Delete, DeleteLeaveAsk);
        }

        private OpResult DeleteLeaveAsk(LeaveAskModels model)
        {
            return irep.Delete(e => e.WorkerId == model.WorkerId && e.LeaveAskDate == model.LeaveAskDate).ToOpResult_Delete(OpContext);
        }

        private OpResult EditLeaveAsk(LeaveAskModels model)
        {
            return irep.Update(k => k.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddLeaveAsk(LeaveAskModels model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
    }
}
