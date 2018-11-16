using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaintenancePlatform.Base;
using MaintenancePlatform.ViewModels.Users;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Users;
using ZNC.DataEntiry;

namespace MaintenancePlatform.Views.Users
{
    /// <summary>
    /// UserEditView.xaml 的交互逻辑
    /// </summary>
    public partial class UserEditView : WindowBase
    {
        public User MD;
        public UserEditView(User md)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(UserEditVM.PageLoad);
            MD = md;
        }

        /// <summary>
        /// 加载部门联动岗位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDepartment.SelectedValue != null)
            {
                var collection = new DepartmentBIZ().Select(" and UplevelCode = "+ cmbDepartment.SelectedValue);
                if (collection.Count > 0)
                {
                    cmbJob.ItemsSource = collection;
                    cmbJob.DisplayMemberPath = "Name";//DisplayMemberPath显示项
                    cmbJob.SelectedValuePath = "Code";//SelectedValuePath绑值项
                                                           //view.cmbType.SelectedItem = view.cmbType.Items[0];
                }
                else
                {
                    UIHelper.ShowMessageBox("数据获取错误!", false);
                }
            }
        }
    }
}
