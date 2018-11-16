using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace ZNC.Component.Controls
{
    public class AlarmWindow : Popup, INotifyPropertyChanged
    {
        DispatcherTimer ShowTimer = new DispatcherTimer();

        #region property
        public static readonly DependencyProperty AlarmMessageProperty = DependencyProperty.Register("AlarmMessage", typeof(string), typeof(AlarmWindow), new PropertyMetadata(new PropertyChangedCallback(OnAlarmMessageChanged)));
        public static readonly DependencyProperty WarningMsgProperty = DependencyProperty.Register("WarningMsg", typeof(string), typeof(AlarmWindow), null);
        public static readonly DependencyProperty AlarmIconProperty = DependencyProperty.Register("AlarmIcon", typeof(string), typeof(AlarmWindow), null);
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(AlarmWindow), null);
        public static readonly DependencyProperty BtnViewVisibleProperty = DependencyProperty.Register("BtnViewVisible", typeof(Visibility), typeof(AlarmWindow), null);

        public string AlarmMessage
        {
            get
            {
                return Convert.ToString(base.GetValue(AlarmMessageProperty));
            }
            set
            {
                base.SetValue(AlarmMessageProperty, value);
            }
        }

        public string WarningMsg
        {
            get
            {
                return Convert.ToString(base.GetValue(WarningMsgProperty));
            }
            set
            {
                base.SetValue(WarningMsgProperty, value);
            }
        }

        public string AlarmIcon
        {
            get
            {
                return Convert.ToString(base.GetValue(AlarmIconProperty));
            }
            set
            {
                base.SetValue(AlarmIconProperty, value);
            }
        }

        public SolidColorBrush Foreground
        {
            get
            {
                return (SolidColorBrush)base.GetValue(ForegroundProperty);
            }
            set
            {
                base.SetValue(ForegroundProperty, value);
            }
        }

        public Visibility BtnViewVisible
        {
            get
            {
                return (Visibility)base.GetValue(BtnViewVisibleProperty);
            }
            set
            {
                base.SetValue(BtnViewVisibleProperty, value);
            }
        }

        #endregion

        #region Construction
        public AlarmWindow()
        {
            AlarmControl c = new AlarmControl();

            c.Width = 300;
            c.Height = 200;
            c.DataContext = this;


            this.PopupAnimation = PopupAnimation.Slide;

            this.Child = c;
            this.Child.Visibility = Visibility.Visible;
            this.AllowsTransparency = true;
            this.Loaded += new System.Windows.RoutedEventHandler(PopupScreen_Loaded);
            c.BtnView.Click += new RoutedEventHandler(BtnView_Click);


            //HideTimer.Interval = 5;
            ShowTimer.Interval = TimeSpan.FromSeconds(5);


            ShowTimer.Tick += delegate(object sender, EventArgs e)
            {
                ShowTimer.Stop();
                this.IsOpen = false;

            };

        }

        void BtnView_Click(object sender, RoutedEventArgs e)
        {
            //MainViewModel.CurrentMainViewModel.OnAlarmLookClicked(MainViewModel.CurrentMainViewModel.CurrentAlarmStation);
            ShowTimer.Stop();
            this.IsOpen = false;
        }

        #endregion

        #region Method
        void PopupScreen_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
            FrameworkElement Child = this.Child as FrameworkElement;
            this.HorizontalOffset = Screen.PrimaryScreen.WorkingArea.Width - Child.ActualWidth;
            this.VerticalOffset = Screen.PrimaryScreen.WorkingArea.Height - Child.ActualHeight;
            //this.IsOpen = false;

        }

        private static void OnAlarmMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewValue.ToString()))
            {
                AlarmWindow aw = d as AlarmWindow;
                if (aw != null)
                {
                    aw.ShowMessBox();
                }

            }
        }

        private void ShowMessBox()
        {
            this.IsOpen = true;
            ShowTimer.Start();
        }

        #endregion


        #region event
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
