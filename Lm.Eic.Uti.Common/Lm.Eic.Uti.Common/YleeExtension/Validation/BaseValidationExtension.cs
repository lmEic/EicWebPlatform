using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lm.Eic.Uti.Common.YleeExtension.Validation
{
    public static class BaseValidationExtension
    {

        #region string
        /// <summary>
        /// 字符串是否为Null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 字符串是否为数字
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(s);
        }


        /// <summary>
        /// 是否为Int类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        /// <summary>
        ///  是否为double类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Isdouble(this string s)
        {
            double i;
            return double.TryParse(s, out i);
        }

        #endregion


        #region Int
        /// <summary>
        /// 是否为奇数
        /// </summary>
        public static bool IsEven(this int value) => (value % 2 == 0) ? true : false;
        #endregion


        #region List
        /// <summary>
        /// 判断此集合是否为空 集合是否大于1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> dt)
        {
            return dt != null && dt.Count >= 1;
        }
        #endregion
    }
}
