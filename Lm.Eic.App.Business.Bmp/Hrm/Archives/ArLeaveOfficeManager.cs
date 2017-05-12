using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using  Lm.Eic.Uti.Common.YleeOOMapper;
using  Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    /// <summary>
    /// 离职管理器
    /// </summary>
    public class ArLeaveOfficeManager 
    {
        private ArleaveOfficeCrud crud = null;
        public ArLeaveOfficeManager()
        {
            this.crud = new ArleaveOfficeCrud();
        }

        /// <summary>
        /// 存储离职信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult StoreLeaveOffInfo(ArLeaveOfficeModel entity)
        {
            return this.crud.Store(entity);
        }
        /// <summary>
        /// 获取某时间段的离职人员信息
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public List<LeaveOfficeMapEntity> GetLeavedWorkers(DateTime dtStart, DateTime dtEnd)
        {
            return this.crud.GetLeavedWorkers(dtStart, dtEnd);
        }
    }
    public class ArleaveOfficeCrud: CrudBase<ArLeaveOfficeModel, IArWorkerLeaveOfficeRepository>
    {
        public ArleaveOfficeCrud()
            : base(new ArWorkerLeaveOfficeRepository(),"离职人员信息")
      { }  
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddWorkerleaveOfficeInfo);
        }

        private OpResult AddWorkerleaveOfficeInfo(ArLeaveOfficeModel entity)
        {
            entity.LeaveDate = entity.LeaveDate.ToDate();
            int record = this.irep.Insert(entity);
            if (record > 0)
            {
                return this.irep.ChangeWorkingStatus("离职", entity.WorkerId).ToOpResult("离职操作成功", "离职存保成功,状态更变失败");
            }
            else return OpResult.SetSuccessResult("离职存保失败", true);
        }

        public List<LeaveOfficeMapEntity> GetLeavedWorkers(DateTime dtStart, DateTime dtEnd)
        {
            return this.irep.GetLeavedWorkers(dtStart, dtEnd);
        }
    }
}
