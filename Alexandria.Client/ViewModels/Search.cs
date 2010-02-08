namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Caliburn.PresentationFramework;
    using Commands;
    using Messages;
    using Rhino.ServiceBus;

    public class Search : PropertyChangedBase
    {
        private readonly IServiceBus bus;
        private readonly AddToQueueCommand addToQueueCommand;
        private bool isSearching;

        public Search(IServiceBus bus, AddToQueueCommand addToQueueCommand)
        {
            this.bus = bus;
            this.addToQueueCommand = addToQueueCommand;

            Results = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> Results { get; private set; }

        public bool IsSearching
        {
            get { return isSearching; }
            set
            {
                isSearching = value;
                NotifyOfPropertyChange(() => IsSearching);
            }
        }

        public void FetchResultsFor(string query)
        {
            IsSearching = true;

            bus.Send(new SearchRequest
                         {
                             UserId = Context.CurrentUserId,
                             Search = query
                         });
        }

        public void AddToQueue(BookModel bookModel)
        {
            Results.Remove(bookModel);
            addToQueueCommand.Execute(bookModel);
        }
    }
}