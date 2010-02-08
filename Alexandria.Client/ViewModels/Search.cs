namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Caliburn.PresentationFramework;
    using Messages;
    using Rhino.ServiceBus;

    public class Search : PropertyChangedBase
    {
        private readonly IServiceBus bus;
        private bool isSearching;

        public Search(IServiceBus bus)
        {
            this.bus = bus;
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
    }
}