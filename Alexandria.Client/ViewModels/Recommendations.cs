namespace Alexandria.Client.ViewModels
{
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Screens;

    public class Recommendations : Screen
    {
        public Recommendations()
        {
            List = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> List { get; set; }
    }
}