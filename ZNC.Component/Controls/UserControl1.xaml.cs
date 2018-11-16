using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ZNC.Component.Helper;

namespace ZNC.Component.Controls
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>    
    public partial class UserControl1 : Canvas
    {
        #region Members
        private int total;
        //private ObservableCollection<AutoCompleteEntry> source2;
        private VisualCollection controls;
        private ObservableCollection<AutoCompleteEntry> autoCompletionList;
        private System.Timers.Timer keypressTimer;
        private delegate void TextChangedCallback();
        private bool insertText;
        private int delayTime;
        private int searchThreshold;
        private readonly TextBox txtUc;
        private readonly DataGrid dgUc;
        private readonly DataPager dpUc;
        #endregion

        #region Constructor
        public UserControl1()
        {
            controls = new VisualCollection(this);
            InitializeComponent();

            autoCompletionList = new ObservableCollection<AutoCompleteEntry>();
            searchThreshold = 0;        // default threshold to 2 char
            delayTime = 100;

            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);

            txtUc = new TextBox();
            //txtUc.Height = 25;
            //txtUc.Width = 120;
            txtUc.GotFocus += new RoutedEventHandler(textBox_GotFocus);
            txtUc.KeyUp += new KeyEventHandler(textBox_KeyUp);
            txtUc.KeyDown += new KeyEventHandler(textBox_KeyDown);
            txtUc.VerticalContentAlignment = VerticalAlignment.Center;
            //txtUc.VerticalContentAlignment = VerticalAlignment.Top;
            //txtUc.HorizontalAlignment = HorizontalAlignment.Left;
            txtUc.TextChanged += new TextChangedEventHandler(txtUc_TextChanged);
            txtUc.LostFocus += new RoutedEventHandler(txtUc_LostFocus);
          

            dgUc = new DataGrid();
            dgUc.IsSynchronizedWithCurrentItem = true;
            dgUc.IsTabStop = false;
            Panel.SetZIndex(dgUc, -1);
            //dgUc.SelectionChanged += new SelectionChangedEventHandler(dg_SelectionChanged);
            dgUc.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgUc.IsReadOnly = true;
           // dgUc.VerticalAlignment = VerticalAlignment.Top;
            dgUc.Visibility = Visibility.Collapsed;
            //dgUc.Visibility = Visibility.Visible;
            //dgUc.Margin = new Thickness(0, 25, 3, 0);
            

            dpUc = new DataPager();
            //dpUc.VerticalAlignment = VerticalAlignment.Top;
           // dpUc.Height = 25;
            dpUc.PageSizeList = "10,20,30";
            Panel.SetZIndex(dpUc, -1);
            dpUc.IsTabStop = false;
       
            dpUc.Visibility = Visibility.Collapsed;
            //dpUc.Visibility = Visibility.Visible;
            //dpUc.Margin = new Thickness(1, 255, 4, 0);
            dpUc.PageChanged += new DataPager.PageChangedEventHandler(dataPager_PageChanged);

            controls.Add(dpUc);
            controls.Add(dgUc);
            controls.Add(txtUc);
          
        }
        #endregion

        #region Methods

        public string Text
        {
            get { return txtUc.Text; }
            set
            {
                insertText = true;
                txtUc.Text = value;
            }
        }

        public int DelayTime
        {
            get { return delayTime; }
            set { delayTime = value; }
        }

        public int Threshold
        {
            get { return searchThreshold; }
            set { searchThreshold = value; }
        }

        public void AddItem(AutoCompleteEntry entry)
        {
            autoCompletionList.Add(entry);
        }

        private void txtUc_TextChanged(object sender, TextChangedEventArgs e)
        {
            //// text was not typed, do nothing and consume the flag
            //if (insertText == true) insertText = false;

            //// if the delay time is set, delay handling of text changed
            //else
            //{
            //    if (delayTime > 0)
            //    {
            //        keypressTimer.Interval = delayTime;
            //        keypressTimer.Start();
            //    }
            //    else TextChanged();
            //}
            TextChanged();
        }

        //焦点消失
        private void txtUc_LostFocus(object sender, RoutedEventArgs e)   //todo:仍不完美，焦点移除该控件后不能使之消失
        {
            if (dgUc.CaptureMouse() || dpUc.CaptureMouse())
            {
                return;
            }
            dgUc.Visibility = Visibility.Collapsed;
            dpUc.Visibility = Visibility.Collapsed;
        }

        private void dataPager_PageChanged(object sender, PageChangedEventArgs args)
        {
            Query(args.PageSize, args.PageIndex);
        }

        public void Query(int size, int pageIndex)
        {
            dpUc.Total = total;
            dgUc.ItemsSource = autoCompletionList.Skip((pageIndex - 1) * size).Take(size).ToList();

        }
        
        private void TextChanged()
        {
            if (txtUc.Text != "")
            {
                dgUc.Visibility = Visibility.Visible;
                dpUc.Visibility = Visibility.Visible;
                //更新数据源
                dgUc.ItemsSource = autoCompletionList.Where(r => r.DisplayName.Contains(txtUc.Text.Trim())).ConvertTo();

                total = dpUc.Total = autoCompletionList.Where(r => r.DisplayName.Contains(txtUc.Text.Trim())).ConvertTo().Count;
            }
            else
            {
                dgUc.Visibility = Visibility.Collapsed;
                dpUc.Visibility = Visibility.Collapsed;
            }
            Query(10, 1);
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(this.TextChanged));
        }

        //获得焦点时
        public void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        public void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUc.IsInputMethodEnabled == true)
            {
                //comboBox.IsDropDownOpen = false;
            }
        }
        //按向下的箭头时
        public void textBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            txtUc.Arrange(new Rect(arrangeSize));
            dgUc.Arrange(new Rect(arrangeSize));
            dpUc.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return controls[index];
        }

        protected override int VisualChildrenCount
        {
            get { return controls.Count; }
        }
        #endregion


    }
}