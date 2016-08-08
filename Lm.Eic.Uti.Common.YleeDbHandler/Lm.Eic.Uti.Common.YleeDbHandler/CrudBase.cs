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
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="repository">数据访问接口</param>
        /// <param name="opContext">操作的数据对象</param>
        public CrudBase(IRep repository,string opContext)
        {
            irep = repository;
            OpContext = opContext;
        }

        private string _opContext = string.Empty;
        /// <summary>
        /// 操作的数据对象
        /// </summary>
        public string OpContext
        {
            get { return _opContext; }
            private set
            {
                _opContext = value;
            }
        }

        private Dictionary<string, Func<TEntity, OpResult>> crudOpDics = new Dictionary<string, Func<TEntity, OpResult>>();
        /// <summary>
        /// 添加Crud操作项目集合
        /// </summary>
        protected abstract void AddCrudOpItems();
        /// <summary>
        /// 添加具体的操作项目
        /// </summary>
        /// <param name="opKey"></param>
        /// <param name="opItem"></param>
        protected virtual void AddOpItem(string opKey,Func<TEntity, OpResult> opItem)
        {
            if (!crudOpDics.ContainsKey(opKey))
            {
                crudOpDics.Add(opKey,opItem);
            }
        }

        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult PersistentDatas(TEntity entity)
        {
            OpResult result = OpResult.SetResult("持久化数据操作失败!");
            string opSign = "default";
            try
            {
                //基本属性赋值
                if (entity == null) return OpResult.SetResult("entity can't set null!");
                var optimePi = IsHasProperty(entity, "OpTime");
                if (optimePi != null) optimePi.SetValue(entity, DateTime.Now.ToDateTime(), null);
                var opDatePi = IsHasProperty(entity, "OpDate");
                if (opDatePi != null) opDatePi.SetValue(entity, DateTime.Now.ToDate(), null);

                //取得操作方法
                PropertyInfo pi = IsHasProperty(entity, "OpSign");
                if (pi == null)
                    return OpResult.SetResult("操作方法不能为空！");
                opSign = pi.GetValue(entity, null) as string;

                //是否包含指定的方法
                if (!crudOpDics.ContainsKey(opSign))
                    AddCrudOpItems();
                //是否包含指定的方法
                if (!crudOpDics.ContainsKey(opSign))
                    return OpResult.SetResult(string.Format("未找到{0}函数", opSign));
                result = (crudOpDics[opSign])(entity);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
            return result;
        }

        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addfn"></param>
        /// <param name="editfn"></param>
        /// <param name="delfn"></param>
        /// <returns></returns>
        protected OpResult PersistentDatas(TEntity entity, Func<TEntity, OpResult> addfn, Func<TEntity, OpResult> editfn, Func<TEntity, OpResult> delfn)
        {
           
            OpResult result = OpResult.SetResult("持久化数据操作失败!");
            string opSign = "default";
            try
            {
                if (entity == null) return OpResult.SetResult("entity can't set null!");
                var optimePi = IsHasProperty(entity, "OpTime");
                if (optimePi != null) optimePi.SetValue(entity, DateTime.Now.ToDateTime(), null);
                var opDatePi = IsHasProperty(entity, "OpDate");
                if (opDatePi != null) opDatePi.SetValue(entity, DateTime.Now.ToDate(), null);

                PropertyInfo pi = IsHasProperty(entity, "OpSign");
                if (pi != null) opSign = pi.GetValue(entity, null) as string;
                switch (opSign)
                {
                    case OpMode.Add:
                        result = addfn(entity);
                        break;
                    case OpMode.Edit:
                        result = editfn(entity);
                        break;
                    case OpMode.Delete:
                        result = delfn(entity);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex) {throw new Exception(ex.InnerException.Message);}
            return result;
        }
        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addfn"></param>
        /// <param name="editfn"></param>
        /// <returns></returns>
        protected OpResult PersistentDatas(TEntity entity, Func<TEntity, OpResult> addfn, Func<TEntity, OpResult> editfn)
        {
            OpResult result = OpResult.SetResult("持久化数据操作失败!");
            string opSign = "default";
            PropertyInfo pi = IsHasProperty(entity, "OpSign");
            if (pi != null) opSign = pi.GetValue(entity, null) as string;
            switch (opSign)
            {
                case OpMode.Add:
                    result = addfn(entity);
                    break;
                case OpMode.Edit:
                    result = editfn(entity);
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
            if (entity == null) return OpResult.SetResult("entity can't set null!");
            var optimePi = IsHasProperty(entity, "OpTime");
            if (optimePi != null) optimePi.SetValue(entity, DateTime.Now.ToDateTime(),null);
            var opDatePi = IsHasProperty(entity, "OpDate");
            if (opDatePi != null) opDatePi.SetValue(entity, DateTime.Now.ToDate(), null);
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
