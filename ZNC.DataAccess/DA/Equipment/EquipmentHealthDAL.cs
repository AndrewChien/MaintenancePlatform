using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataEntiry;

namespace ZNC.DataAccess.DA.Equipment
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class EquipmentHealthDAL
    {
        public EquipmentHealthDAL()
        {

        }

        public ObservableCollection<EquipmentHealth> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<EquipmentHealth>(_sqlwhere);
        }

        public int Insert(EquipmentHealth model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(EquipmentHealth model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(EquipmentHealth model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
