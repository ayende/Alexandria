namespace Alexandria.Client.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;

    public class BookSummary : ContentControl
    {
        public static readonly DependencyProperty BookModelProperty = DependencyProperty.Register(
            "BookModel",
            typeof (BookModel),
            typeof (BookSummary),
            new PropertyMetadata(null));

        public BookModel BookModel
        {
            get { return (BookModel) GetValue(BookModelProperty); }
            set { SetValue(BookModelProperty, value); }
        }
    }
}