using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Attendance;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Attendance;
using Lm.Eic.Uti.Common.YleeDbHandler;
namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    /// <summary>
    /// 考勤Crud工厂
    /// </summary>
    public class AttendCrudFactory
    {
        public AttendAskLeaveCrud AskLeaveCrud
        {
            get { return OBulider.BuildInstance<AttendAskLeaveCrud>(); }
        }
    }

    public class AttendAskLeaveCrud : CrudBase<AttendAskLeaveModel, IAttendAskLeaveRepository>
    {
        public AttendAskLeaveCrud() : base(new AttendAskLeaveRepository(), "请假处理")
        {

        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
