using System;
using System.Collections.Generic;
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
using MaintenancePlatform.Base;
using MaintenancePlatform.ViewModels.Users;
using ZNC.DataEntiry;

namespace MaintenancePlatform.Views.Users
{
    /// <summary>
    /// DepartmentEditView.xaml 的交互逻辑
    /// </summary>
    public partial class DepartmentEditView : WindowBase
    {
        public Department MD;
        public DepartmentEditView(Department md)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DepartmentEditVM.PageLoad);
            MD = md;
        }
    }
}
