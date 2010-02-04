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

        public static readonly DependencyProperty ViewModeIsProperty = DependencyProperty.RegisterAttached(
            "ViewModeIs",
            typeof (ViewMode),
            typeof (RevealWhen),
            new PropertyMetadata(ViewMode.Retrieving, WhenViewModeIsChanges));

        private static void WhenViewModeIsChanges(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var supporter = d as ISupportsViewMode;
            if (supporter == null) return;

            var element = d as FrameworkElement;
            if (element == null) return;

            supporter.PropertyChanged += (sender, args) =>
                                             {
                                                 if (args.PropertyName == "ViewMode")
                                                 {
                                                     var target = GetViewModeIs(element);
                                                     element.Visibility = (supporter.ViewMode == target)
                                                                              ? Visibility.Visible
                                                                              : Visibility.Hidden;
                                                 }
                                             };
        }

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

        public static ViewMode GetViewModeIs(DependencyObject element)
        {
            return (ViewMode) element.GetValue(ViewModeIsProperty);
        }

        public static void SetViewModeIs(DependencyObject element, ViewMode value)
        {
            element.SetValue(ViewModeIsProperty, value);
        }
    }
}