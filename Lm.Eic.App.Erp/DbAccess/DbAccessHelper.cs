using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lm.Eic.App.Erp.DbAccess
{
    public static class ErpDbAccessHelper
    {
        /// <summary>
        /// 查找数据模型
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="sqlFields"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="mapRowAndModel"></param>
        /// <returns></returns>
        public static List<TModel> FindDataBy<TModel>(string sqlFields, string sqlWhere, Action<DataRow, TModel> mapRowAndModel)
            where TModel : class, new()
        {
            List<TModel> mdlList = new List<TModel>();
            StringBuilder sb = new StringBuilder();
            sb.Append(sqlFields).Append(sqlWhere);
            DataTable dt = DbHelper.Erp.LoadTable(sb.ToString());
            TModel m = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    m = new TModel();
                    mapRowAndModel(dr, m);
                    mdlList.Add(m);
                }
            }
            return mdlList;
        }

        public static string ComposeID(string code, string category)
        {
            return string.Format("{0}-{1}", category.Trim(), code.Trim());
        }

        public static IDModel DecomposeID(string ID)
        {
            string[] fileds = ID.Split('-');
            return new IDModel() { Code = fileds[1].Trim(), Category = fileds[0].Trim() };
        }
    }

    public class IDModel
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 单别
        /// </summary>
        public string Category { get; set; }
    }
}