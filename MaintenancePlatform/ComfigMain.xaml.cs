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
using System.Windows.Shapes;
using ZNC.DataAnalysis.BIZ.Systems;
using ZNC.DataEntiry;

namespace MaintenancePlatform
{
    /// <summary>
    /// ComfigMain.xaml 的交互逻辑
    /// </summary>
    public partial class ComfigMain : Window
    {
        public ComfigMain()
        {
            InitializeComponent();
        }

        private void MenuLoadRef()
        {
            // 获取用户功能列表
            var sysmenu = new SystemModuleBIZ().SelectAll();

            foreach (SystemModule data in sysmenu)
            {
                ZNC.Component.ImageButton2.DynamicImageButton ImgBtn = new ZNC.Component.ImageButton2.DynamicImageButton
                {
                    Name = "btn_" + data.Code,
                    Content = data.Name,
                    FontSize = 16,
                    Margin = new Thickness(0, 2, 0, 2),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Visibility = Visibility.Visible,
                    Foreground = new SolidColorBrush(Colors.White)
                };
                ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/" + data.PicUrl));
                //ImgBtn.Click += new RoutedEventHandler(VM.Menu_Click);
                //this.MenuPanel.Children.Add(ImgBtn);
            }

        }
    }
}
