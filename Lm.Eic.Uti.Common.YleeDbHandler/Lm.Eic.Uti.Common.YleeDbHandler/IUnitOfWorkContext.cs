using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    /// <summary>
    /// 数据单元操作上下文接口
    /// </summary>
    public interface IUnitOfWorkContext : IDisposable
    {
        #region 属性

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        bool IsCommitted { get; }

        #endregion 属性

        #region 方法

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        void Rollback();

        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 注册修改的对象到数据库中
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        void RegisterModified<TEntity>(Expression<Func<TEntity, bool>> predicate, TEntity entity) where TEntity : class, new();

        int RegisterModified<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression) where TEntity : class;

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        #endregion 方法
    }

    /// <summary>
    /// 单元操作实现
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        #region constructure

        public UnitOfWorkContextBase()
        {
            this.SetDbContext();
        }

        #endregion constructure

        #region Property

        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        #endregion Property

        #region method

        /// <summary>
        /// 提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            int record = 0;
            if (IsCommitted)
            {
                return record;
            }
            try
            {
                record = Context.SaveChanges();
                IsCommitted = true;
                return record;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    throw new Exception("提交数据更新时发生异常：" + sqlEx.Number + sqlEx.Message);
                    //string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    //throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                else
                {
                    throw new Exception(e.InnerException.Message);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbvaliException)
            {
                throw new Exception(dbvaliException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                // Dispose();
            }
        }

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (!IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }


        /// <summary>
        ///   为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作。
        /// </summary>
        /// <typeparam name="TEntity"> 应为其返回一个集的实体类型。 </typeparam>
        /// <returns> 给定实体类型的 System.Data.Entity.DbSet 实例。 </returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            EntityState state = Context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            //方案一  6.1版本
            //Context.Set<TEntity>().AddRange(entities);
            //IsCommitted = false;
            //方案二  6.1以下的版本
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterNew(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
            IsCommitted = false;
        }

        /// <summary>
        /// 根据条件去修改实体对象
        /// </summary>
        /// <typeparam name="TEntity">泛型对象类型</typeparam>
        /// <param name="predicate">过滤条件</param>
        /// <param name="entity">实体对象</param>
        public void RegisterModified<TEntity>(Expression<Func<TEntity, bool>> predicate, TEntity entity) where TEntity : class, new()
        {
            TEntity oldentity = Context.Set<TEntity>().FirstOrDefault(predicate);
            if (oldentity != null)
            {
                Context.Entry(oldentity).CurrentValues.SetValues(entity);
            }
            IsCommitted = false;
        }

        /// <summary>
        /// 根据条件按需更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="updateExpression"></param>
        public int RegisterModified<TEntity>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateExpression) where TEntity : class
        {
            int record = Context.Set<TEntity>().Where(predicate).Update(updateExpression);
            IsCommitted = false;
            return record;
        }

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            if (Context.Entry(entity).State != EntityState.Detached)
                Context.Entry(entity).State = EntityState.Deleted;
            IsCommitted = false;
        }

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities)
                {
                    RegisterDeleted(entity);
                }
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        #endregion method

        #region Abstract method

        /// <summary>
        /// 制定具体的DbContext,由子类来实现
        /// </summary>
        protected abstract void SetDbContext();

        #endregion Abstract method
    }
}