using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    public abstract class CrudBase<TEntity,IRep> 
        where TEntity : class, new()
    {

        protected IRep irep = default(IRep);

        public CrudBase()
        {
            InitIrep();
        }

        /// <summary>
        /// 初始化数据访问接口
        /// </summary>
        protected abstract void InitIrep();

        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addfn"></param>
        /// <param name="updatefn"></param>
        /// <param name="delfn"></param>
        /// <returns></returns>
        protected OpResult PersistentDatas(TEntity entity, Func<TEntity, OpResult> addfn, Func<TEntity, OpResult> updatefn, Func<TEntity, OpResult> delfn)
        {
            OpResult result = OpResult.SetResult("持久化数据操作失败!", false);
            string opSign="default";
            PropertyInfo pi=IsHasProperty(entity,"OpSign");
            if (pi != null) opSign = pi.GetValue(entity, null) as string;
            switch (opSign)
            {
                case OpMode.Add:
                    result = addfn(entity);
                    break;
                case OpMode.Edit:
                    result = updatefn(entity);
                    break;
                case OpMode.Delete:
                    result = delfn(entity);
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addfn"></param>
        /// <param name="updatefn"></param>
        /// <returns></returns>
        protected OpResult PersistentDatas(TEntity entity, Func<TEntity, OpResult> addfn, Func<TEntity, OpResult> updatefn)
        {
            OpResult result = OpResult.SetResult("持久化数据操作失败!", false);
            string opSign = "default";
            PropertyInfo pi = IsHasProperty(entity, "OpSign");
            if (pi != null) opSign = pi.GetValue(entity, null) as string;
            switch (opSign)
            {
                case OpMode.Add:
                    result = addfn(entity);
                    break;
                case OpMode.Edit:
                    result = updatefn(entity);
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 模型是否含有该属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private PropertyInfo IsHasProperty(TEntity entity,string propertyName)
        {
            Type type = entity.GetType();
            PropertyInfo pi= type.GetProperties().ToList().FirstOrDefault(e => e.Name == propertyName);
            return pi;
        }

    
        protected OpResult StoreEntity(TEntity entity,Func<TEntity,OpResult> storeHandler)
        {
            OpResult result = null;
            if (entity == null) return OpResult.SetResult("entity can't set null!", false);
            var optimePi = IsHasProperty(entity, "OpTime");
            if (optimePi != null) optimePi.SetValue(entity, DateTime.Now.ToDateTime(),null);
            var opDatePi = IsHasProperty(entity, "OpDate");
            if (opDatePi != null) optimePi.SetValue(entity, DateTime.Now.ToDate(), null);
            try
            {
                result=storeHandler(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message); 
            }
            return result;
        }

    }
}
