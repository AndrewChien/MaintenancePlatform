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
    public class JurisdictionDAL
    {
        public JurisdictionDAL()
        {

        }

        public ObservableCollection<Jurisdiction> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<Jurisdiction>(_sqlwhere);
        }

        public int Insert(Jurisdiction model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(Jurisdiction model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(Jurisdiction model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
