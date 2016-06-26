using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeExtension.Conversion
{
  /// <summary>
  /// 基本类型扩展
  /// </summary>
  public static class BaseTypeExtension
    {
      /// <summary>
      /// 转换为yyyy-MM-dd 日期型时间
      /// </summary>
      /// <param name="dt"></param>
      /// <returns></returns>
      public static DateTime ToDate(this DateTime dt)
      {
          return DateTime.Parse (dt.ToString("yyyy-MM-dd"));
      }
      /// <summary>
      /// 将字符串日期转换为短日期格式
      /// </summary>
      /// <param name="dt"></param>
      /// <returns></returns>
      public static DateTime ToDate(this string dt)
      {
          DateTime d = DateTime.Now.ToDate();
          if (DateTime.TryParse(dt, out d))
          {
              return d.ToDate();
          }
          return d;
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
      public static decimal ToDeciaml(this string value)
      {
          decimal r = 0;
          if (decimal.TryParse(value, out r))
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
    }
}
