using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Globalization;
using GifImageLib;
using System.Windows.Media;
using System.Xml;
using System.Windows.Input;
using System.IO;
using System.Reflection;
using System.Threading;
using ZNC.DataEntiry;

namespace MaintenancePlatform.ViewModels
{
    class MainPageViewModel : ChildPageViewModel
    {
        //定时器缓存变量 add by AndrewChien 2014-02-25  16:40
        //private IEnumerable<LOCATE_JKDX> LocalJKDX;
        //private IEnumerable<LOCATE_JKDX> LocalJKDXPerson;
        //private IEnumerable<DeviceClass> LocalDeviceClass;
        //private IEnumerable<LOCATE_JKKS> LocalJKKS;
        //private IEnumerable<LOCATE_APINFO> LocalAPINFO;

        MainPageView View;      // 当前加载页面
        public double XCoordinate = 0.0;
        public double YCoordinate = 0.0;
        public double GlobleHeight = 0.0;
        public double GlobleWidth = 0.0;
        public static double Height = 592.00;
        public static double Width = 1026.00;
        public static int intROOM;
        // 出口阅读器定义，可以定义多个，以逗号分隔
        private string _sOutReadSN = string.Empty;
        //出口阅读器坐标，如果有多个出口，每两个数值为组进行解析
        private string _sOutReadXY = string.Empty;
        //在出口阅读器位置，如果超以下设置的时间(秒)没有读到信号，则认为人员已离开
        private int _iOutReadDown = 0;
        //屏幕分辩率设置模式，对应于LOCATE_APINFO表，用于在不同的屏幕分辨率上显示定位信息
        private int _iScreenMode = 1;
        Image mag;
        public string _SystemName;
        DispatcherTimer Timer;
        // 科室楼层
        private string _sKslc = string.Empty;
        // 监控对象当前的筛选条件
        private string _sFilterCondition = string.Empty;
        //当位置发生改变时，多少秒钟内会出现闪的效果，过了这个时间点，就替换成png静态图片，=0时，不出现闪的效果
        private int _iFlashSecond = 0;
        //是否显示对象名称
        public int ShowTitle = 0;
        //语音报警锁
        public bool RemindLock = true;
        //语音提醒间隔时间
        public int RemindSecond = 0;
        //是否显示按钮
        public int ShowButton = 0;
        //public int MlinkVersion = 1;//中间件版本
        public DispatcherTimer TimerRemindSecond;
        public static StringBuilder sDataText = new StringBuilder();
        private static DateTime dDataPreTime;

        public string gksmc = "";

        //SMSIni smsint = new SMSIni();
        //public SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        //public SpVoice Voice;
        //图片显示模式
        public int ShowModel = 0;
        int pmms = 0;

        public MainPageViewModel()
        {

        }
        internal void MainPageView_Unloaded(object sender, RoutedEventArgs e)
        {
            //Timer.Stop();
        }

        /// <summary>
        /// 贴图标，该方法全屏时自动缩放，并且适应所有屏幕！
        /// </summary>
        /// <param name="_iconurl"></param>
        /// <param name="_iconwidth"></param>
        /// <param name="_iconheight"></param>
        /// <param name="_top"></param>
        /// <param name="_left"></param>
        /// <param name="_iconname"></param>
        /// <param name="_isdynamic"></param>
        private void SetIconAncestor(string _iconurl,double _iconwidth,double _iconheight,double _top,double _left,string _iconname,bool _isdynamic)
        {
            if (_isdynamic)
            {
                var gif = new GifImage
                {
                    Source = _iconurl,
                    Stretch = Stretch.Uniform,
                    Width = _iconwidth,
                    Height = _iconheight,
                    Name = _iconname
                };
                Canvas.SetTop(gif, _top);
                Canvas.SetLeft(gif, _left);
                View.Canvas1.Children.Add(gif);
            }
            else
            {
                var img = new Image();
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_iconurl, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                img.Source = bitmap;
                img.HorizontalAlignment = HorizontalAlignment.Stretch;
                img.Opacity = 100;
                img.Width = _iconwidth;
                img.Height = _iconheight;
                img.Name = _iconname;
                Canvas.SetTop(img, _top);
                Canvas.SetLeft(img, _left);
                View.Canvas1.Children.Add(img);
            }
        }

        /// <summary>
        /// 设置各设备的图标状态
        /// </summary>
        /// <param name="_deviceid"></param>
        /// <param name="_pictype"></param>
        /// <param name="_iconname"></param>
        private void SetIcon(DeviceID _deviceid, PicType _pictype,string _iconname)
        {
            //大小固定为宽度0.2，高度0.2
            SetIconAncestor(AlarmIcon.Picurl[_pictype], 0.2, 0.2, AlarmIcon.Deviceposition[_deviceid].X, 
                AlarmIcon.Deviceposition[_deviceid].Y, _iconname, _pictype.ToString().Contains("gif"));

            ////插入图标
            //SetIconAncestor(greenpng, 0.2, 0.2, 0.1, 0.1, "img_agv1", false);
            //SetIconAncestor(greenpng, 0.2, 0.2, 0.1, 1.9, "img_agv2", false);
            //SetIconAncestor(greenpng, 0.2, 0.2, 0.1, 3.7, "img_agv3", false);
            //SetIconAncestor(greenpng, 0.2, 0.2, 5.28, 0.1, "img_agv4", false);
            //SetIconAncestor(greenpng, 0.2, 0.2, 5.28, 1.9, "img_agv5", false);
            //SetIconAncestor(greenpng, 0.2, 0.2, 5.28, 3.7, "img_agv6", false);

            //SetIconAncestor(redpng, 0.2, 0.2, 0.1, 5.5, "img_cnc1", false);
            //SetIconAncestor(redpng, 0.2, 0.2, 0.1, 7.75, "img_cnc2", false);
            //SetIconAncestor(redpng, 0.2, 0.2, 5.28, 5.5, "img_cnc3", false);
            //SetIconAncestor(redpng, 0.2, 0.2, 5.28, 7.75, "img_cnc4", false);

            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 0.92, "img_robot1", true);
            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 2.32, "img_robot2", true);
            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 3.73, "img_robot3", true);
            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 5.14, "img_robot4", true);
            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 6.54, "img_robot5", true);
            //SetIconAncestor(yellowgif, 0.2, 0.2, 2.26, 7.96, "img_robot6", true);

        }

        internal void PageLoaded(object sender, RoutedEventArgs e)
        {
            View = (MainPageView)sender;
            var gmain = View.MainGrid;



            #region 重置图标点位坐标
            var point = gmain.TransformToAncestor(Window.GetWindow(gmain)).Transform(new Point(0, 0));//MainGrid起始位置

            //MessageBox.Show("起始位置:" + Math.Round(point.X) + " | " + Math.Round(point.Y)
            //    + "宽度：" + Math.Round(gmain.ActualWidth) + "高度：" + Math.Round(gmain.ActualHeight));

            #endregion

            SetIcon(DeviceID.AGV1, PicType.greenpng, "img_agv1");
            SetIcon(DeviceID.AGV2, PicType.greenpng, "img_agv2");
            SetIcon(DeviceID.AGV3, PicType.greenpng, "img_agv3");
            SetIcon(DeviceID.AGV4, PicType.greenpng, "img_agv4");
            SetIcon(DeviceID.AGV5, PicType.greenpng, "img_agv5");
            SetIcon(DeviceID.AGV6, PicType.greenpng, "img_agv6");

            SetIcon(DeviceID.CNC1, PicType.redpng, "img_cnc1");
            SetIcon(DeviceID.CNC2, PicType.redpng, "img_cnc2");
            SetIcon(DeviceID.CNC3, PicType.redpng, "img_cnc3");
            SetIcon(DeviceID.CNC4, PicType.redpng, "img_cnc4");
            
            SetIcon(DeviceID.Robot1, PicType.yellowgif, "img_robot1");
            SetIcon(DeviceID.Robot2, PicType.yellowgif, "img_robot2");
            SetIcon(DeviceID.Robot3, PicType.yellowgif, "img_robot3");
            SetIcon(DeviceID.Robot4, PicType.yellowgif, "img_robot4");
            SetIcon(DeviceID.Robot5, PicType.yellowgif, "img_robot5");
            SetIcon(DeviceID.Robot6, PicType.yellowgif, "img_robot6");
            





            ////MessageBox.Show(App._GMainWindow.Width.ToString());
            ////MessageBox.Show(App._GMainWindow.WindowStartupLocation.ToString());
            ////MessageBox.Show(App._GMainWindow.WindowState.ToString());
            ////MessageBox.Show(App._GMainWindow.WindowStyle.ToString());
            ////App._GMainWindow.Width = 200;
            ////App._GMainWindow.WindowState = WindowState.Normal;
            ////App._GMainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            ////Resolution.Resolution rr = new Resolution.Resolution();
            ////rr.setResolution(800, 1024, 60);

            //View = (MainPageView)sender;
            //_sOutReadSN = System.Configuration.ConfigurationManager.AppSettings["OutReadSN"];
            //_sOutReadXY = System.Configuration.ConfigurationManager.AppSettings["OutReadXY"];
            //_SystemName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
            //_iOutReadDown = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OutReadDown"]);
            //_iScreenMode = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenMode"]);
            //_iFlashSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["FlashSecond"]);
            //double second = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["IntervalSecond"]);
            //string sPrintBarcode = string.Empty;
            //sPrintBarcode = System.Configuration.ConfigurationManager.AppSettings["PrintBarcode"].ToString();
            //string sPrintDoubleBarcode = string.Empty;
            //sPrintDoubleBarcode = System.Configuration.ConfigurationManager.AppSettings["PrintDoubleBarcode"].ToString();
            //App._GMainWindow.sysname.Text = _SystemName;
            //App.ImgPattern = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ImgPattern"].ToString());
            ////App._JKDX_PIC = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX_PIC(App.ImgPattern);
            //ShowTitle = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowTitle"]);
            //RemindLock = System.Configuration.ConfigurationManager.AppSettings["ShowTitle"] != "no";//语音报警锁
            //RemindSecond = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RemindSecond"]);
            //ShowButton = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowButton"]);
            //ShowModel = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowModel"]);
            //pmms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenMode"]);
            ////MlinkVersion = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MlinkVersion"]);
            //if (sPrintBarcode.Length < 1)
            //{
            //    App._PrintBarcode = 0;
            //}
            //else
            //{
            //    App._PrintBarcode = int.Parse(sPrintBarcode);
            //}

            //if (sPrintDoubleBarcode.Length < 1)
            //{
            //    App._PrintDoubleBarcode = 0;
            //}
            //else
            //{
            //    App._PrintDoubleBarcode = int.Parse(sPrintDoubleBarcode);
            //}

            //string sPrintReport = string.Empty;
            //sPrintReport = System.Configuration.ConfigurationManager.AppSettings["PrintReport"].ToString();
            //string sSMSRun = string.Empty;
            //sSMSRun = System.Configuration.ConfigurationManager.AppSettings["SMSRun"].ToString();
            //if (sPrintReport.Length < 1)
            //{
            //    App._PrintReport = 0;
            //}
            //else
            //{
            //    App._PrintReport = int.Parse(sPrintReport);
            //}

            //if (sSMSRun.Length < 1)
            //{
            //    App._SMSRun = 0;
            //}
            //else
            //{
            //    App._SMSRun = int.Parse(sSMSRun);
            //}
            //// 根据科室所在楼层动态加载平面背景图
            //RefImageMainpage();

            ////主定时任务，刷新界面Timer_Tick
            //Timer = new DispatcherTimer();
            //Timer.Interval = TimeSpan.FromSeconds(second);
            //Timer.Tick += new EventHandler(Timer_Tick);
            //Timer.Start();

            //if (ShowButton == 1)   //向界面添加按钮，线程
            //{
            //    Thread thread = new Thread(new ThreadStart(AddButton));
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //if (RemindSecond > 0)   //提醒，定时器
            //{
            //    TimerRemindSecond = new DispatcherTimer();
            //    TimerRemindSecond.Interval = TimeSpan.FromSeconds(RemindSecond);
            //    TimerRemindSecond.Tick += new EventHandler(TimerRemindSecond_Tick);
            //    TimerRemindSecond.Start();
            //}
        }

        #region Method

        private void AsyncAddButton()
        {
            //实例化委托并初赋值
            DelegateAddButton dn = new DelegateAddButton(AddButton);
            //输出参数
            int i;
            //实例化回调方法
            //把AsyncCallback看成Delegate你就懂了，实际上AsyncCallback是一种特殊的Delegate，就像Event似的
            AsyncCallback acb = new AsyncCallback(CallBackMethod);
            //异步开始
            //如果参数acb换成null则表示没有回调方法
            //最后一个参数dn的地方，可以换成任意对象，该对象可以被回调方法从参数中获取出来，写成null也可以。参数dn相当于该线程的ID，如果有多个异步线程，可以都是null，但是绝对不能一样，不能是同一个object，否则异常
            IAsyncResult iar = dn.BeginInvoke(View.MainGrid.Children, out i, acb, dn);
        }

        private void CallBackMethod(IAsyncResult ar)
        {
            //从异步状态ar.AsyncState中，获取委托对象
            DelegateAddButton dn = (DelegateAddButton)ar.AsyncState;
            //输出参数
            int i;

            //一定要EndInvoke，否则你的下场很惨
            string r = dn.EndInvoke(out i, ar);

        }
        private delegate string DelegateAddButton(UIElementCollection source, out int Num);

        //按设备类型分组显示图片
        public void ShowImg()
        {
            string sdxlx = "";
            if (App._GMainWindow._sFilterCondition == "ALL")
            {
                // PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC, "").Where(r => r.TKBZ == 0).ConvertTo();
                sdxlx = "";
            }
            else if (App._GMainWindow._sFilterCondition == "PERSONAL")
            {
                sdxlx = "医院人员";
            }
            else
            {
                //ObservableCollection<DeviceClass> devices = ServiceHelper.ServiceClient.SelectAllDeviceClass().Where(r => r.DeviceClassID == App._GMainWindow._sFilterCondition).ConvertTo();
                //sdxlx = devices[0].DeviceClassName;
            }
            //string floorid = App._GMainWindow.CmbKSLC.SelectedValue.ToString();
            //ObservableCollection<LOCATE_JKKS> jkkss = LocalJKKS.ConvertTo();
            //gksmc = jkkss[0].KSMC;
            //ObservableCollection<LOCATE_APINFO> aps = ServiceHelper.ServiceClient.GetAllAPINFO().Where(r => r.PMMS == pmms).ConvertTo().Where(r => r.SSKSBH == jkkss[0].KSBH).ConvertTo();
            int imgwidth = int.Parse(System.Configuration.ConfigurationManager.AppSettings["imgWidth"]);
            int imgheight = int.Parse(System.Configuration.ConfigurationManager.AppSettings["imgHeight"]);
            List<string> aa = new List<string>() {"123", "456", "789"};
            foreach (string apinfo in aa)
                //foreach (LOCATE_APINFO apinfo in aps)
            {
                //string apbh = apinfo.APBH;
                //int xpoint = apinfo.XPOS_S;
                //int ypoint = apinfo.YPOS_S;
                //int btnpx = apinfo.PX;
                //int btnpy = apinfo.PY;
                //ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.GetRoomAssetCount(apbh, sdxlx, floorid);
                int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                int x = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModex"]);
                int y = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModey"]);
                //for (int i = 0; i < jkdxs.Count(); i++)
                //{
                //    LOCATE_JKDX jkdx = jkdxs[i];
                //    BitmapImage myBitmapImage = new BitmapImage();
                //    int wzx = 0;
                //    int wzy = 0;

                //    if (i != 0)
                //    {
                //        if (i < intROOM)
                //        {
                //            if (App.window == null)
                //            {
                //                wzx = xpoint + i * imgwidth;
                //                wzy = ypoint;
                //            }
                //            else
                //            {
                //                wzx = apinfo.XMPOS_S + i * imgwidth;
                //                wzy = apinfo.YMPOS_S;
                //            }
                //        }
                //        else
                //        {
                //            int row = i / intROOM;
                //            int col = i % intROOM;
                //            if (App.window == null)
                //            {
                //                wzx = xpoint + col * imgwidth;
                //                wzy = ypoint + row * imgheight;
                //            }
                //            else
                //            {
                //                wzx = apinfo.XMPOS_S + col * imgwidth;
                //                wzy = apinfo.YMPOS_S + row * imgheight;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (App.window == null)
                //        {
                //            wzx = apinfo.XPOS_S;
                //            wzy = apinfo.YPOS_S;
                //        }
                //        else
                //        {
                //            wzx = apinfo.XMPOS_S;
                //            wzy = apinfo.YMPOS_S;
                //        }
                //    }
                //    myBitmapImage.BeginInit();
                //    LOCATE_JKDX_PIC jkdxs_pic = null;
                //    if (jkdx.DXXZ == "医院人员")
                //    {
                //        jkdxs_pic = App._JKDX_PIC.Where(r => r.DXLX == "男").ConvertTo()[0];
                //    }
                //    else
                //    {
                //        jkdxs_pic = App._JKDX_PIC.Where(r => r.DXLX == jkdx.DXLX).ConvertTo()[0];
                //    }
                //    string tplj = jkdx.TPLJ;
                //    if (!string.IsNullOrEmpty(tplj))
                //    {
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/" + tplj + ".png");
                //    }
                //    else
                //    {
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/" + jkdxs_pic.PICNAME + ".png");
                //        // myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/" + "DM" + ".png");
                //    }
                //    myBitmapImage.DecodePixelWidth = 500;
                //    myBitmapImage.EndInit();
                //    myBitmapImage.Freeze();

                //    Image mag = new Image();
                //    mag.MouseLeftButtonDown += new MouseButtonEventHandler(mag_MouseLeftButtonDown);

                //    mag.ToolTip = jkdx.DXLX;
                //    mag.Source = myBitmapImage;
                //    mag.Width = jkdxs_pic.TPKD;
                //    mag.Height = jkdxs_pic.TPGD;
                //    //图片的初始坐标
                //    mag.HorizontalAlignment = HorizontalAlignment.Left;
                //    mag.VerticalAlignment = VerticalAlignment.Top;

                //    int width = 0;
                //    int height = 0;

                //    if (App.window == null)
                //    {
                //        width = w * wzx / x;
                //        height = h * wzy / y;
                //    }
                //    else
                //    {
                //        width = wzx;
                //        height = wzy;

                //    }

                //    mag.Margin = new Thickness(width, height, 0, 0);
                //    mag.Tag = apbh + "," + jkdx.DXLX;
                //    mag.Name = jkdxs_pic.PICNAME + apbh;
                //    TextBlock block = new TextBlock();
                //    block.MouseLeftButtonDown += new MouseButtonEventHandler(block_MouseLeftButtonDown);
                //    block.Name = jkdxs_pic.PICNAME + apbh;

                //    block.Text = jkdx.Count.ToString();


                //    block.Margin = new Thickness(width + 10, height - 15, 0, 0);
                //    block.Width = jkdxs_pic.TPKD;
                //    block.Height = 15;
                //    block.FontSize = 10;
                //    block.Tag = apbh + "," + jkdx.DXLX;
                //    block.HorizontalAlignment = HorizontalAlignment.Left;
                //    block.VerticalAlignment = VerticalAlignment.Top;
                //    block.Foreground = Brushes.Red;
                //    block.FontWeight = FontWeights.Bold;
                //    View.MainGrid.Children.Add(mag);
                //    View.MainGrid.Children.Add(block);
                //}

                Button btn = new Button();
                //btn.Name = "btn" + apbh;
                //btn.Content = apinfo.TYQMC;

                //1280,1024

                if (w != x)
                {
                    //不同模式下的正常显示
                    if (App.window == null)
                    {
                        //btn.Margin = new Thickness(w * btnpx / x, h * btnpy / y, 0, 0);
                    }
                    else//不同模式下全屏显示
                    {
                        //btn.Margin = new Thickness(w * apinfo.PMX / x, h * apinfo.PMY / y, 0, 0);
                    }

                }
                else
                {
                    if (App.window == null)
                    {
                        //btn.Margin = new Thickness(btnpx, btnpy, 0, 0);
                    }
                    else
                    {
                        //btn.Margin = new Thickness(apinfo.PMX, apinfo.PMY, 0, 0);
                    }
                }
                btn.Foreground = Brushes.Red;
                btn.Width = 60;
                btn.Height = 20;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.SetResourceReference(Button.StyleProperty, "SBListStyle");
                btn.Click += new RoutedEventHandler(btnFL_Click);
                View.MainGrid.Children.Add(btn);
            }
        }
        public void btnFL_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string apbh = btn.Name.Substring(3, btn.Name.Length - 3);
            //APJKDXView APjkdxs = new APJKDXView(apbh, App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "");
            //if (App.window != null)
            //{
            //    APjkdxs.Owner = App.window;
            //}
            //APjkdxs.ShowDialog();
        }
        public void block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            string tag = block.Tag.ToString();
            string[] list = tag.Split(',');
            string dxlx = list[1];
            string apbh = list[0];
            //APJKDXView APjkdxs = new APJKDXView(apbh, App._GMainWindow.CmbKSLC.SelectedValue.ToString(), dxlx);
            //if (App.window != null)
            //{
            //    APjkdxs.Owner = App.window;
            //}
            //APjkdxs.ShowDialog();
        }
        public void mag_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            string tag = img.Tag.ToString();

            string[] list = tag.Split(',');
            string dxlx = list[1];
            string apbh = list[0];
            //APJKDXView APjkdxs = new APJKDXView(apbh, App._GMainWindow.CmbKSLC.SelectedValue.ToString(), dxlx);
            //if (App.window != null)
            //{
            //    APjkdxs.Owner = App.window;
            //}
            //APjkdxs.ShowDialog();
        }
        //向房间添加一个按钮
        public void AddButton()
        {
            View.MainGrid.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {

                UIElementCollection source = View.MainGrid.Children;
                for (int i = 0; i < source.Count; i++)
                {

                    if (source[i].GetType().Equals(typeof(Button)))
                    {
                        ((source[i] as Button).Parent as Grid).Children.Remove((UIElement)source[i]);
                        i--;
                    }
                }
                int pmms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenMode"]);
                //ObservableCollection<LOCATE_JKKS> jkkss = ServiceHelper.ServiceClient.SelectAllLOCATE_JKKSFromFloorid(int.Parse(App._GMainWindow.CmbKSLC.SelectedValue.ToString())); ;
                //if (jkkss.Count > 0) gksmc = jkkss[0].KSMC;
                //ObservableCollection<LOCATE_APINFO> aps = ServiceHelper.ServiceClient.GetAllAPINFO().Where(r => r.PMMS == pmms).ConvertTo().Where(r => r.SSKSBH == jkkss[0].KSBH).Where(r => r.ISROOM == 1).ConvertTo();
                for (int i = 0; i < 5; i++)
                    //for (int i = 0; i < aps.Count; i++)
                {
                    //LOCATE_APINFO ap = aps[i];
                    Button btn = new Button();
                    //btn.Name = "btn" + ap.APBH;
                    //btn.Content = ap.TYQMC;

                    //1280,1024
                    int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    int x = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModex"]);
                    int y = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModey"]);
                    if (w != x)
                    {
                        //不同模式下的正常显示
                        if (App.window == null)
                        {
                            //dx.SetAttribute("xpoint", (w * xspoint / x).ToString());
                            //dx.SetAttribute("ypoint", (yspoint * h / y).ToString());
                            //btn.Margin = new Thickness(w * ap.PX / x, h * ap.PY / y, 0, 0);
                        }
                        else//不同模式下全屏显示
                        {
                            //dx.SetAttribute("xpoint", (w * xmpoint / x).ToString());
                            //dx.SetAttribute("ypoint", (ympoint * h / y).ToString());
                            //btn.Margin = new Thickness(w * ap.PMX / x, h * ap.PMY / y, 0, 0);
                        }

                    }
                    else
                    {
                        if (App.window == null)
                        {
                            //btn.Margin = new Thickness(ap.PX, ap.PY, 0, 0);
                        }
                        else
                        {
                            //btn.Margin = new Thickness(ap.PMX, ap.PMY, 0, 0);
                        }
                    }
                    btn.Foreground = Brushes.Red;
                    btn.Width = 60;
                    btn.Height = 20;
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.SetResourceReference(Button.StyleProperty, "SBListStyle");

                    btn.Click += new RoutedEventHandler(btn_Click);
                    View.MainGrid.Children.Add(btn);
                }
            });
        }

        public string AddButton(UIElementCollection source, out int num)
        {

            num = 1;

            View.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                for (int i = 0; i < source.Count; i++)
                {

                    if (source[i].GetType().Equals(typeof(Button)))
                    {
                        ((source[i] as Button).Parent as Grid).Children.Remove((UIElement)source[i]);
                        i--;
                    }
                }
                int pmms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenMode"]);
                //if (LocalJKKS == null) LocalJKKS = ServiceHelper.ServiceClient.SelectAllLOCATE_JKKSFromFloorid(int.Parse(App._GMainWindow.CmbKSLC.SelectedValue.ToString()));
                //ObservableCollection<LOCATE_JKKS> jkkss = LocalJKKS.ConvertTo();
                //ObservableCollection<LOCATE_APINFO> aps = ServiceHelper.ServiceClient.GetAllAPINFO().Where(r => r.PMMS == pmms).ConvertTo().Where(r => r.SSKSBH == jkkss[0].KSBH).Where(r => r.ISROOM == 1).ConvertTo();
                for (int i = 0; i < 5; i++)
                    //for (int i = 0; i < aps.Count; i++)
                    {
                    //LOCATE_APINFO ap = aps[i];
                    Button btn = new Button();
                    //btn.Name = "btn" + ap.APBH;
                    //btn.Content = ap.TYQMC;

                    //1280,1024
                    int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    int x = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModex"]);
                    int y = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModey"]);
                    if (w != x)
                    {
                        //不同模式下的正常显示
                        if (App.window == null)
                        {
                            //dx.SetAttribute("xpoint", (w * xspoint / x).ToString());
                            //dx.SetAttribute("ypoint", (yspoint * h / y).ToString());
                            //btn.Margin = new Thickness(w * ap.PX / x, h * ap.PY / y, 0, 0);
                        }
                        else//不同模式下全屏显示
                        {
                            //dx.SetAttribute("xpoint", (w * xmpoint / x).ToString());
                            //dx.SetAttribute("ypoint", (ympoint * h / y).ToString());
                            //btn.Margin = new Thickness(w * ap.PMX / x, h * ap.PMY / y, 0, 0);
                        }

                    }
                    else
                    {
                        if (App.window == null)
                        {
                            //btn.Margin = new Thickness(ap.PX, ap.PY, 0, 0);
                        }
                        else
                        {
                            //btn.Margin = new Thickness(ap.PMX, ap.PMY, 0, 0);
                        }
                    }
                    btn.Foreground = Brushes.Red;
                    btn.Width = 60;
                    btn.Height = 20;
                    btn.HorizontalAlignment = HorizontalAlignment.Left;
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.SetResourceReference(Button.StyleProperty, "SBListStyle");
                    btn.Click += new RoutedEventHandler(btn_Click);
                    View.MainGrid.Children.Add(btn);

                }


            });
            return "1";
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string apbh = btn.Name.Substring(3, btn.Name.Length - 3);
            //RoomJKDXView roomjkdxs = new RoomJKDXView(apbh, App._GMainWindow.CmbKSLC.SelectedValue.ToString());
            //if (App.window != null)
            //{
            //    roomjkdxs.Owner = App.window;
            //}
            //roomjkdxs.ShowDialog();
        }
        //双击退出全屏
        public void MainPageView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (sender.GetType().ToString().Contains("MainPageView"))
                {
                    if (App.window != null)
                    {
                        App.window.Close();
                        App._GMainWindow.mainFrame.Refresh();
                        App.window = null;
                    }
                }
            }
        }
        //按ESC退出全屏
        public void MainPageView_KeyDown(object sender, KeyEventArgs e)
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

        /// <summary>
        /// 根据科室所在楼层动态加载平面背景图
        /// </summary>
        private void RefImageMainpage()
        {
            #region 根据科室所在楼层动态加载平面背景图

            // 刷新图片之前先清除平面图上所有的定位数据
            ForeachDeleteImage(View.MainGrid.Children);

            string sTpmc = string.Empty;
            string sKsbh = string.Empty;
            //_sKslc = App._GMainWindow.CmbKSLC.Text;
            //foreach (LOCATE_KSLC kslc in App._jkkslcDataInfo)
            //{
            //    if (kslc.KSLC == _sKslc)
            //    {
            //        sTpmc = kslc.TPMC;
            //         sKsbh=kslc.KSBH ;
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
            //创建房间分割记录
            CreateRoomXml(sKsbh, _sKslc);

            ImageBrush imageBrush = new ImageBrush();

            imageBrush.ImageSource = new BitmapImage(new Uri("pack://siteoforigin:,,,/image/" + sTpmc));

            View.RecUOne.Fill = imageBrush;

            #endregion

        }

        private void CreateRoomXml(string sKsbh, string _sKslc)
        {
            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
            int Pmms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenMode"]);
            intROOM = int.Parse(System.Configuration.ConfigurationManager.AppSettings["intROOM"]);
            //ObservableCollection<LOCATE_APINFO> collections = ServiceHelper.ServiceClient.GetAPINFOByKSLC(Pmms, sKsbh, _sKslc);
            //ObservableCollection<LOCATE_APINFO> collections = ServiceHelper.ServiceClient.GetAPINFOByKSLCOrAPBH(Pmms, sKsbh, _sKslc, "");
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
        public XmlNode GetMinRoom(string Apbh)
        {
            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
            XmlNode ap = null;
            try
            {
                // 1.创建一个XmlDocument类的对象
                XmlDocument doc = new XmlDocument();

                // 2.把你想要读取的xml文档加载进来
                doc.Load(ExePath);

                XmlNodeList xnl = doc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes["id"].Value.Equals(Apbh))
                    {
                        ap = xn;
                    }

                }

                return minXn(ap.ChildNodes);

            }
            catch
            {
                //MessageBox.Show("配置文件存在异常！");
                return null;
            }

        }
        public XmlNode minXn(XmlNodeList ap)
        {
            XmlNode temp = ap[0];
            for (int i = 0; i < ap.Count; i++)
            {

                if (int.Parse(ap[i].Attributes["value"].Value.ToString()) < int.Parse(temp.Attributes["value"].Value.ToString()))
                    temp = ap[i];

            }
            return temp;
        }
        public void UpXml(XmlNode xn, string Apbh, int DXBH)
        {
            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
            XmlNode ap = null;
            try
            {
                // 1.创建一个XmlDocument类的对象
                XmlDocument doc = new XmlDocument();

                // 2.把你想要读取的xml文档加载进来
                doc.Load(ExePath);

                XmlNodeList xnl = doc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    bool flag = true;
                    if (xn1.Attributes["id"].Value.Equals(Apbh))
                    {
                        for (int i = 0; i < xn1.ChildNodes.Count; i++)
                        {
                            for (int j = 0; j < xn1.ChildNodes[i].ChildNodes.Count; j++)
                            {
                                if (int.Parse(xn1.ChildNodes[i].ChildNodes[j].Attributes["value"].Value.ToString()) == DXBH)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if (flag)
                        {
                            foreach (XmlNode xn2 in xn1.ChildNodes)
                            {
                                if (xn2.Attributes["widthnum"].Value.Equals(xn.Attributes["widthnum"].Value.ToString()) && xn2.Attributes["heightnum"].Value.Equals(xn.Attributes["heightnum"].Value.ToString()))
                                {
                                    XmlElement xe = (XmlElement)xn2;
                                    XmlElement dx = doc.CreateElement("DX");
                                    dx.SetAttribute("value", DXBH.ToString());


                                    //计算设备或人员的显示坐标
                                    //ObservableCollection<LOCATE_APINFO> apinfors = ServiceHelper.ServiceClient.GetAllAPINFO();
                                    //int pmms = 
                                    //apinfors = (apinfors.Where(r => r.APBH == Apbh).ConvertTo()).Where(r => r.PMMS == _iScreenMode).ConvertTo();
                                    int xspoint = 0;
                                    int yspoint = 0;
                                    int xepoint = 0;
                                    int yepoint = 0;
                                    int xmpoint = 0;
                                    int ympoint = 0;
                                    //if (apinfors.Count > 0)
                                    //{
                                    //    xspoint = apinfors[0].XPOS_S;
                                    //    yspoint = apinfors[0].YPOS_S;
                                    //    xepoint = apinfors[0].XPOS_E;
                                    //    yepoint = apinfors[0].YPOS_E;
                                    //    xmpoint = apinfors[0].XMPOS_S;
                                    //    ympoint = apinfors[0].YMPOS_S;
                                    //}
                                    int height = int.Parse(xn2.Attributes["heightnum"].Value);
                                    int width = int.Parse(xn2.Attributes["widthnum"].Value);
                                    int xewidth = int.Parse(xe.Attributes["widthnum"].Value);
                                    int xeheigth = int.Parse(xe.Attributes["heightnum"].Value);

                                    int imgwidth = int.Parse(System.Configuration.ConfigurationManager.AppSettings["imgWidth"]);
                                    int imgheight = int.Parse(System.Configuration.ConfigurationManager.AppSettings["imgHeight"]);

                                    xspoint = xspoint + (width - 1) * imgwidth;
                                    yspoint = yspoint + (height - 1) * imgheight;
                                    xmpoint = xmpoint + (width - 1) * imgwidth;
                                    ympoint = ympoint + (height - 1) * imgheight;

                                    //1280,1024
                                    int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                                    int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                                    //dx.SetAttribute("xpoint", xspoint.ToString());
                                    //dx.SetAttribute("ypoint", yspoint.ToString());
                                    //ServiceHelper.ServiceClient.get

                                    int x = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModex"]);
                                    int y = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ScreenModey"]);
                                    if (w != x && h != y)
                                    {
                                        //不同模式下的正常显示
                                        if (App.window == null)
                                        {
                                            dx.SetAttribute("xpoint", (w * xspoint / x).ToString());
                                            dx.SetAttribute("ypoint", (yspoint * h / y).ToString());
                                        }
                                        else//不同模式下全屏显示
                                        {
                                            dx.SetAttribute("xpoint", (w * xmpoint / x).ToString());
                                            dx.SetAttribute("ypoint", (ympoint * h / y).ToString());
                                        }

                                    }
                                    else
                                    {
                                        if (App.window == null)
                                        {
                                            dx.SetAttribute("xpoint", xspoint.ToString());
                                            dx.SetAttribute("ypoint", yspoint.ToString());
                                        }
                                        else
                                        {
                                            dx.SetAttribute("xpoint", xmpoint.ToString());
                                            dx.SetAttribute("ypoint", ympoint.ToString());
                                        }
                                    }

                                    xe.AppendChild(dx);
                                    xe.SetAttribute("value", (int.Parse(xe.Attributes["value"].Value) + 1).ToString());
                                    break;
                                }

                            }
                        }
                        break;
                    }

                }
                doc.Save("APInfo.xml");


            }
            catch (Exception ex)
            {
                // MessageBox.Show("配置文件存在异常！");

            }
        }
        public void DeleteXml(string Apbh, int DXBH)
        {
            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
            XmlNode ap = null;
            try
            {
                // 1.创建一个XmlDocument类的对象
                XmlDocument doc = new XmlDocument();

                // 2.把你想要读取的xml文档加载进来
                doc.Load(ExePath);

                XmlNodeList xnl = doc.SelectSingleNode("Root").ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1.Attributes["id"].Value.Equals(Apbh))
                    {
                        foreach (XmlNode xn2 in xn1.ChildNodes)
                        {
                            foreach (XmlNode xn3 in xn2.ChildNodes)
                            {
                                if (xn3.Attributes["value"].Value.Equals(DXBH.ToString()))
                                {

                                    xn2.RemoveChild(xn3);
                                    ((XmlElement)xn2).SetAttribute("value", (int.Parse(xn2.Attributes["value"].Value) - 1).ToString());
                                    break;
                                }
                            }

                        }
                        break;
                    }

                }
                doc.Save("APInfo.xml");


            }
            catch
            {
                // MessageBox.Show("配置文件存在异常！");

            }
        }

        ////定时刷新界面方法
        //void TRefresh(object state)
        //{
        //    //TimerRemindSecond.Start();
        //    View.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
        //           {
        //               BitmapImage myBitmapImage;
        //               ObservableCollection<int> collections = new ObservableCollection<int>();

        //               // 判别监控楼层有没有发生变化，如果发生变化，重新加载楼层平面图
        //               string kslc = string.Empty;
        //               kslc = App._GMainWindow.CmbKSLC.Text;
        //               if (kslc != _sKslc)
        //               {
        //                   RefImageMainpage();
        //                   if (ShowButton == 1)
        //                   {
        //                       AddButton();
        //                   }
        //               }

        //               // 如果主界在的筛选条件发生改变，先清除界面原有的监控对象
        //               if (!App._GMainWindow._sFilterCondition.Equals(_sFilterCondition))
        //               {
        //                   _sFilterCondition = App._GMainWindow._sFilterCondition;
        //                   ForeachDeleteImage(View.MainGrid.Children);
        //               }

        //               //为了演示临时新增的方法
        //               if (App._GMainWindow._iObjectTrans > 0)
        //               {
        //                   ServiceHelper.ServiceClient.UpdateJKDXTJQY(App._GMainWindow._iObjectTrans);
        //               }

        //               // 根据不同的筛选条件刷新监控对象列表
        //               if (App._GMainWindow._sFilterCondition == "ALL")
        //               {
        //                   // PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC, "").Where(r => r.TKBZ == 0).ConvertTo();
        //                   PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).ConvertTo();
        //               }
        //               else if (App._GMainWindow._sFilterCondition == "PERSONAL")
        //               {
        //                   // PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC, "医院人员").Where(r => r.TKBZ == 0).ConvertTo();
        //                   PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "医院人员").Where(r => r.TKBZ == 0).ConvertTo();

        //               }
        //               else
        //               {
        //                   // PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC,"").Where(r => r.TKBZ == 0).Where(r=>r.DXLX == App._GMainWindow._sFilterCondition).ConvertTo();
        //                   ObservableCollection<DeviceClass> devices = ServiceHelper.ServiceClient.SelectAllDeviceClass().Where(r => r.DeviceClassID == App._GMainWindow._sFilterCondition).ConvertTo();
        //                   PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo();

        //               }


        //               //绑定主窗口界面中的出口预警列表
        //               SbckjyCollection = ServiceHelper.ServiceClient.SelectALLJYSBCkjyList(App._GYhxx.KSMC).Where(r => r.TKBZ == 0).ConvertTo();//查询所有未退卡的设备出口预警信息
        //               //App._GMainWindow.ListKsCkjy.ItemsSource = SbckjyCollection;

        //               int cLkry = 0;  // 离开人员计数，当离开人员数>0时，出门位置显示GIF提醒图片
        //               //foreach (var item in MainWindowViewModel._StaticPersonalInfos)//遍历循环当前条件内的登记人员和资产信息
        //               foreach (var item in PersonalInfoCollection)    // 遍历循环当前条件内的登记人员和资产信息
        //               {
        //                   if (item.LKBZ == 1) cLkry++;
        //                   myBitmapImage = GiveALogoToView(item, collections);
        //               }

        //               // 遍历所有对象，并将位置信息超过_iFlashSecond所设定的秒数未变的GIF图片更换为Image图片
        //               if (_iFlashSecond > 0)
        //               {
        //                   ForeachDeleteGif_UpdateImage(View.MainGrid.Children);
        //               }
        //               #region 显示或隐藏出口报警的GIF提醒图片

        //               #endregion

        //               #region 更新数量
        //               ObservableCollection<LOCATE_JKDX> jkdxsq = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).ConvertTo();
        //               App._GMainWindow.qb.Content = "全部 ( " + jkdxsq.Count + " )";
        //               ObservableCollection<LOCATE_JKDX> jkdxsry = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "医院人员").Where(r => r.TKBZ == 0).ConvertTo();
        //               App._GMainWindow.ry.Content = "人员 ( " + jkdxsry.Count + " )";
        //               for (int i = 0; i < App._GMainWindow.spsblx.Children.Count; i++)
        //               {
        //                   if (App._GMainWindow.spsblx.Children[i].GetType().Equals(typeof(RadioButton)))
        //                   {
        //                       RadioButton rb = App._GMainWindow.spsblx.Children[i] as RadioButton;
        //                       if (rb.Name != "qb" && rb.Name != "ry" && rb.Name.Contains("rb_"))
        //                       {
        //                           string dxlx = rb.Name.Substring(3, rb.Name.Length - 3);

        //                           ObservableCollection<DeviceClass> devices = ServiceHelper.ServiceClient.SelectAllDeviceClass().Where(r => r.Grade == 2).ConvertTo();
        //                           devices = devices.Where(r => r.DeviceClassID == dxlx).ConvertTo();

        //                           string name = "";
        //                           if (devices.Count > 0)
        //                           {
        //                               ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).ConvertTo().Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo();
        //                               name = devices[0].DeviceClassName;
        //                               rb.Content = name + " ( " + jkdxs.Count + " )";
        //                           }

        //                       }
        //                   }
        //               }

        //           });
        //               #endregion

        //}

        /// <summary>
        /// 3秒后将自动执行Tick事件
        /// 主定时任务，刷新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_Tick(object sender, EventArgs e)
        {
            //Stopwatch stw = new Stopwatch();
            //stw.Start();
            //add by AndrewChien 2014-02-25  16:40
            //LocalJKDX =
            //    ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(
            //        App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0);
            //LocalJKDXPerson =
            //    ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(
            //        App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "医院人员").Where(r => r.TKBZ == 0);
            //LocalDeviceClass = ServiceHelper.ServiceClient.SelectAllDeviceClass();

            //LocalJKKS =
            //   ServiceHelper.ServiceClient.SelectAllLOCATE_JKKSFromFloorid(int.Parse(App._GMainWindow.CmbKSLC.SelectedValue.ToString()));

            BitmapImage myBitmapImage;
            ObservableCollection<int> collections = new ObservableCollection<int>();

            // 判别监控楼层有没有发生变化，如果发生变化，重新加载楼层平面图
            string kslc = string.Empty;
            //kslc = App._GMainWindow.CmbKSLC.Text;
            if (App._GMainWindow._sYFilterCondition != App._GMainWindow._sFilterCondition)
            {
                RefImageMainpage();//根据科室所在楼层动态加载平面背景图
                if (ShowModel == 2)
                {
                    ShowImg();
                }
                App._GMainWindow._sYFilterCondition = App._GMainWindow._sFilterCondition;
            }
            //if (App._GMainWindow._sYCzName != App._GMainWindow.txtCzmc.Text.Trim())
            //{
            //    RefImageMainpage();//根据科室所在楼层动态加载平面背景图
            //    if (ShowModel == 2)
            //    {
            //        ShowImg();
            //    }
            //    App._GMainWindow._sYCzName = App._GMainWindow.txtCzmc.Text.Trim();
            //}
            if (kslc != _sKslc)
            {
                RefImageMainpage();//根据科室所在楼层动态加载平面背景图
                if (ShowModel == 2)
                {
                    ShowImg();
                }
                if (ShowButton == 1)
                {
                    AsyncAddButton();
                }
            }

            // 如果主界在的筛选条件发生改变，先清除界面原有的监控对象
            if (!App._GMainWindow._sFilterCondition.Equals(_sFilterCondition))
            {
                _sFilterCondition = App._GMainWindow._sFilterCondition;
                ForeachDeleteImage(View.MainGrid.Children);
            }

            //为了演示临时新增的方法
            if (App._GMainWindow._iObjectTrans > 0)
            {
                //ServiceHelper.ServiceClient.UpdateJKDXTJQY(App._GMainWindow._iObjectTrans);
            }

            // 根据不同的筛选条件刷新监控对象列表
            //string czname = App._GMainWindow.txtCzmc.Text.Trim();
            //if (!string.IsNullOrEmpty(czname))
            //{
            //    ObservableCollection<LOCATE_JKDX> czjkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXByNameOrRfid(czname, "");

            //    if (czjkdxs.Count == 0)
            //    {
            //        if (App._GMainWindow._sFilterCondition == "ALL")
            //        {
            //            PersonalInfoCollection = LocalJKDX.ConvertTo();
            //        }
            //        else if (App._GMainWindow._sFilterCondition == "PERSONAL")
            //        {
            //            PersonalInfoCollection = LocalJKDXPerson.ConvertTo();
            //        }
            //        else
            //        {
            //            ObservableCollection<DeviceClass> devices = LocalDeviceClass.Where(r => r.DeviceClassID == App._GMainWindow._sFilterCondition).ConvertTo();
            //            PersonalInfoCollection = LocalJKDX.Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo();
            //        }
            //    }
            //    else
            //    {
            //        PersonalInfoCollection = czjkdxs;
            //    }
            //}
            //else
            //{
            //    if (App._GMainWindow._sFilterCondition == "ALL")
            //    {
            //        PersonalInfoCollection = LocalJKDX.ConvertTo();
            //    }
            //    else if (App._GMainWindow._sFilterCondition == "PERSONAL")
            //    {
            //        PersonalInfoCollection = LocalJKDXPerson.ConvertTo();
            //    }
            //    else
            //    {
            //        ObservableCollection<DeviceClass> devices = LocalDeviceClass.Where(r => r.DeviceClassID == App._GMainWindow._sFilterCondition).ConvertTo();
            //        PersonalInfoCollection = LocalJKDX.Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo();
            //    }
            //}

            //绑定主窗口界面中的定位对象列表
            //App._GMainWindow.ListAllJkdx.ItemsSource = PersonalInfoCollection;

            //绑定主窗口界面中的出口预警列表
            //SbckjyCollection = ServiceHelper.ServiceClient.SelectALLJYSBCkjyList(App._GYhxx.KSMC).Where(r => r.TKBZ == 0).ConvertTo();//查询所有未退卡的设备出口预警信息
            //App._GMainWindow.ListKsCkjy.ItemsSource = SbckjyCollection;

            int cLkry = 0;  // 离开人员计数，当离开人员数>0时，出门位置显示GIF提醒图片
            //foreach (var item in MainWindowViewModel._StaticPersonalInfos)//遍历循环当前条件内的登记人员和资产信息
            if (ShowModel == 1)
            {
                foreach (var item in PersonalInfoCollection)    // 遍历循环当前条件内的登记人员和资产信息
                {
                    //if (item.LKBZ == 1) cLkry++;
                    //myBitmapImage = GiveALogoToView(item, collections);
                }

                // 遍历所有对象，并将位置信息超过_iFlashSecond所设定的秒数未变的GIF图片更换为Image图片
                if (_iFlashSecond > 0)
                {
                    ForeachDeleteGif_UpdateImage(View.MainGrid.Children);
                }
            }
            else
            {
                RefImageMainpage();//根据科室所在楼层动态加载平面背景图
                ShowImg();
            }
            #region 显示或隐藏出口报警的GIF提醒图片（已作废）
            //if (cLkry > 0)
            //{
            //    //View.gifImage.Visibility = Visibility.Visible;
            //    string[] _sOutReadXYSplit = _sOutReadXY.Split(new char[] { ',' });
            //    if (_sOutReadXYSplit.Length > 1)
            //    {
            //        gifimage = new GifImage();

            //        gifimage.Margin = new Thickness(int.Parse(_sOutReadXYSplit[0]), int.Parse(_sOutReadXYSplit[1]), 0, 0);
            //        gifimage.Source = "../image/提示.gif";
            //        //gifimage.Source = "..\\image\\提示.gif"; 
            //        gifimage.Width = 40;
            //        gifimage.Height = 40;
            //        //gifimage.Tag = 

            //        //那个块，就添加到那个里面去
            //        View.MainGrid.Children.Add(gifimage);
            //    }
            //}
            #endregion

            #region 更新数量
            //ObservableCollection<LOCATE_JKDX> jkdxsq = LocalJKDX.ConvertTo();
            //App._GMainWindow.qb.Content = "全部 ( " + jkdxsq.Count + " )";
            //ObservableCollection<LOCATE_JKDX> jkdxsry = LocalJKDXPerson.ConvertTo();
            //App._GMainWindow.ry.Content = "人员 ( " + jkdxsry.Count + " )";
            //for (int i = 0; i < App._GMainWindow.spsblx.Children.Count; i++)
            //{
            //    if (App._GMainWindow.spsblx.Children[i].GetType().Equals(typeof(RadioButton)))
            //    {
            //        RadioButton rb = App._GMainWindow.spsblx.Children[i] as RadioButton;
            //        if (rb.Name != "qb" && rb.Name != "ry" && rb.Name.Contains("rb_"))
            //        {
            //            string dxlx = rb.Name.Substring(3, rb.Name.Length - 3);

            //            ObservableCollection<DeviceClass> devices = LocalDeviceClass.Where(r => r.Grade == 2).ConvertTo();
            //            devices = devices.Where(r => r.DeviceClassID == dxlx).ConvertTo();

            //            string name = "";
            //            if (devices.Count > 0)
            //            {
            //                //ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "").Where(r => r.TKBZ == 0).ConvertTo().Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo();
            //                name = devices[0].DeviceClassName;
            //                rb.Content = name + " ( " + LocalJKDX.ConvertTo().Where(r => r.DXLX == devices[0].DeviceClassName).ConvertTo().Count + " )";
            //            }

            //        }
            //    }
            //}

            #endregion

            #region 关闭出口监视器（已作废）

            //ObservableCollection<LOCATE_APINFO> apcks = ServiceHelper.ServiceClient.GetAllAPINFO().Where(r => r.CKBZ == 1).ConvertTo();
            //for (int i = 0; i < apcks.Count; i++)
            //{
            //    string ckbh = apcks[i].CKBH;
            //    string apbh = apcks[i].APBH;
            //    ObservableCollection<LOCATE_JKDX> jkdxcks = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDXBYFloorID(App._GMainWindow.CmbKSLC.SelectedValue.ToString(), "医院人员").Where(r => r.TKBZ == 0).Where(r=>r.APBH == apbh).ConvertTo();

            //    if (jkdxcks.Count == 0)
            //    {
            //        //App._GMainWindow.mlink.MCloseAlarmDoorACT(ckbh, 1);
            //       // OpenOrCloseMlink(ckbh,0);
            //        break;
            //    }
            //    bool isexits = false;
            //    for (int j = 0; j < jkdxcks.Count; j++)
            //    {
            //        DateTime dtnow = DateTime.Now;
            //        ObservableCollection<LOCATE_WCDJ> lastdj = ServiceHelper.ServiceClient.GetLastWCDJ(jkdxcks[j].DXBH.ToString(), 1);
            //        if (lastdj.Count > 0)
            //        {
            //            DateTime dts = lastdj[0].SJS;
            //            DateTime dte = lastdj[0].SJE;
            //            if (dtnow > dte)
            //            {
            //                isexits = true;
            //                break;
            //            }
            //        }
            //    }
            //    if (!isexits)
            //    {
            //        //App._GMainWindow.mlink.MCloseAlarmDoorACT(ckbh, 1);

            //       // OpenOrCloseMlink(ckbh, 0);
            //    }
            //}
            #endregion

            //stw.Stop();
            //MessageBox.Show(stw.ElapsedMilliseconds.ToString() + "ms");
        }

        /// <summary>
        /// 删除界面上之前有的人员或资产图片
        /// </summary>
        /// <param name="source"></param>
        private void ForeachDeleteImage(UIElementCollection source)
        {

            //删除界面上之前有的人员或资产图片
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].GetType().Equals(typeof(Grid)))
                {
                    ForeachDeleteImage((source[i] as Grid).Children);
                }
                else if (source[i].GetType().Equals(typeof(Image)))
                {
                    if (((source[i] as Image).Parent).GetType().Equals(typeof(Grid)))
                        ((source[i] as Image).Parent as Grid).Children.Remove((UIElement)source[i]);

                    i--;
                }
                else if (source[i].GetType().Equals(typeof(TextBlock)))
                {
                    if (((source[i] as TextBlock).Parent).GetType().Equals(typeof(Grid)))
                        ((source[i] as TextBlock).Parent as Grid).Children.Remove((UIElement)source[i]);

                    i--;
                }
                else if (source[i].GetType().Equals(typeof(GifImage)))
                {
                    if (((source[i] as GifImage).Parent).GetType().Equals(typeof(Grid)))
                        ((source[i] as GifImage).Parent as Grid).Children.Remove((UIElement)source[i]);

                    i--;
                }
                //else if (source[i].GetType().Equals(typeof(Button)))
                //{
                //    if (((source[i] as Button).Parent).GetType().Equals(typeof(Button)))
                //        ((source[i] as Button).Parent as Grid).Children.Remove((UIElement)source[i]);
                //    i--;
                //}
                else
                {
                    continue;
                }
            }
            if (ShowButton == 1)
            {
                AsyncAddButton();
            }
        }

        /// <summary>
        /// 删除指定的控件名称，根据tag属性值来判别
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sTagID"> </param>
        /// <param name="sTagApbh"> </param>
        /// <param name="iOutFlag"> </param>
        private int ForeachDeleteImage_SelectTag(UIElementCollection source, string sTagID, string sTagApbh, int iOutFlag)
        {
            // img_tag 的格式表现为: rybh +',' + apbh （人员编号,定位器编号）
            // 下面数组中所定义的img_tag数据长度大于1时，img_tag[0] = rybh ; img_tag[1] = apbh

            // rtn_type 的类型定义：
            // 0 表示原有的图形界面上的资产或人员位置没有发生变化，不需要删除并刷新
            // >0 表示原有的图形界面上已显示有该设备，并已删除
            // 999 表示原有的图形界面上没有显示该设备，可以直接添加或无需刷新
            int rtn_type = 999;
            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].GetType().Equals(typeof(Image)))
                {
                    Image img = (Image)source[i];
                    string[] img_tag = img.Tag.ToString().Split(new char[] { ',' });
                    if (img_tag.Length <= 1) continue;
                    // tag第一项数据与人员或设备编号匹配，则继续判别位置信息，如果位置未发生改变，则除了出口预警位置之外的监控区域，无需删除
                    if (img_tag[0] == sTagID)
                    {
                        if (iOutFlag == 1 || img_tag[1] != sTagApbh)
                        {
                            if (((source[i] as Image).Parent).GetType().Equals(typeof(Grid)))
                                ((source[i] as Image).Parent as Grid).Children.Remove((UIElement)source[i]);

                            i--;
                            rtn_type = 1;
                        }
                        else
                        {
                            rtn_type = 0;
                        }
                    }
                }
                else if (source[i].GetType().Equals(typeof(TextBlock)))
                {
                    TextBlock txb = (TextBlock)source[i];
                    string[] txb_tag = txb.Tag.ToString().Split(new char[] { ',' });
                    if (txb_tag.Length <= 1) continue;
                    if (txb_tag[0] == sTagID)
                    {
                        if (iOutFlag == 1 || txb_tag[1] != sTagApbh)
                        {
                            if (((source[i] as TextBlock).Parent).GetType().Equals(typeof(Grid)))
                                ((source[i] as TextBlock).Parent as Grid).Children.Remove((UIElement)source[i]);
                            DeleteXml(txb_tag[1], int.Parse(txb_tag[0]));
                            i--;
                            rtn_type = 1;
                        }
                        else
                        {
                            rtn_type = 0;
                        }
                    }
                }
                else if (source[i].GetType().Equals(typeof(GifImage)))
                {
                    GifImage gfimg = (GifImage)source[i];
                    string[] gfimg_tag = gfimg.Tag.ToString().Split(new char[] { ',' });
                    if (gfimg_tag.Length <= 1) continue;
                    if (gfimg_tag[0] == sTagID)
                    {
                        if (iOutFlag == 1 || gfimg_tag[1] != sTagApbh)
                        {

                            if (((source[i] as GifImage).Parent).GetType().Equals(typeof(Grid)))
                                ((source[i] as GifImage).Parent as Grid).Children.Remove((UIElement)source[i]);
                            DeleteXml(gfimg_tag[1], int.Parse(gfimg_tag[0]));

                            i--;
                        }
                        else
                        {
                            rtn_type = 0;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            return rtn_type;
        }

        /// <summary>
        /// 删除显示时间超过10秒钟，且位置信息没有发生改变的监控对象的GIF提醒图片,同时替换成同名的image图片
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sTagInfo"></param>
        private void ForeachDeleteGif_UpdateImage(UIElementCollection source)
        {
            //缓存变量装载
            //LocalAPINFO = ServiceHelper.ServiceClient.GetAllAPINFO();
            //var LocalWCDJ = ServiceHelper.ServiceClient.GetAllWCDJ("");
            //var LocalALARM = ServiceHelper.ServiceClient.SelectAllAlarm("", 0);

            // img_tag 的格式表现为: rybh +',' + apbh + 图片名称 + datetime.ticks（人员编号,定位器编号，图片名称,日期时钟）
            // 下面数组中所定义的img_tag数据长度大于1时，img_tag[0] = rybh ; img_tag[1] = apbh ; img_tag[2] = 图片名称; img_tag[3] = 日期时钟

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i].GetType().Equals(typeof(GifImage)))   //gif图片处理方法（移动后头10秒）
                {
                    GifImage gfimg = (GifImage)source[i];
                    string[] gfimg_tag = gfimg.Tag.ToString().Split(new char[] { ',' });
                    if (gfimg_tag.Length <= 1) continue;
                    long nowTicks = DateTime.Now.Ticks;
                    TimeSpan timeTicks = new TimeSpan(nowTicks - long.Parse(gfimg_tag[3]));
                    if (((source[i] as GifImage).Parent).GetType().Equals(typeof(Grid)))
                    {
                        // 当Gif图片显示的时间小于参数设定的秒数时，不更换为Image图片，当GIF图片显示时间超过设定秒数时，更换为Image图片
                        if (timeTicks.TotalSeconds < _iFlashSecond)
                            continue;

                        #region 将GIF图片替换为Image图片
                        BitmapImage myBitmapImage = new BitmapImage();

                        myBitmapImage.BeginInit();
                        //myBitmapImage.UriSource = index.RYXB == "女" ? new Uri("pack://application:,,,/EOLMS;component//Resources/female.png") : new Uri
                        //("pack://application:,,,/EOLMS;component//Resources/male.png");
                        Random ran = new Random();


                        string dxbh = gfimg_tag[0];
                        string apbh = gfimg_tag[1];
                        //LOCATE_JKDX jkdx = ServiceHelper.ServiceClient.GetJKDX(int.Parse(dxbh));
                        //LOCATE_JKDX jkdx = LocalJKDX.Where(r => r.DXBH == int.Parse(dxbh)).ConvertTo()[0];

                        //myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/" + gfimg_tag[2] + ".png");
                        string tplj = "";
                        //if (jkdx != null)
                        //{
                        //    if (jkdx.TKBZ == 1)
                        //    {
                        //        ((source[i] as GifImage).Parent as Grid).Children.Remove((UIElement)source[i]);
                        //        return;
                        //    }
                        //    tplj = jkdx.TPLJ;
                        //}
                        //LOCATE_JKDX_PIC jkdxs_pic2 = null;
                        //if (jkdx.DXXZ == "医院人员")
                        //{
                        //    jkdxs_pic2 = App._JKDX_PIC.Where(r => r.DXLX == jkdx.Usage).ConvertTo()[0];
                        //}
                        //else
                        //{
                        //    jkdxs_pic2 = App._JKDX_PIC.Where(r => r.DXLX == jkdx.DXLX).ConvertTo()[0];
                        //}
                        //bool flagSignal = JudegeSignal(jkdx.RFID);
                        //getPictureName(myBitmapImage, jkdx, jkdxs_pic2, 1); //第一次加载给他默认图片
                        myBitmapImage.DecodePixelWidth = 500;
                        myBitmapImage.EndInit();
                        myBitmapImage.Freeze();

                        mag = new Image();
                        mag.MouseRightButtonDown += new MouseButtonEventHandler(right_MouseRightButtonDown);
                        mag.Source = myBitmapImage;
                        mag.Width = gfimg.Width;
                        mag.Height = gfimg.Height;
                        //图片的初始坐标
                        mag.HorizontalAlignment = HorizontalAlignment.Left;
                        mag.VerticalAlignment = VerticalAlignment.Top;
                        mag.Margin = gfimg.Margin;
                        mag.ToolTip = gfimg.ToolTip;
                        mag.Tag = gfimg_tag[0] + "," + gfimg_tag[1];


                        //将定位对象的图片添加到MainGrid对象中

                        

                        #endregion
                    }
                }
                else
                {
                    if (source[i].GetType().Equals(typeof(Image)))   //静态图片的处理方法（移动10秒后）
                    {
                        Image img = (Image)source[i];
                        string[] img_tag = img.Tag.ToString().Split(new char[] { ',' });
                        string nowTicks = DateTime.Now.Ticks.ToString();
                        GifImage gifjb = new GifImage();
                        gifjb.Width = img.Width;
                        gifjb.Height = img.Height;
                        //图片的初始坐标
                        gifjb.HorizontalAlignment = HorizontalAlignment.Left;
                        gifjb.VerticalAlignment = VerticalAlignment.Top;
                        gifjb.Margin = img.Margin;
                        gifjb.ToolTip = img.ToolTip;
                        gifjb.Name = img.Name;
                        int dxbh = int.Parse(img_tag[0]);
                        string apbh = img_tag[1];
                        

                    }
                }
            }
        }
        public void right_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string dxbh = "";
            if (sender is GifImage)
            {
                GifImage gif = sender as GifImage;
                string str = gif.Tag.ToString();
                string[] arrs = str.Split(',');
                if (arrs.Length > 0)
                {
                    dxbh = arrs[0].Trim();
                }

            }
            if (sender is Image)
            {
                Image img = sender as Image;
                string str = img.Tag.ToString();
                string[] arrs = str.Split(',');
                if (arrs.Length > 0)
                {
                    dxbh = arrs[0].Trim();
                }
            }
            if (!string.IsNullOrEmpty(dxbh))
            {
                //ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX("", "").Where(r => r.DXBH == int.Parse(dxbh)).ConvertTo();
                //LOCATE_JKDX jkdx = null;
                //if (jkdxs.Count > 0)
                //{
                //    jkdx = jkdxs[0];
                //    string dxxz = jkdx.DXXZ;
                //    if (dxxz == "医院人员")
                //    {
                //        JKDXPersonDetails jkdxdetails = new JKDXPersonDetails(jkdx);
                //        jkdxdetails.ShowDialog();
                //    }
                //    if(dxxz=="病人")
                //    {
                //        JKDXPersonDetails jkdxdetails = new JKDXPersonDetails(jkdx);
                //        jkdxdetails.ShowDialog();
                //    }
                //    if(dxxz =="医疗设备")
                //    {

                //        DevicesDetails jkdxdetails = new DevicesDetails(jkdx);
                //        jkdxdetails.ShowDialog();
                //    }
                //}
            }

        }

        /// <summary>
        /// 负责把人员和资产信息的图标放到指定的位置上
        /// </summary>
        /// <returns></returns>
        //private BitmapImage GiveALogoToView(Floor index, ObservableCollection<int> collections)
        //{
        //    BitmapImage myBitmapImage = new BitmapImage();

        //    if (View != null)
        //    {
        //        //少判断了登记时间和进入停留时间的关系，演示后再搞
        //        //取最新一条停经区域信息
        //        //ObservableCollection<LOCATE_JKKS> jkkss = LocalJKKS.ConvertTo();
        //        //LOCATE_TJQY region = ServiceHelper.ServiceClient.SelectLOCATE_TJQYByPNumberAndKs(index.DXBH, jkkss[0].KSBH, App._GMainWindow.CmbKSLC.Text.Trim());

        //        //如果当前登记人资的登记时间在最新一条停经区域进入某区域的时间之前，并且停经区域信息不为空时，才满足此条件判断
        //        //if (region != null && index.LKSJ < region.JRSJ)
        //        //if (region != null)
        //        //{
        //        //    if (region.APBH != null)
        //        //    {
        //        //        if (index.LKBZ == 1)
        //        //        {
        //        //            // 同时删除图形界面上的图示信息
        //        //            ForeachDeleteImage_SelectTag(View.MainGrid.Children, region.GLBH.ToString(), region.APBH, 1);
        //        //        }
        //        //        else
        //        //        {
        //        //            // 往图形界面写入定位图标时，先判别，位置是否发生过变化，如果未发生变化，则刷新图示
        //        //            int rtn_Type = 0;
        //        //            rtn_Type = ForeachDeleteImage_SelectTag(View.MainGrid.Children, region.GLBH.ToString(), region.APBH, 0);

        //        //            if (rtn_Type == 1 || rtn_Type == 999)
        //        //            {
        //        //                if (_iFlashSecond == 0)
        //        //                {
        //        //                    //向界面某一位置插入图片
        //        //                    myBitmapImage = InsertWhere(index, region);
        //        //                }
        //        //                else
        //        //                {
        //        //                    //向界面某一位置插入动态图片
        //        //                    InsertGifWhere(index, region);
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //    }

        //    return myBitmapImage;
        //}

        /// <summary>
        /// 显示最近10秒内位置发生改变的监控对象的GIF提醒图片
        /// </summary>
        /// <param name="index"></param>
        /// <param name="region"></param>
        //private void InsertGifWhere(Floor index, Floor region)
        //{
        //    if (ShowModel == 1)
        //    {
        //        #region 显示最近10秒内位置发生改变的监控对象的GIF提醒图片

        //        string gifSourceName = string.Empty;
        //        //获取AP坐标
        //        //LOCATE_APINFO apInfo = ServiceHelper.ServiceClient.GetAPINFOByAPNumber(region.APBH, _iScreenMode);
        //        //LOCATE_APINFO apInfo = new LOCATE_APINFO();
        //        //ObservableCollection<LOCATE_JKKS> jkkss = LocalJKKS.ConvertTo();
        //        //ObservableCollection<LOCATE_APINFO> aps = ServiceHelper.ServiceClient.GetAPINFOByKSLC(_iScreenMode, jkkss[0].KSBH, "");
        //        //ObservableCollection<LOCATE_APINFO> ap = aps.Where(r => r.APBH == region.APBH).ConvertTo();
        //        //ObservableCollection<LOCATE_APINFO> preaps = null;
        //        //if (ap.Count > 0)
        //        //{
        //        //    if (!string.IsNullOrEmpty(ap[0].PREAPBH))
        //        //    {
        //        //        preaps = aps.Where(r => r.APBH == ap[0].PREAPBH).ConvertTo();
        //        //    }
        //        //    else
        //        //    {
        //        //        foreach (LOCATE_APINFO tmp in aps)
        //        //        {
        //        //            if (tmp.APBH == region.APBH)
        //        //            {
        //        //                apInfo = tmp;
        //        //                break;
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        //if (preaps != null)
        //        //{
        //        //    if (preaps.Count > 0)
        //        //    {
        //        //        apInfo = preaps[0];
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    foreach (LOCATE_APINFO tmp in aps)
        //        //    {
        //        //        if (tmp.APBH == region.APBH)
        //        //        {
        //        //            apInfo = tmp;
        //        //            break;
        //        //        }
        //        //    }
        //        //}
        //        XmlNode xn = GetMinRoom("");
        //        if (xn != null)
        //        {
        //            //System.Threading.Thread.Sleep(200);
        //            Random ran = new Random();
        //            int widthnum = int.Parse(xn.Attributes["widthnum"].Value.ToString());
        //            int heightnum = int.Parse(xn.Attributes["heightnum"].Value.ToString());
        //            //UpXml(xn, apInfo.APBH, index.DXBH);
        //            int g = int.Parse(intROOM.ToString());
        //            //int chang = (apInfo.XPOS_E - apInfo.XPOS_S) / g;
        //            //int kuan = (apInfo.YPOS_E - apInfo.YPOS_S) / g;
        //            //int width = ran.Next(apInfo.XPOS_S + chang * (widthnum - 1), apInfo.XPOS_S + chang * widthnum);
        //            //int height = ran.Next(apInfo.YPOS_S + kuan * (heightnum - 1), apInfo.YPOS_S + kuan * heightnum);

        //            //从APINFO中取监控对象
        //            string ExePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\APInfo.xml";
        //            XmlNode jkdx = null;
        //            try
        //            {
        //                // 1.创建一个XmlDocument类的对象
        //                XmlDocument doc = new XmlDocument();

        //                // 2.把你想要读取的xml文档加载进来
        //                doc.Load(ExePath);

        //                XmlNodeList xnl = doc.SelectSingleNode("Root").ChildNodes;
        //                foreach (XmlNode xn2 in xnl)
        //                {
        //                    if (xn2.Attributes["id"].Value.Equals("123"))
        //                    {
        //                        for (int i = 0; i < xn2.ChildNodes.Count; i++)
        //                        {
        //                            for (int j = 0; j < xn2.ChildNodes[i].ChildNodes.Count; j++)
        //                                if (xn2.ChildNodes[i].ChildNodes[j].Attributes["value"].Value == "234")
        //                                {
        //                                    jkdx = xn2.ChildNodes[i].ChildNodes[j];
        //                                    break;
        //                                }
        //                        }
        //                    }

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("配置文件异常！");
        //            }




        //            int width = int.Parse(jkdx.Attributes["xpoint"].Value);
        //            int height = int.Parse(jkdx.Attributes["ypoint"].Value);
        //            GifImage gifimage = new GifImage();

        //            gifimage.MouseMove += new MouseEventHandler(gifimage_MouseMove);
        //            gifimage.Margin = new Thickness(width, height, 0, 0);




                    


        //            //图片的初始坐标
        //            string nowTicks = DateTime.Now.Ticks.ToString();
        //            TextBlock block = new TextBlock();
        //            block.MouseDown += new MouseButtonEventHandler(block_MouseDown);
        //            //block.Name = "gifb" + index.DXBH.ToString();
        //            if (ShowTitle == 1)
        //            {
        //                //block.Text = index.DXMC;
        //            }
        //            //if (flag)//禁止入显示名称
        //            //{
        //            //    block.Text = index.DXMC;
        //            //}
        //            //if (yjbdck != null)//禁止出显示名称
        //            //{
        //            //    if (region.APBH != yjbdck.APBH)
        //            //    {
        //            //        block.Text = index.DXMC;
        //            //    }
        //            //}
        //            //if (index.HJBZ == 1)//呼叫显示名称
        //            //{
        //            //    block.Text = index.DXMC;
        //            //}
        //            block.Margin = new Thickness(width - 5, height - 15, 0, 0);
        //            block.FontSize = 10;
        //            //block.Tag = index.DXBH.ToString() + "," + region.APBH + "," + gifSourceName + "," + nowTicks;
        //            block.Foreground = Brushes.Navy;
        //            block.FontWeight = FontWeights.Bold;
        //            gifimage.HorizontalAlignment = HorizontalAlignment.Left;
        //            gifimage.VerticalAlignment = VerticalAlignment.Top;
        //            //gifimage.ToolTip = string.Format("名称：{0}\r\n型号：{1}\r\n备注：{2}", index.DXMC, index.ZWXH, index.BZXX);

        //            //if (index.DXXZ == "医院人员" || index.DXXZ == "参观人员")
        //            //{
        //            //    gifimage.ToolTip = string.Format("编号：{0}\r\n名称：{1}\r\nRFID：{2}\r\n科室：{3}", index.DXGH, index.DXMC, index.RFID, index.KSMC);
        //            //}
        //            //if (index.DXXZ == "病人")
        //            //{
        //            //    gifimage.ToolTip = string.Format("病例号：{0}\r\n名称：{1}\r\n性别：{2}\r\nRFID：{3}", index.DXGH, index.DXMC, index.Usage, index.RFID);
        //            //}
        //            //if (index.DXXZ == "医疗设备")
        //            //{
        //            //    string ztms = "";
        //            //    switch (index.DXFL)
        //            //    {
        //            //        case 1:
        //            //            ztms = "在用";
        //            //            break;
        //            //        case 2:
        //            //            ztms = "维修";
        //            //            break;
        //            //        case 3:
        //            //            ztms = "报废";
        //            //            break;

        //            //    }
        //            //    gifimage.ToolTip = string.Format("编号：{0}\r\n名称：{1}\r\n状态：{2}\r\nRFID：{3}\r\n科室：{4}", index.DXGH, index.DXMC, ztms, index.RFID, index.KSMC);
        //            //}
        //            //gifimage.ToolTip = string.Format("编号：{0}\r\n名称：{1}\r\nRFID：{2}", index.Number, index.DXMC, index.RFID);

        //            //gifimage.Tag = index.DXBH + "," + region.APBH + "," + gifSourceName + "," + nowTicks;
        //            //gifimage.Name = "gifg" + index.DXBH.ToString();
        //            //那个块，就添加到那个里面去

        //            View.MainGrid.Children.Add(gifimage);

        //            View.MainGrid.Children.Add(block);

        //            //新区域Debug模式下所有时间戳记录
        //            //AndrewChien 2014-03-18 10:27
        //            //string[] rectime = region.DebugTime.Split('*');
        //            //WriteData("\r\n------Debug时间集：中间件时间戳：" + rectime[0] + "监听时间戳：" + rectime[1] + "分发时间戳：" + rectime[2] +
        //            //          " 显示时间戳：" + DateTime.Now + "\r\n");

        //            ////展厅程序需要设置每个定位区域都发送短信
        //            //if (index.DXXZ == "医院人员")
        //            //{
        //            //    //发送短信
        //            //    if (App._PrintReport == 1)
        //            //    {
        //            //        string smsContent = "尊敬的" + index.DXMC + "您好，欢迎进入杭州医惠物联网应用体验区" + index.TJQY + "！";

        //            //        if (smsint.StartSms())
        //            //        {
        //            //            if (string.IsNullOrEmpty(region.TLSJ))
        //            //            {
        //            //                if (region.XXFS == 0)
        //            //                {
        //            //                    smsint.SendSms(smsContent, index.LXFS);
        //            //                    //更新发送标志，以免重复发送
        //            //                    ServiceHelper.ServiceClient.UpdateTjqy(region.iCount);
        //            //                }

        //            //            }
        //            //        }
        //            //    }
        //            //}

        //            #region 提醒临时处理
        //            //  EventHandleTest(index,region);
        //            //ObservableCollection<LOCATE_Alert> alerts = ServiceHelper.ServiceClient.GetAllAlert().Where(r => r.ID == 2).ConvertTo();
        //            if (true)
        //            {
        //                //发送短信
        //                if (App._PrintReport == 1)
        //                {
        //                }
        //            }
        //            #endregion

        //        }

        //        //WriteData(index.DXMC + "  " + index.TJQY + "   " + index.DXBH + " 显示时间：" + DateTime.Now.ToString() + "\r\n");
        //        #endregion
        //    }
        //}

        //提醒消息处理
        public void EventHandle(string sjlx)
        {
            //ObservableCollection<LOCATE_AlertRelation> alerts = null;
            #region 移动到新地点
            if (sjlx == "")
            {
                //foreach (LOCATE_AlertRelation alert in alerts)
                //{
                //    int id = alert.ID;
                //    //语音提醒
                //    if (id == 1)
                //    {
                //        if (RemindLock)
                //        {
                //            Voice.Speak(alert.Path, SpFlags);
                //        }
                //    }
                //    ////短信提醒
                //    //if (id == 2)
                //    //{
                //    //    if (App._PrintReport == 1)
                //    //    {
                //    //        string smsContent = alert.Path;

                //    //        if (smsint.StartSms())
                //    //        {
                //    //            if (string.IsNullOrEmpty(region.TLSJ))
                //    //            {
                //    //                if (region.XXFS == 0)
                //    //                {
                //    //                    smsint.SendSms(smsContent, index.LXFS);
                //    //                    //更新发送标志，以免重复发送
                //    //                    ServiceHelper.ServiceClient.UpdateTjqy(region.iCount);
                //    //                }

                //    //            }
                //    //        }
                //    //    }
                //    //}

                //    //图片提醒
                //    if (id == 3)
                //    {
                //    }
                //}
            }
            #endregion
        }

        #region 临时
        void TimerRemindSecond_Tick(object sender, EventArgs e)
        {

            //??????
            //Voice.Speak(scontent, SpFlags);
            //Voice.WaitUntilDone(-1);
            
            //Thread.Sleep(1500);
        }

        //提醒消息临时处理方法
        public void EventHandleTest()
        {
            //ObservableCollection<LOCATE_AlertRelation> alerts = ServiceHelper.ServiceClient.GetAllAlertRelation().Where(r => r.LXBH == "001").ConvertTo();
            //foreach (LOCATE_AlertRelation alert in alerts)
            //{
            //    int id = alert.Aid;
            //    //语音提醒
            //    if (id == 1)
            //    {
            //        if (RemindLock)
            //        {
            //            Voice.Speak(alert.Path, SpFlags);
            //        }
            //    }
            //    ////短信提醒
            //    //if (id == 2)
            //    //{
            //    //    if (App._PrintReport == 1)
            //    //    {
            //    //        string smsContent = alert.Path;

            //    //        if (smsint.StartSms())
            //    //        {
            //    //            if (string.IsNullOrEmpty(region.TLSJ))
            //    //            {
            //    //                if (region.XXFS == 0)
            //    //                {
            //    //                    smsint.SendSms(smsContent, index.LXFS);
            //    //                    //更新发送标志，以免重复发送
            //    //                    ServiceHelper.ServiceClient.UpdateTjqy(region.iCount);
            //    //                }

            //    //            }
            //    //        }
            //    //    }
            //    //}

            //    //图片提醒
            //    if (id == 3)
            //    {
            //    }
            //}

        }

        #endregion
        public void block_MouseDown(object obj, MouseEventArgs arg)
        {

        }
        private void gifimage_MouseMove(object sender, MouseEventArgs arg)
        {

        }
        /// <summary>
        /// 向主界面动态写入定位对象
        /// </summary>
        /// <param name="index"></param>
        /// <param name="region"></param>
        /// <param name="collections"></param>
        /// <param name="ran"></param>
        private BitmapImage InsertWhere()
        {

            //获取AP坐标
            //LOCATE_APINFO apInfo = ServiceHelper.ServiceClient.GetAPINFOByAPNumber(region.APBH, _iScreenMode);

            BitmapImage myBitmapImage = new BitmapImage();


            myBitmapImage.BeginInit();
            //myBitmapImage.UriSource = index.RYXB == "女" ? new Uri("pack://application:,,,/EOLMS;component//Resources/female.png") : new Uri("pack://application:,,,/EOLMS;component//Resources/male.png");
            if (true)
            {
                //switch (index.FloorName)
                //{
                //    case "女":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/female.png");
                //        myBitmapImage.DecodePixelWidth = 16;
                //        myBitmapImage.DecodePixelHeight = 50;
                //        break;
                //    case "男":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/male.png");
                //        myBitmapImage.DecodePixelWidth = 24;
                //        myBitmapImage.DecodePixelHeight = 50;
                //        break;
                //    case "呼吸机":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/hxj.png");
                //        myBitmapImage.DecodePixelWidth = 20;
                //        myBitmapImage.DecodePixelHeight = 28;
                //        break;
                //    case "微量泵":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/wlb.png");
                //        myBitmapImage.DecodePixelWidth = 35;
                //        myBitmapImage.DecodePixelHeight = 16;
                //        break;
                //    case "监护仪":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/jhy.png");
                //        myBitmapImage.DecodePixelWidth = 18;
                //        myBitmapImage.DecodePixelHeight = 16;
                //        break;
                //    case "查房设备":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/cfsb.png");
                //        myBitmapImage.DecodePixelWidth = 20;
                //        myBitmapImage.DecodePixelHeight = 30;
                //        break;
                //    case "EDA":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/eda.png");
                //        myBitmapImage.DecodePixelWidth = 16;
                //        myBitmapImage.DecodePixelHeight = 18;
                //        break;
                //    case "垃圾车":
                //        myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/垃圾车.png");
                //        myBitmapImage.DecodePixelWidth = 24;
                //        myBitmapImage.DecodePixelHeight = 40;

                //        break;
                //}
            }
            else
            {
                myBitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/image/jb.png");
                myBitmapImage.DecodePixelWidth = 24;
                myBitmapImage.DecodePixelHeight = 50;
            }
            myBitmapImage.DecodePixelWidth = 500;
            myBitmapImage.EndInit();
            myBitmapImage.Freeze();

            mag = new Image();
            mag.Source = myBitmapImage;



            //图片的初始坐标
            mag.HorizontalAlignment = HorizontalAlignment.Left;
            mag.VerticalAlignment = VerticalAlignment.Top;

            Random ran = new Random();
            //int width = ran.Next(apInfo.XPOS_S, apInfo.XPOS_E);
            //int height = ran.Next(apInfo.YPOS_S, apInfo.YPOS_E);
            TextBlock block = new TextBlock();

            if (ShowTitle == 1)
            {
                //block.Text = index.DXMC;
            }
            //block.Margin = new Thickness(width - 5, height - 10, 0, 0);
            //block.Margin = new Thickness(width - 5, height - 15, 0, 0);
            //block.Tag = index.DXBH.ToString() + "," + region.APBH;
            block.Foreground = Brushes.White;
            //mag.Margin = new Thickness(width, height, 0, 0);
            //mag.ToolTip = string.Format("名称：{0}\r\n型号：{1}\r\n备注：{2}", index.DXMC, index.ZWXH, index.BZXX);
            //mag.ToolTip = string.Format("编号：{0}\r\n名称：{1}\r\nRFID：{2}", index.Number, index.DXMC, index.RFID);
            //mag.Tag = index.DXBH + "," + region.APBH;

            //将定位对象的图片添加到MainGrid对象中
            View.MainGrid.Children.Add(block);
            View.MainGrid.Children.Add(mag);

            return myBitmapImage;
        }

        /// <summary>
        /// 判断是否有相同坐标已经使用，以此来避免定位重叠
        /// </summary>
        /// <param name="collections"></param>
        /// <param name="width"></param>
        /// <param name="actualWidth"> </param>
        /// <returns></returns>
        private int CheckRepeatValue(IEnumerable<int> collections, int width, int actualWidth)
        {
            var v = collections.Where(c => width > c - 5 && width < c + 5).FirstOrDefault();
            if (v > 0)
            {
                var r = new Random();
                return CheckRepeatValue(collections, r.Next(Math.Abs(actualWidth - 30)), actualWidth);
            }
            else
            {
                return width;
            }
        }

        #endregion

        #region Command

        #endregion

        #region Property

        /// <summary>
        /// 所有未退卡的人员清单
        /// </summary>
        private ObservableCollection<AcquisitionBaseData> _PersonalInfoCollection;
        public ObservableCollection<AcquisitionBaseData> PersonalInfoCollection
        {
            get
            {
                if (_PersonalInfoCollection == null)
                {
                    _PersonalInfoCollection = new ObservableCollection<AcquisitionBaseData>();
                    //_PersonalInfoCollection = ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX(App._GYhxx.KSMC, "").Where(r => r.TKBZ == 0).ConvertTo();//查询所有领卡的人员信息
                }
                return _PersonalInfoCollection;
            }
            set
            {
                base.SetValue(ref _PersonalInfoCollection, value, () => this.PersonalInfoCollection);
            }
        }

        /// <summary>
        /// 所有登记在册的当前科室的AP信息
        /// </summary>
        private ObservableCollection<AcquisitionBaseData> _APInfoCollection;
        public ObservableCollection<AcquisitionBaseData> APInfoCollection
        {
            get
            {
                if (_APInfoCollection == null)
                {
                    _APInfoCollection = new ObservableCollection<AcquisitionBaseData>();

                    //_APInfoCollection = ServiceHelper.ServiceClient.GetAPINFOByKSLC(_iScreenMode, App._GYhxx.SSKS, "");
                }
                return _APInfoCollection;
            }
            set
            {
                base.SetValue(ref _APInfoCollection, value, () => this.APInfoCollection);
            }
        }

        /// <summary>
        /// 属于本科室的所有设备的出口预警列表
        /// </summary>
        private ObservableCollection<AcquisitionBaseData> _SbckjyCollection;
        public ObservableCollection<AcquisitionBaseData> SbckjyCollection
        {
            get
            {
                if (_SbckjyCollection == null)
                {
                    _SbckjyCollection = new ObservableCollection<AcquisitionBaseData>();
                    //ObservableCollection<Floor> jkkss = LocalJKKS.ConvertTo();
                    //LOCATE_TJQY region = ServiceHelper.ServiceClient.SelectLOCATE_TJQYByPNumberAndKs(index.DXBH, jkkss[0].KSBH, App._GMainWindow.CmbKSLC.Text.Trim());
                    //_SbckjyCollection = ServiceHelper.ServiceClient.SelectALLJYSBCkjyList(jkkss[0].KSMC).Where(r => r.TKBZ == 0).ConvertTo();//查询所有未退卡的设备出口预警信息
                }
                return _SbckjyCollection;
            }
            set
            {
                base.SetValue(ref _SbckjyCollection, value, () => this.SbckjyCollection);
            }
        }

        #endregion

        public void MainPageView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<PersonCall> personcall = ServiceHelper.ServiceClient.GetPersonCall(0);
            //ObservableCollection<LOCATE_JKDX> jkdxs = ServiceHelper.ServiceClient.SelectJBLOCATE_JKDX("");
            //for (int i = 0; i < jkdxs.Count; i++)
            //{
            //    PersonCall p = new PersonCall();
            //    p.DXLX = "出口报警";
            //    personcall.Add(p);
            //}
            //if (personcall.Count > 0)
            //{
            //    CallSelect CallSel = new CallSelect(personcall);
            //    CallSel.ShowDialog();
            //}
        }

    }
}
