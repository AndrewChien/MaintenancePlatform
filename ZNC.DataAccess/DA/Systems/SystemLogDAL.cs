﻿using System;
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
    public class SystemLogDAL
    {
        public SystemLogDAL()
        {

        }

        public ObservableCollection<SystemLog> Select(string _sqlwhere)
        {
            return CommonAccess.ComSQLiteSelect<SystemLog>(_sqlwhere);
        }

        public int Insert(SystemLog model)
        {
            return CommonAccess.ComSQLiteInsert(model);
        }

        public int Update(SystemLog model, string _identity)
        {
            return CommonAccess.ComSQLiteUpdate(model, _identity);
        }

        public int Delete(SystemLog model)
        {
            return CommonAccess.ComSQLiteDelete(model, "ID");
        }
    }
}
