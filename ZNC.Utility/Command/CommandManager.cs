using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ZNC.Utility.Command
{
    public class CommandManager
    {
        #region DependencyProperty declarations
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
                typeof(ICommand),
                typeof(CommandManager),
                new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));

        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.RegisterAttached("EventName",
                typeof(String),
                typeof(CommandManager),
                new PropertyMetadata(new PropertyChangedCallback(OnEventNameChanged)));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
                typeof(object),
                typeof(CommandManager),
                new PropertyMetadata(new PropertyChangedCallback(OnCommandParameterChanged)));

        public static readonly DependencyProperty CommandsProperty =
            DependencyProperty.RegisterAttached("Commands",
                typeof(CommandGroup),
                typeof(CommandManager),
                new PropertyMetadata(new CommandGroup(), new PropertyChangedCallback(OnCommandsChanged)));

        private static DependencyProperty ContextProperty =
            DependencyProperty.Register("Context",
                                        typeof(object),
                                        typeof(FrameworkElement),
                                        new PropertyMetadata(null, new PropertyChangedCallback(OnContextChanged)));

        private static DependencyProperty DefaultCommandProperty =
            DependencyProperty.Register("DefaultCommand",
                                        typeof(CommandBinding),
                                        typeof(FrameworkElement),
                                        new PropertyMetadata(null));

        #endregion

        #region Dependency Property Getters and Setters
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }
        public static void SetCommand(DependencyObject obj, ICommand command)
        {
            obj.SetValue(CommandProperty, command);
        }

        public static string GetEventName(DependencyObject obj)
        {
            return (string)obj.GetValue(EventNameProperty);
        }
        public static void SetEventName(DependencyObject obj, string eventName)
        {
            obj.SetValue(EventNameProperty, eventName);
        }

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(DependencyObject obj, object commandParameter)
        {
            obj.SetValue(CommandParameterProperty, commandParameter);
        }

        public static CommandGroup GetCommands(DependencyObject obj)
        {
            return (CommandGroup)obj.GetValue(CommandsProperty);
        }
        public static void SetCommands(DependencyObject obj, CommandGroup commands)
        {
            obj.SetValue(CommandsProperty, commands);
        }
        #endregion

        #region DependencyProperty Callbacks
        public static void OnContextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CommandGroup cmds = ((FrameworkElement)obj).GetValue(CommandsProperty) as CommandGroup;
            if (cmds != null)
            {
                cmds.Children.ForEach(c => c.DataContext = e.NewValue);
            }
        }
        static void OnCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            GetDefaultCommandBinding((FrameworkElement)obj).Command = (ICommand)e.NewValue;
        }
        static void OnEventNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            GetDefaultCommandBinding((FrameworkElement)obj).EventName = (string)e.NewValue;
        }
        static void OnCommandParameterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            GetDefaultCommandBinding((FrameworkElement)obj).CommandParameter = e.NewValue;
        }
        static void OnCommandsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = obj as FrameworkElement;
            CommandGroup cmds = e.OldValue as CommandGroup;
            if (cmds != null)
            {
                cmds.Children.ForEach(b => b.Unbind());
                element.SetValue(ContextProperty, null);
            }

            cmds = e.NewValue as CommandGroup;
            if (cmds != null)
            {
                cmds.Children.ForEach(b => b.Owner = element);
                element.SetBinding(ContextProperty, new Binding());
            }
        }
        #endregion

        #region Private Methods
        static CommandBinding GetDefaultCommandBinding(FrameworkElement obj)
        {
            CommandBinding binding = obj.GetValue(DefaultCommandProperty) as CommandBinding;
            if (binding == null)
            {
                binding = new CommandBinding() { Owner = obj };
                obj.SetValue(DefaultCommandProperty, binding);
            }
            return binding;
        }
        #endregion
    }
}
