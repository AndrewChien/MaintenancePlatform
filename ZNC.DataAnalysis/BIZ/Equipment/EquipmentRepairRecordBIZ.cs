using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataAccess.DA.Equipment;
using ZNC.DataEntiry;

namespace ZNC.DataAnalysis.BIZ.Equipment
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class EquipmentRepairRecordBIZ
    {
        private readonly EquipmentRepairRecordDAL dal = new EquipmentRepairRecordDAL();
        public ObservableCollection<EquipmentRepairRecord> SelectAll()
        {
            return dal.Select("");
        }

        public ObservableCollection<EquipmentRepairRecord> Select(string _sqlwhere)
        {
            return dal.Select(_sqlwhere);
        }

        /// <summary>
        /// select * from table where 1=1 and 1=1 order by ID desc,ID limit 0,1; 
        /// </summary>
        /// <param name="_sqland"></param>
        /// <param name="_orderbyidentity"></param>
        /// <returns></returns>
        public ObservableCollection<EquipmentRepairRecord> SelectTop(string _sqland, string _orderbyidentity)
        {
            return dal.Select(_sqland + " order by " + _orderbyidentity + " desc," + _orderbyidentity + " limit 0,1");
        }

        public bool SelectExist(string _sqlwhere)
        {
            return dal.Select(_sqlwhere).Count > 0;
        }

        public int Insert(EquipmentRepairRecord _model)
        {
            return dal.Insert(_model);
        }

        public int Update(EquipmentRepairRecord _model)
        {
            return dal.Update(_model, "ID");
        }

        public int UpdateBy(EquipmentRepairRecord _model, string _identity)
        {
            return dal.Update(_model, _identity);
        }

        public int InsertOrUpdateBy(EquipmentRepairRecord _model, string _identity)
        {
            return _model.ID == 0 ? dal.Insert(_model) : dal.Update(_model, _identity);
        }

        public int delete(EquipmentRepairRecord _model)
        {
            return dal.Delete(_model);
        }
    }
}
