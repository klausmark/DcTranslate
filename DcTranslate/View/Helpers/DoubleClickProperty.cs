using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DcTranslate.View.Helpers
{
    public static class DoubleClickProperty
    {
        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommand);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommand, value);
        }

        public static readonly DependencyProperty DoubleClickCommand =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand),
                typeof(DoubleClickProperty), new PropertyMetadata(null, DoubleClickCommandChanged));

        private static void DoubleClickCommandChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var control = (Control) dependencyObject;
            control.MouseDoubleClick += (sender, args) =>
            {
                var command = (ICommand) eventArgs.NewValue;
                if (command.CanExecute(null)) command.Execute(null);
            };
        }
    }
}