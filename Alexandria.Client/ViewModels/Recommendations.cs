namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Caliburn.PresentationFramework;
    using Commands;

    public class Recommendations : PropertyChangedBase
    {
        private readonly AddToQueueCommand addToQueueCommand;

        public Recommendations(AddToQueueCommand addToQueueCommand)
        {
            this.addToQueueCommand = addToQueueCommand;
            List = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> List { get; set; }

        public void AddToQueue(BookModel bookModel)
        {
            List.Remove(bookModel);
            addToQueueCommand.Execute(bookModel);
        }
    }
}