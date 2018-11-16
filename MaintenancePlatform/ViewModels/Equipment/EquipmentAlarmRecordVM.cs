using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MaintenancePlatform.Views.Equipment;
using Telerik.Windows.Data;
using ZNC.DataAnalysis.BIZ.Equipment;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Equipment
{

    /// <summary>
    /// AndrewChien 2017/10/22 10:27:26
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class EquipmentAlarmRecordVM : ChildPageViewModel
    {
        EquipmentAlarmRecordView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (EquipmentAlarmRecordView)sender;
        }

        #region Command
        private ICommand _BtnSearch;
        public ICommand BtnSearch
        {
            get
            {
                if (_BtnSearch == null)
                {
                    _BtnSearch = new DelegateCommand<object>(BtnSearch_Click);
                }
                return _BtnSearch;
            }
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        private void BtnSearch_Click(object sender)
        {
            ObservableCollection<EquipmentAlarmRecord> source = new EquipmentAlarmRecordBIZ().SelectAll();
            var pagedSource = new QueryableCollectionView(source);
            View.DGSelect.ItemsSource = pagedSource;
            View.searchDataPager.Source = pagedSource;
            View.DGSelect.SelectedItems.Remove(View.DGSelect.SelectedItem);//取消首行选中
        }
        private ICommand _BtnInsert;
        public ICommand BtnInsert
        {
            get
            {
                if (_BtnInsert == null)
                {
                    _BtnInsert = new DelegateCommand<object>(BtnInsert_Click);
                }
                return _BtnInsert;
            }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        private void BtnInsert_Click(object sender)
        {

        }

        private ICommand _BtnDelete;
        public ICommand BtnDelete
        {
            get
            {
                if (_BtnDelete == null)
                {
                    _BtnDelete = new DelegateCommand<object>(BtnDelete_Click);
                }
                return _BtnDelete;
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        private void BtnDelete_Click(object sender)
        {

        }

        private ICommand _BtnUpdate;
        public ICommand BtnUpdate
        {
            get
            {
                if (_BtnUpdate == null)
                {
                    _BtnUpdate = new DelegateCommand<object>(BtnUpdate_Click);
                }
                return _BtnUpdate;
            }
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        private void BtnUpdate_Click(object sender)
        {

        }
        #endregion
        #region
        private ObservableCollection<EquipmentAlarmRecord> _EquipmentAlarmRecordCollection;
        public ObservableCollection<EquipmentAlarmRecord> EquipmentAlarmRecordCollection
        {
            get
            {
                if (_EquipmentAlarmRecordCollection == null)
                {
                    _EquipmentAlarmRecordCollection = new ObservableCollection<EquipmentAlarmRecord>();
                }
                return _EquipmentAlarmRecordCollection;
            }
            set
            {
                base.SetValue(ref _EquipmentAlarmRecordCollection, value, () => this.EquipmentAlarmRecordCollection);

            }
        }
        #endregion
    }
}
