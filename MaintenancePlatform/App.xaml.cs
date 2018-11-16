using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using MaintenancePlatform.Print;
using ZNC.DataEntiry;

namespace MaintenancePlatform
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region 全局变量

        // 主界面窗体对象
        public static MainWindow _GMainWindow;
        //public static FullMainWindowView _FullMainWindowView;
        // 登录用户信息
        //public static LOCATE_YHGL _GYhxx = new LOCATE_YHGL();
        // 登录用户所在科室对应用的楼层信息
        //public static ObservableCollection<LOCATE_KSLC> _jkkslcDataInfo;
        //新版本用户所在科室对应用的楼层信息
        //public static ObservableCollection<Floor> _Floor;
        //保存用户功能修改
        public static Hashtable htyhgn = new Hashtable();
        // 是否需要打印标签
        public static int _PrintBarcode = 0;  // 0 表示不打印条码；1 表示打印参观人员条码
        public static int _PrintDoubleBarcode = 0;   // 0 表示不打印双联条码；1 表示打印单联条码
        //是否启用短信提醒
        public static int _PrintReport = 0;  //表示启用短信提醒功能 :0 表示不启用短信提醒功能;1相反
        public static int _SMSRun = 0;   // 表示启用参观区域延时短信提醒功能 :0 表示不启用参观区域延时短信提醒功能;1相反
        //加载图片模式
        //public static ObservableCollection<LOCATE_JKDX_PIC> _JKDX_PIC;

        public static string DXLX="";
       
        public static int ImgPattern = 1;

        //全屏显示
        public static NavigationWindow window = null;
        //关系映射全局变量
        public static string RelationMapConn = "";
        //关系映射全局变量
        public static string RelationMapDBType = "";
        #endregion

        public App()
        {
            this.InitializeComponent();

            //程序意外关闭，捕获异常
            ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            //以下全局抓错误
            App.Current.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Current_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message);
            e.Handled = true;
            //UIHelper.WriteLog(e.Exception.Message);
            Log.CreateLog(e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MessageBox.Show((e.ExceptionObject as Exception).Message);
            //UIHelper.WriteLog((e.ExceptionObject as Exception).Message);
            Log.CreateLog(e.ExceptionObject as Exception);
        }

        #region 建立一个消息队列，立即响应
        private static readonly DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        public static void DoEvents()
        {
            var nestedFrame = new DispatcherFrame();
            var exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, exitFrameCallback, nestedFrame);
            Dispatcher.PushFrame(nestedFrame);
            if (exitOperation.Status != DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }
        private static object ExitFrame(object state)
        {
            var frame = state as DispatcherFrame;
            if (frame != null) frame.Continue = false;
            return null;
        }
        #endregion
    }
}
