namespace Alexandria.Client.Infrastructure
{
    using System.Windows;
    using System.Windows.Media;

    public class RevealWhen : DependencyObject
    {
        public static readonly DependencyProperty HoverOverParentProperty = DependencyProperty.RegisterAttached(
            "HoverOverParent",
            typeof (string),
            typeof (RevealWhen),
            new PropertyMetadata(null, WhenHoverOverParentChanges));

        private static void WhenHoverOverParentChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var child = d as FrameworkElement;
            if (child == null) return;

            var parent = VisualTreeHelper.GetParent(child) as FrameworkElement;
            if (parent == null) return;

            child.Visibility = Visibility.Hidden;

            parent.MouseEnter += delegate { child.Visibility = Visibility.Visible; };

            parent.MouseLeave += delegate { child.Visibility = Visibility.Hidden; };
        }

        public static string GetHoverOverParent(DependencyObject element)
        {
            return (string) element.GetValue(HoverOverParentProperty);
        }

        public static void SetHoverOverParent(DependencyObject element, object value)
        {
            element.SetValue(HoverOverParentProperty, value);
        }
    }
}