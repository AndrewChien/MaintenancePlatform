using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ZNC.DataAccess.DA.Systems;
using ZNC.DataEntiry;

namespace ZNC.DataAnalysis.BIZ.Systems
{
    /// <summary>
    /// AndrewChien 2017/10/22 9:37:11
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class AlarmHistoryBIZ
    {
        private readonly AlarmHistoryDAL dal = new AlarmHistoryDAL();
        public ObservableCollection<AlarmHistory> SelectAll()
        {
            return dal.Select("");
        }

        public ObservableCollection<AlarmHistory> Select(string _sqlwhere)
        {
            return dal.Select(_sqlwhere);
        }

        /// <summary>
        /// select * from table where 1=1 and 1=1 order by ID desc,ID limit 0,1; 
        /// </summary>
        /// <param name="_sqland"></param>
        /// <param name="_orderbyidentity"></param>
        /// <returns></returns>
        public ObservableCollection<AlarmHistory> SelectTop(string _sqland, string _orderbyidentity)
        {
            return dal.Select(_sqland + " order by " + _orderbyidentity + " desc," + _orderbyidentity + " limit 0,1");
        }

        public bool SelectExist(string _sqlwhere)
        {
            return dal.Select(_sqlwhere).Count > 0;
        }

        public int Insert(AlarmHistory _model)
        {
            return dal.Insert(_model);
        }

        public int Update(AlarmHistory _model)
        {
            return dal.Update(_model, "ID");
        }

        public int UpdateBy(AlarmHistory _model, string _identity)
        {
            return dal.Update(_model, _identity);
        }

        public int InsertOrUpdateBy(AlarmHistory _model, string _identity)
        {
            return _model.ID == 0 ? dal.Insert(_model) : dal.Update(_model, _identity);
        }

        public int delete(AlarmHistory _model)
        {
            return dal.Delete(_model);
        }
    }
}
