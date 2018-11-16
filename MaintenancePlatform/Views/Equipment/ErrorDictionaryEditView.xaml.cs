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
using ZNC.DataEntiry;

namespace MaintenancePlatform.Views.Equipment
{
    /// <summary>
    /// ErrorDictionaryEditView.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorDictionaryEditView : WindowBase
    {
        public ErrorDictionary MD;
        public ErrorDictionaryEditView(ErrorDictionary md)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ErrorDictionaryEditVM.PageLoad);
            MD = md;
        }
    }
}
