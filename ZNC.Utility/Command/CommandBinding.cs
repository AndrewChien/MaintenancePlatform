using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ZNC.Utility.Command
{
    public class CommandBinding : FrameworkElement
    {
        #region DependencyProperties
        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter",
                                        typeof(object),
                                        typeof(CommandBinding),
                                        null);
        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
                                        typeof(ICommand),
                                        typeof(CommandBinding),
                                        new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));
        public static DependencyProperty EventNameProperty =
            DependencyProperty.Register("EventName",
                                        typeof(string),
                                        typeof(CommandBinding),
                                        new PropertyMetadata(new PropertyChangedCallback(OnEventNameChanged)));

        public static void OnEventNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CommandBinding)obj).SetUpHandler();
        }
        public static void OnCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CommandBinding)obj).SetUpHandler();
        }
        #endregion

        FrameworkElement _owner = null;
        EventInfo _currentEvent = null;
        Delegate _currentDelegate = null;

        public void Unbind()
        {
            _owner = null;
            CommandParameter = null;
            Command = null;
            EventName = null;
        }

        private void Handler(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(new CommandEventArgs()
                {
                    CommandParameter=CommandParameter,
                    EventArgs=e,
                    sender=sender
                });
        }
        private void SetUpHandler()
        {
            if (_currentEvent != null)
            {
                _currentEvent.RemoveEventHandler(Owner, _currentDelegate);
                _currentEvent = null;
                _currentDelegate = null;
            }

            if (Owner != null && !string.IsNullOrEmpty(EventName))
            {
                _currentEvent = GetEventInfo(EventName);
                if (_currentEvent == null)
                    throw new Exception("Cannot find event: " + EventName);
                _currentDelegate = GetDelegate();
                _currentEvent.AddEventHandler(Owner, _currentDelegate);
            }
        }
        private EventInfo GetEventInfo(string eventName)
        {
            Type t = _owner.GetType();
            EventInfo ev = t.GetEvent(eventName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return ev;
        }
        private Delegate GetDelegate()
        {
            EventInfo ev = GetEventInfo(EventName);
            return Delegate.CreateDelegate(ev.EventHandlerType, this, this.GetType()
                .GetMethod("Handler", BindingFlags.Instance | BindingFlags.NonPublic), true);
        }

        private void SetUpBindings()
        {
            if (Owner != null)
            {
                if (Owner.Parent == null)
                {
                    ((FrameworkElement)Owner).Loaded += delegate { SetUpBindings(); };
                    return;
                }
                CompensateElementBinding(CommandParameterProperty);
                CompensateElementBinding(CommandProperty);
                CompensateElementBinding(EventNameProperty);
            }
        }
        private void CompensateElementBinding(DependencyProperty property)
        {
            BindingExpression bindingExpression = this.GetBindingExpression(property);
            if (bindingExpression == null)
                return;

            Binding binding = bindingExpression.ParentBinding;
            if (binding.ElementName != null)
            {
                FrameworkElement rootVisualElement = GetRootElement();
                string path = binding.Path.Path;
                string sourceName = binding.ElementName;
                object source = rootVisualElement.FindName(sourceName); ;
                this.SetBinding(property, new System.Windows.Data.Binding(path) { Source = source });
            }
        }
        private FrameworkElement GetRootElement()
        {
            if (Owner == null)
                return null;

            FrameworkElement parent = (FrameworkElement)Owner.Parent;
            while (parent.Parent != null)
            {
                parent = (FrameworkElement)parent.Parent;
            }

            return parent;
        }

        internal FrameworkElement Owner
        {
            get { return _owner; }
            set
            {
                if (_owner == value)
                    return;
                _owner = value;
                SetUpBindings();
            }
        }
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }
    }
}
