using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.Authenticate.Business
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public static class AuthenService
    {
        /// <summary>
        /// 用户管理
        /// </summary>
        public static UserManager UserManager
        {
            get { return OBulider.BuildInstance<UserManager>(); }
        }

        /// <summary>
        /// 角色管理器
        /// </summary>
        public static RoleManager RoleManager
        {
            get { return OBulider.BuildInstance<RoleManager>(); }
        }

        /// <summary>
        /// 程序集数据管理器
        /// </summary>
        public static AssemblyManager AssemblyManager
        {
            get { return OBulider.BuildInstance<AssemblyManager>(); }
        }

        /// <summary>
        /// 模块管理器
        /// </summary>
        public static ModuleNavManager ModuleManager
        {
            get
            {
                return OBulider.BuildInstance<ModuleNavManager>();
            }
        }

        /// <summary>
        /// 审核管理器
        /// </summary>
        public static AuditManager AuditManager
        {
            get { return OBulider.BuildInstance<AuditManager>(); }
        }
        /// <summary>
        /// 载入日历管理器
        /// </summary>
        public static CalendarManager CalendarManager
        {
            get { return OBulider.BuildInstance<CalendarManager>(); }
        }
    }

    /// <summary>
    /// 在线用户信息
    /// </summary>
    public class OnLineUser
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
    }
}