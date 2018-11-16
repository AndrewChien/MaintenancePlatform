using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ZNC.DataAccess.DA
{
    /// <summary>
    /// AndrewChien 2017-10-19 22:10:12
    /// </summary>
    public class CommonAccess
    {
        /// <summary>
        /// 根据配置数据库配置名称生成Database对象
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase()
        {
            Database db = null;
            //if (string.IsNullOrEmpty(dbConfigName))
            //{
            //    db = DatabaseFactory.CreateDatabase();
            //}
            //else
            //{
            //    db = DatabaseFactory.CreateDatabase(dbConfigName);
            //}

            DbConnectionStringBuilder sb = db.DbProviderFactory.CreateConnectionStringBuilder();
            sb.ConnectionString = GetConnectionString();
            GenericDatabase newDb = new GenericDatabase(sb.ToString(), db.DbProviderFactory);
            db = newDb;

            return db;
        }

        public static string GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;
            //connectionString = connectionString.Replace("$Start$", AppDomain.CurrentDomain.BaseDirectory);
            connectionString = connectionString.Replace("$Start$", "");
            return connectionString;
        }

        #region 基本CURD

        /// <summary>
        /// 数据库的插入或更新的方法（使用微软企业库）
        /// </summary>
        /// <param name="obj">要存入数据库的对象</param>
        /// <returns></returns>
        public static int Insert(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object obj)
        {
            //var db = DBFactory.CreateDefault();
            var n = 0;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlend = new StringBuilder();
                //获取对象的属性数组
                PropertyInfo[] pro = obj.GetType().GetProperties();
                //主键属性数组
                List<PropertyInfo> idlist = GetIdProperty(pro);
                //要更新的数据表
                string table = FindPropertyInfoValue(obj, "TableName").ToString();
                //执行的sql语句
                string sqltext = string.Empty;
                //INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
                sql.Append("INSERT INTO " + table + "(");
                sqlend.Append(" VALUES (");

                foreach (PropertyInfo item in pro)
                {//拼接sql语句主体
                    if (item.Name == "TableName")
                    {
                        continue;
                    }
                    else
                    {
                        string columnValue = item.GetValue(obj, null) + "";
                        if (string.IsNullOrEmpty(columnValue))
                        {//去掉空属性
                            continue;
                        }
                        if (item.PropertyType == typeof(DateTime))
                        {//时间属性初始化时未赋值会变为默认最小值
                            DateTime dt;
                            DateTime.TryParse(columnValue, out dt);
                            if (dt <= SqlDateTime.MinValue.Value)
                                continue;
                        }
                        sql.Append(" " + item.Name + ",");
                        sqlend.Append(" '" + columnValue + "',");
                    }
                }
                string start = sql.ToString();
                start = start.Substring(0, start.Length - 1) + ")";
                string end = sqlend.ToString();
                end = end.Substring(0, end.Length - 1) + ")";
                sqltext = start + end;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqltext;
                    n = cmd.ExecuteNonQuery();
                }
            }
            return n;
        }

        /// <summary>
        /// 数据库更新方法（使用微软企业库）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Update(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object obj)
        {
            //var db = DBFactory.CreateDefault();
            int n = 0;
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlend = new StringBuilder();
                //获取对象的属性数组
                PropertyInfo[] pro = obj.GetType().GetProperties();
                //主键属性数组
                List<PropertyInfo> idlist = GetIdProperty(pro);
                //要更新的数据表
                string table = FindPropertyInfoValue(obj, "TableName").ToString();
                //执行的sql语句
                string sqltext = string.Empty;
                //UPDATE 表名称 SET 列名称 = 新值 WHERE 列名称 = 某值 and  列2=某值
                sql.Append("UPDATE " + table + " set");
                sqlend.Append("WHERE");
                //拼接sql语句主体
                foreach (PropertyInfo item in pro)
                {
                    if (item.Name == "TableName")
                    {
                        continue;
                    }
                    else
                    {
                        sql.Append(" " + item.Name + "= " + item.GetValue(obj, null));
                    }
                }
                //根据主键增加定位条件
                foreach (PropertyInfo item in idlist)
                {
                    sqlend.Append(" " + item.Name + "= '" + item.GetValue(obj, null) + ", and ");
                }
                string start = sql.ToString();
                start = start.Substring(0, start.Length - 1) + " ";
                string end = sqlend.ToString();
                end = end.Substring(0, end.Length - 5) + " ";
                sqltext = start + end;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqltext;
                    n = cmd.ExecuteNonQuery();
                }
            }
            return n;
        }

        /// <summary>
        /// 数据库主键查询（使用微软企业库）
        /// </summary>
        /// <param name="pdata"></param>
        /// <returns></returns>
        public static object GetById(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object pdata)
        {
            //var db = DBFactory.CreateDefault();
            PropertyInfo[] pro = pdata.GetType().GetProperties();//获取传来的对象的属性
            List<PropertyInfo> idlist = GetIdProperty(pro);
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlend = new StringBuilder();
                string table = FindPropertyInfoValue(pdata, "TableName").ToString();
                string sqltext = string.Empty;

                sql.AppendFormat("select * from {0} where", table);
                foreach (PropertyInfo item in idlist)
                {
                    if (item.Name == "TableName")
                    {
                        continue;
                    }
                    else
                    {
                        sql.Append(" " + item.Name + " = '" + item.GetValue(pdata, null) + "'");
                    }
                }
                sqltext = sql.ToString();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqltext;
                    DbDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    dr.Read();
                    foreach (PropertyInfo item in pro)
                    {
                        if (item.Name == "TableName")
                        {
                            continue;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(dr[item.Name].ToString()))
                            {
                                continue;
                            }
                            item.SetValue(pdata, dr[item.Name], null);
                        }
                    }
                }
            }
            return pdata;
        }

        /// <summary>
        /// 全部查询（使用微软企业库）
        /// </summary>
        /// <returns></returns>
        private static object GetAll(object _model, string _sql, string _tablename)
        {
            //try
            //{
            //    var ds = new DSInspectionRegistration();
            //    Microsoft.Practices.EnterpriseLibrary.Data.Database db = DBFactory.CreateDefault();
            //    DbCommand cmd = db.GetSqlStringCommand("select * from common.applicationdd where BaseDDType ='Common.TestMethod'");
            //    //DataSet dss = db.ExecuteDataSet(cmd);
            //    db.LoadDataSet(cmd, ds, new string[] { "ApplicationDD" });//xsd表名TestItemSub
            //    return ds;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return null;
        }


        /// <summary>
        /// 数据库删除（使用微软企业库）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="pdata"></param>
        /// <returns></returns>
        public static int DeleteById(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object pdata)
        {
            //var db = DBFactory.CreateDefault();
            int n = 0;//影响的行数
            PropertyInfo[] pro = pdata.GetType().GetProperties();//获取传来的对象的属性
            List<PropertyInfo> idlist = GetIdProperty(pro);//找到主键属性
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlend = new StringBuilder();
                string table = FindPropertyInfoValue(pdata, "TableName").ToString();
                string sqltext = string.Empty;

                sql.AppendFormat("delete from {0} where", table);
                foreach (PropertyInfo item in idlist)
                {
                    sql.Append(" " + item.Name + " = '" + item.GetValue(pdata, null) + "'");
                }
                sqltext = sql.ToString();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqltext;
                    n = cmd.ExecuteNonQuery();
                }
            }
            return n;
        }

        /*
        public static List<T> GetPagedList<T>(T obj, int pPageIndex, int pPageSize, string pOrderBy, string pSortExpression, out int pRecordCount)
        {
            if (pPageIndex <= 1)
                pPageIndex = 1;
            List<T> list = new List<T>();
            Query q = Repair.CreateQuery();
            pRecordCount = q.GetRecordCount();
            q.PageIndex = pPageIndex;
            q.PageSize = pPageSize;
            q.ORDER_BY(pSortExpression, pOrderBy.ToString());
            RepairCollection collection = new RepairCollection();
            collection.LoadAndCloseReader(q.ExecuteReader());

            foreach (Repair repair in collection)
            {
                T repairInfo = new T();
                LoadFromDAL(T, repair);
                list.Add(repairInfo);
            }


            return list;

        }
         */

        #endregion

        #region 数据集通用方法分步

        #region Get方法

        /// <summary>
        /// 不限定数据库表前缀查询（要求T所有表均有对应的实体表）
        /// </summary>
        /// <typeparam name="T">数据集</typeparam>
        /// <param name="_db"></param>
        /// <returns></returns>
        public static T UniversalizationGetAll<T>(Microsoft.Practices.EnterpriseLibrary.Data.Database _db)
        {
            return UniversalizationGetAll<T>(_db, "");
        }

        /// <summary>
        /// 限定数据库表前缀查询（要求T所有表均有对应的实体表）
        /// 示例（所有表获取）：DAOHelp.UniversalizationGetAll<DSSLMSEntering>(CommonApp.DBFactory.CreateDefault(), "[SLMSDB_I].[dbo].");
        /// </summary>
        /// <typeparam name="T">数据集</typeparam>
        /// <param name="_db"></param>
        /// <param name="_tablenameprefix">前缀为空时，则指数据集表名与数据库表名一致</param>
        /// <returns></returns>
        public static T UniversalizationGetAll<T>(Microsoft.Practices.EnterpriseLibrary.Data.Database _db, string _tablenameprefix)
        {
            try
            {
                var type = typeof(T);
                var ppt = type.GetProperties();
                var model = Activator.CreateInstance<T>();
                var sql = "";
                //获取表集合
                var tbs = new List<string>();
                foreach (var t in ppt)
                {
                    var eltype = t.ToString().Split(' ')[0];
                    if (!eltype.Contains("DataTable"))
                        continue;
                    if (eltype.Substring(eltype.Length - 9) != "DataTable")
                        continue;
                    sql += "select * from " + _tablenameprefix + t.Name + ";"; //注意：xsd中表名应当跟数据库表名一致
                    tbs.Add(t.Name);
                }
                var cmd = _db.GetSqlStringCommand(sql);
                _db.LoadDataSet(cmd, model as DataSet, tbs.ToArray());//return回来的dataset表名
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定的表(适用含虚体表的T)
        /// 示例（指定表获取）：DAOHelp.UniversalizationGetAll<DSSLMSEntering>(CommonApp.DBFactory.CreateDefault(), new List<string>() { "[SLMSDB_I].[dbo].[SampleInfo]", "[SLMSDB_I].[dbo].[SampleResult]" });
        /// </summary>
        /// <typeparam name="T">数据集</typeparam>
        /// <param name="_db"></param>
        /// <param name="_tablenames">注意表名要用数据库全称</param>
        /// <returns></returns>
        public static T UniversalizationGetAll<T>(Microsoft.Practices.EnterpriseLibrary.Data.Database _db, List<string> _tablenames)
        {
            try
            {
                var model = Activator.CreateInstance<T>();
                var sql = _tablenames.Aggregate("", (current, _tb) => current + ("select * from " + _tb + ";"));
                var cmd = _db.GetSqlStringCommand(sql);
                //注意：如果需要使返回的table名不含有数据库表的限定名，则如下：
                var list = new List<string>();
                foreach (var tbn in _tablenames)
                {
                    var ele = "";
                    if (tbn.Contains("."))
                    {
                        ele = (tbn.Split('.'))[(tbn.Split('.')).Length - 1];
                    }
                    if (ele.StartsWith("["))
                    {
                        ele = ele.Substring(1, ele.Length - 2);
                    }
                    list.Add(ele);
                }
                _db.LoadDataSet(cmd, model as DataSet, list.ToArray());//return回来的dataset表名
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据条件where获取单表
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_tablename">注意表名要用数据库全称</param>
        /// <param name="_sqlwhere">限定语句，必须and开头，可留空</param>
        /// <returns></returns>
        public static T UniversalizationGetwhere<T>(Microsoft.Practices.EnterpriseLibrary.Data.Database _db, string _tablename, string _sqlwhere)
        {
            try
            {
                var sql = "select * from " + _tablename + " where 1=1 " + _sqlwhere;
                var model = Activator.CreateInstance<T>();
                var cmd = _db.GetSqlStringCommand(sql);
                //注意：如果需要使返回的table名不含有数据库表的限定名，则如下：
                if (_tablename.Contains("."))
                {
                    _tablename = (_tablename.Split('.'))[(_tablename.Split('.')).Length - 1];
                }
                if (_tablename.StartsWith("["))
                {
                    _tablename = _tablename.Substring(1, _tablename.Length - 2);
                }
                _db.LoadDataSet(cmd, model as DataSet, _tablename);//return回来的dataset表名
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询数据库是否有对应记录（是否唯一）
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_tablename">>注意表名要用数据库全称</param>
        /// <param name="_sqlwhere">限定语句，必须and开头，可留空</param>
        /// <returns></returns>
        public static bool UniversalizationGetUnique(Microsoft.Practices.EnterpriseLibrary.Data.Database _db, string _tablename, string _sqlwhere)
        {
            try
            {
                var ds = new DataSet();
                var sql = "select * from " + _tablename + " where 1=1 " + _sqlwhere;
                var cmd = _db.GetSqlStringCommand(sql);
                _db.LoadDataSet(cmd, ds, _tablename);
                return ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Set方法

        /// <summary>
        /// 通用保存（带前缀、不限制_isIDENTITY_INSERT_OFF的指定表获取）
        /// 少用（插入时会连带ID）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_model"></param>
        /// <param name="_tablenameprefix"></param>
        /// <param name="_tablenameNoprefix"></param>
        /// <returns></returns>
        public static int UniversalizationSave(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object _model, string _tablenameprefix, string _tablenameNoprefix)
        {
            return UniversalizationSave(db, _model, _tablenameprefix, _tablenameNoprefix, false, "");
        }

        /// <summary>
        /// 通用保存（带前缀、限制_isIDENTITY_INSERT_OFF的全表获取）
        /// </summary>
        /// <param name="db"></param>
        /// <param name="_model"></param>
        /// <param name="_tablenameprefix"></param>
        /// <param name="_isIDENTITY_INSERT_OFF">是否关闭主键列插入功能（插入使用，一般为ID）注意此处要求数据集中所有表主键同名</param>
        /// <param name="_identity">显式指定主键名称（_isIDENTITY_INSERT_OFF为true时，与其一起使用）</param>
        /// <returns></returns>
        public static int UniversalizationSave(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object _model,
            string _tablenameprefix, bool _isIDENTITY_INSERT_OFF, string _identity)
        {
            return UniversalizationSave(db, _model, _tablenameprefix, "", _isIDENTITY_INSERT_OFF, _identity);
        }

        /// <summary>
        /// 通用保存（带前缀、限制_isIDENTITY_INSERT_OFF的单表获取）
        /// 示例（所有表保存）：DAOHelp.UniversalizationSave(CommonApp.DBFactory.CreateDefault(), res, "[SLMSDB_I].[dbo].", "", true, "ID");
        /// 示例（指定表保存）：DAOHelp.UniversalizationSave(CommonApp.DBFactory.CreateDefault(), res, "[SLMSDB_I].[dbo].", "SampleInfo", true, "ID");
        /// </summary>
        /// <param name="db">Database db = CommonApp.DBFactory.CreateDefault();</param>
        /// <param name="_model">model载具</param>
        /// <param name="_tablenameprefix">如果数据库表有限定开头(如[Common].[table1])，则必须加上前缀；没有前缀则留空（注意本处留空不能让所有表保存）</param>
        /// <param name="_tablenameNoprefix">指定表保存(注意这里是数据集的table，无前缀无[]),注意本处留空则所有表保存</param>
        /// <param name="_isIDENTITY_INSERT_OFF">是否关闭主键列插入功能（插入使用，一般为ID）</param>
        /// <param name="_identity">显式指定主键名称（_isIDENTITY_INSERT_OFF为true时，与其一起使用）</param>
        /// <returns></returns>
        public static int UniversalizationSave(Microsoft.Practices.EnterpriseLibrary.Data.Database db, object _model,
            string _tablenameprefix, string _tablenameNoprefix, bool _isIDENTITY_INSERT_OFF, string _identity)
        {
            using (var conn = db.CreateConnection())
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    if (string.IsNullOrEmpty(_tablenameNoprefix)) //_table留空，保存全部
                    {
                        var tables = _model.GetType().GetProperties();//获取表集合
                        foreach (var t in tables)
                        {
                            var eltype = t.ToString().Split(' ')[0];
                            if (!eltype.Contains("DataTable"))
                                continue;
                            if (eltype.Substring(eltype.Length - 9) != "DataTable")
                                continue;
                            var table = t.GetValue(_model, null) as DataTable;//获取集合中的元素
                            if (table == null) continue;
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                //逐行执行
                                if (table.Rows[i].RowState == DataRowState.Added)
                                {
                                    UniversalizationInsert(db, table.Rows[i], _tablenameprefix + t.Name,
                                        _isIDENTITY_INSERT_OFF, _identity);
                                    continue;
                                }
                                if (table.Rows[i].RowState == DataRowState.Modified)
                                {
                                    UniversalizationUpdate(db, table.Rows[i], _tablenameprefix + t.Name, _identity);
                                    continue;
                                }
                                if (table.Rows[i].RowState == DataRowState.Deleted)
                                {
                                    UniversalizationDelete(db, table.Rows[i], _tablenameprefix + t.Name, _identity);
                                    continue;
                                }
                                //trans.Commit();
                            }
                        }
                    }
                    else //_table不留空保存指定表
                    {
                        var tables = _model.GetType().GetProperties();//获取集合
                        DataTable table = null;
                        foreach (var info in tables)
                        {
                            if (info.Name == _tablenameNoprefix)
                            {
                                table = info.GetValue(_model, null) as DataTable;//获取集合中的元素
                            }
                        }
                        if (table == null) return 0;
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            //逐行执行
                            if (table.Rows[i].RowState == DataRowState.Added)
                            {
                                UniversalizationInsert(db, table.Rows[i], _tablenameprefix + _tablenameNoprefix,
                                    _isIDENTITY_INSERT_OFF, _identity);
                                continue;
                            }
                            if (table.Rows[i].RowState == DataRowState.Modified)
                            {
                                UniversalizationUpdate(db, table.Rows[i], _tablenameprefix + _tablenameNoprefix, _identity);
                                continue;
                            }
                            if (table.Rows[i].RowState == DataRowState.Deleted)
                            {
                                UniversalizationDelete(db, table.Rows[i], _tablenameprefix + _tablenameNoprefix, _identity);
                                continue;
                            }
                            //trans.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            return 0;
        }

        /// <summary>
        /// 通用插入
        /// </summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="row">DataRow</param>
        /// <param name="_table">插入表名称</param>
        /// <param name="_isIDENTITY_INSERT_OFF">是否关闭主键列插入功能</param>
        /// <param name="_identity">主键列名称</param>
        public static int UniversalizationInsert(Microsoft.Practices.EnterpriseLibrary.Data.Database db, DataRow row, string _table, bool _isIDENTITY_INSERT_OFF, string _identity)
        {
            try
            {
                var sql = new StringBuilder();
                var sqlend = new StringBuilder();
                sql.Append("INSERT INTO " + _table + " (");
                sqlend.Append(" VALUES (");
                var cmd = db.DbProviderFactory.CreateCommand();
                //获取所有的row的属性（获取列集合）
                var cols = row.GetType().GetProperties();
                foreach (var col in cols)
                {
                    //insert去掉主键列插入（仅当数据库IDENTITY_INSERT=OFF时适用；为ON时不用该判断）
                    if (_isIDENTITY_INSERT_OFF && col.Name == _identity)
                    {
                        continue;
                    }
                    //获取某列的值
                    var val = Try2String(GetRowValue(row, col.Name));
                    //去掉空属性
                    if (string.IsNullOrEmpty(val))
                    {
                        continue;
                    }
                    //时间属性初始化时未赋值会变为默认最小值
                    if (col.PropertyType == typeof(DateTime))
                    {
                        DateTime dt;
                        DateTime.TryParse(val, out dt);
                        if (dt <= SqlDateTime.MinValue.Value)
                            continue;
                    }
                    sql.Append(" " + col.Name + ",");
                    sqlend.Append(" '" + val + "',");
                }
                var start = sql.ToString();
                start = start.Substring(0, start.Length - 1) + ")";
                var end = sqlend.ToString();
                end = end.Substring(0, end.Length - 1) + ")";
                cmd.CommandText = start + end;
                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通用更新
        /// </summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="row">DataRow</param>
        /// <param name="_table">更新表名称</param>
        /// <param name="_identity">主键列名称</param>
        /// <returns></returns>
        public static int UniversalizationUpdate(Microsoft.Practices.EnterpriseLibrary.Data.Database db, DataRow row, string _table, string _identity)
        {
            try
            {
                var cmd = db.DbProviderFactory.CreateCommand();
                var sql = new StringBuilder();
                var sqlend = new StringBuilder();
                sql.Append("UPDATE " + _table + " set");
                sqlend.Append("WHERE");
                //获取所有的row的属性（获取列集合）
                var cols = row.GetType().GetProperties();
                foreach (var col in cols)
                {
                    //主键列不更新
                    if (col.Name == _identity)
                    {
                        continue;
                    }
                    //获取某列的值
                    var val = Try2String(GetRowValue(row, col.Name));
                    //去掉空属性
                    if (string.IsNullOrEmpty(val))
                    {
                        continue;
                    }
                    //时间属性初始化时未赋值会变为默认最小值
                    if (col.PropertyType == typeof(DateTime))
                    {
                        DateTime dt;
                        DateTime.TryParse(val, out dt);
                        if (dt <= SqlDateTime.MinValue.Value)
                            continue;
                    }
                    sql.Append(" " + col.Name + " = '" + val + "',");
                }
                sqlend.Append(" " + _identity + "= '" + Try2String(GetRowValue(row, _identity)) + "'");
                var start = sql.ToString();
                start = start.Substring(0, start.Length - 1) + " ";
                var end = sqlend.ToString();
                end = end.Substring(0, end.Length) + " ";
                cmd.CommandText = start + end;
                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通用删除
        /// </summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="row">DataRow</param>
        /// <param name="_table">表名称</param>
        /// <param name="_identity">主键列名称</param>
        /// <returns></returns>
        public static int UniversalizationDelete(Microsoft.Practices.EnterpriseLibrary.Data.Database db, DataRow row, string _table, string _identity)
        {
            var idval = Try2String(row[_identity, DataRowVersion.Original]);
            if (idval == "") return 0;
            var Sql = "delete from " + _table + " where  " + _identity + "='" + idval + "';";
            try
            {
                var cmd = db.DbProviderFactory.CreateCommand();
                cmd.CommandText = Sql;
                return db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据条件where单表删除记录
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_tablename">注意表名要用数据库全称</param>
        /// <param name="_sqlwhere">限定语句，必须and开头，不能为空！</param>
        /// <returns></returns>
        public static int UniversalizationDeleteWhere(Microsoft.Practices.EnterpriseLibrary.Data.Database _db, string _tablename, string _sqlwhere)
        {
            if (string.IsNullOrEmpty(_sqlwhere)) return 0;//防止误操作，禁止无条件删除
            try
            {
                var sql = "delete from " + _tablename + " where 1=1 " + _sqlwhere;
                var cmd = _db.DbProviderFactory.CreateCommand();
                cmd.CommandText = sql;
                return _db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region 辅助方法

        /// <summary>
        /// 查询转换为List数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sdr"></param>
        /// <returns></returns>
        public static List<T> ConvertData<T>(DbDataReader sdr)
        {
            List<T> list = new List<T>();
            Type type = typeof(T);//获取类型
            PropertyInfo[] properties = type.GetProperties();//获取
            while (sdr.Read())
            {
                T model = Activator.CreateInstance<T>();
                for (int i = 0; i < properties.Length; i++)
                {
                    for (int j = 0; j < sdr.FieldCount; j++)
                    {
                        //判断属性的名称和字段的名称是否相同
                        if (properties[i].Name == sdr.GetName(j))
                        {
                            Object value = sdr[j];
                            //将字段的值赋值给User中的属性
                            properties[i].SetValue(model, value, null);
                        }
                    }
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 查找返回属性数组中指定名称的属性
        /// </summary>
        /// <param name="pdata"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        private static object FindPropertyInfoValue(object pdata, string proName)
        {
            try
            {
                Type tp = pdata.GetType();
                PropertyInfo pro = tp.GetProperty(proName);
                return pro.GetValue(pdata, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回对应名称的属性
        /// </summary>
        /// <param name="pros"></param>
        /// <param name="proName"></param>
        /// <returns></returns>
        private static PropertyInfo FindPropertyInfo(PropertyInfo[] pros, string proName)
        {
            foreach (PropertyInfo item in pros)
            {
                if (item.Name == proName)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 返回主键属性
        /// </summary>
        /// <param name="pros">属性数组</param>
        /// <returns>主键属性</returns>
        private static List<PropertyInfo> GetIdProperty(PropertyInfo[] pros)
        {
            List<PropertyInfo> keyIds = new List<PropertyInfo>();
            foreach (PropertyInfo item in pros)
            {
                //KeyFlagAttribute flag = item.GetCustomAttributes(typeof(KeyFlagAttribute), false)[0] as KeyFlagAttribute;
                //if (flag.IsKey)
                //{
                //    return item;
                //}
                object[] attrs = item.GetCustomAttributes(false);
                foreach (var id in attrs)
                {
                    if (id.GetType().Name == "KeyFlagAttribute")
                    {
                        keyIds.Add(item);
                    }
                }

            }
            return keyIds;
        }

        private static object GetRowValue(object pros, string proName)
        {
            try
            {
                var r = pros as DataRow;
                if (r != null) return r[proName];
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static string Try2String(object _o)
        {
            if (_o == null || _o == DBNull.Value) return "";
            try
            {
                return _o.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        /*
         * 附录：
         * Microsoft.Practices.EnterpriseLibrary.Data的使用
         * 
       Database db = null;
      
      
      #region 一般调用   
      db = DatabaseFactory.CreateDatabase("Connection String");   
      int count = (int)db.ExecuteScalar(CommandType.Text, "SELECT Count(*) From cms_company");   
      string message = string.Format("There are {0} customers in the database", count.ToString());   
      Response.Write(message);  
      #endregion  
 
 
      #region 带返回参数，返回值和返回数据集，一般参数的存储过程   
      //CREATE PROCEDURE [dbo].[kword]   
      //@kword varchar(250)='',   
      //@top int,   
      //@otop varchar(250) output   
      //As   
      //select top 10 * from Table1 where ntitle like '%'+@kword+'%' and id>@top   
      //declare @flag int   
      //select @flag=100   
      //SET @otop='返回值'   
      //return @flag   
      db = DatabaseFactory.CreateDatabase("serverConnectionString");//连接字符串变量名   
      DbCommand dbcomm = db.GetStoredProcCommand("kword");//存储过程名   
      db.AddInParameter(dbcomm, "@kword", DbType.String, "创业");//参数名 类型 值   
      db.AddInParameter(dbcomm, "top", DbType.Int32, 2);//参数名 类型 值   
      db.AddOutParameter(dbcomm, "otop", DbType.String, 250);//output参数名 类型 长度   
      //关键在这里，添加一个参数@RETURN_VALUE 类型为ReturnValue   
      db.AddParameter(dbcomm, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);   
      DataSet ds = db.ExecuteDataSet(dbcomm);//必须有执行的动作后面才能获取值   
      //title = (string)db.ExecuteScalar(dbcomm);如果返回只有一个数据，这样也是可以的   
      GridView1.DataSource = ds;   
      GridView1.DataBind();   
  
      Response.Write("<br>output输出参数值：" + db.GetParameterValue(dbcomm, "otop").ToString());   
      // int testvalue = (int)dbcomm.Parameters["@RETURN_VALUE"].Value; //另一种获取值的方式   
      Response.Write("<br />return返回参数值：" + db.GetParameterValue(dbcomm, "RETURN_VALUE").ToString());  
      #endregion  
 
 
 
      #region 使用事务记录操作数据库   
      //CREATE TABLE [dbo].[Table1](   
      //    [id] [int] IDENTITY(1,1) NOT NULL,   
      //    [ntitle] [varchar](250) NOT NULL,   
      //    [valuea] [varchar](250) NULL,   
      // CONSTRAINT [PK_Table1] PRIMARY KEY CLUSTERED    
      //(   
      //    [ntitle] ASC   
      //)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]   
      //) ON [PRIMARY]   
      db = DatabaseFactory.CreateDatabase("serverConnectionString");   
      using (IDbConnection conn = db.CreateConnection())   
      {   
          conn.Open();   
          IDbTransaction _trans = conn.BeginTransaction();   
          try   
          {   
              DbCommand _cmd = db.GetSqlStringCommand("INSERT INTO [Table1]([ntitle],[valuea]) VALUES(@ntitle,@valuea)");   
              db.AddInParameter(_cmd, "ntitle", DbType.String, "AA");   
              db.AddInParameter(_cmd, "valuea", DbType.String, "AA");   
              db.ExecuteNonQuery(_cmd, _trans as DbTransaction);   
              db.ExecuteNonQuery(_cmd, _trans as DbTransaction);//ntitle字段上建有唯一索引，故第二次插入同样记录时会报错   
              _trans.Commit();   
          }   
          catch   
          {   
              try   
              {   
                  _trans.Rollback();//事务提交失败时，则回滚(是否回滚成功，可查看表中有无AA的记录即可)   
              }   
              catch { }   
          }   
          finally   
          {   
              conn.Close();   
          }   
      }  
      #endregion
         * 
         * 
         */

        #region SQLite通用方法

        private const string SQLiteConn = "Data Source=ZNCPlatform.db;Version=3;";

        /// <summary>
        /// SQLite通用插入
        /// </summary>
        /// <param name="_model">实体</param>
        /// <returns></returns>
        public static int ComSQLiteInsert(object _model)
        {
            var conn = new SQLiteConnection(SQLiteConn);
            var tbname = _model.GetType().Name;
            var sql = new StringBuilder();
            var sqlend = new StringBuilder();
            var head = "INSERT INTO " + tbname + " (";
            sqlend.Append(" VALUES (");
            var cols = _model.GetType().GetProperties();
            foreach (var col in cols)
            {
                if (col.Module.Name == "ZNC.DataEntiry.dll") //仅实体类本身的属性，不包含父类属性
                {
                    var val = col.GetValue(_model, null);
                    if (string.IsNullOrEmpty(val?.ToString()))
                    {
                        continue;
                    }
                    if (col.PropertyType == typeof(DateTime))
                    {
                        DateTime dt;
                        DateTime.TryParse(val.ToString(), out dt);
                        if (dt <= SqlDateTime.MinValue.Value)
                            continue;
                    }
                    sql.Append(" " + col.Name + ",");
                    sqlend.Append(" '" + val + "',");
                }
            }
            var start = sql.ToString();
            start = start.Substring(0, start.Length - 1) + ")";
            var end = sqlend.ToString();
            end = end.Substring(0, end.Length - 1) + ")";
            var realsql = head + start + end;
            return SqLiteHelper.ExecuteNonQuery(conn, realsql, null);
        }

        /// <summary>
        /// SQLite通用更新
        /// </summary>
        /// <param name="_model">实体</param>
        /// <param name="_identity">主键</param>
        /// <returns></returns>
        public static int ComSQLiteUpdate(object _model, string _identity)
        {
            var conn = new SQLiteConnection(SQLiteConn);
            var tbname = _model.GetType().Name;
            var sql = new StringBuilder();
            var sqlend = new StringBuilder();
            var head = "UPDATE " + tbname + " set";
            sqlend.Append("WHERE");
            var cols = _model.GetType().GetProperties();
            var _identityval = "";
            foreach (var col in cols)
            {
                if (col.Module.Name == "ZNC.DataEntiry.dll") //仅实体类本身的属性，不包含父类属性
                {
                    var val = col.GetValue(_model, null);
                    if (col.Name == _identity)
                    {
                        _identityval = val.ToString();
                        continue;
                    }
                    if (string.IsNullOrEmpty(val?.ToString()))
                    {
                        continue;
                    }
                    if (col.PropertyType == typeof(DateTime))
                    {
                        DateTime dt;
                        DateTime.TryParse(val.ToString(), out dt);
                        if (dt <= SqlDateTime.MinValue.Value)
                            continue;
                    }
                    sql.Append(" " + col.Name + " = '" + val + "',");
                }
            }
            sqlend.Append(" " + _identity + "= '" + _identityval + "'");
            var start = sql.ToString();
            start = start.Substring(0, start.Length - 1) + " ";
            var end = sqlend.ToString();
            end = end.Substring(0, end.Length) + " ";
            var realsql = head + start + end;
            return SqLiteHelper.ExecuteNonQuery(conn, realsql, null);
        }

        /// <summary>
        /// SQLite通用删除
        /// </summary>
        /// <param name="_model">实体</param>
        /// <param name="_identity">主键</param>
        /// <returns></returns>
        public static int ComSQLiteDelete(object _model, string _identity)
        {
            var cols = _model.GetType().GetProperties();
            var tbname = _model.GetType().Name;
            var _identityval = "";
            foreach (var col in cols)
            {
                if (col.Module.Name == "ZNC.DataEntiry.dll") //仅实体类本身的属性，不包含父类属性
                {
                    var val = col.GetValue(_model, null);
                    if (col.Name != _identity) continue;
                    _identityval = val.ToString();
                }
            }
            var sql = "DELETE FROM [" + tbname + "] WHERE [" + _identity + "] = " + _identityval;
            var conn = new SQLiteConnection(SQLiteConn);
            return SqLiteHelper.ExecuteNonQuery(conn, sql, null);
        }

        /// <summary>
        /// 通用查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_sqlwhere"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ComSQLiteSelect<T>(string _sqlwhere)
        {
            try
            {
                var collection = new ObservableCollection<T>();
                var model = Activator.CreateInstance<T>();
                var modeltype = model.GetType();
                var sql = "select * from " + modeltype.Name + " where 1=1 " + _sqlwhere;
                var conn = new SQLiteConnection(SQLiteConn);
                var ds = SqLiteHelper.ExecuteDataSet(conn, sql, null);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    model = Activator.CreateInstance<T>();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        var propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                        if (propertyInfo != null && dr[i] != DBNull.Value)
                            propertyInfo.SetValue(model, dr[i], null);
                    }
                    collection.Add(model);
                }
                return collection;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /*
         * 
         * 
                //Type t = typeof(T);
                //var instance = Activator.CreateInstance<T>();
                ////循环赋值 
                //int i = 0;
                //foreach (var item in t.GetProperties())
                //{
                //    item.SetValue(instance, i, null);
                //    i += 1;
                //}
                ////单独赋值 
                //t.GetProperty("five").SetValue(instance, 11111111, null);
                ////循环获取 
                //StringBuilder sb = new StringBuilder();
                //foreach (var item in t.GetProperties())
                //{
                //    sb.Append("类型：" + item.PropertyType.FullName + " 属性名：" + item.Name + " 值：" + item.GetValue(instance, null) + "<br />");
                //}
                ////单独取值 
                //int five = Convert.ToInt32(t.GetProperty("five").GetValue(instance, null));
                //sb.Append("单独取five的值：" + five);
                //string result = sb.ToString();
         * 
         */

        #endregion
    }
}
