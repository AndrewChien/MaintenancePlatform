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
using MaintenancePlatform.ViewModels.Equipment;
using MaintenancePlatform.ViewModels.Users;
using ZNC.DataEntiry;

namespace MaintenancePlatform.Views.Equipment
{
    /// <summary>
    /// EquipmentCardEditView.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentCardEditView : WindowBase
    {
        public EquipmentCard MD;
        public EquipmentCardEditView(EquipmentCard md)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EquipmentCardEditVM.PageLoad);
            MD = md;
        }
    }
}
