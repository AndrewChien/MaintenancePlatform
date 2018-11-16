using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ZNC.Component
{
    /// <summary>
    /// 下拉框过滤控件.
    /// </summary>
    /// <example>
    /// In Xaml:
    /// 1) Add xmlns namespace:
    /// <code>
    ///     xmlns:sui="http://schemas.ewell-hk.com/2011/xaml/presentation"
    /// </code>
    /// 2) Add AutoFilteredComboBox Control:
    /// <code>
    ///     &lt;sui:AutoFilteredComboBox IsEditable="True" DisplayMemberPath="Name" SelectedValuePath="ID" TextSearchPath="Code" ItemsSource="{Binding Items}" Height="20" Width="160" /&gt;
    /// </code>
    /// </example>
    public class AutoFilteredComboBox : ComboBox
    {
        private int silenceEvents = 0;

        /// <summary>
        /// Creates a new instance of <see cref="AutoFilteredComboBox" />.
        /// </summary>
        public AutoFilteredComboBox()
        {
            AddValueChanged();
            this.Unloaded += new RoutedEventHandler(AutoFilteredComboBox_Unloaded);
        }

        void AutoFilteredComboBox_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unloaded -= new RoutedEventHandler(AutoFilteredComboBox_Unloaded);
            RemoveValueChanged();
        }

        #region IsCaseSensitive Dependency Property
        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="IsCaseSensitive" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsCaseSensitiveProperty =
            DependencyProperty.Register("IsCaseSensitive", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the way the combo box treats the case sensitivity of typed text.
        /// </summary>
        /// <value>The way the combo box treats the case sensitivity of typed text.</value>
        [System.ComponentModel.Description("The way the combo box treats the case sensitivity of typed text.")]
        [System.ComponentModel.Category("AutoFiltered ComboBox")]
        [System.ComponentModel.DefaultValue(true)]
        public bool IsCaseSensitive
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (bool)this.GetValue(IsCaseSensitiveProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                this.SetValue(IsCaseSensitiveProperty, value);
            }
        }

        /// <summary>
        /// Called when is case sensitive changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnIsCaseSensitiveChanged(object sender, EventArgs e)
        {
            if (this.IsCaseSensitive)
                this.IsTextSearchEnabled = false;

            this.RefreshFilter();
        }

        /// <summary>
        /// Registers the is case sensitive change notification.
        /// </summary>
        private void RegisterIsCaseSensitiveChangeNotification()
        {
            System.ComponentModel.DependencyPropertyDescriptor.FromProperty(IsCaseSensitiveProperty, typeof(AutoFilteredComboBox)).AddValueChanged(
                this, this.OnIsCaseSensitiveChanged);
        }
        private void UnregisterIsCaseSensitiveChangeNotification()
        {
            System.ComponentModel.DependencyPropertyDescriptor.FromProperty(IsCaseSensitiveProperty, typeof(AutoFilteredComboBox)).RemoveValueChanged(
                this, this.OnIsCaseSensitiveChanged);
        }
        #endregion

        #region DropDownOnFocus Dependency Property
        /// <summary>
        /// The <see cref="DependencyProperty"/> object of the <see cref="DropDownOnFocus" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DropDownOnFocusProperty =
            DependencyProperty.Register("DropDownOnFocus", typeof(bool), typeof(AutoFilteredComboBox), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets or sets the way the combo box behaves when it receives focus.
        /// </summary>
        /// <value>The way the combo box behaves when it receives focus.</value>
        [System.ComponentModel.Description("The way the combo box behaves when it receives focus.")]
        [System.ComponentModel.Category("AutoFiltered ComboBox")]
        [System.ComponentModel.DefaultValue(true)]
        public bool DropDownOnFocus
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (bool)this.GetValue(DropDownOnFocusProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                this.SetValue(DropDownOnFocusProperty, value);
            }
        }
        #endregion

        #region TextSearchPath Dependency Property

        // Using a DependencyProperty as the backing store for TextSearchPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSearchPathProperty =
            DependencyProperty.Register("TextSearchPath", typeof(string), typeof(AutoFilteredComboBox));

        /// <summary>
        /// 获取或设置一个值, 该值用于指定搜索的属性名称.
        /// </summary>
        /// <value>The text search path.</value>
        /// <remarks>
        /// 若该值未设置或不存在该属性, 则按照控件的 DisplayMemberPath 过滤, DisplayMemberPath 不存在则以整个子元素进行过滤.
        /// </remarks>
        public string TextSearchPath
        {
            get { return (string)GetValue(TextSearchPathProperty); }
            set { SetValue(TextSearchPathProperty, value); }
        }

        #endregion

        #region | Handle selection |
        /// <summary>
        /// Called when <see cref="ComboBox.ApplyTemplate()"/> is called.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.EditableTextBox.SelectionChanged += this.EditableTextBox_SelectionChanged;
            }
        }

        /// <summary>
        /// Gets the text box in charge of the editable portion of the combo box.
        /// </summary>
        protected TextBox EditableTextBox
        {
            get
            {
                return ((TextBox)base.GetTemplateChild("PART_EditableTextBox"));
            }
        }

        private int start = 0, length = 0;

        private void EditableTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (this.silenceEvents == 0)
            {
                this.start = ((TextBox)(e.OriginalSource)).SelectionStart;
                this.length = ((TextBox)(e.OriginalSource)).SelectionLength;


                this.RefreshFilter();

                return;
            }
            if (this.silenceEvents > 0) this.silenceEvents--;
        }
        #endregion

        #region | Handle focus |
        /// <summary>
        /// Invoked whenever an unhandled <see cref="UIElement.GotFocus" /> event
        /// reaches this element in its route.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if (this.ItemsSource != null && this.DropDownOnFocus)
            {
                this.IsDropDownOpen = true;
            }
        }
        #endregion

        #region | Handle filtering |

        private void RefreshFilter()
        {
            if (this.ItemsSource != null)
            {
                ICollectionView view;
                //if (this.ItemsSource is IList)
                //{
                //    view = new ListCollectionView(this.ItemsSource as IList);
                //}
                //else
                //{
                //    System.Diagnostics.Debug.Assert(false, "引发内存泄露, 请尝试修改数据源类型为IList.");
                    view = CollectionViewSource.GetDefaultView(this.ItemsSource);
                //}
                view.Refresh();
                this.IsDropDownOpen = true;
            }
            else if (this.ItemsSource == null)
            {
                this.IsDropDownOpen = false;
            }
        }

        private bool FilterPredicate(object value)
        {
            // We don't like nulls.
            if (value == null)
                return false;

            // If there is no text, there's no reason to filter.
            if (this.Text == null || this.Text.Length == 0)
                return true;

            if (TextSearchPath != null && DisplayMemberPath != null && TextSearchPath != DisplayMemberPath)
            {
                PropertyInfo piDisplay = value.GetType().GetProperty(DisplayMemberPath, BindingFlags.Public | BindingFlags.Instance);
                if (piDisplay != null)
                {
                    foreach (object item in this.ItemsSource)
                    {
                        if (this.Text.Equals(piDisplay.GetValue(item, null)))
                            return true;
                    }
                }
            }
            else
            {
                return true;
            }

            string prefix = this.Text;

            // If the end of the text is selected, do not mind it.
            if (this.length > 0 && this.start + this.length == this.Text.Length)
            {
                prefix = prefix.Substring(0, this.start);
            }

            if (TextSearchPath != null || DisplayMemberPath != null)
            {
                PropertyInfo pi;
                object temp;
                if (TextSearchPath != null)
                {
                    pi = value.GetType().GetProperty(TextSearchPath, BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        temp = pi.GetValue(value, null);
                        if (temp == null) return false;

                        return temp.ToString().StartsWith(prefix, !this.IsCaseSensitive, CultureInfo.CurrentCulture);
                    }
                }
                if (DisplayMemberPath != null)
                {
                    pi = value.GetType().GetProperty(DisplayMemberPath, BindingFlags.Public | BindingFlags.Instance);
                    if (pi != null)
                    {
                        temp = pi.GetValue(value, null);
                        if (temp == null) return false;

                        return temp.ToString().StartsWith(prefix, !this.IsCaseSensitive, CultureInfo.CurrentCulture);
                    }
                }
            }
            return value.ToString()
                .StartsWith(prefix, !this.IsCaseSensitive, CultureInfo.CurrentCulture);
        }
        #endregion

        #region Property Value Changed

        private void AddValueChanged()
        {
            DependencyPropertyDescriptor textProperty = DependencyPropertyDescriptor.FromProperty(
                ComboBox.TextProperty, typeof(AutoFilteredComboBox));
            textProperty.AddValueChanged(this, this.OnTextChanged);
            DependencyPropertyDescriptor selectedItemProperty = DependencyPropertyDescriptor.FromProperty(
                ComboBox.SelectedItemProperty, typeof(AutoFilteredComboBox));
            selectedItemProperty.AddValueChanged(this, this.OnSelectedItemChanged);

            this.RegisterIsCaseSensitiveChangeNotification();
        }

        private void RemoveValueChanged()
        {
            DependencyPropertyDescriptor textProperty = DependencyPropertyDescriptor.FromProperty(
                ComboBox.TextProperty, typeof(AutoFilteredComboBox));
            textProperty.RemoveValueChanged(this, this.OnTextChanged);
            DependencyPropertyDescriptor selectedItemProperty = DependencyPropertyDescriptor.FromProperty(
                ComboBox.SelectedItemProperty, typeof(AutoFilteredComboBox));
            selectedItemProperty.RemoveValueChanged(this, this.OnSelectedItemChanged);

            UnregisterIsCaseSensitiveChangeNotification();
        }

        #endregion

        /// <summary>
        /// Called when the source of an item in a selector changes.
        /// </summary>
        /// <param name="oldValue">Old value of the source.</param>
        /// <param name="newValue">New value of the source.</param>
        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            if (newValue != null)
            {
                //if (newValue is IList)
                //{
                //    ListCollectionView view = new ListCollectionView(newValue as IList);
                //    view.Filter += this.FilterPredicate;
                //}
                //else
                {
                    //System.Diagnostics.Debug.Assert(false, "引发内存泄露, 请尝试修改数据源类型为IList.");
                    ICollectionView view = CollectionViewSource.GetDefaultView(newValue);
                    view.Filter += this.FilterPredicate;
                }
            }

            if (oldValue != null)
            {
                //if (oldValue is IList)
                //{
                //    ListCollectionView view = new ListCollectionView(oldValue as IList);
                //    view.Filter -= this.FilterPredicate;
                //}
                //else
                {
                    //System.Diagnostics.Debug.Assert(false, "引发内存泄露, 请尝试修改数据源类型为IList.");
                    ICollectionView view = CollectionViewSource.GetDefaultView(oldValue);
                    view.Filter -= this.FilterPredicate;
                }
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        /// <summary>
        /// Called when text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!this.IsTextSearchEnabled && this.silenceEvents == 0)
            {
                this.RefreshFilter();

                // Manually simulate the automatic selection that would have been
                // available if the IsTextSearchEnabled dependency property was set.
                if (this.Text.Length > 0)
                {
                    foreach (object item in CollectionViewSource.GetDefaultView(this.ItemsSource))
                    {
                        int text = item.ToString().Length, prefix = this.Text.Length;
                        this.SelectedItem = item;

                        this.silenceEvents++;
                        this.EditableTextBox.Text = item.ToString();
                        this.EditableTextBox.Select(prefix, text - prefix);
                        this.silenceEvents--;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Called when selected item changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnSelectedItemChanged(object sender, EventArgs e)
        {
            if ((sender as AutoFilteredComboBox).SelectedIndex >= 0)
                this.silenceEvents++;
        }
    }
}
