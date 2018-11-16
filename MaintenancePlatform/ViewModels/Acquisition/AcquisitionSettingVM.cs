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

namespace MaintenancePlatform.ViewModels.Acquisition
{

    /// <summary>
    /// AndrewChien 2017/10/22 10:26:41
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class AcquisitionSettingVM : ChildPageViewModel
    {
        AcquisitionSettingView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (AcquisitionSettingView)sender;
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
            ObservableCollection<AcquisitionSetting> source = new AcquisitionSettingBIZ().SelectAll();
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
            // NavigationService.Navigate(new Uri("/Views/Maintenance/RFIDAddView.xaml", UriKind.RelativeOrAbsolute));
            AcquisitionSettingEditView view = new AcquisitionSettingEditView(null);
            view.ShowDialog();
            BtnSearch_Click(null);
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
            if (View.DGSelect.SelectedItem == null)
            {
                MessageBox.Show("请选择要删除的功能模块！");
            }
            foreach (var item in View.DGSelect.Items)
            {

                if (View.DGSelect.SelectedItem == item)
                {
                    AcquisitionSetting model = item as AcquisitionSetting;
                    new AcquisitionSettingBIZ().delete(model);
                    MessageBox.Show("删除成功！");
                    BtnSearch_Click(null);
                }
            }
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
            if (View.DGSelect.SelectedItem == null)
            {
                MessageBox.Show("请选择要修改的记录！");
            }
            foreach (var item in View.DGSelect.Items)
            {

                if (View.DGSelect.SelectedItem == item)
                {
                    AcquisitionSetting model = item as AcquisitionSetting;
                    //FuncModule gnmk = (FuncModule)sender;
                    AcquisitionSettingEditView view = new AcquisitionSettingEditView(model);
                    view.ShowDialog();
                    BtnSearch_Click(null);

                }
            }
        }
        #endregion
        #region
        private ObservableCollection<AcquisitionSetting> _AcquisitionSettingCollection;
        public ObservableCollection<AcquisitionSetting> AcquisitionSettingCollection
        {
            get
            {
                if (_AcquisitionSettingCollection == null)
                {
                    _AcquisitionSettingCollection = new ObservableCollection<AcquisitionSetting>();
                }
                return _AcquisitionSettingCollection;
            }
            set
            {
                base.SetValue(ref _AcquisitionSettingCollection, value, () => this.AcquisitionSettingCollection);

            }
        }
        #endregion
    }
}
