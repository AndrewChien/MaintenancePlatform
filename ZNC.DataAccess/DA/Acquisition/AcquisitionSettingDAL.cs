using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataEntiry;

namespace ZNC.DataAccess.DA.Acquisition
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class AcquisitionSettingDAL
    {
        public AcquisitionSettingDAL()
        {

        }

        public ObservableCollection<AcquisitionSetting> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<AcquisitionSetting>(_sqlwhere);
        }

        public int Insert(AcquisitionSetting model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(AcquisitionSetting model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(AcquisitionSetting model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
