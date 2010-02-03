using System.Windows.Controls;

namespace Alexandria.Client.Controls
{
    using System;
    using System.Windows;

    public class HorizontalItemsControl : ItemsControl
	{
        public HorizontalItemsControl()
        {
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
        }

        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }

        protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }
	}

    public class BindingPanel:StackPanel
    {
        public BindingPanel()
        {
            Orientation = Orientation.Horizontal;
        }
        protected override void OnVisualChildrenChanged(System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            var p = visualAdded as ContentPresenter;
            if(p != null)
            {
                p.Loaded += new System.Windows.RoutedEventHandler(p_Loaded);
            }
        }

        void p_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var p = (ContentPresenter)sender;
            var template = p.ContentTemplate;
        }


    }
}