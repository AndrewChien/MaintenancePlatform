using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using ZNC.DataEntiry;

namespace ZNC.DataAccess.DA.Acquisition
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class AcquisitionBaseDataDAL
    {
        public AcquisitionBaseDataDAL()
        {

        }

        public ObservableCollection<AcquisitionBaseData> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<AcquisitionBaseData>(_sqlwhere);

            //var collection = new ObservableCollection<AcquisitionBaseData>();
            //var sql = "SELECT * FROM [AcquisitionBaseData] ";
            //var conn = new SQLiteConnection(connstr);
            //var ds = SqLiteHelper.ExecuteDataSet(conn, sql, null);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        var resource = new AcquisitionBaseData();
            //        resource.Id = int.Parse(dr["Id"].ToString());
            //        resource.Code = int.Parse(dr["Code"].ToString());
            //        resource.Confidence = int.Parse(dr["Confidence"].ToString());
            //        resource.Name = dr["Name"].ToString();
            //        resource.Remark = dr["Remark"].ToString();
            //        collection.Add(resource);
            //    }
            //}
            //return collection;
        }

        public int Insert(AcquisitionBaseData model)
        {
            return CommonAccess.ComSQLiteInsert(model);

            //var sql = "INSERT INTO [AcquisitionBaseData] ([Code],[Confidence],[Remark],[Name],[Value]) VALUES (@Code,@Confidence,@Remark,@Name,@Value)";
            //var conn = new SQLiteConnection(connstr);
            //object[] para = {
            //                        new SQLiteParameter("@Code", model.Code),
            //                        new SQLiteParameter("@Confidence", model.Confidence),
            //                        new SQLiteParameter("@Remark", model.Remark),
            //                        new SQLiteParameter("@Name", model.Name),
            //                        new SQLiteParameter("@Value", model.Value)
            //                      };
            //return SqLiteHelper.ExecuteNonQuery(conn, sql, para);
        }

        public int Update(AcquisitionBaseData model,string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);

            //var sql = "Update [AcquisitionBaseData] set [Code] = @Code,[Confidence]=@Confidence,[Remark] = @Remark,[Name]=@Name,[Value]=@Value WHERE [Id] = @Id";
            //var conn = new SQLiteConnection(connstr);
            //object[] para = {
            //                        new SQLiteParameter("@Code",model.Code),
            //                        new SQLiteParameter("@Confidence",model.Confidence),
            //                        new SQLiteParameter("@Remark",model.Remark),
            //                        new SQLiteParameter("@Name",model.Name),
            //                        new SQLiteParameter("@Value",model.Value),
            //                        new SQLiteParameter("@Id",model.Id)
            //                      };
            //return SqLiteHelper.ExecuteNonQuery(conn, sql, para);
        }

        public int Delete(AcquisitionBaseData model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");

            //var sql = "DELETE FROM [AcquisitionBaseData] WHERE [Id]=@Id";
            //var conn = new SQLiteConnection(connstr);
            //object[] para = { new SQLiteParameter("@Id", model.Id) };
            //return SqLiteHelper.ExecuteNonQuery(conn, sql, para);
        }

        public ObservableCollection<AcquisitionBaseData> SelectBy(string _colname, string _value, bool _isdesc)
        {
            string connstr = "Data Source=ZNCPlatform.db;Version=3;";
            var col = "ID";//默认为ID
            var sort = _isdesc ? "desc" : "asc";
            if (_colname != "")
            {
                col = _colname;
            }
            var collection = new ObservableCollection<AcquisitionBaseData>();
            var sql = "SELECT * FROM [AcquisitionBaseData] WHERE " + col + " = " + _value + " order by ID " + _isdesc;
            var conn = new SQLiteConnection(connstr);
            var ds = SqLiteHelper.ExecuteDataSet(conn, sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var resource = new AcquisitionBaseData();
                    resource.ID = int.Parse(dr["FloorID"].ToString());
                    resource.Code = int.Parse(dr["FloorName"].ToString());
                    resource.Confidence = int.Parse(dr["BuildingID"].ToString());
                    resource.Name = dr["Invalid"].ToString();
                    resource.Remark = dr["Remark"].ToString();
                    collection.Add(resource);
                }
            }
            return collection;
        }
    }
}
