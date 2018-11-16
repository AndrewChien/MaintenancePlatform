using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataEntiry;

namespace ZNC.DataAccess.DA.Systems
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class SystemServiceDAL
    {
        public SystemServiceDAL()
        {

        }

        public ObservableCollection<SystemService> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<SystemService>(_sqlwhere);
        }

        public int Insert(SystemService model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(SystemService model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(SystemService model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
