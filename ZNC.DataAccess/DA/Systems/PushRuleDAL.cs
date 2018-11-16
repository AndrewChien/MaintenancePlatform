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
    public class PushRuleDAL
    {
        public PushRuleDAL()
        {

        }

        public ObservableCollection<PushRule> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<PushRule>(_sqlwhere);
        }

        public int Insert(PushRule model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(PushRule model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(PushRule model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
