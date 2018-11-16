using System;
using System.Collections;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZNC.Component.Controls
{
    /// <summary>
    /// Autocomplete User control
    /// </summary>
    public partial class AutoComplete : ComboBox
    {
        #region Events and Delegates
        /// <summary>
        /// Used to pass arguments for autocomplete control
        /// </summary>
        public class AutoCompleteArgs : EventArgs
        {
            private string _pattern;
            /// <summary>
            /// The current pattern in the auto complete
            /// </summary>
            public string Pattern
            {
                get
                {
                    return this._pattern;
                }
            }

            private IEnumerable _dataSource;
            /// <summary>
            /// Used to the the new datasource for the autocomplete
            /// </summary>
            public IEnumerable DataSource
            {
                get
                {
                    return this._dataSource;
                }
                set
                {
                    this._dataSource = value;
                }
            }

            /// <summary>
            /// default false
            /// </summary>
            private bool _cancelBinding = false;
            /// <summary>
            /// Determines weather or not the datasource should be bounded to the autocomplete control
            /// </summary>
            public bool CancelBinding
            {
                get
                {
                    return this._cancelBinding;
                }
                set
                {
                    this._cancelBinding = value;
                }
            }

            public AutoCompleteArgs(string Pattern)
            {
                this._pattern = Pattern;
            }
        }

        /// <summary>
        /// event handler for auto complete pattern changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void AutoCompleteHandler(object sender, AutoCompleteArgs args);
        /// <summary>
        /// Event for when pattern has been changed
        /// </summary>
        public event AutoCompleteHandler PatternChanged;
        #endregion

        #region Fields
        private Timer _interval;
        private int _maxRecords = 10;
        private bool _typeAhead = false;
        private bool IsKeyEvent = false;
        private bool _clearOnEmpty = true;
        private int _delay = DEFAULT_DELAY;
        private const int DEFAULT_DELAY = 800; // 1 seconds delay
        #endregion

        #region Properties
        /// <summary>
        /// The timespan between keypress and pattern changed event
        /// </summary>
        [DefaultValue(DEFAULT_DELAY)]
        public int Delay
        {
            get
            {
                return this._delay;
            }
            set
            {
                this._delay = value;
            }
        }
        /// <summary>
        /// The maximum number of records to show in the drop down
        /// </summary>
        public int MaxRecords
        {
            get
            {
                return this._maxRecords;
            }
            set
            {
                this._maxRecords = value;
            }
        }

        /// <summary>
        /// Determines weather textbox does type ahead
        /// </summary>
        public bool TypeAhead
        {
            get
            {
                return this._typeAhead;
            }
            set
            {
                this._typeAhead = value;
            }
        }

        /// <summary>
        /// Gets the text box in charge of the editable portion of the combo box.
        /// </summary>
        protected TextBox EditableTextBox
        {
            get
            {
                return base.GetTemplateChild("PART_EditableTextBox") as TextBox;
            }
        }

        /// <summary>
        /// returns the selected items text representation
        /// </summary>
        public string SelectedText
        {
            get
            {
                if (this.SelectedIndex == -1) return string.Empty;

                return this.SelectedItem.GetType().GetProperty(this.DisplayMemberPath).GetValue(this.SelectedItem, null).ToString();
            }
        }

        /// <summary>
        /// Specify weather or not to clear the datasource when the text is set to empty string
        /// </summary>
        /// <remarks>When this property is set to true, the patternchanged event will not be fired when the text is empty.</remarks>
        public bool ClearOnEmpty
        {
            get
            {
                return this._clearOnEmpty;
            }
            set
            {
                this._clearOnEmpty = true;
            }
        }
        #endregion

        #region Constructor(s)
        public AutoComplete()
        {
            //InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this._interval = new Timer(this.Delay);
                this._interval.AutoReset = true;
                this._interval.Elapsed += new ElapsedEventHandler(_interval_Elapsed);
            }
        }
        #endregion

        #region Event Handlers
        void _interval_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsKeyEvent = false;
            // pause the timer
            this._interval.Stop();

            // show the drop down when user starts typing then stops
            this.Dispatcher.BeginInvoke((Action)delegate
            {
                this.IsDropDownOpen = true;
                // load from event source
                if (this.PatternChanged != null)
                {
                    AutoCompleteArgs args = new AutoCompleteArgs(this.Text);
                    this.PatternChanged(this, args);
                    // bind the new datasource if user did not cancel
                    if (!args.CancelBinding)
                    {
                        this.ItemsSource = args.DataSource;

                        if (!this.TypeAhead)
                        {
                            // override auto complete behavior
                            this.Text = args.Pattern;
                            this.EditableTextBox.CaretIndex = args.Pattern.Length;
                        }
                    }
                }
            },
                System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }

        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsKeyEvent = false;
            this.IsDropDownOpen = false;
            // to prevent misunderstanding that user has entered some information
            if (this.SelectedIndex == -1)
                this.Text = string.Empty;
            // syncronize text
            else
                this.Text = this.SelectedText;
            // release timer resources
            this._interval.Close();

            try
            {
                this.EditableTextBox.CaretIndex = 0;
            }
            catch { }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._interval.Stop();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //load the text box control
            if (this.EditableTextBox != null)
            {
                this.EditableTextBox.PreviewKeyDown += new KeyEventHandler(EditableTextBox_PreviewKeyDown);
                this.EditableTextBox.TextChanged += new TextChangedEventHandler(EditableTextBox_TextChanged);
            }
        }

        void EditableTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.IsKeyEvent = true;
        }

        void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // clear the itemsource when text is empty
            if (this.ClearOnEmpty && string.IsNullOrEmpty(this.EditableTextBox.Text.Trim()))
                this.ItemsSource = null; // this should also clear selection
            else if (IsKeyEvent)
                this.ResetTimer();
        }

        protected void ResetTimer()
        {
            this._interval.Stop();
            this._interval.Start();
        }
        #endregion
    }
}