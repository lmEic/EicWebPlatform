using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    /// <summary>
    /// 考勤Crud工厂
    /// </summary>
    internal class AttendCrudFactory
    {
        internal static AttendAskLeaveCrud AskLeaveCrud
        {
            get { return OBulider.BuildInstance<AttendAskLeaveCrud>(); }
        }
    }

    internal class AttendAskLeaveCrud : CrudBase<AttendAskLeaveModel, IAttendAskLeaveRepository>
    {
        public AttendAskLeaveCrud() : base(new AttendAskLeaveRepository(), "请假处理")
        {

        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }

        internal OpResult Add(AttendAskLeaveModel entity)
        {

            return OpResult.SetSuccessResult("成功！");
        }
    }
}
