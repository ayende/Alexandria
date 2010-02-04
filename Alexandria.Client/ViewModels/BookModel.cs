namespace Alexandria.Client.ViewModels
{
    using System.Windows.Media;
    using Caliburn.Core;

    public class BookModel : PropertyChangedBase
    {
        public string[] Authors { get; set; }
        public ImageSource Image { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}