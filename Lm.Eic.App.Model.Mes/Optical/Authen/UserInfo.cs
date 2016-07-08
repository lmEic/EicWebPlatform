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

        private string _userid;

        /// <summary>
        ///用户帐号
        /// </summary>
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        private string _username;

        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        private string _password;

        /// <summary>
        ///用户密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        private DateTime _createtime;

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        private string _currentstate;

        /// <summary>
        ///当前状态
        /// </summary>
        public string CurrentState
        {
            set { _currentstate = value; }
            get { return _currentstate; }
        }

        private string _roleid;

        /// <summary>
        ///角色ID
        /// </summary>
        public string RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }

        private string _groupid;

        /// <summary>
        ///群组ID
        /// </summary>
        public string GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }

        private decimal _id_key;

        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }

        #endregion Model
    }

    public class WorkerCell
    {
        public string WorkerId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
    }
}