using System.Collections.Generic;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    /// <summary>
    /// Db数据库访问助手
    /// </summary>
    public class DbHelper
    {
        #region constracture

        private static readonly Dictionary<string, DbAcess> cache = new Dictionary<string, DbAcess>();

        private static DbAcess CreateInstance(string key, string serverIp, string database, string userName, string pwd)
        {
            DbAcess insatance = null;
            if (cache.ContainsKey(key))
            {
                insatance = cache[key];
            }
            else
            {
                insatance = new DbAcess(serverIp, database, userName, pwd);
            }
            return insatance;
        }

        #endregion constracture

        #region property

        /// <summary>
        /// ERP数据访问入口
        /// </summary>
        public static DbAcess Erp
        {
            get { return CreateInstance("Erp", "192.168.0.246", "LightMaster", "sa", "yifeisa"); }
        }

        /// <summary>
        /// MES数据访问入口
        /// </summary>
        public static DbAcess Mes
        {
            get { return CreateInstance("Mes", "MS5", "LightMasterMes", "sa", "lm2011"); }
        }

        public static DbAcess MesAttach
        {
            get { return CreateInstance("MesAttach", "MS589", "MesAttach", "sa", "lm2011"); }
        }

        /// <summary>
        /// NBOSA数据访问入口
        /// </summary>
        public static DbAcess Nbosa
        {
            get { return CreateInstance("NBOSA", "MS5", "NBOSA", "sa", "lm2011"); }
        }

        /// <summary>
        /// Hrm数据访问入口
        /// </summary>
        public static DbAcess Hrm
        {
            get { return CreateInstance("Hrm", "MS5", "LightMasterHRM", "sa", "lm2011"); }
        }

        /// <summary>
        /// 权限数据访问接口
        /// </summary>
        public static DbAcess Authen
        {
            get { return CreateInstance("Authen", "MS5", "LmAuthenticatCenter", "sa", "lm2011"); }
        }

        /// <summary>
        /// 考勤系统数据访问入口
        /// </summary>
        public static DbAcess Sxt
        {
            get { return CreateInstance("Sxt", "192.168.0.244", "SXT", "sa", "gsadminsxt"); }
        }

        /// <summary>
        /// 生产日报
        /// </summary>
        public static DbAcess ProReport
        {
            get { return CreateInstance("ProReport", "192.168.0.165", "ProduceReportSystemMES", "sa", "lm2011"); }
        }

        /// <summary>
        /// 创建数据访问实例
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static DbAcess CreateDbAccessInstance(string serverName, string databaseName, string userName, string password)
        {
            return new DbAcess(serverName, databaseName, userName, password);
        }

        #endregion property
    }
}