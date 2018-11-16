using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ZNC.Component.DynamicImageButton;
using ZNC.Component.Helper;
using ZNC.DataAnalysis.BIZ.Equipment;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels
{
    public partial class MainWindowViewModel : ChildPageViewModel
    {
        private Frame _curMainFrame;
        private ChildPageViewModel _lastViewModel;
        private object _lockObject = new object();
        public bool _bYhch_Mode = false;

        private bool _isInNavigation = false;

 
        public MainWindowViewModel()
        {
            if ((Application.Current == null) || (Application.Current.GetType() == typeof(Application)))
            { }
            else
            {
            }
         
        }
        internal void MainPageView_Unloaded(object sender,RoutedEventArgs e)
        {
            MessageBox.Show ("hello");
        }

        #region 列表信息model
        ObservableCollection<ZNC.DataEntiry.Equipment> deviceinfo = new ObservableCollection<ZNC.DataEntiry.Equipment>();
        internal ObservableCollection<ZNC.DataEntiry.Equipment> Deviceinfo
        {
            get { return deviceinfo; }
            set { deviceinfo = value; }
        }

        ObservableCollection<ZNC.DataEntiry.Equipment> alarminfo = new ObservableCollection<ZNC.DataEntiry.Equipment>();
        internal ObservableCollection<ZNC.DataEntiry.Equipment> Alarminfo
        {
            get { return alarminfo; }
            set { alarminfo = value; }
        }

        ObservableCollection<ZNC.DataEntiry.Equipment> stablerate = new ObservableCollection<ZNC.DataEntiry.Equipment>();
        internal ObservableCollection<ZNC.DataEntiry.Equipment> Stablerate
        {
            get { return stablerate; }
            set { stablerate = value; }
        }

        ObservableCollection<ZNC.DataEntiry.Equipment> fixstatus = new ObservableCollection<ZNC.DataEntiry.Equipment>();
        internal ObservableCollection<ZNC.DataEntiry.Equipment> Fixstatus
        {
            get { return fixstatus; }
            set { fixstatus = value; }
        }
        #endregion

        internal void PageLoaded(object sender, RoutedEventArgs e)
        {
            //Resolution.Resolution rr = new Resolution.Resolution();
            //rr.setResolution(800, 1024, 60);

            App._GMainWindow = (MainWindow)sender;
            _bYhch_Mode = false; 
            _CurrentViewModel = this;



            #region  临时展示

            var View = (MainWindow)sender;
            deviceinfo = new EquipmentBIZ().SelectAll();
            alarminfo = deviceinfo;
            stablerate = deviceinfo;
            fixstatus = deviceinfo;
            View.lvDeviceinfo.ItemsSource = Deviceinfo;
            View.lvAlarminfo.ItemsSource = Alarminfo;
            View.lvStablerate.ItemsSource = Stablerate;
            View.lvFixstatus.ItemsSource = Fixstatus;

            #endregion


            // 初始化界面控件
            //ObservableCollection<LOCATE_JKDX> info = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX();
            //_StaticPersonalInfos = info;

            //MainWindowState = WindowState.Maximized;
            //ObservableCollection<DeviceClass> devices = ServiceHelper.ServiceClient.SelectAllDeviceClass();
            //devices = devices.Where(r => r.Grade == 2).ConvertTo();
            //for (int i = 0; i < devices.Count; i++)
            //{
            //    RadioButton rb = new RadioButton();
            //    //Foreground="#FFF8F2F2" FontSize="14" Checked="spsblx_Checked"  Width="120" Height="25"
            //    rb.Name = "rb_"+devices[i].DeviceClassID.ToString ();
            //    rb.Width = 120;
            //    rb.Height = 25;
            //    rb.FontSize = 14;

            //    //new SolidColorBrush( Color.FromArgb(255, 255, 0, 0));
            //    //rb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            //    rb.Foreground = new SolidColorBrush(Colors.White);
            //   // ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC, "").Where(r => r.TKBZ == 0).ConvertTo().Where(r => r.DXLX == devices[i].PreDeviceClassID.ToString()).ConvertTo();
            //    ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).ConvertTo().Where(r => r.DXLX == devices[i].DeviceClassName).ConvertTo();
            //    rb.Content = devices[i].DeviceClassName+" ( "+jkdxs.Count()+" )";
            //    rb.Checked +=new RoutedEventHandler(rb_Checked);
            //    App._GMainWindow.spsblx.Children.Add(rb);
            //}

            //ObservableCollection<FuncModule> modules = ServiceHelper.ServiceClient.SelectAllFuncModule();
            //监控管理
            //ObservableCollection<FuncModule> usermodules = ServiceHelper.ServiceClient.SelectAllUserFuncs(App._GYhxx.YHBH,"0005");
            //if (usermodules.Count > 0)
            //{
            //    Button btnjk = new Button();
            //    btnjk.Name = "btnjk";
            //    btnjk.Click += new RoutedEventHandler(btnjk_Click);
            //    btnjk.Width = 172;
            //    btnjk.Height = 40;
            //    btnjk.SetResourceReference(Button.StyleProperty, "MenuButtonJKGLStyle");
            //    App._GMainWindow.MenuPanel.Children.Add(btnjk);
            //    StackPanel spjkgl = new StackPanel();

            //    spjkgl.Width = 172;


            //    ObservableCollection<FuncModule> jkgl = modules.Where(r => r.PreFuncID == "0005").ConvertTo();
            //    for (int i = 0; i < usermodules.Count; i++)
            //    {
            //        Button btn = new Button();
            //        btn.Name = "btn_" + usermodules[i].FuncID;
            //        btn.Click += new RoutedEventHandler(btn_Click);
            //        btn.HorizontalAlignment = HorizontalAlignment.Right;
            //        btn.Width = 150;
            //        btn.SetResourceReference(Button.StyleProperty, usermodules[i].Style);
            //        if (i == 0)
            //        {
            //            btn.SetResourceReference(Button.StyleProperty, usermodules[i].Style + "2");
            //            gbtn = btn;
            //            // NavigationService.Navigate(new Uri(jkgl[i].Path, UriKind.RelativeOrAbsolute));
            //        }
            //        spjkgl.Children.Add(btn);


            //    }
            //    App._GMainWindow.MenuPanel.Children.Add(spjkgl);
            //}

            //基础字典
            //ObservableCollection<FuncModule> usermodules2 = ServiceHelper.ServiceClient.SelectAllUserFuncs(App._GYhxx.YHBH, "0004");
            //if (usermodules2.Count > 0)
            //{
            //    Button btnjczd = new Button();
            //    btnjczd.Name = "btnjczd";
            //    btnjczd.Click += new RoutedEventHandler(btnjk_Click);
            //    btnjczd.Width = 172;
            //    btnjczd.Height = 40;
            //    btnjczd.SetResourceReference(Button.StyleProperty, "MenuButtonJCZDStyle");
            //    App._GMainWindow.MenuPanel.Children.Add(btnjczd);
            //    StackPanel spjczd = new StackPanel();
            //    spjczd.Name = "spjczd";
            //    spjczd.Width = 172;
            //    ObservableCollection<FuncModule> jczd = modules.Where(r => r.PreFuncID == "0004").ConvertTo();
            //    for (int i = 0; i < usermodules2.Count; i++)
            //    {
            //        Button btn = new Button();
            //        btn.Click += new RoutedEventHandler(btn_Click);
            //        btn.Name = "btn_" + usermodules2[i].FuncID;
            //        btn.HorizontalAlignment = HorizontalAlignment.Right;
            //        btn.Width = 150;
            //        btn.SetResourceReference(Button.StyleProperty, usermodules2[i].Style);
            //        spjczd.Children.Add(btn);

            //    }
            //    App._GMainWindow.MenuPanel.Children.Add(spjczd);
            //    spjczd.Height = 0;
            //}

            //权限维护
            //ObservableCollection<FuncModule> usermodules3 = ServiceHelper.ServiceClient.SelectAllUserFuncs(App._GYhxx.YHBH, "0003");
            //if (usermodules3.Count > 0)
            //{
            //    Button btnqxwh = new Button();
            //    btnqxwh.Name = "btnqxwh";
            //    btnqxwh.Click += new RoutedEventHandler(btnjk_Click);
            //    btnqxwh.Width = 172;
            //    btnqxwh.Height = 40;
            //    btnqxwh.SetResourceReference(Button.StyleProperty, "MenuButtonQXWHStyle");
            //    App._GMainWindow.MenuPanel.Children.Add(btnqxwh);

            //    StackPanel spqxwh = new StackPanel();
            //    spqxwh.Name = "spqxwh";
            //    spqxwh.Width = 172;
            //    ObservableCollection<FuncModule> qxwh = modules.Where(r => r.PreFuncID == "0003").ConvertTo();
            //    for (int i = 0; i < usermodules3.Count; i++)
            //    {
            //        Button btn = new Button();
            //        btn.Click += new RoutedEventHandler(btn_Click);
            //        btn.Name = "btn_" + usermodules3[i].FuncID;
            //        btn.HorizontalAlignment = HorizontalAlignment.Right;
            //        btn.Width = 150;
            //        btn.SetResourceReference(Button.StyleProperty, usermodules3[i].Style);
            //        spqxwh.Children.Add(btn);

            //    }
            //    App._GMainWindow.MenuPanel.Children.Add(spqxwh);
            //    spqxwh.Height = 0;
            //}

            //系统设置
            //ObservableCollection<FuncModule> usermodules4 = ServiceHelper.ServiceClient.SelectAllUserFuncs(App._GYhxx.YHBH, "0002");
            //if (usermodules4.Count > 0)
            //{
            //    Button btnxtsz = new Button();
            //    btnxtsz.Name = "btnxtsz";
            //    btnxtsz.Click += new RoutedEventHandler(btnjk_Click);
            //    btnxtsz.Width = 172;
            //    btnxtsz.Height = 40;
            //    btnxtsz.SetResourceReference(Button.StyleProperty, "MenuButtonXTSZStyle");
            //    App._GMainWindow.MenuPanel.Children.Add(btnxtsz);

            //    StackPanel spxtsz = new StackPanel();
            //    spxtsz.Name = "spxtsz";
            //    spxtsz.Width = 172;
            //    ObservableCollection<FuncModule> xtsz = modules.Where(r => r.PreFuncID == "0002").ConvertTo();
            //    for (int i = 0; i < usermodules4.Count; i++)
            //    {
            //        Button btn = new Button();
            //        btn.Click += new RoutedEventHandler(btn_Click);
            //        btn.Name = "btn_" + usermodules4[i].FuncID;
            //        btn.HorizontalAlignment = HorizontalAlignment.Right;
            //        btn.Width = 150;
            //        btn.SetResourceReference(Button.StyleProperty, usermodules4[i].Style);
            //        spxtsz.Children.Add(btn);

            //    }
            //    App._GMainWindow.MenuPanel.Children.Add(spxtsz);
            //    spxtsz.Height = 0;
            //}
        }

        private void btnjk_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //ObservableCollection<FuncModule> modules = ServiceHelper.ServiceClient.SelectAllFuncModule();
            if (btn.Name == "btnjk")
            {
                foreach (object obj in App._GMainWindow.MenuPanel.Children)
                {
                    if (obj.GetType().Equals(typeof(StackPanel)))
                    {
                        StackPanel sp = obj as StackPanel;
                        string name = sp.Name;
                        if ("spjkgl".Equals(name))
                        {
                            int count = sp.Children.Count;
                            sp.Height = 28*count;
                        }
                        if ("spjczd".Equals(name) || "spqxwh".Equals(name) || "spxtsz".Equals(name))
                        {
                            sp.Height = 0;
                        }
                        foreach (object obj2 in sp.Children)
                        {
                            if (obj2.GetType().Equals(typeof(Button)))
                            {
                                //setDeatult("0005", obj2, modules);
                            }
                        }
                    }
                   
                }
            }
            if (btn.Name == "btnjczd")
            {
                //StackPanel sp = App._GMainWindow.MenuPanel.FindName("spjkgl") as StackPanel;
                //sp.Height = 0;
                //StackPanel spjczd = App._GMainWindow.MenuPanel.FindName("spjczd") as StackPanel;
                //spjczd.Height = 35;
                foreach (object obj in App._GMainWindow.MenuPanel.Children)
                {
                    if (obj.GetType().Equals(typeof(StackPanel)))
                    {
                        StackPanel sp = obj as StackPanel;
                        string name = sp.Name;
                        if ("spjkgl".Equals(name) || "spqxwh".Equals(name) || "spxtsz".Equals(name))
                        {
                            sp.Height = 0;
                        }
                        if("spjczd".Equals(name))
                        {
                            int count = sp.Children.Count;
                            sp.Height = 28 * count;
                        }
                        foreach (object obj2 in sp.Children)
                        {
                            if (obj2.GetType().Equals(typeof(Button)))
                            {
                                  //setDeatult("0004", obj2, modules);
                            }
                        }
                    }
                 
                }
            }
            if (btn.Name == "btnqxwh")
            {

                foreach (object obj in App._GMainWindow.MenuPanel.Children)
                {
                    if (obj.GetType().Equals(typeof(StackPanel)))
                    {
                        StackPanel sp = obj as StackPanel;
                        string name = sp.Name;
                        if ("spqxwh".Equals(name))
                        {
                            int count = sp.Children.Count;
                            sp.Height = 28 * count;
                        }
                        if ("spjczd".Equals(name) || "spjkgl".Equals(name) || "spxtsz".Equals(name))
                        {
                            sp.Height = 0;
                        }
                        foreach (object obj2 in sp.Children)
                        {
                            if (obj2.GetType().Equals(typeof(Button)))
                            {
                                    //setDeatult("0003", obj2, modules);
             
                            }
                        }
                    }
                  
                }
            }
            if (btn.Name == "btnxtsz")
            {

                foreach (object obj in App._GMainWindow.MenuPanel.Children)
                {
                    if (obj.GetType().Equals(typeof(StackPanel)))
                    {
                        StackPanel sp = obj as StackPanel;
                        string name = sp.Name;
                        if ("spxtsz".Equals(name))
                        {
                            int count = sp.Children.Count;
                            sp.Height = 28 * count;
                        }
                        if ("spjczd".Equals(name) || "spjkgl".Equals(name) || "spqxwh".Equals(name))
                        {
                            sp.Height = 0;
                        }
                        foreach (object obj2 in sp.Children)
                        {
                            if (obj2.GetType().Equals(typeof(Button)))
                            {
                                //setDeatult("0002", obj2, modules);
                            }
                        }
                    }
                  
                }
            }
        }

        public void setDeatult(string funid,object obj, ObservableCollection<FuncModule> modules)
        {
            //ObservableCollection<FuncModule> func = modules.Where(r => r.PreFuncID == funid).ConvertTo();
            //ObservableCollection<FuncModule> func = ServiceHelper.ServiceClient.SelectAllUserFuncs(App._GYhxx.YHBH, funid);
            Button btnmenu = obj as Button;
            //if (func.Count > 0)
            //{
            //    if (btnmenu.Name.Substring(4).Equals(func[0].FuncID))
            //    {
            //        if (gbtn != null)
            //        {
            //            ObservableCollection<FuncModule> gfunc = modules.Where(r => r.FuncID == gbtn.Name.Substring(4)).ConvertTo();
            //            if (gfunc.Count > 0)
            //            {
            //                gbtn.SetResourceReference(Button.StyleProperty, gfunc[0].Style);
            //            }
            //        }
            //        btnmenu.SetResourceReference(Button.StyleProperty, func[0].Style + "2");
            //        gbtn = btnmenu;
            //        if (func[0].Path.Contains("MainPageView"))
            //        {
            //            //6,14,201,-96
            //            Thickness tkn = new Thickness();
            //            tkn.Left = 6;
            //            tkn.Top = 14;
            //            tkn.Right = 201;
            //            tkn.Bottom = -96;
            //            App._GMainWindow.mainFrame.Margin = tkn;
            //            //0,0,6,0
            //            Thickness tkgrid = new Thickness();
            //            tkgrid.Left = 0;
            //            tkgrid.Top = 0;
            //            tkgrid.Right = 6;
            //            tkgrid.Bottom = 0;
            //            App._GMainWindow.mainGrid.Margin = tkgrid;

            //            //5,10,201,-96
            //            Thickness tkrec = new Thickness();
            //            tkrec.Left = 5;
            //            tkrec.Top = 10;
            //            tkrec.Right = 201;
            //            tkrec.Bottom = -96;
            //            App._GMainWindow.rtmain.Margin = tkrec;
            //            App._GMainWindow.spright.Visibility = Visibility.Visible;
            //            BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/main-1.jpg"));
            //            App._GMainWindow.mainImg.Source = bi;
            //        }
            //        else
            //        {

            //            Thickness tkn = new Thickness();
            //            tkn.Left = 9;
            //            tkn.Top = 14;
            //            tkn.Right = 40;
            //            tkn.Bottom = -96;
            //            App._GMainWindow.mainFrame.Margin = tkn;

            //            Thickness tkgrid = new Thickness();
            //            tkgrid.Left = 0;
            //            tkgrid.Top = 0;
            //            tkgrid.Right = -1;
            //            tkgrid.Bottom = 0;
            //            App._GMainWindow.mainGrid.Margin = tkgrid;

            //            Thickness tkrec = new Thickness();
            //            tkrec.Left = 9;
            //            tkrec.Top = 10;
            //            tkrec.Right = 40;
            //            tkrec.Bottom = -96;
            //            App._GMainWindow.rtmain.Margin = tkrec;
            //            App._GMainWindow.spright.Visibility = Visibility.Hidden;
            //            BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/background.jpg"));

            //            App._GMainWindow.mainImg.Source = bi;
            //        }
            //        NavigationService.Navigate(new Uri(func[0].Path, UriKind.RelativeOrAbsolute));
            //    }
            //}
        }
        public Button gbtn= null;
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
           
            string id = btn.Name.Substring(4);
            //ObservableCollection<FuncModule> funcs = ServiceHelper.ServiceClient.SelectAllFuncModule();
            //ObservableCollection<FuncModule>  func = funcs.Where(r => r.FuncID == id).ConvertTo();
            //if(gbtn!=null)
            //{
            //    ObservableCollection<FuncModule>  gfunc = funcs.Where(r => r.FuncID == gbtn.Name.Substring(4)).ConvertTo();
            //    if(gfunc.Count >0)
            //    {
            //        gbtn.SetResourceReference(Button.StyleProperty, gfunc[0].Style ); 
            //    }
            //}
            //if (func.Count > 0)
            //{
            //    if (func[0].Path.Contains("MainPageView"))
            //    {
            //        //6,14,201,-96
            //        Thickness tkn = new Thickness();
            //        tkn.Left = 6;
            //        tkn.Top = 14;
            //        tkn.Right = 201;
            //        tkn.Bottom = -96;
            //        App._GMainWindow.mainFrame.Margin = tkn;
            //        //0,0,6,0
            //        Thickness tkgrid = new Thickness();
            //        tkgrid.Left = 0;
            //        tkgrid.Top = 0;
            //        tkgrid.Right = 6;
            //        tkgrid.Bottom = 0;
            //        App._GMainWindow.mainGrid.Margin = tkgrid;

            //        //5,10,201,-96
            //        Thickness tkrec = new Thickness();
            //        tkrec.Left = 5;
            //        tkrec.Top = 10;
            //        tkrec.Right = 201;
            //        tkrec.Bottom = -96;
            //        App._GMainWindow.rtmain.Margin = tkrec;
            //        App._GMainWindow.spright.Visibility = Visibility.Visible;
            //        BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/main-1.jpg"));
            //        App._GMainWindow.mainImg.Source = bi;
            //    }
            //    else
            //    {

            //        Thickness tkn = new Thickness();
            //        tkn.Left = 9;
            //        tkn.Top = 14;
            //        tkn.Right = 40;
            //        tkn.Bottom = -96;
            //        App._GMainWindow.mainFrame.Margin = tkn;

            //        Thickness tkgrid = new Thickness();
            //        tkgrid.Left = 0;
            //        tkgrid.Top = 0;
            //        tkgrid.Right = -1;
            //        tkgrid.Bottom = 0;
            //        App._GMainWindow.mainGrid.Margin = tkgrid;

            //        Thickness tkrec = new Thickness();
            //        tkrec.Left = 9;
            //        tkrec.Top = 10;
            //        tkrec.Right = 40;
            //        tkrec.Bottom = -96;
            //        App._GMainWindow.rtmain.Margin = tkrec;
            //        App._GMainWindow.spright.Visibility = Visibility.Hidden;
            //        BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/background.jpg"));

            //        App._GMainWindow.mainImg.Source = bi;
            //    }
            //     btn.SetResourceReference(Button.StyleProperty, func[0].Style + "2");
            //    gbtn = btn;
            //    NavigationService.Navigate(new Uri(func[0].Path, UriKind.RelativeOrAbsolute));
            //}
        }
        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Name == "qb")
            {
                App._GMainWindow._sFilterCondition = "ALL";
            }
            else
            {
                string filter = rb.Name.Substring(3, rb.Name.Length-3);
                App._GMainWindow._sFilterCondition = filter;
               
            }
                  
        }
        /// <summary>
        /// 主窗口动态菜单按钮的响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Menu_Click(object sender, RoutedEventArgs e)
        {
            ZNC.Component.ImageButton2.DynamicImageButton btn = (ZNC.Component.ImageButton2.DynamicImageButton)sender;
            string gnbh = btn.Name.Substring(4); // 从第四位开始截取功能编号，“btn_XXXXX”

            // 动态加工的菜单的功能响应事件
            //foreach (LOCATE_YHGN data in App._GMainWindow._locate_yhgnLists)
            //{
            //    if (data.GNBH.Trim() == gnbh)
            //    {
            //        if (data.GNLJ.Contains("MainPageView"))
            //        {
            //            //6,14,201,-96
            //            Thickness tkn = new Thickness();
            //            tkn.Left = 6;
            //            tkn.Top = 14;
            //            tkn.Right = 201;
            //            tkn.Bottom = -96;
            //            App._GMainWindow.mainFrame.Margin = tkn;
            //            //0,0,6,0
            //            Thickness tkgrid = new Thickness();
            //            tkgrid.Left = 0;
            //            tkgrid.Top = 0;
            //            tkgrid.Right = 6;
            //            tkgrid.Bottom = 0;
            //            App._GMainWindow.mainGrid.Margin = tkgrid;

            //            //5,10,201,-96
            //            Thickness tkrec = new Thickness();
            //            tkrec.Left = 5;
            //            tkrec.Top = 10;
            //            tkrec.Right = 201;
            //            tkrec.Bottom = -96;
            //            App._GMainWindow.rtmain.Margin = tkrec;
            //            App._GMainWindow.spright.Visibility = Visibility.Visible;
            //            BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/main-1.jpg"));
            //            App._GMainWindow.mainImg.Source = bi;
            //        }
            //        else
            //        {
                       
            //            Thickness tkn = new Thickness();
            //            tkn.Left = 9;
            //            tkn.Top = 14;
            //            tkn.Right = 40;
            //            tkn.Bottom = -96;
            //            App._GMainWindow.mainFrame.Margin = tkn;

            //            Thickness tkgrid = new Thickness();
            //            tkgrid.Left = 0;
            //            tkgrid.Top = 0;
            //            tkgrid.Right = -1;
            //            tkgrid.Bottom = 0;
            //            App._GMainWindow.mainGrid.Margin = tkgrid;

            //            Thickness tkrec = new Thickness();
            //            tkrec.Left = 9;
            //            tkrec.Top = 10;
            //            tkrec.Right = 40;
            //            tkrec.Bottom = -96;
            //            App._GMainWindow.rtmain.Margin = tkrec;
            //            App._GMainWindow.spright.Visibility = Visibility.Hidden;
            //            BitmapImage bi = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/background.jpg"));

            //            App._GMainWindow.mainImg.Source = bi;
            //        }
            //        NavigationService.Navigate(new Uri(data.GNLJ, UriKind.RelativeOrAbsolute));
            //        break;
            //    }
            //}
        }

        #region Method

        void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavigationService.OnNavigated(e.Uri, e.ExtraData);

            if (e.Content == null || !(e.Content is Page))
            {
                _isInNavigation = false;
                return;
            }

            Page page = e.Content as Page;

            if (page.DataContext == null || !(page.DataContext is ChildPageViewModel))
            {
                _isInNavigation = false;
                return;
            }

            // If the navigate service catch exception, quit navigation mode, and throw exception.
            try
            {
                if (_lastViewModel != null)
                {
                    //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATED_FROM_START);
                    _lastViewModel.OnNavigatedFrom(sender, e);
                    //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATED_FROM_END);
                }
                ChildPageViewModel vm = page.DataContext as ChildPageViewModel;
                vm.UpdateMode(NavigationService.NavigationMode);
                //try { SynchronousMenuSelected(e.Uri.OriginalString); }
                //catch (Exception ex) { LogHelper.Write("同步菜单出错", LogMessageType.Error, ex); }
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATED_TO_START);
                vm.OnNavigatedTo(sender, e);
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATED_TO_END);
                _lastViewModel = vm;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _isInNavigation = false;
            }
            //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATED_END);
        }

        void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // 判断当前是否正处于页面跳转状态.
            if (_isInNavigation) { e.Cancel = true; return; }

            //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATING_START);
            Frame frame = _curMainFrame;
            if (frame == null)
            {
                if (!(sender is Frame)) return;
                frame = sender as Frame;
            }

            _isInNavigation = true;

            Page page = frame.Content as Page;
            if (page.DataContext == null || !(page.DataContext is ChildPageViewModel)) return;

            // If the navigate service catch exception, quit navigation mode, and throw exception.
            try
            {
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATING_C_STRAT);
                (page.DataContext as ChildPageViewModel).OnNavigating(sender, e);
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATING_C_END);
                //NavigationService.Navigate(new Uri("/Views/MainPageView.xaml", UriKind.RelativeOrAbsolute), _MainWindow);
            }
            catch (Exception)
            {
                _isInNavigation = false;
                throw;
            }
            if (!e.Cancel)
            {
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_SAVECACHE_START);
                (page.DataContext as ChildPageViewModel).OnSaveState(sender, e);
                (page.DataContext as ChildPageViewModel).UpdateCache();
                //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_SAVECACHE_END);
            }
            else
            {
                _isInNavigation = false;
            }
            //InterLockHelper.LogDebugInfo(InterLockConstants.DEBUG_NAVIGATION_NAVIGATING_END);
        }

        #endregion

        #region Command

        /// <summary>
        /// 科室楼层选择时，自动刷新并加载主界面平面图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CmbKSLC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 在MainPage窗口中自动刷新主界面平面图
            #region 根据科室所在楼层动态加载平面背景图

            //NavigationService.Navigate(new Uri("/Views/MainPageView.xaml", UriKind.RelativeOrAbsolute));


            #endregion
            // 刷新该楼层的对象列表

            // 刷新出口提示信息列表
        }
        
        private ICommand _BtnExit;
        public ICommand BtnExit
        {
            get
            {
                if (_BtnExit == null)
                {
                    _BtnExit = new DelegateCommand(BtnExit_Click);
                }
                return _BtnExit;
            }
        }
        /// <summary>
        /// 退出按钮事件
        /// </summary>
        private void BtnExit_Click()
        {
            //NavigationService.Navigate(new Uri("/Views/SecondPageView.xaml", UriKind.Relative));
            App.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            try
            {
                foreach (Window win in App.Current.Windows)
                {
                    win.Close();
                }
            }
            catch (Exception ex)
            {
                //throw;
            }

            Application.Current.Dispatcher.Thread.Abort();//关闭当前进程exe程序
        }

        /// <summary>
        /// 系统退出按钮的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Exit_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Views/SecondPageView.xaml", UriKind.Relative));
            App.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            //try
            //{
            //    foreach (Window win in App.Current.Windows)
            //    {
            //        win.Close();
            //    }
            //}
            //catch (Exception)
            //{
            //    //throw;
            //}

            //Application.Current.Dispatcher.Thread.Abort();//关闭当前进程exe程序
            //ServiceHelper.ServiceClient.UpdateCount(-1);
            Environment.Exit(0);
        }

        /// <summary>
        /// 普通系统按钮的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Btn_Click(object sender, RoutedEventArgs e)
        {
            //App.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // 用户切换功能
            ZNC.Component.DynamicImageButton.DynamicButton ImgBtn = (ZNC.Component.DynamicImageButton.DynamicButton)sender;
            if (ImgBtn.Name == "btn_user")
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.Name == "")
                        _bYhch_Mode = true; 

                    win.Close();
                    
                }

                //LoginView LoginWindow = new LoginView();
                //LoginWindow.Show();

            }
        }

        /// <summary>
        /// 鼠标进入退出按钮范围时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Btn_MouseEnter(object sender, MouseEventArgs e ) 
        {
            ZNC.Component.DynamicImageButton.DynamicButton ImgBtn = (ZNC.Component.DynamicImageButton.DynamicButton)sender ;

            if (ImgBtn.Name == "btn_exit")
            {
                ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/button_exit1.png"));
            }
            else if (ImgBtn.Name == "btn_user")
            {
                ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/button_UserCH1.png"));
            }
        }

        /// <summary>
        /// 鼠标退出按钮范围时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            ZNC.Component.DynamicImageButton.DynamicButton ImgBtn = (ZNC.Component.DynamicImageButton.DynamicButton)sender;

            if (ImgBtn.Name == "btn_exit")
            {
                ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/button_exit.png"));
            }
            else if (ImgBtn.Name == "btn_user")
            {
                ImgBtn.IconImage = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/button_userCH.png"));
            }
        }

        /// <summary>
        /// 双击查看定位对象的详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                //NavigationService.Navigate(new Uri("/Views/Maintenance/APDetailsView.xaml", UriKind.RelativeOrAbsolute), sender as LOCATE_APINFO);
            }
        }

        #endregion

        #region Property

        private WindowState _MainWindowState = WindowState.Maximized;
        public WindowState MainWindowState
        {
            get { return _MainWindowState; }
            set { base.SetValue(ref _MainWindowState, value, () => this.MainWindowState); }
        }

        private static MainWindowViewModel _CurrentViewModel;
        public static MainWindowViewModel CurrentViewModel
        {
            get { return MainWindowViewModel._CurrentViewModel; }
        }

        private NavigationHelper _navigateService;
        public NavigationHelper NavigationService
        {
            get
            {
                if (_navigateService == null)
                {
                    lock (_lockObject)
                    {
                        try
                        {
                            if (_navigateService == null && Application.Current != null && Application.Current.MainWindow != null && Application.Current.MainWindow is MainWindow)
                            {
                                _curMainFrame = (Application.Current.MainWindow as MainWindow).mainFrame;
                                _curMainFrame.Navigating += new NavigatingCancelEventHandler(MainFrame_Navigating);
                                _curMainFrame.Navigated += new NavigatedEventHandler(MainFrame_Navigated);

                                _navigateService = new NavigationHelper(_curMainFrame);
                            }
                        }
                        catch (Exception ex)
                        {
                            //LogHelper.Write( ex.ToString(), LogMessageType.Error);
                        }
                    }
                }
                return _navigateService;
            }
        }

        private bool _MaleChecked = true;
        public bool MaleChecked
        {
            get
            {
                return _MaleChecked;
            }
            set
            {
                base.SetValue(ref _MaleChecked, value, () => this.MaleChecked);
            }
        }

        private bool _FemaleChecked = true;
        public bool FemaleChecked
        {
            get
            {
                return _FemaleChecked;
            }
            set
            {
                base.SetValue(ref _FemaleChecked, value, () => this.FemaleChecked);
            }
        }

        private bool _FixedChecked = true;
        public bool FixedChecked
        {
            get
            {
                return _FixedChecked;
            }
            set
            {
                base.SetValue(ref _FixedChecked, value, () => this.FixedChecked);
            }
        }

        private bool _MobileChecked = true;
        public bool MobileChecked
        {
            get
            {
                return _MobileChecked;
            }
            set
            {
                base.SetValue(ref _MobileChecked, value, () => this.MobileChecked);
            }
        }

        /// <summary>
        /// 弹出框窗体的标题.
        /// </summary>
        private string _msgTitle = "定位信息提醒:";
        public string MsgTitle
        {
            get
            {
                return _msgTitle;
            }
            set
            {
                base.SetValue(ref _msgTitle, value, () => this.MsgTitle, false);
            }
        }

        #endregion



        /// <summary>
        /// 窗口自动响应回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.F1)
            //{

            //    App._GMainWindow.Visibility = Visibility.Hidden;
            //    FullMainWindowView mw = new FullMainWindowView();
         
            //    mw.Show();
                

            //}
        }
    }
}
