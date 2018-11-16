using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZNC.DataAccess.DA.Users
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class UserDAL
    {
        public UserDAL()
        {

        }

        public ObservableCollection<DataEntiry.User> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<DataEntiry.User>(_sqlwhere);
        }

        public int Insert(DataEntiry.User model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(DataEntiry.User model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(DataEntiry.User model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
