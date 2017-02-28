using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 修改数据仓库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual OpResult Store(TEntity model)
        {
            return this.PersistentDatas(model);
        }
        /// <summary>
        /// 设定固定字段值
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void SetFixFieldValue(TEntity entity)
        {
            var optimePi = IsHasProperty(entity, "OpTime");
            if (optimePi != null) optimePi.SetValue(entity, DateTime.Now.ToDateTime(), null);
            var opDatePi = IsHasProperty(entity, "OpDate");
            if (opDatePi != null) opDatePi.SetValue(entity, DateTime.Now.ToDate(), null);
        }

        /// <summary>
        /// 设置固定字段的值
        /// </summary>
        /// <param name="entityList">列表</param>
        /// <param name="opMode">操作标示</param>
        protected virtual void SetFixFieldValue(IEnumerable<TEntity> entityList,string opMode)
        {
            entityList.ToList().ForEach((m) =>
            {
                SetFixFieldValue(m);
                var opSignPi = IsHasProperty(m, "OpSign");
                if (opSignPi != null) opSignPi.SetValue(m, opMode, null);
            });
        }

        /// <summary>
        /// 设置固定字段的值
        /// </summary>
        /// <param name="entityList">列表</param>
        /// <param name="opMode">操作标示</param>
        protected virtual void SetFixFieldValue(IEnumerable<TEntity> entityList, string opMode, Action<TEntity> setFixFieldMethod)
        {
            entityList.ToList().ForEach((m) =>
            {
                SetFixFieldValue(m);
                var opSignPi = IsHasProperty(m, "OpSign");
                if (opSignPi != null) opSignPi.SetValue(m, opMode, null);
                setFixFieldMethod(m);
            });
        }


        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OpResult PersistentDatas(TEntity entity, bool isNeedEntity = false)
        {
            OpResult result = OpResult.SetResult("持久化数据操作失败!");
            string opSign = "default";
            if (entity == null)
                return OpResult.SetResult(string.Format("{0}不能为null！", OpContext));
            try
            {
                SetFixFieldValue(entity);
                //取得操作方法
                PropertyInfo pi = IsHasProperty(entity, "OpSign");
                if (pi != null)
                    opSign = pi.GetValue(entity, null) as string;
                //是否包含指定的方法
                if (!crudOpDics.ContainsKey(opSign))
                    AddCrudOpItems();
                //是否包含指定的方法
                if (!crudOpDics.ContainsKey(opSign))
                    return OpResult.SetResult(string.Format("未找到{0}的实现函数", opSign));
                result = (crudOpDics[opSign])(entity);

                if (isNeedEntity)
                    result.Entity = entity;
                //取得操作方法
                PropertyInfo piIdKey = IsHasProperty(entity, "Id_Key");
                if (piIdKey == null)
                {
                    string idKey = piIdKey.GetValue(entity, null) as string;
                    result.Id_Key = idKey.ToDeciaml();
                }
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
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
        protected OpResult StoreEntity(TEntity entity,Func<TEntity,OpResult> storeHandler,bool isNeedEntity=false)
        {
            OpResult result = null;
            if (entity == null) return OpResult.SetResult("entity can't set null!");
            SetFixFieldValue(entity);
            try
            {
                result=storeHandler(entity);
                if (isNeedEntity)
                    result.Entity = entity;
                //取得操作方法
                PropertyInfo piIdKey = IsHasProperty(entity, "Id_Key");
                if (piIdKey == null)
                {
                    string idKey = piIdKey.GetValue(entity, null) as string;
                    result.Id_Key = idKey.ToDeciaml();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message); 
            }
            return result;
        }

        /// <summary>
        /// 模型转换
        /// </summary>
        /// <typeparam name="TDestination">目标模型</typeparam>
        /// <param name="oldModel"></param>
        /// <returns></returns>
        protected TDestination ConventModel<TDestination>(TEntity oldModel) where TDestination : class, new()
        {
            TDestination newModel = new TDestination();
            OOMaper.Mapper(oldModel, newModel);
            return newModel;
        }
    }
}
