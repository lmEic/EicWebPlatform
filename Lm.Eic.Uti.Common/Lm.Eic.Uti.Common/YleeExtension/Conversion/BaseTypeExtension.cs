using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Lm.Eic.Uti.Common.YleeExtension.Conversion
{
    /// <summary>
    /// 基本类型扩展
    /// </summary>
    public static class BaseTypeExtension
    {
        #region DateTime

        /// <summary>
        /// 转换为yyyy-MM-dd 日期型时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToDate(this DateTime dt)
        {
            return DateTime.Parse(dt.ToString("yyyy-MM-dd"));
        }
        /// <summary>
        /// 转换为yyyyMM 年月份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToYearMonth(this DateTime dt, string concatChar = null)
        {
            if (concatChar == null)
                return dt.ToString("yyyyMM");
            else
                return string.Format("{0}{1}{2}", dt.Year.ToString(), concatChar, dt.Month.ToString().PadLeft(2, '0'));
        }
        /// <summary>
        /// 转换为yyyy-MM-dd 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateStr(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转换为yyyy-MM-dd HH:mm:ss 日期型时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this DateTime dt)
        {
            return DateTime.Parse(dt.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        /// <summary>
        /// 转换为yyyy-MM-dd HH:mm:ss字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeStr(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为yyyyMMdd 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeShortStr(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }


        /// <summary>
        /// 获取给定日期所在当年的周数
        /// </summary>
        /// <param name="giveDay"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime givenDay)
        {
            CultureInfo ci = new CultureInfo("zh-CN");
            System.Globalization.Calendar cal = ci.Calendar;
            CalendarWeekRule cwr = ci.DateTimeFormat.CalendarWeekRule;
            DayOfWeek dow = DayOfWeek.Sunday;
            int week = cal.GetWeekOfYear(givenDay, cwr, dow);
            return week;
        }

        #endregion DateTime

        #region String

        /// <summary>
        /// 将字符串日期转换为短日期格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string dt)
        {
            DateTime d = DateTime.Now.ToDate();
            if (dt == string.Empty) return d;

            if (DateTime.TryParse(dt, out d))
            {
                return d.ToDate();
            }
            if (DateTime.TryParseExact(dt, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d))
            {
                return d.ToDate();
            }
            return d;
        }

        /// <summary>
        /// 将字符串转换为int类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this string value)
        {
            int r = 0;
            if (int.TryParse(value, out r))
            {
                return r;
            }
            return r;
        }

        /// <summary>
        /// 将字符串转换为数字类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value)
        {
            decimal r = 0;
            if (decimal.TryParse(value, out r))
            {
                return r;
            }
            return r;
        }

        /// <summary>
        /// 将字符串转换为double数字类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            double r = 0;
            if (double.TryParse(value, out r))
            {
                return r;
            }
            return r;
        }
        /// <summary>
        /// 将字符串转换为long数字类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this string value)
        {
            long r = 0;
            if (long.TryParse(value, out r))
            { return r; }
            return r;
        }
        /// <summary>
        /// 扩展方法：把字符串转换为字节类型
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte toByte(this string text)
        {
            if (text.Length == 0) text = "";
            return Convert.ToByte(text);
        }

        /// <summary>
        /// 将英文的星期天数转换为中文
        /// </summary>
        /// <param name="englishDay"></param>
        /// <returns></returns>
        public static string ToChineseWeekDay(this string englishDay)
        {
            string day = "星期一";
            switch (englishDay)
            {
                case "Monday":
                    day = "星期一";
                    break;

                case "Tuesday":
                    day = "星期二";
                    break;

                case "Wednesday":
                    day = "星期三";
                    break;

                case "Thursday":
                    day = "星期四";
                    break;

                case "Friday":
                    day = "星期五";
                    break;

                case "Saturday":
                    day = "星期六";
                    break;

                case "Sunday":
                    day = "星期日";
                    break;

                default:
                    break;
            }
            return day;
        }

        #endregion String

        #region Image
        /// <summary>
        /// 将图片转换为字节数组
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns></returns>
        public static byte[] toByte(this Image img)
        {
            if (img == null) return null;
            Bitmap map = new Bitmap(img);
            MemoryStream ms = new MemoryStream();
            saveImageToMs(img, ms);
            ms.Position = 0;
            byte[] mybite = new byte[int.Parse(ms.Length.ToString())];
            ms.Read(mybite, 0, int.Parse(ms.Length.ToString()));
            ms.Close();
            ms.Dispose();
            return mybite;
        }

        private static void saveImageToMs(Image img, MemoryStream ms)
        {
            ImageFormat format = img.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                img.Save(ms, ImageFormat.Jpeg);
            }
            else if (format.Equals(ImageFormat.Png))
            {
                img.Save(ms, ImageFormat.Png);
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                img.Save(ms, ImageFormat.Bmp);
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                img.Save(ms, ImageFormat.Gif);
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                img.Save(ms, ImageFormat.Icon);
            }
        }
        /// <summary>
        /// 将Image转换为内存流
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this Image img)
        {
            if (img == null) return null;
            Bitmap map = new Bitmap(img);
            MemoryStream ms = new MemoryStream();
            saveImageToMs(img, ms);
            return ms;
        }
        /// <summary>
        /// 读取文件到字节数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] ToPhotoByte(this string fileName)
        {
            byte[] photo_byte = null;
            if (!File.Exists(fileName)) return photo_byte;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    photo_byte = br.ReadBytes((int)fs.Length);
                }
            }
            return photo_byte;
        }
        #endregion Image

        #region Byte[]

        /// <summary>
        /// 将字节数组转换为图像
        /// </summary>
        /// <param name="mybite">字节数组</param>
        /// <returns></returns>
        public static Image toImage(this byte[] mybite)
        {
            if (mybite == null) return null;
            MemoryStream ms = new MemoryStream(mybite);
            Image img = Image.FromStream(ms);
            return img;
        }

        #endregion Byte[]

        #region Int

        /// <summary>
        /// 转换为操作结果 添加
        /// </summary>
        /// <param name="context">操作的对象</param>
        /// <returns></returns>
        public static OpResult ToOpResult_Add(this int record, string context, decimal id_Key)
        {
            string sucessMsg = string.Format("添加{0}数据成功", context);
            string failMsg = string.Format("添加{0}数据失败", context);
            return OpResult.SetResult(sucessMsg, failMsg, record, id_Key);
        }

        /// <summary>
        /// 转换为操作结果 添加
        /// </summary>
        /// <param name="context">操作的对象</param>
        /// <returns></returns>
        public static OpResult ToOpResult_Add(this int record, string context)
        {
            string sucessMsg = string.Format("添加{0}数据成功", context);
            string failMsg = string.Format("添加{0}数据失败", context);
            return OpResult.SetResult(sucessMsg, failMsg, record);
        }

        /// <summary>
        /// 转换为操作结果 更新
        /// </summary>
        /// <param name="context">操作的对象</param>
        /// <returns></returns>
        public static OpResult ToOpResult_Eidt(this int record, string context)
        {
            string sucessMsg = string.Format("更新{0}数据成功", context);
            string failMsg = string.Format("更新{0}数据失败", context);
            return OpResult.SetResult(sucessMsg, failMsg, record);
        }

        /// <summary>
        /// 转换为操作结果 删除
        /// </summary>
        /// <param name="context">操作的对象</param>
        /// <returns></returns>
        public static OpResult ToOpResult_Delete(this int record, string context)
        {
            string sucessMsg = string.Format("删除{0}数据成功", context);
            string failMsg = string.Format("删除{0}数据失败", context);
            return OpResult.SetResult(sucessMsg, failMsg, record);
        }

        /// <summary>
        /// 转换为操作结果
        /// </summary>
        /// <param name="successMessage">成功后的消息</param>
        /// <param name="falseMessage">失败后的消息</param>
        /// <returns></returns>
        public static OpResult ToOpResult(this int record, string successMessage, string falseMessage)
        {
            return OpResult.SetResult(successMessage, falseMessage, record);
        }

        /// <summary>
        /// 转换为操作结果
        /// </summary>
        /// <param name="successMessage">成功后的消息</param>
        /// <param name="Id_Key">Model.Id_Key</param>
        /// <returns></returns>
        public static OpResult ToOpResult(this int record, string successMessage, decimal Id_Key)
        {
            return OpResult.SetSuccessResult(successMessage, record > 0, Id_Key);
        }

        /// <summary>
        /// 转换为操作结果
        /// </summary>
        /// <param name="successMessage">成功后的消息</param>
        /// <param name="Id_Key">Model.Id_Key</param>
        /// <returns></returns>
        public static OpResult ToOpResult(this int record, string successMessage)
        {
            return OpResult.SetSuccessResult(successMessage, record > 0);
        }

        #endregion Int

        #region trim
        public static string TrimEndNewLine(this string content)
        {
            return content.TrimEnd((char[])"\r\n".ToCharArray());
        }
        #endregion
    }
}