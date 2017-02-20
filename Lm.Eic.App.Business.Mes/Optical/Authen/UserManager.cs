using Lm.Eic.App.DbAccess.Mes.Optical.AuthenDb;
using Lm.Eic.App.DomainModel.Mes.Optical.Authen;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;

namespace Lm.Eic.App.Business.Mes.Optical.Authen
{
    public class MesUserManager
    {
        #region member

        private IUserRepository irep = null;

        #endregion member

        #region constructure

        public MesUserManager()
        {
            this.irep = new UserRepository();
        }

        #endregion constructure

        #region method

        public List<WorkerCell> GetWorkers()
        {
            return this.irep.GetWorkers();
        }

        public OpResult RegistUser(UserInfo user)
        {
            int record = 0;
            if (!irep.IsExist(e => e.UserID == user.UserID))
            {
                user.RoleID = RolesDic[user.RoleID];
                user.GroupID = "0005";
                user.CreateTime = DateTime.Now;
                user.CurrentState = "已使用";
                user.Password = user.UserID;
                record = irep.Insert(user);
            }
            return OpResult.SetResult("添加用户成功！", record > 0);
        }

        private Dictionary<string, string> RolesDic
        {
            get
            {
                return new Dictionary<string, string>() {
                   {"部门经理","0002"},{"主管","0003"},{"副主管","0004"},
                   {"工程师","0005"},{"技术员","0006"},{"助理","0007"},
                   {"组长","0008"},{"作业员","0009"},{"检验员","0010"}
                };
            }
        }

        #endregion method
    }
}