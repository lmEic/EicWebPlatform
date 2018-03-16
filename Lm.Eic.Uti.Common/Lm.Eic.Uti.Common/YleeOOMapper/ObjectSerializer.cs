using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;
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
            if (value == null) return "";
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
            if (value == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, obj);
                string szJson = Encoding.UTF8.GetString(ms.ToArray());
                return szJson;
            }
        }
        /// <summary>
        /// 把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="szJson">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T ParseFormJson<T>(string szJson)
        {
                T obj = Activator.CreateInstance<T>();
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
                {
                    try
                    {
                    DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
                        return (T)dcj.ReadObject(ms);
                    }
                    catch (Exception ex)
                    {
                       return default(T);
                      throw new Exception(ex.Message);
                    }

                }
        }
    }
}
