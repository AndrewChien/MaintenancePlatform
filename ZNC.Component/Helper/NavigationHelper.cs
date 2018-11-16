using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZNC.Component.Helper
{
    /// <summary>
    /// 导航帮助类.
    /// </summary>
    public class NavigationHelper
    {
        #region Fields

        private int _selectedPageIndex;
        private List<NavigationCache> _navigationCaches = new List<NavigationCache>();
        private Frame _mainFrame;
        private bool _isOnNavigateMode = false, _isSameUri = false;
        private NavigateMode _curMode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationHelper"/> class.
        /// </summary>
        /// <param name="mainFrame">The main frame.</param>
        public NavigationHelper(Frame mainFrame)
        {
            if (mainFrame == null) throw new ArgumentNullException("mainFrame");

            _mainFrame = mainFrame;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates asynchronously to content that is specified by a uniform resource identifier (URI).
        /// </summary>
        /// <param name="uri">A <see cref="System.Uri"/> object initialized with the URI for the desired content.</param>
        /// <returns><c>true</c> if navigation is not canceled; otherwise, <c>false</c>.</returns>
        public bool Navigate(Uri uri)
        {
            _isOnNavigateMode = false;
            _isSameUri = false;
            return _mainFrame.Navigate(uri);
        }

        /// <summary>
        /// Navigates asynchronously to source content located at a uniform resource identifier (URI), and passes an object that contains data to be used for processing during navigation.
        /// </summary>
        /// <param name="uri">A <see cref="System.Uri"/> object initialized with the URI for the desired content.</param>
        /// <param name="extraData">A <see cref="System.Object"/> that contains data to be used for processing during navigation.</param>
        /// <returns><c>true</c> if navigation is not canceled; otherwise, <c>false</c>.</returns>
        public bool Navigate(Uri uri, object extraData)
        {
            _isOnNavigateMode = false;
            _isSameUri = false;
            return _mainFrame.Navigate(uri, extraData);
        }

        /// <summary>
        /// Navigates to the most recent item in back navigation history.
        /// </summary>
        /// <returns><c>true</c> if navigation is successed; otherwise, <c>false</c>.</returns>
        public bool GoBack()
        {
            return Navigate(NavigateMode.Back);
        }

        /// <summary>
        /// Navigates to the most recent item in forward navigation history.
        /// </summary>
        /// <returns><c>true</c> if navigation is successed; otherwise, <c>false</c>.</returns>
        public bool GoForward()
        {
            return Navigate(NavigateMode.Forward);
        }

        /// <summary>
        /// Reloads the current content.
        /// </summary>
        public void Refresh()
        {
            _isOnNavigateMode = true;
            _mainFrame.Refresh();
        }

        /// <summary>
        /// Clears this navigation.
        /// </summary>
        public void Clear()
        {
            _selectedPageIndex = 0;
            _navigationCaches = new List<NavigationCache>();
            _mainFrame = null;
        }

        /// <summary>
        /// Navigates to the most recent item in navigation history.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns><c>true</c> if navigation is successed; otherwise, <c>false</c>.</returns>
        bool Navigate(NavigateMode mode)
        {
            _curMode = mode;
            if (!CanGoBack && mode == NavigateMode.Back) return false;
            if (!CanGoForward && mode == NavigateMode.Forward) return false;

            NavigationCache cache = FindHistory(mode);
            if (cache == null) return false;

            _isSameUri = CurrentItem != null && CurrentItem.Uri.Equals(cache.Uri);
            if (_isSameUri)
            {
                _isOnNavigateMode = true;
            }
            bool isNavigated = _mainFrame.Navigate(cache.Uri, cache.ExtraData);
            if (!_isSameUri && isNavigated)
            {
                switch (mode)
                {
                    case NavigateMode.Back:
                        _selectedPageIndex = _selectedPageIndex - 1;
                        _isOnNavigateMode = true;
                        break;

                    case NavigateMode.Forward:
                        _selectedPageIndex = _selectedPageIndex + 1;
                        _isOnNavigateMode = true;
                        break;

                    default:
                        break;
                }
            }

            return isNavigated;
        }

        public void OnNavigated(Uri uri, object extraData)
        {
            if (_isOnNavigateMode)
            {
                if (_isSameUri)
                {
                    switch (_curMode)
                    {
                        case NavigateMode.Back:
                            _selectedPageIndex = _selectedPageIndex - 1;
                            break;
                        case NavigateMode.Forward:
                            _selectedPageIndex = _selectedPageIndex + 1;
                            break;
                        default:
                            break;
                    }
                }
                return;
            }

            if (_selectedPageIndex < _navigationCaches.Count - 1)
            {
                _navigationCaches.RemoveRange(_selectedPageIndex + 1, _navigationCaches.Count - _selectedPageIndex - 1);
            }
            _navigationCaches.Add(new NavigationCache(uri, extraData));
            _selectedPageIndex = _navigationCaches.Count - 1;
        }

        NavigationCache FindHistory(NavigateMode mode)
        {
            if (_navigationCaches == null || _navigationCaches.Count == 0) return null;

            switch (mode)
            {
                case NavigateMode.Back:
                    if (_selectedPageIndex - 1 < 0)
                    {
                        return null;
                    }

                    return _navigationCaches[_selectedPageIndex - 1];

                case NavigateMode.Forward:
                    if (_selectedPageIndex + 1 >= _navigationCaches.Count)
                    {
                        return null;
                    }

                    return _navigationCaches[_selectedPageIndex + 1];

                default:
                    return null;
            }
        }

        public void UpdateNavigationCache(string guid, PageStateCollection pageStates)
        {
            if (CurrentItem == null) return;

            CurrentItem.Guid = guid;
            CurrentItem.PageStates = pageStates;
        }

        #endregion

        #region Properties

        //public int SelectedPage { get { return _selectedPageIndex; } }

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        /// <value>true if there is at least one entry in back navigation history; false if there are no entries in back navigation history or the System.Windows.Controls.Frame does not own its own navigation history.</value>
        public bool CanGoBack
        {
            get
            {
                return _navigationCaches != null && _navigationCaches.Count > 0 && _selectedPageIndex > 0;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in forward navigation history.
        /// </summary>
        /// <value>
        /// true if there is at least one entry in forward navigation history; false if there are no entries in forward navigation history or the System.Windows.Controls.Frame does not own its own navigation history.
        /// </value>
        public bool CanGoForward
        {
            get
            {
                return _navigationCaches != null && _navigationCaches.Count > 0 && _selectedPageIndex < _navigationCaches.Count - 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public NavigationCache CurrentItem
        {
            get
            {
                if (_navigationCaches == null || _navigationCaches.Count == 0 || _navigationCaches.Count <= _selectedPageIndex)
                {
                    return null;
                }

                return _navigationCaches[_selectedPageIndex];
            }
        }

        public EWellNavigationMode NavigationMode { get { return _isOnNavigateMode ? EWellNavigationMode.History : EWellNavigationMode.Navigate; } }

        #endregion

        #region Nested Types

        /// <summary>
        /// 导航模式.
        /// </summary>
        enum NavigateMode
        {
            /// <summary>
            /// 后退
            /// </summary>
            Back,

            /// <summary>
            /// 前进
            /// </summary>
            Forward,
        }

        #endregion
    }

    /// <summary>
    /// 导航缓存对象.
    /// </summary>
    public class NavigationCache
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationCache"/> class.
        /// </summary>
        public NavigationCache()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationCache"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="parameter">The parameter.</param>
        public NavigationCache(Uri uri, object parameter)
        {
            Uri = uri;
            ExtraData = parameter;
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Uri"/> object initialized with the URI for the desired content.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Uri { get; private set; }

        /// <summary>
        /// Gets or sets a <see cref="System.Object"/> that contains data to be used for processing during navigation.
        /// </summary>
        /// <value>The extra data.</value>
        public object ExtraData { get; private set; }

        ///// <summary>
        ///// Gets or sets the parameters.
        ///// </summary>
        ///// <value>The parameters.</value>
        //public Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the page states.
        /// </summary>
        /// <value>The page states.</value>
        public PageStateCollection PageStates { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class EWellNavigationEventArgs : EventArgs
    {
        private NavigationEventArgs _eventArgs;
        private EWellNavigationMode _mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterLockNavigationEventArgs"/> class.
        /// </summary>
        /// <param name="eventArgs">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        /// <param name="mode">The mode.</param>
        public EWellNavigationEventArgs(NavigationEventArgs eventArgs, EWellNavigationMode mode)
        {
            _eventArgs = eventArgs;
            _mode = mode;
        }

        /// <summary>
        /// Gets the root node of the target page's content.
        /// </summary>
        /// <value>The root element of the target page's content.</value>
        public object Content { get { return _eventArgs.Content; } }

        /// <summary>
        /// Gets an optional user-defined data object.
        /// </summary>
        /// <value>The data object.</value>
        public object ExtraData { get { return _eventArgs.ExtraData; } }

        /// <summary>
        /// Gets a value that indicates whether the current navigator initiated the navigation.
        /// </summary>
        /// <value>true if the navigation was initiated inside the current frame; false if the parent navigator is also navigating.</value>
        public bool IsNavigationInitiator { get { return _eventArgs.IsNavigationInitiator; } }

        /// <summary>
        /// Gets the navigator that raised the event
        /// </summary>
        /// <value>The navigator that raised the event.</value>
        public object Navigator { get { return _eventArgs.Navigator; } }

        /// <summary>
        /// Gets the uniform resource identifier (URI) of the target page.
        /// </summary>
        /// <value>The URI of the target page.</value>
        public Uri Uri { get { return _eventArgs.Uri; } }

        /// <summary>
        /// Gets the Web response to allow access to HTTP headers and other properties.
        /// </summary>
        /// <value>The Web response.</value>
        public WebResponse WebResponse { get { return _eventArgs.WebResponse; } }

        /// <summary>
        /// 
        /// </summary>
        public EWellNavigationMode NavigationMode { get { return _mode; } }
    }

    /// <summary>
    /// 页面导航类型.
    /// </summary>
    public enum EWellNavigationMode
    {
        /// <summary>
        /// 页面跳转.
        /// </summary>
        Navigate,

        /// <summary>
        /// 历史记录.
        /// </summary>
        History,
    }

    /// <summary>
    /// 页面缓存数据项.
    /// </summary>
    public class PageStateItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageStateItem"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="propType">Type of the prop.</param>
        /// <param name="value">The value.</param>
        public PageStateItem(int id, string propType, object value)
        {
            _id = id;
            _propType = propType;
            _value = value;
            _stateType = PageStateType.System;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageStateItem"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public PageStateItem(string key, object value)
        {
            _key = key;
            _value = value;
            _stateType = PageStateType.Custom;
        }

        private int _id;
        /// <summary>
        /// ID.
        /// </summary>
        public int UniqueId { get { return _id; } }

        private string _propType;
        /// <summary>
        /// 类型.
        /// </summary>
        public string PropType { get { return _propType; } }

        private string _key;
        /// <summary>
        /// 键.
        /// </summary>
        public string Key { get { return _key; } }

        private object _value;
        /// <summary>
        /// 值.
        /// </summary>
        public object Value { get { return _value; } }

        private PageStateType _stateType;
        /// <summary>
        /// 类型.
        /// </summary>
        public PageStateType StateType { get { return _stateType; } }
    }

    /// <summary>
    /// 缓存数据类型
    /// </summary>
    public enum PageStateType
    {
        /// <summary>
        /// 系统
        /// </summary>
        System,
        /// <summary>
        /// 自定义
        /// </summary>
        Custom,
    }

    /// <summary>
    /// 页面数据缓存集合.
    /// </summary>
    public class PageStateCollection : List<PageStateItem>
    {
        /// <summary>
        /// 将项添加至集合结尾.
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(string key, object value)
        {
            this.Add(new PageStateItem(key, value));
        }

        /// <summary>
        /// 确定 <see cref="PageStateCollection"/> 中是否包含指定元素.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public bool ContainsKey(int id)
        {
            return this.FirstOrDefault(item => item.StateType == PageStateType.System && item.UniqueId == id) != null;
        }

        /// <summary>
        /// 确定 <see cref="PageStateCollection"/> 中是否包含指定元素.
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propType">类型</param>
        /// <returns></returns>
        public bool ContainsKey(int id, string propType)
        {
            return this.FirstOrDefault(item => item.StateType == PageStateType.System && item.UniqueId == id && item.PropType == propType) != null;
        }

        /// <summary>
        /// 确定 <see cref="PageStateCollection"/> 中是否包含指定元素.
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.FirstOrDefault(item => item.StateType == PageStateType.Custom && item.Key == key) != null;
        }

        /// <summary>
        /// 获取指定键相关联的值.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public object this[int id]
        {
            get
            {
                var pagestate = this.FirstOrDefault(item => item.StateType == PageStateType.System && item.UniqueId == id);
                if (pagestate == null) return null;

                return pagestate.Value;
            }
        }

        /// <summary>
        /// 获取指定键相关联的值.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public object this[int id, string propType]
        {
            get
            {
                var pagestate = this.FirstOrDefault(item => item.StateType == PageStateType.System && item.UniqueId == id && item.PropType == propType);
                if (pagestate == null) return null;

                return pagestate.Value;
            }
        }

        /// <summary>
        /// 获取指定键相关联的值.
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                var pagestate = this.FirstOrDefault(item => item.StateType == PageStateType.Custom && item.Key == key);
                if (pagestate == null) return null;

                return pagestate.Value;
            }
        }
    }
}