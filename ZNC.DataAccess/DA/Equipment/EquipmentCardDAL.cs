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
    public class EquipmentCardDAL
    {
        public EquipmentCardDAL()
        {

        }

        public ObservableCollection<EquipmentCard> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<EquipmentCard>(_sqlwhere);
        }

        public int Insert(EquipmentCard model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(EquipmentCard model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(EquipmentCard model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
