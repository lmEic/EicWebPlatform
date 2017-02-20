using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Mes.Optical.Authen
{
    /// <summary>
    /// 制三部MES系统权限管理器
    /// </summary>
    public static class AuthenManager
    {
        /// <summary>
        /// 用户管理
        /// </summary>
        public static MesUserManager User
        {
            get { return OBulider.BuildInstance<MesUserManager>(); }
        }
    }
}