using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lm.Eic.Uti.Common.YleeObjectBuilder
{
    /// <summary>
    /// 对象建造器
    /// </summary>
    public static class OBulider
    {
        private static Dictionary<string, object> cache = new Dictionary<string, object>();
        private static object _lock = new object();

        /// <summary>
        /// 利用反射根据命名空间与类名称创建对象,适用于不在同一个程序集内时
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="assemblyName">程序集的名称</param>
        /// <param name="namespaceName">命名空间,不包含类名称</param>
        /// <param name="className">类名称</param>
        /// <returns></returns>
        public static T BuildT<T>(string assemblyName, string namespaceName, string className)
        {
            T Tvalue;
            try
            {
                string key = typeof(T).Name;
                if (cache.ContainsKey(key))
                    Tvalue = (T)cache[key];
                else
                {
                    namespaceName = namespaceName.EndsWith(".") ? namespaceName : namespaceName + ".";
                    string instanceStr = namespaceName + className;
                    //方法一：Assembly.Load(程序集名称).CreateInstance(命名空间.类名)
                    Tvalue = (T)Assembly.Load(assemblyName).CreateInstance(instanceStr, true);
                    cache.Add(key, Tvalue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Tvalue;
        }

        /// <summary>
        /// 利用反射根据命名空间与类名称创建对象,适用于不在同一个程序集内时
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="assemblyName">程序集的名称</param>
        /// <param name="namespaceName">命名空间,不包含类名称</param>
        /// <param name="className">类名称</param>
        /// <param name="isCache">是否缓存此实例</param>
        /// <returns></returns>
        public static T BuildT<T>(string assemblyName, string namespaceName, string className, bool isCache)
        {
            T Tvalue;
            if (isCache) Tvalue = BuildT<T>(assemblyName, namespaceName, className);
            else
            {
                namespaceName = namespaceName.EndsWith(".") ? namespaceName : namespaceName + ".";
                string instanceStr = namespaceName + className;
                //方法一：Assembly.Load(程序集名称).CreateInstance(命名空间.类名)
                Tvalue = (T)Assembly.Load(assemblyName).CreateInstance(instanceStr, true);
            }
            return Tvalue;
        }

        /// <summary>
        /// 创建对象并加入缓存中
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <returns></returns>
        public static T BuildInstance<T>()
        where T : class, new()
        {
            T Tvalue = null;
            try
            {
                string key = typeof(T).Name;
                if (cache.ContainsKey(key))
                    Tvalue = cache[key] as T;
                else
                {
                    lock (_lock)
                    {
                        Tvalue = new T();
                        if (!cache.ContainsKey(key))
                            cache.Add(key, Tvalue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
            return Tvalue;
        }

        /// <summary>
        /// 创建对象并加入缓存中
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="args">类实例化所需参数集合</param>
        /// <returns></returns>
        public static T BuildInstance<T>(params object[] args)
        where T : class, new()
        {
            T Tvalue = null;
            try
            {
                string key = typeof(T).Name;
                if (cache.ContainsKey(key))
                    Tvalue = cache[key] as T;
                else
                {
                    lock (_lock)
                    {
                        Tvalue = (T)Activator.CreateInstance(Tvalue.GetType(), args);
                        if (!cache.ContainsKey(key))
                            cache.Add(key, Tvalue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
            return Tvalue;
        }
    }
}