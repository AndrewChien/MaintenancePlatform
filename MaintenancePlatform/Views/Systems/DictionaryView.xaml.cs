using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Systems;
using ZNC.DataEntiry;

namespace MaintenancePlatform.Views.Systems
{
    /// <summary>
    /// DictionaryView.xaml 的交互逻辑
    /// </summary>
    public partial class DictionaryView : Page
    {
        public DictionaryView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DictionaryVM.PageLoad);
        }

        RadTreeViewItem selectedItem = null;
        private void radTreeView_Selected(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadTreeView source = sender as RadTreeView;
            selectedItem = source.SelectedItem as RadTreeViewItem;
            int ID = int.Parse(selectedItem.Tag.ToString());
            if (ID == 0)
            {
                MessageBox.Show("请选择设备类别!");

            }
            else
            {
                ObservableCollection<Dictionary> dcs = new DictionaryBIZ().Select(" and Code=" + selectedItem.Tag);
                Dictionary dc = dcs[0];
                txtID.Text = dc.ID.ToString();
                txtCode.Text = dc.Code.ToString();
                txtName.Text = dc.Name;
                cmbEnable.SelectedValue = dc.EnableStatus;
                txtUpCode.Text = dc.UplevelCode.ToString();
                cmbUpName.SelectedValue = dc.UplevelCode;
                txtType.Text = dc.Type;
                txtRemark.Text = dc.Remark;
            }
        }

        private void cmbUpName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbUpName.SelectedValue!=null)
            {
                txtUpCode.Text = cmbUpName.SelectedValue.ToString();
            }
        }
    }
}
