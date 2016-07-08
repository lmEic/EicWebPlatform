using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.Attendance
{
    /// <summary>
    /// 考勤管理服务门面接口
    /// </summary>
    public static class AttendanceService
    {
        /// <summary>
        /// 班别设置器
        /// </summary>
        public static AttendClassTypeSetter ClassTypeSetter
        {
            get { return OBulider.BuildInstance<AttendClassTypeSetter>(); }
        }

        /// <summary>
        /// 考勤数据管理器
        /// </summary>
        public static AttendSlodPrintManager AttendSlodPrintManager
        {
            get { return OBulider.BuildInstance<AttendSlodPrintManager>(); }
        }

        /// <summary>
        /// 请假管理器
        /// </summary>
        public static AttendAskLeaveManager AttendAskLeaveManager
        {
            get { return OBulider.BuildInstance<AttendAskLeaveManager>(); }
        }
    }
}