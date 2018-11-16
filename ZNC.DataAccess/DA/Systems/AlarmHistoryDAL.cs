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
    public class AlarmHistoryDAL
    {
        public AlarmHistoryDAL()
        {

        }

        public ObservableCollection<AlarmHistory> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<AlarmHistory>(_sqlwhere);
        }

        public int Insert(AlarmHistory model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(AlarmHistory model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(AlarmHistory model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
