using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZNC.DataAccess.DA.Equipment
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class EquipmentDAL
    {
        public EquipmentDAL()
        {

        }

        public ObservableCollection<DataEntiry.Equipment> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<DataEntiry.Equipment>(_sqlwhere);
        }

        public int Insert(DataEntiry.Equipment model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(DataEntiry.Equipment model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(DataEntiry.Equipment model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
