using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
 public   class ArWorkerIdChangeManager
    {
        ArWorkerIdChangeCurd crud = null;
       
        public ArWorkerIdChangeManager()
        {
            crud = new ArWorkerIdChangeCurd();
        }

        public OpResult StoreWorkerIdChangeInfo(WorkerChangedModel entity)
        {
            return this.crud.Store(entity);
        }
    }

internal class ArWorkerIdChangeCurd:CrudBase <WorkerChangedModel,IArWorkerIdChangedRepository>
    {
        public ArWorkerIdChangeCurd()
            : base(new ArworkerIdChangedRepository(),"职工更变工号")
      { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddWorkerIdChange);
        }
        private OpResult AddWorkerIdChange(WorkerChangedModel entity)
        {
            int record = this.irep.Insert(entity);
            if (record > 0)
            {
                return this.irep.UpdateAllTableWorkerId(entity.OldWorkerId,entity.NewWorkerId).ToOpResult("工号变更操作成功", "工号变更更变失败");
            }
            else return OpResult.SetResult("工号变更失败", true);

        }
    }
}
