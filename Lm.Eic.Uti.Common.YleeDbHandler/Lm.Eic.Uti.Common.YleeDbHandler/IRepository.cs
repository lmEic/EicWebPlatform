using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using EntityFramework.Extensions;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    /// <summary>
    ///  定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity : class,new()
    {
        #region 属性
        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 插入实体记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isSave">是否执行保存,默认执行单个实体保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(TEntity entity, bool isSave = true);

        /// <summary>
        /// 直接插入领域模型记录
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        int InsertDto<TDto>(TDto dto) where TDto : class ,new();

        /// <summary>
        /// 批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存,默认执行单个实体保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);
        /// <summary>
        /// 批量插入领域模型记录
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dtos"></param>
        /// <returns></returns>
        int InsertDto<TDto>(IEnumerable<TDto> dtos) where TDto : class,new();

        /// <summary>
        /// 删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave">是否执行保存,默认执行单个实体保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(object id, bool isSave = true);
        /// <summary>
        ///  删除实体记录
        /// </summary>
        /// <param name="entity">实体对象 </param>
        /// <param name="isSave">是否执行保存,默认执行单个实体保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);
        /// <summary>
        ///  删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave">是否执行保存,默认执行单个实体保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);
        /// <summary>
        ///  删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存</param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);
        /// <summary>
        ///  删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件去更新实体
        /// </summary>
        /// <typeparam name="TDto">领域模型对象类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="dto">领域模型对象</param>
        /// <returns></returns>
        int UpdateDto<TDto>(Expression<Func<TEntity, bool>> predicate, TDto dto, bool isSave = true) where TDto : class,new();
        /// <summary>
        /// 根据条件去更新实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> predicate,TEntity entity, bool isSave = true);
        /// <summary>
        /// 根据条件按需更新实体
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="updateExpression">按需更新实体值</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression);
        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);
        /// <summary>
        /// 全部提交
        /// </summary>
        /// <returns></returns>
        int Commit();
        #endregion

        #region 查询方法
        IList<TDto> FindAll<TDto>() where TDto : class,new();
        /// <summary>
        /// 按指定条件查询，直接返回领域模型集合,如果过滤条件为null，则直接返回所有数据
        /// </summary>
        /// <typeparam name="TDto">泛型领域模型类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IList<TDto> FindAll<TDto>(Expression<Func<TEntity, bool>> predicate) where TDto : class,new();
        /// <summary>
        /// 按指定条件查询，直接返回领域模型集合,如果过滤条件为null，则直接返回所有数据
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IList<TDto> FindAll<TDto>(string predicate, params object[] values) where TDto : class,new();
        /// <summary>
        /// 查询第一条记录，直接返回领域模型
        /// </summary>
        /// <typeparam name="TDto">泛型领域模型类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        TDto FirstOfDefault<TDto>(Expression<Func<TEntity, bool>> predicate) where TDto : class,new();
        /// <summary>
        /// 返回第一个实体数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOfDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 是否存在,存在返回True,否则返回False
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
    /// <summary>
    ///     EntityFramework仓储操作基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public abstract class EFRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class,new()
    {
        #region 构造器
        public EFRepositoryBase()
        {
            SetUnitOfWorkContext();
        }
        #endregion

        #region 属性
        /// <summary>
        ///     获取或设置 EntityFramework的数据仓储上下文
        /// </summary>
        protected IUnitOfWorkContext EFContext { get; set; }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity>(); }
        }

        #endregion

        #region 公共方法
        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool isSave = true)
        {
            //PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterNew(entity);
            return isSave ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            if (entities != null && entities.Count() > 0)
            {
                //PublicHelper.CheckArgument(entities, "entities");
                EFContext.RegisterNew(entities);
                return isSave ? EFContext.Commit() : 0;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(object id, bool isSave = true)
        {
            //PublicHelper.CheckArgument(id, "id");
            TEntity entity = EFContext.Set<TEntity>().Find(id);
            return entity != null ? Delete(entity, isSave) : 0;
        }
        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool isSave = true)
        {
            //PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterDeleted(entity);
            return isSave ? EFContext.Commit() : 0;
        }
        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            if (entities != null && entities.Count() > 0)
            {
                //PublicHelper.CheckArgument(entities, "entities");
                EFContext.RegisterDeleted(entities);
                return isSave ? EFContext.Commit() : 0;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        ///  删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            //PublicHelper.CheckArgument(predicate, "predicate");
            List<TEntity> entities = EFContext.Set<TEntity>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, isSave) : 0;
        }
        /// <summary>
        /// 直接删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return Delete(predicate, true);
        }
        public int UpdateDto<TDto>(Expression<Func<TEntity, bool>> predicate, TDto dto, bool isSave = true) where TDto : class,new()
        {
            TEntity entity = new TEntity();
            ObjectMapper.OOMapper<TDto, TEntity>(dto, entity);
            EFContext.RegisterModified<TEntity>(predicate, entity);
            return isSave ? EFContext.Commit() : 0;
        }
        public int Update(Expression<Func<TEntity, bool>> predicate,TEntity entity, bool isSave = true)
        {
            EFContext.RegisterModified<TEntity>(predicate, entity);
            return isSave ? EFContext.Commit() : 0;
        }
        public int Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return EFContext.RegisterModified<TEntity>(predicate, updateExpression);
        }
        /// <summary>
        /// 按需更新实体
        /// </summary>
        /// <param name="predicate">更新条件</param>
        /// <param name="updateExpression">按需给实体赋值</param>
        /// <returns></returns>


        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            //PublicHelper.CheckArgument(key, "key");
            return EFContext.Set<TEntity>().Find(key);
        }

        public int Commit()
        {
            return this.EFContext.Commit();
        }
        #endregion

        #region 抽象保护方法，必须由子类进行重写
        /// <summary>
        /// 设置具体数据操作单元实例，在子类中进行具体化
        /// </summary>
        protected abstract void SetUnitOfWorkContext();
        #endregion

        #region 直接更新,查询领域模型数据方法
        public int InsertDto<TDto>(TDto dto) where TDto : class, new()
        {
            TEntity entity = new TEntity();
            ObjectMapper.OOMapper<TDto, TEntity>(dto, entity);
            return Insert(entity);
        }
        public int InsertDto<TDto>(IEnumerable<TDto> dtos) where TDto : class, new()
        {
            IEnumerable<TEntity> entities = ObjectMapper.OOMapper<TDto, TEntity>(dtos);
            return Insert(entities);
        }
        public IList<TDto> FindAll<TDto>() where TDto : class, new()
        {
            IEnumerable<TEntity> entities = null;
            entities = this.Entities.ToList();
            IEnumerable<TDto> dtos = new List<TDto>();
            if (entities != null && entities.Count() > 0)
            {
                dtos = ObjectMapper.OOMapper<TEntity, TDto>(entities);
            }
            return dtos.ToList();

        }
        public IList<TDto> FindAll<TDto>(Expression<Func<TEntity, bool>> predicate) where TDto : class, new()
        {
            IEnumerable<TEntity> entities = this.Entities.Where(predicate).ToList();
            IEnumerable<TDto> dtos = new List<TDto>();
            if (entities != null && entities.Count() > 0)
            {
                dtos = ObjectMapper.OOMapper<TEntity, TDto>(entities);
            }
            return dtos.ToList();
        }
        public IList<TDto> FindAll<TDto>(string predicate, params object[] values) where TDto : class, new()
        {
            IEnumerable<TEntity> entities = null;
            if (predicate == null)
            {
                entities = this.Entities.ToList();
            }
            else
            {
                entities = this.Entities.Where(predicate, values).ToList();
            }
            IEnumerable<TDto> dtos = new List<TDto>();
            if (entities != null && entities.Count() > 0)
            {
                dtos = ObjectMapper.OOMapper<TEntity, TDto>(entities);
            }
            return dtos.ToList();
        }
        public TDto FirstOfDefault<TDto>(Expression<Func<TEntity, bool>> predicate) where TDto : class, new()
        {
            TDto dto = null;
            try
            {
                TEntity entity = this.Entities.FirstOrDefault(predicate);
                if (entity != null)
                {
                    dto = new TDto();
                    ObjectMapper.OOMapper<TEntity, TDto>(entity, dto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return dto;
        }
        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate) != null;
        }
        
        public TEntity FirstOfDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate);
        }
        #endregion
    }
    /// <summary>
    /// 对象映射器
    /// </summary>
    internal static class ObjectMapper
    {
        internal static void OOMapper<TObjectSource, TObjectDestination>(TObjectSource source, TObjectDestination destination)
            where TObjectSource : class,new()
            where TObjectDestination : class,new()
        {
            try
            {
                Type sourceT = source.GetType();
                var destiPts = destination.GetType().GetProperties();
                var sourcePts = source.GetType().GetProperties().ToList();
                if (destiPts.Length > 0)
                {
                    foreach (PropertyInfo ptdesti in destiPts)
                    {
                        PropertyInfo ptsource = sourcePts.FirstOrDefault(p => p.Name == ptdesti.Name);
                        if (ptsource != null)
                        {
                            if (ptdesti.Name.ToUpper() == "ENTITYSTATE" || ptdesti.Name.ToUpper() == "ENTITYKEY")
                            { }
                            else
                            {
                                object piSourceValue = ptsource.GetValue(source, null);
                                ptdesti.SetValue(destination, piSourceValue, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        internal static IEnumerable<TObjectDestination> OOMapper<TObjectSource, TObjectDestination>(IEnumerable<TObjectSource> sources)
            where TObjectSource : class,new()
            where TObjectDestination : class,new()
        {
            List<TObjectDestination> destinations = new List<TObjectDestination>();
            try
            {
                foreach (var s in sources)
                {
                    TObjectDestination d = new TObjectDestination();
                    OOMapper<TObjectSource, TObjectDestination>(s, d);
                    destinations.Add(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return destinations;
        }
    }
}
