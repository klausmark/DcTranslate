using System.Windows;
using System.Windows.Controls;

namespace DcTranslate.View.Helpers
{
    public static class ButtonProperties
    {
        public static bool GetDialogResultOk(DependencyObject obj)
        {
            return (bool)obj.GetValue(DialogResultOk);
        }

        public static void SetDialogResultOk(DependencyObject obj, bool value)
        {
            obj.SetValue(DialogResultOk, value);
        }

        public static readonly DependencyProperty DialogResultOk =
            DependencyProperty.RegisterAttached("DialogResultOk", typeof(bool),
                typeof(ButtonProperties), new PropertyMetadata(false, DialogResultOkChanged));

        private static void DialogResultOkChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var button = (Button)dependencyObject;
            button.Click += (sender, args) => Window.GetWindow(button).DialogResult = true;
        }
    }
}