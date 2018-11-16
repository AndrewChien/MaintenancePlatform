using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZNC.Component.Controls
{
    /// <summary>
    /// PagedTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class PagedTextBox : UserControl
    {
        IntPtr ActiveWindowHandle;  //定义活动窗体的句柄 
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();  //获得父窗体句柄  

        private ObservableCollection<AutoCompleteEntry> autoCompletionList;
        private int total;
        private int Position = 0;
        private string value = "";

        public PagedTextBox()
        {
            ////若owner赋予值为null（假设为null则自动识别）  
            //if (Owner == null)
            //{
            //    ActiveWindowHandle = GetActiveWindow();  //获取父窗体句柄  
            //    WindowInteropHelper helper = new WindowInteropHelper(this);
            //    helper.Owner = ActiveWindowHandle;
            //}
            //else
            //{
            //    this.Owner = owner;
            //} 
            InitializeComponent();
            autoCompletionList = new ObservableCollection<AutoCompleteEntry>();
            this.KeyUp += new KeyEventHandler(uc_KeyUp);
            txtUc.KeyDown += new KeyEventHandler(textBox_KeyDown);
            dgUc.MouseDoubleClick += new MouseButtonEventHandler(dgUc_MouseDoubleClick);//鼠标双击行
            dgUc.MouseDown += new MouseButtonEventHandler(dgUc_MouseDown);
            AutoReLocation();
        }

        /// <summary>
        /// 文本框靠右边自动调整下拉框右对齐
        /// </summary>
        private void AutoReLocation()
        {
            if (true)
            {
                //MessageBox.Show("控件："+(dgUc.PointFromScreen(new Point(0, 0)).X + dgUc.Width).ToString());
                //MessageBox.Show("显示器宽度：" + SystemParameters.WorkArea.Width);
            }
        }

        void dgUc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Position = dgUc.Items.CurrentPosition;
        }

        void dgUc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgUc.SelectedItems.Count > 0)
            {
                try
                {
                    var a = (AutoCompleteEntry)dgUc.SelectedItem;
                    txtUc.Text = a.DisplayName;
                    value = a.KeywordStrings[0];
                    dgUc.Visibility = Visibility.Collapsed;
                    //dpUc.Visibility = Visibility.Collapsed;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 文本框值
        /// </summary>
        public string Text
        {
            get { return txtUc.Text; }
            set
            {
                txtUc.Text = value;
            }
        }

        /// <summary>
        /// 左列标题
        /// </summary>
        public string Title1
        {
            get { return title1.Header.ToString(); }
            set
            {
                title1.Header = value;
            }
        }

        /// <summary>
        /// 右列标题
        /// </summary>
        public string Title2
        {
            get { return title2.Header.ToString(); }
            set
            {
                title2.Header = value;
            }
        }

        /// <summary>
        /// 控件的值
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        /// <summary>
        /// 文本框宽度
        /// </summary>
        public new double Width
        {
            get { return txtUc.Width; }
            set
            {
                txtUc.Width = value;
            }
        }

        public int Total
        {
            get { return total; }
            set
            {
                //dpUc.Total = value;
                total = value;
            }
        }

        public void AddItem(AutoCompleteEntry entry)
        {
            autoCompletionList.Add(entry);
        }

        public ObservableCollection<AutoCompleteEntry> Source
        {
            get { return autoCompletionList; }
            set
            {
                dgUc.ItemsSource = value;
                autoCompletionList = value;
            }
        }

        private void txtUc_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txtUc.Text != "")
            //{
            //    dgUc.Visibility = Visibility.Visible;
            //    //dpUc.Visibility = Visibility.Visible;
            //    //更新数据源
            //    try
            //    {
            //        //若为科室做下拉，需添加拼音码做识别
            //        if (Source.GetType()==typeof(LOCATE_JKKS))
            //        {
            //            var newsource =
            //                autoCompletionList.Where(
            //                    r =>
            //                    r.KeywordStrings[0].Contains(txtUc.Text.Trim()) || r.DisplayName.Contains(txtUc.Text.Trim()))
            //                    .ConvertTo();
            //            //以下添加拼音码识别


            //            dgUc.ItemsSource = newsource;
            //            total = dpUc.Total = newsource.Count;
            //        }
            //        else  //其他情况
            //        {
            //            var newsource =
            //                autoCompletionList.Where(
            //                    r =>
            //                    r.KeywordStrings[0].Contains(txtUc.Text.Trim()) || r.DisplayName.Contains(txtUc.Text.Trim()))
            //                    .ConvertTo();
            //            dgUc.ItemsSource = newsource;
            //            total = dpUc.Total = newsource.Count;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
            //    dgUc.Visibility = Visibility.Collapsed;
            //    dpUc.Visibility = Visibility.Collapsed;
            //}
            Query(10, 1);
        }

        //焦点消失
        private void txtUc_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (dgUc.CaptureMouse() || dpUc.CaptureMouse())
            //{
            //    return;
            //}
            //dgUc.Visibility = Visibility.Collapsed;
            //dpUc.Visibility = Visibility.Collapsed;
        }

        public void textBox_KeyDown(object sender, KeyEventArgs e)
        {

        } 

        public void uc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Enter)
            {
                return;
            }
            //按向下的箭头时
            if (e.Key == Key.Down && dgUc.Visibility == Visibility.Visible && dgUc.SelectedItems.Count == 0)
            {
                dgUc.SelectedItem = dgUc.Items[0];
                Position = 0;
                return;
            }
            //按向下的箭头时
            if (e.Key == Key.Down && dgUc.Visibility == Visibility.Visible && dgUc.SelectedItems.Count > 0)
            {
                try
                {
                    dgUc.SelectedItem = dgUc.Items[Position + 1];
                    Position++;
                }
                catch
                {
                }
            }
            if (e.Key == Key.Up && dgUc.Visibility == Visibility.Visible && dgUc.SelectedItems.Count > 0)
            {
                try
                {
                    dgUc.SelectedItem = dgUc.Items[Position - 1];
                    Position--;
                }
                catch
                {
                }
            }
            if (e.Key == Key.Enter && dgUc.Visibility == Visibility.Visible)
            {
                try
                {
                    var a = (AutoCompleteEntry)dgUc.SelectedItem;
                    txtUc.Text = a.DisplayName;
                    value = a.KeywordStrings[0];
                    dgUc.Visibility = Visibility.Collapsed;
                    //dpUc.Visibility = Visibility.Collapsed;
                }
                catch
                {
                }
            }
        }

        private void dataPager_PageChanged(object sender, PageChangedEventArgs args)
        {
            Query(args.PageSize, args.PageIndex);
        }

        public void Query(int size, int pageIndex)
        {
            //dpUc.Total = total;
            //try
            //{
            //    if (txtUc.Text != "")
            //    {
            //        dgUc.ItemsSource = autoCompletionList.Where(r => r.KeywordStrings[0].Contains(txtUc.Text.Trim())).ConvertTo().Skip((pageIndex - 1) * size).Take(size).ToList();
            //    }
            //    else
            //    {
            //        dgUc.ItemsSource = autoCompletionList.Skip((pageIndex - 1) * size).Take(size).ToList();
            //    }
            //}
            //catch
            //{
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dgUc.Visibility = Visibility.Collapsed;
            //dpUc.Visibility = Visibility.Collapsed;
            title1.Header = "编号";
            title2.Header = "名称";
        }
    }
}
