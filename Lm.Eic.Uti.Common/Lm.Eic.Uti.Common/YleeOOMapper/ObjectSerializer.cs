using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Lm.Eic.Uti.Common.YleeOOMapper
{
    /// <summary>
    /// 对象序列化器
    /// </summary>
    public class ObjectSerializer
    {
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
        /// <summary>
        /// 将对象反序列.NET Type 类型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
