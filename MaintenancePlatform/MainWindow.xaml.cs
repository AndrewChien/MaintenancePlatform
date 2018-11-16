using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using ZNC.Component;
//using ZNC.Component.DynamicImageButton;
using ZNC.DataEntiry;
using ZNC.Component.ImageButton2;
using Application = System.Windows.Application;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;
using MouseEventHandler = System.Windows.Input.MouseEventHandler;
using RadioButton = System.Windows.Controls.RadioButton;

namespace MaintenancePlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ObservableCollection<Floor> _locate_yhgnLists = new ObservableCollection<Floor>();
        public string _sFilterCondition = "ALL";//选中后值
        public string _sYFilterCondition = "ALL";//选中前值
        public int _iObjectTrans = 0;
        public bool isFull = false;
        //public Jkdx_FilterCondition _jkdxFilter;

        //public MLink_DLL_CS.MLink_DLL mlink = null;
        //public MLink_SDK2010 mlink2 = null;
        public string murl = "";
        public string muser = "";
        public string mpwd = "";

        public string _sCzName = "";//输入后值
        public string _sYCzName = "";//名称输入前值
        public string _sYCzRfid = "";//rfid输入前值

        public MainWindow()
        {
            //DispatcherTimer Timer = new DispatcherTimer();
            //Timer.Interval = TimeSpan.FromSeconds(5);
            //Timer.Tick += new EventHandler(Timer_Tick);
            //Timer.Start();
            App.Current.MainWindow = this;
            InitializeComponent();
            //qb.IsChecked = true;
            this.Loaded += new RoutedEventHandler(VM.PageLoaded);
            //CmbKSLC.SelectionChanged += new SelectionChangedEventHandler(VM.CmbKSLC_SelectionChanged);
            this.KeyDown += new KeyEventHandler(VM.Window_KeyDown);
        }

        private void CreateRoomXml(string sKsbh, string _sKslc)
        {
            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
            //ObservableCollection<LOCATE_APINFO> collections = ServiceHelper.ServiceClient.GetAPINFOByKSLC(Pmms, sKsbh, _sKslc);
            XmlTextWriter writer = new XmlTextWriter("APInfo.xml", System.Text.Encoding.UTF8);
            //使用自动缩进便于阅读
            writer.Formatting = Formatting.Indented;

            //XML声明
            writer.WriteStartDocument();

            //书写根元素
            writer.WriteStartElement("Root");

            //foreach (LOCATE_APINFO aP in collections)
            //{
            //    //开始一个元素
            //    writer.WriteStartElement("AP");

            //    //向先前创建的元素中添加一个属性
            //    writer.WriteAttributeString("id", aP.APBH);

            //    //添加子元素writer.WriteElementString("title", "表单");
            //    for (int i = 1; i <= intROOM; i++)
            //    {
            //        for (int j = 1; j <= intROOM; j++)
            //        {
            //            writer.WriteStartElement("Room");
            //            writer.WriteAttributeString("widthnum", j.ToString());
            //            writer.WriteAttributeString("heightnum", i.ToString());
            //            writer.WriteAttributeString("value", "0");
            //            writer.WriteEndElement();
            //        }

            //    }

            //    //关闭item元素
            //    writer.WriteEndElement(); // 关闭元素
            //}

            //在节点间添加一些空
            writer.Close();



        }
        /// <summary>
        /// 动态加载窗口菜单，从LOCATE_GNMK表中获取
        /// </summary>
        private void MenuLoadRef()
        {
            // 获取用户功能列表
            //_locate_yhgnLists = ServiceHelper.ServiceClient.SelectYHGNOfYHBH(App._GYhxx.YHBH);

            //if (_locate_yhgnLists == null)
            //{
            //    MessageBox.Show("系统功能未维护，请与管理员联系！", "提示");
            //    return;
            //}

            //foreach (LOCATE_YHGN data in _locate_yhgnLists)
            //{
            //    ImageButton.DynamicImageButton ImgBtn = new ImageButton.DynamicImageButton
            //    {
            //        Name = "btn_" + data.GNBH.Trim(),
            //        Content = data.GNMC,
            //        FontSize = 16,
            //        Margin = new Thickness(0, 2, 0, 2),
            //        HorizontalContentAlignment = HorizontalAlignment.Center,
            //        VerticalAlignment = VerticalAlignment.Center,
            //        Visibility = Visibility.Visible,
            //        Foreground = new SolidColorBrush(Colors.White)
            //    };

            //    ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/" + data.PIC));
            //    ImgBtn.Click += new RoutedEventHandler(VM.Menu_Click);

            //    this.MenuPanel.Children.Add(ImgBtn);

            //}

        }

        /// <summary>
        /// 界面分类过滤条件刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Filter_Clicked(object sender, RoutedEventArgs e)
        {
            MenuItem typeMenuItem = new MenuItem();
            typeMenuItem = (MenuItem)sender;
            switch (typeMenuItem.Name)
            {
                case "FilterAll":       // 过滤所有的监控对象
                    _sFilterCondition = "ALL";
                    break;
                case "FilterAsset":     // 根据资产设备过滤
                    _sFilterCondition = "ASSET";
                    break;
                case "FilterPersonal":  // 根据人员信息过滤
                    _sFilterCondition = "PERSONAL";
                    break;
                case "FilterCondition": // 根据自定义条件过滤
                    //_sFilterCondition = "CONDITION";
                    //_jkdxFilter = new Jkdx_FilterCondition();
                    //_jkdxFilter.sKsmc = App._GYhxx.KSMC;
                    //MainWindow_JKDXFilterCondition WinJkdxFilterCondition = new MainWindow_JKDXFilterCondition();
                    //WinJkdxFilterCondition.Show();
                    break;
                case "ObjectTrans":
                    if (_iObjectTrans == 0)
                    {
                        _iObjectTrans = 1;
                        break;
                    }

                    if (_iObjectTrans == 1)
                    {
                        _iObjectTrans = 2;
                        break;
                    }

                    if (_iObjectTrans == 2)
                    {
                        _iObjectTrans = 1;
                        break;
                    }

                    break;
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            foreach (Window win in App.Current.Windows)
            {
                win.Close();
            }
            //ServiceHelper.ServiceClient.UpdateCount(-1);
            // 如果不是用户切换操作，则退出应用程序
            // 如果_bYhch_Mode = true ，表示正在点击操作用户切换功能，则不关闭应用程序
            if (!VM._bYhch_Mode)
                Application.Current.Dispatcher.Thread.Abort();//关闭当前进程exe程序

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        private void mainFrame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this.mainFrame.Width = SystemParameters.PrimaryScreenWidth;
            //this.mainFrame.Height = SystemParameters.PrimaryScreenHeight;

            if (sender.ToString().Contains("MainPageView.xaml"))
            {
                App.window = new NavigationWindow();
                App.window.Source = new Uri("../MainPageView.xaml", UriKind.Relative);//page.xaml为启动的page页面
                App.window.WindowState = WindowState.Maximized;
                App.window.WindowStyle = WindowStyle.None;
                App.window.ResizeMode = ResizeMode.NoResize;
                App.window.Topmost = true;
                App.window.Left = 0.0;
                App.window.Top = 0.0;
                App.window.Width = SystemParameters.PrimaryScreenWidth;
                App.window.Height = SystemParameters.PrimaryScreenHeight;
                App.window.ShowInTaskbar = false;
                App.window.ShowsNavigationUI = false;
                App.window.KeyDown += new KeyEventHandler(window_KeyDown);
                App.window.Show();
            }


            //  this.Close();

        }

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        //按ESC退出全屏
        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (App.window != null)
                {
                    App.window.Close();
                    App.window = null;
                }
            }
        }
        //按ESC退出全屏
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void spsblx_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "qb":       // 过滤所有的监控对象
                    _sFilterCondition = "ALL";
                    break;
                case "ry":
                    _sFilterCondition = "PERSONAL";
                    break;
                case "hxj":
                    _sFilterCondition = "HXJ";
                    break;
                case "wlb":
                    _sFilterCondition = "WLB";
                    break;
                case "jhy":
                    _sFilterCondition = "JHY";
                    break;
                case "cfsb":
                    _sFilterCondition = "CFSB";
                    break;
                case "other":
                    _sFilterCondition = "OTHER";
                    break;
            }
            if (App._GMainWindow != null)
            {
                string sTpmc = string.Empty;
                string sKsbh = string.Empty;
                //string _sKslc = App._GMainWindow.CmbKSLC.Text;
                //foreach (LOCATE_KSLC kslc in App._jkkslcDataInfo)
                //{
                //    if (kslc.KSLC == _sKslc)
                //    {
                //        sTpmc = kslc.TPMC;
                //        sKsbh = kslc.KSBH;
                //        break;
                //    }
                //}
                //foreach (Floor kslc in App._Floor)
                //{
                //    if (kslc.FloorName == _sKslc)
                //    {
                //        sTpmc = kslc.ImgPath;
                //        sKsbh = kslc.KSBH;
                //        break;
                //    }
                //}
                ////创建房间分割记录(刷新)
                //if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml"))
                //{
                //    File.Delete(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml");
                //}
                //CreateRoomXml(sKsbh, _sKslc);
            }

        }

    }
}
