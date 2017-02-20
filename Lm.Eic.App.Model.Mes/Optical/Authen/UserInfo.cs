using System;

namespace Lm.Eic.App.DomainModel.Mes.Optical.Authen
{
    /// <summary>
    ///MES系统用户模型
    /// </summary>
    [Serializable]
    public partial class UserInfo
    {
        public UserInfo()
        { }

        #region Model

        /// <summary>
        ///用户帐号
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///用户密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///当前状态
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        ///角色ID
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        ///群组ID
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key { get; set; }

        #endregion Model
    }

    public class WorkerCell
    {
        public string WorkerId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
    }
}