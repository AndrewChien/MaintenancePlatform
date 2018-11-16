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

namespace MaintenancePlatform
{
    /// <summary>
    /// MainPageView.xaml 的交互逻辑
    /// </summary>
    public partial class MainPageView : Page
    {
        public MainPageView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(VM.PageLoaded);
            // this.KeyDown += new KeyEventHandler(VM.MainPageView_KeyDown);
            this.MouseDown += new MouseButtonEventHandler(VM.MainPageView_MouseDown);
            this.KeyDown += new KeyEventHandler(VM.MainPageView_KeyDown);
            this.MouseRightButtonDown += new MouseButtonEventHandler(VM.MainPageView_MouseRightButtonDown);
            this.Unloaded += new RoutedEventHandler(VM.MainPageView_Unloaded);
        }
    }
}
