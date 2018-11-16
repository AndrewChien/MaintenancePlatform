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
    public class UploadSettingDAL
    {
        public UploadSettingDAL()
        {

        }

        public ObservableCollection<UploadSetting> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<UploadSetting>(_sqlwhere);
        }

        public int Insert(UploadSetting model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(UploadSetting model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(UploadSetting model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
