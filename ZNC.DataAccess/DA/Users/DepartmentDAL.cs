using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataEntiry;

namespace ZNC.DataAccess.DA.Users
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class DepartmentDAL
    {
        public DepartmentDAL()
        {

        }

        public ObservableCollection<Department> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<Department>(_sqlwhere);
        }

        public int Insert(Department model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(Department model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(Department model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
