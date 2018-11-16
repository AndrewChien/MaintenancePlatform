using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZNC.Component;
using ZNC.Component.Helper;
using ZNC.Utility;

namespace MaintenancePlatform.ViewModels
{
    public class ChildPageViewModel : DataModelBase
    {
        #region Fields

        private EWellNavigationMode _navigationMode;
        private bool _autoSavePageState = true;
        private string _guid;
        /// <summary>
        /// 当前页面参数列表.
        /// </summary>
        /*protected*/
        internal Dictionary<string, object> _parametersMapping;
        protected Dictionary<string, string> QueryString;
        protected PageStateCollection _pageStates;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildPageViewModel"/> class.
        /// </summary>
        public ChildPageViewModel()
        {
            _guid = Guid.NewGuid().ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取指定参数的参数值.
        /// </summary>
        /// <param name="paramKey">参数名称.</param>
        /// <returns>参数值, 若不存在该参数则返回<c>null</c>.</returns>
        protected string LoadParameter(string paramKey)
        {
            if (QueryString == null || !QueryString.ContainsKey(paramKey)) return null;
            return QueryString[paramKey];
        }

        /// <summary>
        /// 获取当前页面的参数集合.
        /// </summary>
        /// <typeparam name="T">参数类型.</typeparam>
        /// <param name="paramKey">参数名称.</param>
        /// <param name="defaultVal">参数默认值.</param>
        /// <returns>参数值, 若不存在该参数则返回默认值.</returns>
        protected T LoadParameter<T>(string paramKey, bool isQueryUri = false, T defaultVal = default(T))
        {
            T result;
            if (_parametersMapping != null && _parametersMapping.Count > 0 && _parametersMapping.ContainsKey(paramKey))
            {
                result = (T)_parametersMapping[paramKey];
                if (result != null) return result;
            }

            if (isQueryUri && QueryString != null && QueryString.ContainsKey(paramKey))
            {
                result = (T)Convert.ChangeType(LoadParameter(paramKey), typeof(T));
                if (result != null) return result;
            }

            return defaultVal;
        }

        /// <summary>
        /// 获取页面自定义缓存中的值.
        /// </summary>
        /// <typeparam name="T">参数类型.</typeparam>
        /// <param name="key">参数名称.</param>
        /// <param name="defaultVal">参数默认值.</param>
        /// <returns>参数值, 若不存在该参数则返回默认值.</returns>
        protected T LoadCache<T>(string key, T defaultVal = default(T))
        {
            if (!_pageStates.ContainsKey(key)) return defaultVal;

            return (T)_pageStates[key];
        }

        /// <summary>
        /// 设置参数集合.
        /// </summary>
        /// <param name="parameters"></param>
        internal void SetParameter(Dictionary<string, object> parameters)
        {
            _parametersMapping = parameters;
        }

        /// <summary>
        /// 获取控件状态信息的最大继承层次.
        /// </summary>
        const int MAX_STATE_LEVEL = 2;

        void LoadState(FrameworkElement parent)
        {
            IEnumerable ie = LogicalTreeHelper.GetChildren(parent);
            UIElement element;
            DependencyProperty dp;
            FieldInfo fi;
            foreach (var child in ie)
            {
                if (!(child is FrameworkElement)) continue;
                if (child is TextBlock || child is Rectangle) continue;

                element = (child as UIElement);
                if ((bool)element.GetValue(JournalEntry.KeepAliveProperty))
                {
                    fi = child.GetType().GetDependencyPropertyField(element.GetValue(JournalEntry.NameProperty).ToString() + "Property");
                    if (fi != null && fi.FieldType == typeof(DependencyProperty))
                    {
                        dp = (DependencyProperty)fi.GetValue(null);
                        if (dp != null)
                        {
                            _pageStates.Add(new PageStateItem(element.PersistId, dp.Name, element.GetValue(dp)));
                        }
                    }
                }

                // 获取当前控件的状态信息.
                LoadControlState(child, child.GetType());

                // 获取子控件的状态信息.
                LoadState(child as FrameworkElement);
            }
        }

        void LoadControlState(object control, Type controlType, int curLevel = 1)
        {
            if (control is Grid || control is StackPanel || control is Canvas) return;
            if (control is Button || control is Label || control is Border) return;

            DependencyProperty dp;
            UIElement element = (control as UIElement);
            foreach (var field in controlType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public))
            {
                if (field.FieldType == typeof(DependencyProperty))
                {
                    dp = (DependencyProperty)field.GetValue(null);
                    if (dp == null) continue;
                    PropertyMetadata pm = dp.GetMetadata(element);
                    if (pm == null)
                    {
                        continue;
                    }
                    /*if ((pm is FrameworkPropertyMetadata) && !(pm as FrameworkPropertyMetadata).Journal)
                    {
                        continue;
                    }
                    else */
                    if (pm is PropertyMetadata || pm is FrameworkPropertyMetadata)
                    {
                        BindingExpression be = (control as FrameworkElement).GetBindingExpression(dp);
                        if (be == null || be.ParentBinding == null) continue;
                        if (be.ParentBinding.Mode != BindingMode.TwoWay) continue;
                    }
                    else
                    {
                        continue;
                    }

                    _pageStates.Add(new PageStateItem(element.PersistId, dp.Name, element.GetValue(dp)));
                }
            }

            if (curLevel <= MAX_STATE_LEVEL && controlType.BaseType != null)
            {
                LoadControlState(control, controlType.BaseType, curLevel + 1);
            }
        }

        void RestoreState(FrameworkElement parent)
        {
            IEnumerable ie = LogicalTreeHelper.GetChildren(parent);
            FrameworkElement element;
            DependencyProperty dp;
            FieldInfo fi;
            foreach (var child in ie)
            {
                if (!(child is FrameworkElement)) continue;
                if (child is TextBlock || child is Rectangle) continue;

                element = (child as FrameworkElement);

                if ((bool)element.GetValue(JournalEntry.KeepAliveProperty))
                {
                    fi = child.GetType().GetDependencyPropertyField(element.GetValue(JournalEntry.NameProperty).ToString() + "Property");
                    if (fi != null && fi.FieldType == typeof(DependencyProperty))
                    {
                        dp = (DependencyProperty)fi.GetValue(null);

                        if (dp != null && _pageStates.ContainsKey(element.PersistId, dp.Name))
                        {
                            (child as DependencyObject).SetValue(dp, _pageStates[element.PersistId, dp.Name]);
                            BindingExpression exp = element.GetBindingExpression(dp);
                            if (exp != null && exp.Status == BindingStatus.Active)
                            {
                                exp.UpdateSource();
                            }
                        }
                    }
                }
                RestoreControlState(child, child.GetType());

                RestoreState(child as FrameworkElement);
            }
        }

        void RestoreControlState(object control, Type controlType, int curLevel = 1)
        {
            if (control is Grid || control is StackPanel || control is Canvas) return;
            if (control is Button || control is Label || control is Border) return;

            DependencyProperty dp;
            FrameworkElement element = (control as FrameworkElement);

            foreach (var field in controlType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public))
            {
                if (field.FieldType == typeof(DependencyProperty))
                {
                    dp = (DependencyProperty)field.GetValue(null);
                    if (dp == null) continue;
                    PropertyMetadata pm = dp.GetMetadata(element);
                    //if (pm == null || !(pm is FrameworkPropertyMetadata) || !(pm as FrameworkPropertyMetadata).Journal)
                    //    continue;
                    if (pm == null)
                    {
                        continue;
                    }
                    /*if ((pm is FrameworkPropertyMetadata) && !(pm as FrameworkPropertyMetadata).Journal)
                    {
                        continue;
                    }
                    else */
                    if (pm is PropertyMetadata || pm is FrameworkPropertyMetadata)
                    {
                        BindingExpression be = (control as FrameworkElement).GetBindingExpression(dp);
                        if (be == null || be.ParentBinding == null) continue;
                        if (be.ParentBinding.Mode != BindingMode.TwoWay) continue;
                    }
                    else
                    {
                        continue;
                    }

                    if (_pageStates.ContainsKey(element.PersistId, dp.Name))
                    {
                        (control as DependencyObject).SetValue(dp, _pageStates[element.PersistId, dp.Name]);
                        BindingExpression exp = element.GetBindingExpression(dp);
                        if (exp != null && exp.Status == BindingStatus.Active)
                        {
                            exp.UpdateSource();
                        }
                    }
                }
            }

            if (curLevel <= MAX_STATE_LEVEL && controlType.BaseType != null)
            {
                RestoreControlState(control, controlType.BaseType, curLevel + 1);
            }
        }

        internal void UpdateCache()
        {
            NavigationService.UpdateNavigationCache(this._guid, this._pageStates);
        }

        internal void UpdateMode(EWellNavigationMode mode)
        {
            _navigationMode = mode;
        }

        #endregion

        #region Events

        /// <summary>
        /// 当已经找到目标导航内容且可通过 Content 属性获得时发生，源导航内容触发。
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        internal virtual void OnNavigatedFrom(object sender, NavigationEventArgs e)
        {
        }

        /// <summary>
        /// 当已经找到目标导航内容且可通过 Content 属性获得时发生，即使此时可能尚未完成加载，目标导航内容触发。
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigationEventArgs"/> instance containing the event data.</param>
        internal virtual void OnNavigatedTo(object sender, NavigationEventArgs e)
        {
            if (NavigationService.NavigationMode == EWellNavigationMode.History)
            {
                _guid = NavigationService.CurrentItem.Guid;
            }

            if (e.Uri != null)
            {
                int queryIndex = e.Uri.ToString().IndexOf('?');
                if (queryIndex > 0)
                {
                    QueryString = new Dictionary<string, string>();

                    //var ie = from match in e.Uri.ToString().Split('?').
                    //             Where(m => m.Contains('=')).
                    //             SelectMany(pr => pr.Split('&'))
                    //         where match.Contains('=')
                    //         select new KeyValuePair<string, String>(
                    //            match.Split('=')[0],
                    //            match.Split('=')[1]);
                    var ienumerable = from param in e.Uri.ToString().Substring(queryIndex + 1).Split('&')
                                      where param.Contains('=')
                                      select new KeyValuePair<string, string>(param.Split('=')[0], param.Split('=')[1]);
                    ienumerable.ToList().ForEach(kvp => QueryString.Add(kvp.Key, kvp.Value));
                    //ienumerable.ToList().ForEach(kvp => QueryString.Add(kvp.Key, HttpUtility.UrlDecode(kvp.Value)));
                }
            }

            if (NavigationService.NavigationMode == EWellNavigationMode.History)
            {
                _pageStates = NavigationService.CurrentItem.PageStates;
            }
        }

        /// <summary>
        /// 在请求新导航时发生。
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigatingCancelEventArgs"/> instance containing the event data.</param>
        internal virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// 当请求新导航发生后, 保存当前页面的数据.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Navigation.NavigatingCancelEventArgs"/> instance containing the event data.</param>
        internal virtual void OnSaveState(object sender, NavigatingCancelEventArgs e)
        {
            _pageStates = new PageStateCollection();
            if (AutoSavePageState)
            {
                LoadState((sender as Frame).Content as FrameworkElement);
            }
        }

        /// <summary>
        /// 当页面加载时, 还原历史记录中的数据. 只适用于前进后退的历史记录.
        /// </summary>
        /// <param name="sender"></param>
        internal virtual void OnRestoreState(object sender)
        {
            if (NavigationMode == EWellNavigationMode.History)
            {
                if (_pageStates == null || _pageStates.Count == 0) return;

                RestoreState((sender as Frame).Content as FrameworkElement);
            }
            OnRestoreCustomState();
        }

        /// <summary>
        /// 当页面加载时, 还原历史记录中的自定义数据.
        /// </summary>
        internal virtual void OnRestoreCustomState()
        {
        }

        #endregion

        #region Properties

        ///// <summary>
        ///// 主页面导航框架控件.
        ///// </summary>
        //protected Frame MainFrame
        //{
        //    get
        //    {
        //        return MainViewModel.CurrentMainViewModel.MainFrame;
        //    }
        //}

        /// <summary>
        /// 主页面导航服务类.
        /// </summary>
        protected NavigationHelper NavigationService
        {
            get
            {
                return MainWindowViewModel.CurrentViewModel.NavigationService;
            }
        }

        /// <summary>
        /// 获取当前页面导航类型.
        /// </summary>
        protected EWellNavigationMode NavigationMode
        {
            get { return _navigationMode; }
        }

        /// <summary>
        /// 获取或设置一个值, 该值用于标识当前页面是否需要自动保存页面数据.
        /// </summary>
        protected bool AutoSavePageState
        {
            get { return _autoSavePageState; }
            set { _autoSavePageState = value; }
        }

        #endregion
    }
}
