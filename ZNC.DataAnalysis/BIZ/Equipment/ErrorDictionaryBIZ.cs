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
    public class ErrorDictionaryBIZ
    {
        private readonly ErrorDictionaryDAL dal = new ErrorDictionaryDAL();
        public ObservableCollection<ErrorDictionary> SelectAll()
        {
            return dal.Select("");
        }

        public ObservableCollection<ErrorDictionary> Select(string _sqlwhere)
        {
            return dal.Select(_sqlwhere);
        }

        /// <summary>
        /// select * from table where 1=1 and 1=1 order by ID desc,ID limit 0,1; 
        /// </summary>
        /// <param name="_sqland"></param>
        /// <param name="_orderbyidentity"></param>
        /// <returns></returns>
        public ObservableCollection<ErrorDictionary> SelectTop(string _sqland, string _orderbyidentity)
        {
            return dal.Select(_sqland + " order by " + _orderbyidentity + " desc," + _orderbyidentity + " limit 0,1");
        }

        public bool SelectExist(string _sqlwhere)
        {
            return dal.Select(_sqlwhere).Count > 0;
        }

        public int Insert(ErrorDictionary _model)
        {
            return dal.Insert(_model);
        }

        public int Update(ErrorDictionary _model)
        {
            return dal.Update(_model, "ID");
        }

        public int UpdateBy(ErrorDictionary _model, string _identity)
        {
            return dal.Update(_model, _identity);
        }

        public int InsertOrUpdateBy(ErrorDictionary _model, string _identity)
        {
            return _model.ID == 0 ? dal.Insert(_model) : dal.Update(_model, _identity);
        }

        public int delete(ErrorDictionary _model)
        {
            return dal.Delete(_model);
        }
    }
}
