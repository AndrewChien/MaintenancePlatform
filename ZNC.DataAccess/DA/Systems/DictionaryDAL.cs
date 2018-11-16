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
    public class DictionaryDAL
    {
        public DictionaryDAL()
        {

        }

        public ObservableCollection<Dictionary> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<Dictionary>(_sqlwhere);
        }

        public int Insert(Dictionary model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(Dictionary model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(Dictionary model, string _identit)
        {
            return CommonAccess.ComSQLiteDelete(model, _identit);
        }
    }
}
