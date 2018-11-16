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
using MaintenancePlatform.ViewModels.Acquisition;

namespace MaintenancePlatform.Views.Acquisition
{
    /// <summary>
    /// AcquisitionSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class AcquisitionSettingView : Page
    {
        public AcquisitionSettingView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AcquisitionSettingVM.PageLoad);
        }
    }
}
