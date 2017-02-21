using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    public class ArCalendarManger
    {
        /// <summary>
        /// 
        /// </summary>
        ArcalendarCurd ArcalendarCurd
        {
            get { return OBulider.BuildInstance<ArcalendarCurd>(); }
        }
        public  List<CalendarModel> GetDateDictionary(int nowYear, int nowMonth)
        {
            List<CalendarModel> returnDateDictionary = new List<CalendarModel>();
            var ListModel = ArcalendarCurd.FindCalendarDateListBy(nowYear, nowMonth);
            if (ListModel == null || ListModel.Count <= 0) return returnDateDictionary;
            //得到当月所有日期周次
            var nowMonthWeeksList = ListModel.Select(e => e.NowMonthWeekNumber).Distinct().ToList();
            if (nowMonthWeeksList == null || nowMonthWeeksList.Count <= 0) return returnDateDictionary;
            nowMonthWeeksList.ForEach(W =>
            {
                var models = ListModel.Where(e => e.NowMonthWeekNumber == W).ToList();
                int modelsCount = models.Count;
                if (0 < modelsCount && modelsCount < 7)
                {
                    int InsertIndex = (W == 1) ? 0 : modelsCount;
                    int yearWeek = models.FirstOrDefault().YearWeekNumber;
                    for (int n = 1; n <= 7 - modelsCount; n++)
                    { models.Insert(InsertIndex, new CalendarModel() { YearWeekNumber = yearWeek, CalendarDay = string.Empty }); }
                }
                models.ForEach(e =>
                {
                    if (e.CalendarDay != string.Empty)
                    { e.ChineseCalendar = new ChineseCalendar(e.CalendarDate).ChineseDayString;}
                    returnDateDictionary.Add(e);
                });
            });
            return returnDateDictionary;
        }



        public OpResult store(CalendarModel model)
        { 
            return ArcalendarCurd.Store(model);
        }


    }

    internal class ArcalendarCurd : CrudBase<CalendarModel, ICalendarsRepository>
    {
        public ArcalendarCurd()
            : base(new CalendarsRepository(), "行事历")
        { }
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddReportAttendence);
            AddOpItem(OpMode.Edit, EditReportAttendece);
        }

        private OpResult EditReportAttendece(CalendarModel model)
        {
              model.DateColor = CalendarColor(model.DateProperty);
              return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        private  string CalendarColor (string CalendarProty)
        {
            switch(CalendarProty)
            {
                case "法定假日":
                    return "#29B8CB";
                case "补班":
                    return "yellow";
                case "休假":
                    return "violet";
                case "星期六日":
                    return "red";
                case "正常":
                    return "white";
                default:
                    return "white";  
            }
        }
        /// <summary>
        /// 添加一年行事历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddReportAttendence(CalendarModel model)
        {
            try
            {
                int i = 0;
                string year = model.CalendarYear.ToString("0000");
                if (irep.IsExist(e => e.CalendarYear == model.CalendarYear))
                    return OpResult.SetResult(year + "年行事历已经存在");
                DateTime beginDate = DateTime.Parse(year + "-01-01");
                DateTime endDate = DateTime.Parse(year + "-12-31");
                List<DateTime> adlldate = new List<DateTime>();
                while (beginDate <= endDate)
                {
                    adlldate.Add(beginDate); 
                    beginDate = beginDate.AddDays(1);
                }
                adlldate.ForEach(d =>
                {
                    string dateProperty = GetDateProperty((int)d.DayOfWeek);
                    var newModel = new CalendarModel()
                    {
                        CalendarDate = d,
                        CalendarDay = d.Day.ToString(),
                        CalendarMonth = d.Month,
                        CalendarYear = d.Year,
                        CalendarWeek = (int)d.DayOfWeek,
                        NowMonthWeekNumber = GetDateWeekBy(d),
                        ChineseCalendar = GetchineseCalendar(d),
                        Title = "",
                        DateProperty = dateProperty,
                        DateColor = CalendarColor(dateProperty),
                        YearWeekNumber = GetWeekOfYear(d, true),
                        OpDate = DateTime.Now.Date,
                        OpSign = "add",
                        OpTime = DateTime.Now
                    };
                    i += irep.Insert(newModel);
                });

                if (i == 365)
                    return i.ToOpResult(OpContext + "一年日行事历操作成功");
                else return i.ToOpResult(OpContext + (365 - i) + "天" + "操作失败");
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        private string GetDateProperty(int  CalendarWeek)
        {
            if (CalendarWeek == 0 | CalendarWeek == 6)
                return "星期六日";
            else return "正常";
        }

        public List<CalendarModel> FindCalendarDateListBy(int nowYear, int nowMonth)
        {
            return irep.Entities.Where(e => e.CalendarYear == nowYear && e.CalendarMonth == nowMonth).ToList();
        }
        /// <summary>
        /// 获取日期是当月中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int GetDateWeekBy(DateTime dt)
        {
            bool sundayStart = true;
            //如果要判断的日期为1号，则肯定是第一周了
            if (dt.Day == 1)
                return 1;
            else
            {
                //得到本月第一天
                DateTime dtStart = new DateTime(dt.Year, dt.Month, 1);
                //得到本月第一天是周几
                int dayofweek = (int)dtStart.DayOfWeek;
                //如果不是以周日开始，需要重新计算一下dayofweek，详细DayOfWeek枚举的定义
                if (!sundayStart)
                {
                    dayofweek = dayofweek - 1;
                    if (dayofweek < 0)
                        dayofweek = 7;
                }
                //得到本月的第一周一共有几天
                int startWeekDays = 7 - dayofweek;
                //如果要判断的日期在第一周范围内，返回1
                if (dt.Day <= startWeekDays)
                    return 1;
                else
                {
                    int aday = dt.Day + 7 - startWeekDays;
                    return aday / 7 + (aday % 7 > 0 ? 1 : 0);
                }
            }
        }
        /// <summary>
        /// 获取日期是全年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sundayFirstDay">星期天是否本周第一天</param>
        /// <returns></returns>

       public  int GetWeekOfYear(DateTime dt ,bool sundayFirstDay)
        {
            try
            {
                GregorianCalendar gc = new GregorianCalendar();
                if (sundayFirstDay)
                    return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
                else return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
            catch (Exception )
            {

                throw;
            }
          
           
        }


        private static string GetchineseCalendar(DateTime date)
        {
            return new ChineseCalendar(date).ChineseDayString;
        }
    }
 

    #region  中国日历处理
    /// <summary>
    /// 中国日历异常处理
    /// </summary>
    internal class ChineseCalendarException : System.Exception
    {
        public ChineseCalendarException(string msg)
            : base(msg)
        {
        }
    }
    /// <summary>
    /// 中国农历类 版本V1.0 支持 1900.1.31日起至 2049.12.31日止的数据
    /// </summary>
    /// <remarks>
    /// 本程序使用数据来源于网上的万年历查询，并综合了一些其它数据
    /// </remarks>
    public class ChineseCalendar
    {
        #region 内部结构
        private struct SolarHolidayStruct
        {
            public int Month;
            public int Day;
            public int Recess; //假期长度
            public string HolidayName;
            public SolarHolidayStruct(int month, int day, int recess, string name)
            {
                Month = month;
                Day = day;
                Recess = recess;
                HolidayName = name;
            }
        }

        private struct LunarHolidayStruct
        {
            public int Month;
            public int Day;
            public int Recess;
            public string HolidayName;

            public LunarHolidayStruct(int month, int day, int recess, string name)
            {
                Month = month;
                Day = day;
                Recess = recess;
                HolidayName = name;
            }
        }

        private struct WeekHolidayStruct
        {
            public int Month;
            public int WeekAtMonth;
            public int WeekDay;
            public string HolidayName;

            public WeekHolidayStruct(int month, int weekAtMonth, int weekDay, string name)
            {
                Month = month;
                WeekAtMonth = weekAtMonth;
                WeekDay = weekDay;
                HolidayName = name;
            }
        }
        #endregion

        #region 内部变量
        private DateTime _date;
        private DateTime _datetime;

        private int _cYear;
        private int _cMonth;
        private int _cDay;
        private bool _cIsLeapMonth; //当月是否闰月
        private bool _cIsLeapYear; //当年是否有闰月
        #endregion

        #region 基础数据
        #region 基本常量
        private const int MinYear = 1900;
        private const int MaxYear = 2050;
        private static DateTime MinDay = new DateTime(1900, 1, 30);
        private static DateTime MaxDay = new DateTime(2117, 12, 31);
        private const int GanZhiStartYear = 1864; //干支计算起始年
        private static DateTime GanZhiStartDay = new DateTime(1899, 12, 22);//起始日
        private const string HZNum = "零一二三四五六七八九";
        private const int AnimalStartYear = 1900; //1900年为鼠年
        private static DateTime ChineseConstellationReferDay = new DateTime(2007, 9, 13);//28星宿参考值,本日为角
        #endregion

        #region 阴历数据
        /// <summary>
        /// 来源于网上的农历数据
        /// </summary>
        /// <remarks>
        /// 数据结构如下，共使用17位数据
        /// 第17位：表示闰月天数，0表示29天   1表示30天
        /// 第16位-第5位（共12位）表示12个月，其中第16位表示第一月，如果该月为30天则为1，29天为0
        /// 第4位-第1位（共4位）表示闰月是哪个月，如果当年没有闰月，则置0
        ///</remarks>
        private static int[] LunarDateArray = new int[]{
                0x04BD8,0x04AE0,0x0A570,0x054D5,0x0D260,0x0D950,0x16554,0x056A0,0x09AD0,0x055D2,
                0x04AE0,0x0A5B6,0x0A4D0,0x0D250,0x1D255,0x0B540,0x0D6A0,0x0ADA2,0x095B0,0x14977,
                0x04970,0x0A4B0,0x0B4B5,0x06A50,0x06D40,0x1AB54,0x02B60,0x09570,0x052F2,0x04970,
                0x06566,0x0D4A0,0x0EA50,0x06E95,0x05AD0,0x02B60,0x186E3,0x092E0,0x1C8D7,0x0C950,
                0x0D4A0,0x1D8A6,0x0B550,0x056A0,0x1A5B4,0x025D0,0x092D0,0x0D2B2,0x0A950,0x0B557,
                0x06CA0,0x0B550,0x15355,0x04DA0,0x0A5B0,0x14573,0x052B0,0x0A9A8,0x0E950,0x06AA0,
                0x0AEA6,0x0AB50,0x04B60,0x0AAE4,0x0A570,0x05260,0x0F263,0x0D950,0x05B57,0x056A0,
                0x096D0,0x04DD5,0x04AD0,0x0A4D0,0x0D4D4,0x0D250,0x0D558,0x0B540,0x0B6A0,0x195A6,
                0x095B0,0x049B0,0x0A974,0x0A4B0,0x0B27A,0x06A50,0x06D40,0x0AF46,0x0AB60,0x09570,
                0x04AF5,0x04970,0x064B0,0x074A3,0x0EA50,0x06B58,0x055C0,0x0AB60,0x096D5,0x092E0,
                0x0C960,0x0D954,0x0D4A0,0x0DA50,0x07552,0x056A0,0x0ABB7,0x025D0,0x092D0,0x0CAB5,
                0x0A950,0x0B4A0,0x0BAA4,0x0AD50,0x055D9,0x04BA0,0x0A5B0,0x15176,0x052B0,0x0A930,
                0x07954,0x06AA0,0x0AD50,0x05B52,0x04B60,0x0A6E6,0x0A4E0,0x0D260,0x0EA65,0x0D530,
                0x05AA0,0x076A3,0x096D0,0x04BD7,0x04AD0,0x0A4D0,0x1D0B6,0x0D250,0x0D520,0x0DD45,
                0x0B5A0,0x056D0,0x055B2,0x049B0,0x0A577,0x0A4B0,0x0AA50,0x1B255,0x06D20,0x0ADA0,
                0x14B63        
                };

        #endregion

        #region 二十四节气
        private static string[] _lunarHolidayName = 
                    { 
                    "小寒", "大寒", "立春", "雨水", 
                    "惊蛰", "春分", "清明", "谷雨", 
                    "立夏", "小满", "芒种", "夏至", 
                    "小暑", "大暑", "立秋", "处暑", 
                    "白露", "秋分", "寒露", "霜降", 
                    "立冬", "小雪", "大雪", "冬至"
                    };
        #endregion

        #region 节气数据
        private static string[] SolarTerm = new string[] { "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至" };
        private static int[] sTermInfo = new int[] { 0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758 };
        #endregion

        #region 农历相关数据
        private static string ganStr = "甲乙丙丁戊己庚辛壬癸";
        private static string zhiStr = "子丑寅卯辰巳午未申酉戌亥";
        private static string nStr1 = "日一二三四五六七八九";
        private static string nStr2 = "初十廿卅";
        private static string[] _monthString =
                {
                    "出错","正月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","腊月"
                };
        #endregion

        #region 按公历计算的节日
        private static SolarHolidayStruct[] sHolidayInfo = new SolarHolidayStruct[]{
            new SolarHolidayStruct(1, 1, 1, "元旦"),
            new SolarHolidayStruct(2, 14, 0, "情人节"),
            new SolarHolidayStruct(3, 8, 0, "妇女节"), 
            new SolarHolidayStruct(3, 12, 0, "植树节"), 
            new SolarHolidayStruct(4, 1, 0, "愚人节"),
            new SolarHolidayStruct(5, 1, 1, "劳动节"),
            new SolarHolidayStruct(5, 4, 0, "青年节"), 
            new SolarHolidayStruct(7, 1, 0, "建党节"),
            new SolarHolidayStruct(8, 1, 0, "建军节"), 
            new SolarHolidayStruct(9, 10, 0, "教师节"),
            new SolarHolidayStruct(10, 1, 1, "国庆节"), 
            new SolarHolidayStruct(12, 24, 0, "平安夜"), 
            new SolarHolidayStruct(12, 25, 0, "圣诞节")
           };
        #endregion

        #region 按农历计算的节日
        private static LunarHolidayStruct[] lHolidayInfo = new LunarHolidayStruct[]{
            new LunarHolidayStruct(1, 1, 1, "春节"), 
            new LunarHolidayStruct(1, 15, 0, "元宵节"), 
            new LunarHolidayStruct(5, 5, 0, "端午节"),
            new LunarHolidayStruct(8, 15, 0, "中秋节"), 
            new LunarHolidayStruct(9, 9, 0, "重阳节"), 
            new LunarHolidayStruct(12, 8, 0, "腊八节"),
            new LunarHolidayStruct(12, 23, 0, "小年"),
            //new LunarHolidayStruct(12, 30, 0, "除夕")  //注意除夕需要其它方法进行计算
        };
        #endregion
        #endregion

        #region 构造函数
        #region ChinaCalendar <公历日期初始化>
        /// <summary>
        /// 用一个标准的公历日期来初使化
        /// </summary>
        /// <param name="dt"></param>
        public ChineseCalendar(DateTime dt)
        {
            int i;
            int leap;
            int temp;
            int offset;

            CheckDateLimit(dt);

            _date = dt.Date;
            _datetime = dt;

            //农历日期计算部分
            leap = 0;
            temp = 0;

            TimeSpan ts = _date - ChineseCalendar.MinDay;//计算两天的基本差距
            offset = ts.Days;

            for (i = MinYear; i <= MaxYear; i++)
            {
                temp = GetChineseYearDays(i);  //求当年农历年天数
                if (offset - temp < 1)
                    break;
                else
                {
                    offset = offset - temp;
                }
            }
            _cYear = i;

            leap = GetChineseLeapMonth(_cYear);//计算该年闰哪个月
            //设定当年是否有闰月
            if (leap > 0)
            {
                _cIsLeapYear = true;
            }
            else
            {
                _cIsLeapYear = false;
            }

            _cIsLeapMonth = false;
            for (i = 1; i <= 12; i++)
            {
                //闰月
                if ((leap > 0) && (i == leap + 1) && (_cIsLeapMonth == false))
                {
                    _cIsLeapMonth = true;
                    i = i - 1;
                    temp = GetChineseLeapMonthDays(_cYear); //计算闰月天数
                }
                else
                {
                    _cIsLeapMonth = false;
                    temp = GetChineseMonthDays(_cYear, i);//计算非闰月天数
                }

                offset = offset - temp;
                if (offset <= 0) break;
            }

            offset = offset + temp;
            _cMonth = i;
            _cDay = offset;
        }
        #endregion

        #region ChinaCalendar <农历日期初始化>
        /// <summary>
        /// 用农历的日期来初使化
        /// </summary>
        /// <param name="cy">农历年</param>
        /// <param name="cm">农历月</param>
        /// <param name="cd">农历日</param>
        /// <param name="LeapFlag">闰月标志</param>
        public ChineseCalendar(int cy, int cm, int cd, bool leapMonthFlag)
        {
            int i, leap, Temp, offset;

            CheckChineseDateLimit(cy, cm, cd, leapMonthFlag);

            _cYear = cy;
            _cMonth = cm;
            _cDay = cd;

            offset = 0;

            for (i = MinYear; i < cy; i++)
            {
                Temp = GetChineseYearDays(i); //求当年农历年天数
                offset = offset + Temp;
            }

            leap = GetChineseLeapMonth(cy);// 计算该年应该闰哪个月
            if (leap != 0)
            {
                this._cIsLeapYear = true;
            }
            else
            {
                this._cIsLeapYear = false;
            }

            if (cm != leap)
            {
                _cIsLeapMonth = false;  //当前日期并非闰月
            }
            else
            {
                _cIsLeapMonth = leapMonthFlag;  //使用用户输入的是否闰月月份
            }


            if ((_cIsLeapYear == false) || //当年没有闰月
                 (cm < leap)) //计算月份小于闰月     
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i);//计算非闰月天数
                    offset = offset + Temp;
                }

                //检查日期是否大于最大天
                if (cd > GetChineseMonthDays(cy, cm))
                {
                    throw new ChineseCalendarException("不合法的农历日期");
                }
                offset = offset + cd; //加上当月的天数
                #endregion
            }
            else   //是闰年，且计算月份大于或等于闰月
            {
                #region ...
                for (i = 1; i < cm; i++)
                {
                    Temp = GetChineseMonthDays(cy, i); //计算非闰月天数
                    offset = offset + Temp;
                }

                if (cm > leap) //计算月大于闰月
                {
                    Temp = GetChineseLeapMonthDays(cy);   //计算闰月天数
                    offset = offset + Temp;               //加上闰月天数

                    if (cd > GetChineseMonthDays(cy, cm))
                    {
                        throw new ChineseCalendarException("不合法的农历日期");
                    }
                    offset = offset + cd;
                }
                else  //计算月等于闰月
                {
                    //如果需要计算的是闰月，则应首先加上与闰月对应的普通月的天数
                    if (this._cIsLeapMonth == true) //计算月为闰月
                    {
                        Temp = GetChineseMonthDays(cy, cm); //计算非闰月天数
                        offset = offset + Temp;
                    }

                    if (cd > GetChineseLeapMonthDays(cy))
                    {
                        throw new ChineseCalendarException("不合法的农历日期");
                    }
                    offset = offset + cd;
                }
                #endregion
            }


            _date = MinDay.AddDays(offset);
        }
        #endregion
        #endregion

        #region 私有函数

        #region GetChineseMonthDays
        //传回农历 y年m月的总天数
        private int GetChineseMonthDays(int year, int month)
        {
            if (BitTest32((LunarDateArray[year - MinYear] & 0x0000FFFF), (16 - month)))
            {
                return 30;
            }
            else
            {
                return 29;
            }
        }
        #endregion

        #region GetChineseLeapMonth
        //传回农历 y年闰哪个月 1-12 , 没闰传回 0
        private int GetChineseLeapMonth(int year)
        {

            return LunarDateArray[year - MinYear] & 0xF;

        }
        #endregion

        #region GetChineseLeapMonthDays
        //传回农历 y年闰月的天数
        private int GetChineseLeapMonthDays(int year)
        {
            if (GetChineseLeapMonth(year) != 0)
            {
                if ((LunarDateArray[year - MinYear] & 0x10000) != 0)
                {
                    return 30;
                }
                else
                {
                    return 29;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region GetChineseYearDays
        /// <summary>
        /// 取农历年一年的天数
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetChineseYearDays(int year)
        {
            int i, f, sumDay, info;

            sumDay = 348; //29天 X 12个月
            i = 0x8000;
            info = LunarDateArray[year - MinYear] & 0x0FFFF;

            //计算12个月中有多少天为30天
            for (int m = 0; m < 12; m++)
            {
                f = info & i;
                if (f != 0)
                {
                    sumDay++;
                }
                i = i >> 1;
            }
            return sumDay + GetChineseLeapMonthDays(year);
        }
        #endregion

        #region GetChineseHour
        /// <summary>
        /// 获得当前时间的时辰
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// 
        private string GetChineseHour(DateTime dt)
        {

            int _hour, _minute, offset, i;
            int indexGan;
            //string ganHour, zhiHour;
            string tmpGan;

            //计算时辰的地支
            _hour = dt.Hour;    //获得当前时间小时
            _minute = dt.Minute;  //获得当前时间分钟

            if (_minute != 0) _hour += 1;
            offset = _hour / 2;
            if (offset >= 12) offset = 0;
            //zhiHour = zhiStr[offset].ToString();

            //计算天干
            TimeSpan ts = this._date - GanZhiStartDay;
            i = ts.Days % 60;

            indexGan = ((i % 10 + 1) * 2 - 1) % 10 - 1; //ganStr[i % 10] 为日的天干,(n*2-1) %10得出地支对应,n从1开始
            tmpGan = ganStr.Substring(indexGan) + ganStr.Substring(0, indexGan + 2);//凑齐12位
            //ganHour = ganStr[((i % 10 + 1) * 2 - 1) % 10 - 1].ToString();

            return tmpGan[offset].ToString() + zhiStr[offset].ToString();

        }
        #endregion

        #region CheckDateLimit
        /// <summary>
        /// 检查公历日期是否符合要求
        /// </summary>
        /// <param name="dt"></param>
        private void CheckDateLimit(DateTime dt)
        {
            if ((dt < MinDay) || (dt > MaxDay))
            {
                throw new ChineseCalendarException("超出可转换的日期");
            }
        }
        #endregion

        #region CheckChineseDateLimit
        /// <summary>
        /// 检查农历日期是否合理
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="leapMonth"></param>
        private void CheckChineseDateLimit(int year, int month, int day, bool leapMonth)
        {
            if ((year < MinYear) || (year > MaxYear))
            {
                throw new ChineseCalendarException("非法农历日期");
            }
            if ((month < 1) || (month > 12))
            {
                throw new ChineseCalendarException("非法农历日期");
            }
            if ((day < 1) || (day > 30)) //中国的月最多30天
            {
                throw new ChineseCalendarException("非法农历日期");
            }

            int leap = GetChineseLeapMonth(year);// 计算该年应该闰哪个月
            if ((leapMonth == true) && (month != leap))
            {
                throw new ChineseCalendarException("非法农历日期");
            }


        }
        #endregion

        #region ConvertNumToChineseNum
        /// <summary>
        /// 将0-9转成汉字形式
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string ConvertNumToChineseNum(char n)
        {
            if ((n < '0') || (n > '9')) return "";
            switch (n)
            {
                case '0':
                    return HZNum[0].ToString();
                case '1':
                    return HZNum[1].ToString();
                case '2':
                    return HZNum[2].ToString();
                case '3':
                    return HZNum[3].ToString();
                case '4':
                    return HZNum[4].ToString();
                case '5':
                    return HZNum[5].ToString();
                case '6':
                    return HZNum[6].ToString();
                case '7':
                    return HZNum[7].ToString();
                case '8':
                    return HZNum[8].ToString();
                case '9':
                    return HZNum[9].ToString();
                default:
                    return "";
            }
        }
        #endregion

        #region BitTest32
        /// <summary>
        /// 测试某位是否为真
        /// </summary>
        /// <param name="num"></param>
        /// <param name="bitpostion"></param>
        /// <returns></returns>
        private bool BitTest32(int num, int bitpostion)
        {

            if ((bitpostion > 31) || (bitpostion < 0))
                throw new Exception("Error Param: bitpostion[0-31]:" + bitpostion.ToString());

            int bit = 1 << bitpostion;

            if ((num & bit) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region ConvertDayOfWeek
        /// <summary>
        /// 将星期几转成数字表示
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private int ConvertDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return 1;
                case DayOfWeek.Monday:
                    return 2;
                case DayOfWeek.Tuesday:
                    return 3;
                case DayOfWeek.Wednesday:
                    return 4;
                case DayOfWeek.Thursday:
                    return 5;
                case DayOfWeek.Friday:
                    return 6;
                case DayOfWeek.Saturday:
                    return 7;
                default:
                    return 0;
            }
        }
        #endregion

        #region CompareWeekDayHoliday
        /// <summary>
        /// 比较当天是不是指定的第周几
        /// </summary>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private bool CompareWeekDayHoliday(DateTime date, int month, int week, int day)
        {
            bool ret = false;

            if (date.Month == month) //月份相同
            {
                if (ConvertDayOfWeek(date.DayOfWeek) == day) //星期几相同
                {
                    DateTime firstDay = new DateTime(date.Year, date.Month, 1);//生成当月第一天
                    int i = ConvertDayOfWeek(firstDay.DayOfWeek);
                    int firWeekDays = 7 - ConvertDayOfWeek(firstDay.DayOfWeek) + 1; //计算第一周剩余天数

                    if (i > day)
                    {
                        if ((week - 1) * 7 + day + firWeekDays == date.Day)
                        {
                            ret = true;
                        }
                    }
                    else
                    {
                        if (day + firWeekDays + (week - 2) * 7 == date.Day)
                        {
                            ret = true;
                        }
                    }
                }
            }

            return ret;
        }
        #endregion
        #endregion

        #region  属性

        #region 节日
        #region ChineseCalendarHoliday
        /// <summary>
        /// 计算中国农历节日
        /// </summary>
        public string ChineseCalendarHoliday
        {
            get
            {
                string tempStr = "";
                if (this._cIsLeapMonth == false) //闰月不计算节日
                {
                    foreach (LunarHolidayStruct lh in lHolidayInfo)
                    {
                        if ((lh.Month == this._cMonth) && (lh.Day == this._cDay))
                        {

                            tempStr = lh.HolidayName;
                            break;

                        }
                    }

                    //对除夕进行特别处理
                    if (this._cMonth == 12)
                    {
                        int i = GetChineseMonthDays(this._cYear, 12); //计算当年农历12月的总天数
                        if (this._cDay == i) //如果为最后一天
                        {
                            tempStr = "除夕";
                        }
                    }
                }
                return tempStr;
            }
        }
        #endregion



        #region DateHoliday
        /// <summary>
        /// 按公历日计算的节日
        /// </summary>
        public string DateHoliday
        {
            get
            {
                string tempStr = "";

                foreach (SolarHolidayStruct sh in sHolidayInfo)
                {
                    if ((sh.Month == _date.Month) && (sh.Day == _date.Day))
                    {
                        tempStr = sh.HolidayName;
                        break;
                    }
                }
                return tempStr;
            }
        }
        #endregion
        #endregion

        #region 公历日期
        #region Date
        /// <summary>
        /// 取对应的公历日期
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

        #region WeekDay
        /// <summary>
        /// 取星期几
        /// </summary>
        public DayOfWeek WeekDay
        {
            get { return _date.DayOfWeek; }
        }
        #endregion

        #region WeekDayStr
        /// <summary>
        /// 周几的字符
        /// </summary>
        public string WeekDayStr
        {
            get
            {
                switch (_date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return "星期日";
                    case DayOfWeek.Monday:
                        return "星期一";
                    case DayOfWeek.Tuesday:
                        return "星期二";
                    case DayOfWeek.Wednesday:
                        return "星期三";
                    case DayOfWeek.Thursday:
                        return "星期四";
                    case DayOfWeek.Friday:
                        return "星期五";
                    default:
                        return "星期六";
                }
            }
        }
        #endregion

        #region DateString
        /// <summary>
        /// 公历日期中文表示法 如一九九七年七月一日
        /// </summary>
        public string DateString
        {
            get
            {
                return "公元" + this._date.ToLongDateString();
            }
        }
        #endregion

        #region IsLeapYear
        /// <summary>
        /// 当前是否公历闰年
        /// </summary>
        public bool IsLeapYear
        {
            get
            {
                return DateTime.IsLeapYear(this._date.Year);
            }
        }
        #endregion

        #region ChineseHour
        /// <summary>
        /// 时辰
        /// </summary>
        public string ChineseHour
        {
            get
            {
                return GetChineseHour(_datetime);
            }
        }
        #endregion

        #endregion

        #region 农历日期
        #region IsChineseLeapMonth
        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsChineseLeapMonth
        {
            get { return this._cIsLeapMonth; }
        }
        #endregion

        #region IsChineseLeapYear
        /// <summary>
        /// 当年是否有闰月
        /// </summary>
        public bool IsChineseLeapYear
        {
            get
            {
                return this._cIsLeapYear;
            }
        }
        #endregion

        #region ChineseDay
        /// <summary>
        /// 农历日
        /// </summary>
        public int ChineseDay
        {
            get { return this._cDay; }
        }
        #endregion

        #region ChineseDayString
        /// <summary>
        /// 农历日中文表示
        /// </summary>
        public string ChineseDayString
        {
            get
            {
                string chineseDayString = string.Empty;
                switch (this._cDay)
                {
                    case 0:
                        return chineseDayString;
                    case 1:
                        chineseDayString = _monthString[this._cMonth];
                        break ;
                    case 10:
                        chineseDayString =  "初十";
                        break;
                    case 20:
                        chineseDayString = "二十";
                        break;
                    case 30:
                        chineseDayString = "三十";
                        break;
                    default:
                        chineseDayString = nStr2[(int)(_cDay / 10)].ToString() + nStr1[_cDay % 10].ToString();
                        break;
                }
                //如果有（公历，农历）节日，显示节日，如有二十四节气，显示节气
                chineseDayString = ChineseCalendarHoliday != string.Empty ?
                           ChineseCalendarHoliday:(DateHoliday!= string.Empty ?
                           DateHoliday:(ChineseTwentyFourDay!= string.Empty ?
                           ChineseTwentyFourDay : chineseDayString));
                return chineseDayString;
            }
        }
        #endregion

        #region ChineseMonth
        /// <summary>
        /// 农历的月份
        /// </summary>
        public int ChineseMonth
        {
            get { return this._cMonth; }
        }
        #endregion

        #region ChineseMonthString
        /// <summary>
        /// 农历月份字符串
        /// </summary>
        public string ChineseMonthString
        {
            get
            {
                return _monthString[this._cMonth];
            }
        }
        #endregion

        #region ChineseYear
        /// <summary>
        /// 取农历年份
        /// </summary>
        public int ChineseYear
        {
            get { return this._cYear; }
        }
        #endregion

        #region ChineseYearString
        /// <summary>
        /// 取农历年字符串如，一九九七年
        /// </summary>
        public string ChineseYearString
        {
            get
            {
                string tempStr = "";
                string num = this._cYear.ToString();
                for (int i = 0; i < 4; i++)
                {
                    tempStr += ConvertNumToChineseNum(num[i]);
                }
                return tempStr + "年";
            }
        }
        #endregion

        #region ChineseDateString
        /// <summary>
        /// 取农历日期表示法：农历一九九七年正月初五
        /// </summary>
        public string ChineseDateString
        {
            get
            {
                if (this._cIsLeapMonth == true)
                {
                    return "农历" + ChineseYearString + "闰" + ChineseMonthString + ChineseDayString;
                }
                else
                {
                    return "农历" + ChineseYearString + ChineseMonthString + ChineseDayString;
                }
            }
        }
        #endregion

        #region ChineseTwentyFourDay
        /// <summary>
        /// 定气法计算二十四节气,二十四节气是按地球公转来计算的，并非是阴历计算的
        /// </summary>
        /// <remarks>
        /// 节气的定法有两种。古代历法采用的称为"恒气"，即按时间把一年等分为24份，
        /// 每一节气平均得15天有余，所以又称"平气"。现代农历采用的称为"定气"，即
        /// 按地球在轨道上的位置为标准，一周360°，两节气之间相隔15°。由于冬至时地
        /// 球位于近日点附近，运动速度较快，因而太阳在黄道上移动15°的时间不到15天。
        /// 夏至前后的情况正好相反，太阳在黄道上移动较慢，一个节气达16天之多。采用
        /// 定气时可以保证春、秋两分必然在昼夜平分的那两天。
        /// </remarks>
        public string ChineseTwentyFourDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";

                y = this._date.Year;

                for (int i = 1; i <= 24; i++)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];

                    newDate = baseDateAndTime.AddMinutes(num);//按分钟计算
                    if (newDate.DayOfYear == _date.DayOfYear)
                    {
                        tempStr = SolarTerm[i - 1];
                        break;
                    }
                }
                return tempStr;
            }
        }

        //当前日期前一个最近节气
        public string ChineseTwentyFourPrevDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";

                y = this._date.Year;

                for (int i = 24; i >= 1; i--)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];

                    newDate = baseDateAndTime.AddMinutes(num);//按分钟计算

                    if (newDate.DayOfYear < _date.DayOfYear)
                    {
                        tempStr = string.Format("{0}[{1}]", SolarTerm[i - 1], newDate.ToString("yyyy-MM-dd"));
                        break;
                    }
                }

                return tempStr;
            }

        }

        //当前日期后一个最近节气
        public string ChineseTwentyFourNextDay
        {
            get
            {
                DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0); //#1/6/1900 2:05:00 AM#
                DateTime newDate;
                double num;
                int y;
                string tempStr = "";

                y = this._date.Year;

                for (int i = 1; i <= 24; i++)
                {
                    num = 525948.76 * (y - 1900) + sTermInfo[i - 1];

                    newDate = baseDateAndTime.AddMinutes(num);//按分钟计算

                    if (newDate.DayOfYear > _date.DayOfYear)
                    {
                        tempStr = string.Format("{0}[{1}]", SolarTerm[i - 1], newDate.ToString("yyyy-MM-dd"));
                        break;
                    }
                }
                return tempStr;
            }

        }
        #endregion
        #endregion
        #endregion


    }
    #endregion
}

