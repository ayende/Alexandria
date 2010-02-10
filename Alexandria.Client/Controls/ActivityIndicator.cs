namespace Alexandria.Client.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public class ActivityIndicator : ContentControl
    {
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive",
            typeof (bool),
            typeof (ActivityIndicator),
            new PropertyMetadata(false, WhenIsActiveSet));

        public ActivityIndicator()
        {
            Visibility = Visibility.Hidden;
        }

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void WhenIsActiveSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ActivityIndicator) d;
            c.Visibility = c.IsActive
                               ? Visibility.Visible
                               : Visibility.Hidden;
        }
    }
}