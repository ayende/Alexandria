namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Caliburn.PresentationFramework;

    public class Recommendations : PropertyChangedBase
    {
        public Recommendations()
        {
            List = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> List { get; set; }
    }
}