using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MaintenancePlatform.Views.Acquisition;
using Telerik.Windows.Data;
using ZNC.DataAnalysis.BIZ.Acquisition;
using ZNC.DataEntiry;
using ZNC.Utility.Command;
using ZNC.Component.Helper;

namespace MaintenancePlatform.ViewModels.Acquisition
{
    public class AcquisitionBaseDataVM : ChildPageViewModel
    {
        AcquisitionBaseDataView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (AcquisitionBaseDataView)sender;
        }

        #region Command
        private ICommand _BtnSearch;
        public ICommand BtnSearch
        {
            get
            {
                if (_BtnSearch == null)
                {
                    _BtnSearch = new DelegateCommand(BtnSearch_Click);
                }
                return _BtnSearch;
            }
        }
        /// <summary>
        ///查询
        /// </summary>
        private void BtnSearch_Click()
        {
            ObservableCollection<AcquisitionBaseData> source = new AcquisitionBaseDataBIZ().SelectAll();
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
                    _BtnInsert = new DelegateCommand(BtnInsert_Click);
                }
                return _BtnInsert;
            }
        }
        /// <summary>
        /// 添加科室
        /// </summary>
        private void BtnInsert_Click()
        {

        }

        private ICommand _BtnDelete;
        public ICommand BtnDelete
        {
            get
            {
                if (_BtnDelete == null)
                {
                    _BtnDelete = new DelegateCommand(BtnDelete_Click);
                }
                return _BtnDelete;
            }
        }

        private void BtnDelete_Click()
        {
            if (View.DGSelect.SelectedItem == null)
            {
                MessageBox.Show("请选择要删除记录！");
                return;
            }
            foreach (var item in View.DGSelect.SelectedItems)
            {
                AcquisitionBaseData row = item as AcquisitionBaseData;
                var success = new AcquisitionBaseDataBIZ().delete(row) > 0;

                if (success == false)
                {
                    MessageBox.Show("删除失败！");
                    break;
                }
            }
            MessageBox.Show("删除成功！");
            BtnSearch_Click();
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
        private void BtnUpdate_Click(object sender)
        {

        }
        #endregion
        #region
        private ObservableCollection<AcquisitionBaseData> _AcquisitionBaseDataCollection;
        public ObservableCollection<AcquisitionBaseData> AcquisitionBaseDataCollection
        {
            get
            {
                if (_AcquisitionBaseDataCollection == null)
                {
                    _AcquisitionBaseDataCollection = new ObservableCollection<AcquisitionBaseData>();
                }
                return _AcquisitionBaseDataCollection;
            }
            set
            {
                base.SetValue(ref _AcquisitionBaseDataCollection, value, () => this.AcquisitionBaseDataCollection);

            }
        }
        #endregion
    }
}
