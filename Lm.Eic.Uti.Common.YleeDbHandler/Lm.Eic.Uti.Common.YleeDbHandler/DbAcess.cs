using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeDbHandler
{
    /// <summary>
    /// Db访问助手
    /// </summary>
    public class DbAcess
    {
        #region property

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string ServerIP
        { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database
        { get; set; }

        /// <summary>
        /// 访问密码
        /// </summary>
        public string Password
        { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        { get; set; }

        #endregion property

        #region new

        public DbAcess(string serverIp, string database, string userName, string pwd)
        {
            this.ServerIP = serverIp;
            this.Database = database;
            this.UserName = userName;
            this.Password = pwd;
        }

        private string ConnStr()
        {
            string MyConnStr = "Server='" + this.ServerIP + "';Database='" + this.Database + "';User ID='" + this.UserName + "';Password='" + this.Password + "';";
            return MyConnStr;
        }

        #endregion new

        #region LoadTable

        public DataTable LoadTable(string SqlText, SqlParameter[] sqlParameters)
        {
            DataTable Table = new DataTable();
            DataSet Ds = new DataSet();
            try
            {
                using (SqlConnection SqlConn = new SqlConnection(ConnStr()))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlText, SqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (sqlParameters != null)
                        {
                            foreach (SqlParameter sqlParameter in sqlParameters)
                            {
                                cmd.Parameters.Add(sqlParameter);
                            }
                        }
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd))
                        {
                            sqlAdapter.Fill(Ds);
                            Table = Ds.Tables[0];
                        }
                    }
                }
                return Table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
            }
        }

        public DataTable LoadTable(string SqlText)
        {
            return LoadTable(SqlText, null);
        }

        public bool IsExist(string sqlText)
        {
            DataTable tbl = LoadTable(sqlText);
            return (tbl == null) ? false : tbl.Rows.Count > 0;
        }

        /// <summary>
        /// 载入数据到泛型模型集合中,Sql语句中的字段名称必须与实体中的属性名称一致，才可转换
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public List<TEntity> LoadEntities<TEntity>(string sqlText, SqlParameter[] sqlParameters) where TEntity : class, new()
        {
            List<TEntity> entities = new List<TEntity>();
            Type entityType = typeof(TEntity);

            Dictionary<string, PropertyInfo> dic = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] pts = entityType.GetProperties();
            foreach (PropertyInfo info in pts)
            {
                dic.Add(info.Name, info);
            }
            string columnName = string.Empty;
            using (SqlConnection SqlConn = new SqlConnection(ConnStr()))
            {
                if (SqlConn.State == ConnectionState.Closed) SqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlText, SqlConn))
                {
                    cmd.CommandType = CommandType.Text;
                    if (sqlParameters != null)
                    {
                        foreach (SqlParameter sqlParameter in sqlParameters)
                        {
                            cmd.Parameters.Add(sqlParameter);
                        }
                    }
                    using (IDataReader sreader = cmd.ExecuteReader())
                    {
                        while (sreader.Read())
                        {
                            TEntity t = ConvertReadToTEntity<TEntity>(dic, ref columnName, sreader);
                            entities.Add(t);
                        }
                    }
                }
            }
            return entities;
        }

        private static TEntity ConvertReadToTEntity<TEntity>(Dictionary<string, PropertyInfo> dic, ref string columnName, IDataReader sreader) where TEntity : class, new()
        {
            TEntity t = new TEntity();
            foreach (KeyValuePair<string, PropertyInfo> attribute in dic)
            {
                columnName = attribute.Key.ToUpper();
                int filedIndex = 0;
                while (filedIndex < sreader.FieldCount)
                {
                    if (!IsNullOrDBNull(sreader[filedIndex]))
                    {
                        if (sreader.GetName(filedIndex).ToUpper() == columnName)
                        {
                            attribute.Value.SetValue(t, CheckType(sreader[filedIndex], attribute.Value.PropertyType), null);
                            break;
                        }
                    }
                    filedIndex++;
                }
            }
            return t;
        }

        /// <summary>
        /// 对可空类型进行判断转换(*要不然会报错)
        /// </summary>
        /// <param name="value">DataReader字段的值</param>
        /// <param name="conversionType">该字段的类型</param>
        /// <returns></returns>
        private static object CheckType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 判断指定对象是否是有效值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsNullOrDBNull(object obj)
        {
            return (obj == null || (obj is DBNull)) ? true : false;
        }

        /// <summary>
        /// 载入数据到泛型模型集合中,Sql语句中的字段名称必须与实体中的属性名称一致，才可转换
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public List<TEntity> LoadEntities<TEntity>(string sqlText) where TEntity : class, new()
        {
            return LoadEntities<TEntity>(sqlText, null);
        }

        /// <summary>
        /// SQL语句原生态查询，并转化为对应的实体，要求，查询字段与实体字段一一对应，不区分大小写
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="tableName">表名称</param>
        /// <param name="appendSelectFiedls">选择字段</param>
        /// <param name="appendWhere">Where过滤条件,AppendWhere为null,则返回null;若为all，则选择所有，不进行过滤</param>
        /// <returns></returns>
        public List<TEntity> FindAll<TEntity>(string tableName, string appendSelectFiedls, string appendWhere) where TEntity : class, new()
        {
            if (appendWhere == null) return new List<TEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append("Select ");
            sql.Append(appendSelectFiedls);
            sql.Append(" from ").Append(tableName).Append(" ");
            if (appendWhere != null && appendWhere.ToLower() != "all")
            {
                sql.Append(appendWhere);
            }
            return LoadEntities<TEntity>(sql.ToString());
        }

        /// <summary>
        /// 载入某列数据集合列表
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public List<string> LoadList(string sqlText, string columnName)
        {
            List<string> dataList = null;
            DataTable dt = LoadTable(sqlText);
            if (dt != null && dt.Rows.Count > 0)
            {
                dataList = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    dataList.Add(dr[columnName].ToString().Trim());
                }
            }
            else
            {
                dataList = new List<string>() { "没有找到数据" };
            }
            return dataList;
        }

        /// <summary>
        /// 载入指定表中的所有字段列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<string> LoadTableFields(string tableName)
        {
            string sqlText = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where Table_Name='{0}'", tableName);
            return LoadList(sqlText, "COLUMN_NAME");
        }

        public List<string> LoadTables(string databaseName)
        {
            string sqlText = string.Format("Select Table_Name from INFORMATION_SCHEMA.Tables Where Table_Catalog='{0}' Order By Table_Name", databaseName);
            return LoadList(sqlText, "Table_Name");
        }

        public List<TableColumnInfo> LoadTableColumnInfos(string tableName)
        {
            string sqlText = string.Format("Select '" + tableName + "' as TableName, COLUMN_NAME as ColumnName,Data_Type as ColumnType  from INFORMATION_SCHEMA.Columns Where Table_Name='{0}'", tableName);
            return LoadEntities<TableColumnInfo>(sqlText);
        }

        #endregion LoadTable

        #region Fast Upload Data

        /// <summary>
        /// 返回数据库连接
        /// </summary>
        /// <returns>返回数据库连接</returns>
        /// <remarks></remarks>
        private SqlConnection GetSqlConection()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(this.ConnStr());
                return myConnection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 根据查询Sql语句返回数据填充适配器
        /// </summary>
        /// <param name="SelectSqlText">查询SQL语句</param>
        /// <returns>返回数据填充适配器</returns>
        /// <remarks>根据查询Sql语句返回数据填充适配器</remarks>
        private SqlDataAdapter GetSqlDataAdapter(string SelectSqlText)
        {
            SqlConnection Conn = this.GetSqlConection();
            try
            {
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(SelectSqlText, Conn);
                myDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                return myDataAdapter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void BulkUpdataToServer(SqlConnection Conn, DataSet Ds, string DesTableName, int UpRowsCount)
        {
            Conn.Open();
            using (SqlBulkCopy Bulk = new SqlBulkCopy(Conn))
            {
                Bulk.BatchSize = UpRowsCount;
                Bulk.DestinationTableName = DesTableName;
                Bulk.BulkCopyTimeout = 600;
                Bulk.WriteToServer(Ds.Tables[DesTableName]);
            }
            Conn.Close();
            Conn.Dispose();
        }

        /// <summary>
        /// 快速批量上传数据
        /// </summary>
        /// <param name="sqlDestination">目标表的Sql语句</param>
        /// <param name="entities">实体的属性要与目标表的字段名称及类型一致</param>
        /// <returns></returns>
        public int FastUpdataTo<T>(string sqlDestination, List<T> entities) where T : class, new()
        {
            SqlDataAdapter myDataAdapter = this.GetSqlDataAdapter(sqlDestination);
            SqlConnection myConnection = myDataAdapter.SelectCommand.Connection;
            int RecordCount = 0;
            string tableName = GetTableNameFromSql(sqlDestination);
            DataSet myDataSet = new DataSet();
            //如果字串比較為區分大小寫，則為 true，否則為 false。預設值為 false。
            myDataSet.CaseSensitive = true;
            //Try
            myDataAdapter.Fill(myDataSet, tableName);
            try
            {
                if (entities.Count > 0)
                {
                    entities.ForEach(e =>
                    {
                        // 呼叫 DataTable 对象的 NewRow 方法来建立一个 DataRow 对象
                        DataRow newRow = myDataSet.Tables[tableName].NewRow();
                        SetTentityToDataRow<T>(e, newRow);
                        // 将新数据行新增至数据集内的数据表。
                        myDataSet.Tables[tableName].Rows.Add(newRow);
                        RecordCount += 1;
                    });

                    myDataAdapter.UpdateBatchSize = RecordCount;
                    this.BulkUpdataToServer(myConnection, myDataSet, tableName, RecordCount);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return RecordCount;
        }

        public int FastUpdataTo<T>(string sqlDestination, List<T> entities, out Exception exception) where T : class, new()
        {
            exception = null;
            SqlDataAdapter myDataAdapter = this.GetSqlDataAdapter(sqlDestination);
            SqlConnection myConnection = myDataAdapter.SelectCommand.Connection;
            int RecordCount = 0;
            string tableName = GetTableNameFromSql(sqlDestination);
            DataSet myDataSet = new DataSet();
            //如果字串比較為區分大小寫，則為 true，否則為 false。預設值為 false。
            myDataSet.CaseSensitive = true;
            //Try
            myDataAdapter.Fill(myDataSet, tableName);
            try
            {
                if (entities.Count > 0)
                {
                    entities.ForEach(e =>
                    {
                        // 呼叫 DataTable 对象的 NewRow 方法来建立一个 DataRow 对象
                        DataRow newRow = myDataSet.Tables[tableName].NewRow();
                        SetTentityToDataRow<T>(e, newRow);
                        // 将新数据行新增至数据集内的数据表。
                        myDataSet.Tables[tableName].Rows.Add(newRow);
                        RecordCount += 1;
                    });

                    myDataAdapter.UpdateBatchSize = RecordCount;
                    this.BulkUpdataToServer(myConnection, myDataSet, tableName, RecordCount);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return RecordCount;
        }

        /// <summary>
        /// 快速批量上传数据
        /// </summary>
        /// <param name="sqlDestination">目标表的Sql语句</param>
        /// <param name="entities">实体的属性要与目标表的字段名称及类型一致</param>
        /// <param name="checkIsExist">检测记录是否存在的处理程序</param>
        /// <returns></returns>
        public int FastUpdataTo<T>(string sqlDestination, List<T> entities, Func<T, bool> checkIsExist) where T : class, new()
        {
            SqlDataAdapter myDataAdapter = this.GetSqlDataAdapter(sqlDestination);
            SqlConnection myConnection = myDataAdapter.SelectCommand.Connection;
            int RecordCount = 0;
            string tableName = GetTableNameFromSql(sqlDestination);
            DataSet myDataSet = new DataSet();
            //如果字串比較為區分大小寫，則為 true，否則為 false。預設值為 false。
            myDataSet.CaseSensitive = true;
            //Try
            myDataAdapter.Fill(myDataSet, tableName);
            try
            {
                if (entities.Count > 0)
                {
                    entities.ForEach(e =>
                    {
                        // 呼叫 DataTable 对象的 NewRow 方法来建立一个 DataRow 对象
                        DataRow newRow = myDataSet.Tables[tableName].NewRow();
                        if (!checkIsExist(e))
                        {
                            SetTentityToDataRow<T>(e, newRow);
                            // 将新数据行新增至数据集内的数据表。
                            myDataSet.Tables[tableName].Rows.Add(newRow);
                            RecordCount += 1;
                        }
                    });

                    myDataAdapter.UpdateBatchSize = RecordCount;
                    this.BulkUpdataToServer(myConnection, myDataSet, tableName, RecordCount);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return RecordCount;
        }

        /// <summary>
        /// 快速上批量传数据
        /// </summary>
        /// <param name="sqlDestination">目标表的Sql语句</param>
        /// <param name="data">要上传的数据，字段名称要与Sql查询出来表的字段名称一样，但不要求类型一样</param>
        /// <param name="MapSRowToDRow">处理字段类型不一样的情况</param>
        /// <returns></returns>
        public int FastUpdataTo(string sqlDestination, DataTable data, Action<DataRow, DataRow> MapSRowToDRow)
        {
            SqlDataAdapter myDataAdapter = this.GetSqlDataAdapter(sqlDestination);
            SqlConnection myConnection = myDataAdapter.SelectCommand.Connection;
            int RecordCount = 0;
            string tableName = GetTableNameFromSql(sqlDestination);
            DataSet myDataSet = new DataSet();
            //如果字串比較為區分大小寫，則為 true，否則為 false。預設值為 false。
            myDataSet.CaseSensitive = true;
            //Try
            myDataAdapter.Fill(myDataSet, tableName);
            try
            {
                if (data.Rows.Count > 0)
                {
                    foreach (DataRow dr in data.Rows)
                    {
                        // 呼叫 DataTable 对象的 NewRow 方法来建立一个 DataRow 对象
                        DataRow newRow = myDataSet.Tables[tableName].NewRow();
                        //赋值
                        MapSRowToDRow(dr, newRow);
                        // 将新数据行新增至数据集内的数据表。
                        myDataSet.Tables[tableName].Rows.Add(newRow);
                        RecordCount += 1;
                    }
                    myDataAdapter.UpdateBatchSize = RecordCount;
                    this.BulkUpdataToServer(myConnection, myDataSet, tableName, RecordCount);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return RecordCount;
        }

        /// <summary>
        /// 快速批量上传数据
        /// </summary>
        /// <param name="sqlDestination">目标表的Sql语句</param>
        /// <param name="data">要上传的数据，字段及字段类型要与Sql查询出来表的字段一样</param>
        /// <returns>适合原表与目标表字段类型及结构完全一样的情况</returns>
        public int FastUpdataTo(string sqlDestination, DataTable data)
        {
            SqlDataAdapter myDataAdapter = this.GetSqlDataAdapter(sqlDestination);
            SqlConnection myConnection = myDataAdapter.SelectCommand.Connection;
            int RecordCount = 0;
            string tableName = GetTableNameFromSql(sqlDestination);
            DataSet myDataSet = new DataSet();
            //如果字串比較為區分大小寫，則為 true，否則為 false。預設值為 false。
            myDataSet.CaseSensitive = true;
            //Try
            myDataAdapter.Fill(myDataSet, tableName);
            try
            {
                if (data.Rows.Count > 0)
                {
                    myDataSet.Tables[tableName].Merge(data);
                    myDataAdapter.UpdateBatchSize = RecordCount;
                    this.BulkUpdataToServer(myConnection, myDataSet, tableName, RecordCount);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return RecordCount;
        }

        private void SetTentityToDataRow<T>(T entity, DataRow dr)
        {
            try
            {
                Type tity = entity.GetType();
                PropertyInfo[] Pis = tity.GetProperties();
                if (Pis.Length > 0)
                {
                    foreach (PropertyInfo pi in Pis)
                    {
                        if (pi.Name.ToUpper() == "ENTITYSTATE" || pi.Name.ToUpper() == "ENTITYKEY" || pi.Name.ToUpper() == "ID_KEY")
                        { }
                        else
                        {
                            object piProxyValue = pi.GetValue(entity, null);
                            if (dr.Table.Columns.Contains(pi.Name))
                                dr[pi.Name] = piProxyValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private string GetTableNameFromSql(string sql)
        {
            int start = sql.ToUpper().IndexOf("FROM", StringComparison.CurrentCulture) + 4;
            string subsql = sql.Substring(start, sql.Length - start).Trim();

            int whereIndex = sql.ToUpper().IndexOf("WHERE", StringComparison.CurrentCulture);
            if (whereIndex > 0)
            {
                return sql.Substring(start, whereIndex - start).Trim();
            }
            else
            {
                return subsql;
            }
        }

        #endregion Fast Upload Data

        #region ExcuteNonQuery

        public int ExecuteNonQueryWithTransaction(string Sql1, string Sql2)
        {
            int Count = 0;
            using (SqlConnection SqlConn = new SqlConnection(this.ConnStr()))
            {
                if (SqlConn.State != ConnectionState.Open)
                {
                    SqlConn.Open();
                }
                SqlTransaction SqlTran = SqlConn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = SqlConn;
                        cmd.Transaction = SqlTran;
                        cmd.CommandTimeout = 300;
                        cmd.CommandText = Sql1;
                        int count1 = cmd.ExecuteNonQuery();
                        cmd.CommandText = Sql2;
                        int count2 = cmd.ExecuteNonQuery();
                        SqlTran.Commit();
                        Count = count1 + count2;
                    }
                }
                catch (Exception ex)
                {
                    SqlTran.Rollback();
                    throw new Exception(ex.ToString());
                }
            }
            return Count;
        }

        public int ExecuteNonQuery(string Sql, SqlParameter[] sqlParas)
        {
            int count = 0;
            using (SqlConnection SqlConn = new SqlConnection(this.ConnStr()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (sqlParas != null)
                        {
                            foreach (SqlParameter sqlpara in sqlParas)
                            {
                                cmd.Parameters.Add(sqlpara);
                            }
                        }

                        cmd.Connection = SqlConn;
                        cmd.CommandTimeout = 300;
                        cmd.CommandText = Sql;
                        if (SqlConn.State != ConnectionState.Open)
                        {
                            SqlConn.Open();
                            count = cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            return count;
        }

        public int ExecuteNonQuery(string Sql, SqlParameter[] sqlParas, out Exception exception)
        {
            int count = 0;
            exception = null;
            using (SqlConnection SqlConn = new SqlConnection(this.ConnStr()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (sqlParas != null)
                        {
                            foreach (SqlParameter sqlpara in sqlParas)
                            {
                                cmd.Parameters.Add(sqlpara);
                            }
                        }

                        cmd.Connection = SqlConn;
                        cmd.CommandTimeout = 300;
                        cmd.CommandText = Sql;
                        if (SqlConn.State != ConnectionState.Open)
                        {
                            SqlConn.Open();
                            count = cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }
            return count;
        }

        public int ExecuteNonQuery(string Sql)
        {
            return ExecuteNonQuery(Sql, null);
        }

        public int ExecuteNonQuery(string Sql, out Exception exception)
        {
            return ExecuteNonQuery(Sql, null, out exception);
        }

        #endregion ExcuteNonQuery

        #region Insert
        /// <summary>
        /// 插入实体数据模型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">实体模型</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public int Insert<TEntity>(TEntity entity, string tableName)
        {
            int record = 0;
            StringBuilder sbFields = new StringBuilder();
            StringBuilder sbFieldValues = new StringBuilder();
            string sql = string.Format("Insert into {0} (", tableName);
            try
            {
                Type tity = entity.GetType();
                PropertyInfo[] Pis = tity.GetProperties();
                if (Pis.Length > 0)
                {
                    foreach (PropertyInfo pi in Pis)
                    {
                        if (pi.Name.ToUpper() == "ENTITYSTATE" || pi.Name.ToUpper() == "ENTITYKEY" || pi.Name.ToUpper() == "ID_KEY")
                        { }
                        else
                        {
                            sbFields.Append(pi.Name.Trim() + ",");
                            object piProxyValue = pi.GetValue(entity, null);
                            sbFieldValues.AppendFormat("'{0}',", piProxyValue);
                        }
                    }
                    sql = string.Format("{0}{1}) values ({2})", sql, sbFields.ToString().TrimEnd(','), sbFieldValues.ToString().TrimEnd(','));
                    return ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return record;
        }
        /// <summary>
        /// 插入实体数据模型,表名称从特性标注中获取，需要用特性的方式进行指定
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">实体模型</param>
        /// <returns></returns>
        public int Insert<TEntity>(TEntity entity)
        {
            Type tity = entity.GetType();
            var attribute = tity.GetCustomAttributes(typeof(LTableNameAttribute), false).FirstOrDefault();
            if (attribute == null) return 0;
            string tableName = ((LTableNameAttribute)attribute).TableName;
            return Insert<TEntity>(entity, tableName);
        }
        #endregion
    }
    /// <summary>
    /// 将对象持久化到文件中，
    /// 从文件中读取数据持久化到数据库中
    /// 整个操作只根据实体模型进行
    /// </summary>
    public class FileDbHelper
    {
        private static string GetTableNameFrom<TEntity>(TEntity entity)
        {
            string tableName = string.Empty;
            Type tity = entity.GetType();
            var attribute = tity.GetCustomAttributes(typeof(LTableNameAttribute), false).FirstOrDefault();
            if (attribute == null) return tableName;
            tableName = ((LTableNameAttribute)attribute).TableName;
            return tableName;
        }
        /// <summary>
        /// 从文件中读取内容转换为实体字典对象,包含表的名称
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> GetEntityDicFromTxtFile(string filePath)
        {
            List<Dictionary<string, object>> listDatas = new List<Dictionary<string, object>>();
            if (!File.Exists(filePath))
            {
                ErrorMessageTracer.LogMsgToFile("GetEntityDicFromTxtFile", string.Format("{0}不存在！", filePath));
                return listDatas;
            }
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    Dictionary<string, object> dataDic = null;
                    string line = sr.ReadLine();
                    string[] tblFields = line.Split(':');
                    if (tblFields.Length != 2)
                    {
                        ErrorMessageTracer.LogMsgToFile("GetEntityDicFromTxtFile", "文件中没有包含表名称的信息");
                        return listDatas;
                    }
                    if (tblFields[0].ToString().ToUpper() != "LEETABLENAME")
                    {
                        ErrorMessageTracer.LogMsgToFile("GetEntityDicFromTxtFile", "文件中没有包含表名称键值的信息");
                        return listDatas;
                    }
                    string tblKey = tblFields[0].ToString().Trim();
                    string tblName = tblFields[1].ToString().Trim();
                    while (!string.IsNullOrEmpty(line))
                    {
                        line = sr.ReadLine();
                        string[] fieldPair = line.Split(',');
                        if (fieldPair.Length > 0)
                        {
                            dataDic = new Dictionary<string, object>();
                            dataDic.Add(tblKey, tblName);
                            foreach (string fp in fieldPair)
                            {
                                string[] fv = fp.Split('*');
                                if (fv.Length == 2)
                                {
                                    dataDic.Add(fv[0].Trim(), fv[1].Trim());
                                }
                            }
                            listDatas.Add(dataDic);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessageTracer.LogErrorMsgToFile("GetEntityDicFromFile", ex);
            }
            return listDatas;
        }
        /// <summary>
        /// 将实体模型数据添加到文件中，文件中会记录实体模型对应的表名称
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="filePath"></param>
        public static void AppendFile<TEntity>(TEntity entity, string filePath) where TEntity : class, new()
        {
            try
            {
                Type tity = entity.GetType();
                PropertyInfo[] Pis = tity.GetProperties();
                if (Pis.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    string tableName = GetTableNameFrom<TEntity>(entity);
                    if (!File.Exists(filePath))
                        sb.AppendLine(string.Format("leeTableName:{0}", tableName));
                    foreach (PropertyInfo pi in Pis)
                    {
                        if (pi.Name.ToUpper() == "ENTITYSTATE" || pi.Name.ToUpper() == "ENTITYKEY" || pi.Name.ToUpper() == "ID_KEY")
                        { }
                        else
                        {
                            object piProxyValue = pi.GetValue(entity, null);
                            sb.AppendFormat("{0}*{1},", pi.Name, piProxyValue);
                        }
                    }
                    filePath.AppendFile(sb.ToString().TrimEnd(','));
                }
            }
            catch (Exception ex)
            {
                ErrorMessageTracer.LogErrorMsgToFile("AppendFile<TEntity>", ex);
            }
        }
    }

    [System.AttributeUsage(AttributeTargets.Class)]
    public class LTableNameAttribute : Attribute
    {
        private string tableName;
        /// <summary>
        /// 模型映射表的名字
        /// </summary>
        public string TableName
        {
            get { return tableName; }
        }

        public LTableNameAttribute(string tablename)
        {
            this.tableName = tablename;
        }
    }
    [System.AttributeUsage(AttributeTargets.Property)]
    public class LTableFieldLengthAttribute : Attribute
    {
        private int fieldLength;
        public int FieldLength { get; set; }

        public LTableFieldLengthAttribute(int fieldlength)
        {
            this.fieldLength = fieldlength;
        }
    }
    /// <summary>
    /// 表的字段信息
    /// </summary>
    public class TableColumnInfo
    {
        public string TableName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }
    }

    public static class FileOpExtension
    {
        /// <summary>
        /// 对已经存在的文件进行内容附加
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">要附加的文件内容</param>
        /// <param name="encoding">写入文件时采用的编码格式</param>
        public static void AppendFile(this string filePath, string fileContent, Encoding encoding)
        {
            string DirectoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(DirectoryPath)) Directory.CreateDirectory(DirectoryPath);
            using (StreamWriter sw = new StreamWriter(filePath, true, encoding))
            {
                sw.WriteLine(fileContent);
                sw.Flush();
            }
        }
        /// <summary>
        /// 向文件中写入文本内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileContent">文件内容</param>
        public static void AppendFile(this string filePath, string fileContent)
        {
            filePath.AppendFile(fileContent, Encoding.Default);
        }
    }
}