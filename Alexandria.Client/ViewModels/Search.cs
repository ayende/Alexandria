namespace Alexandria.Client.ViewModels
{
    using Caliburn.Core;
    using Caliburn.PresentationFramework;
    using Messages;
    using Rhino.ServiceBus;

    public class Search : PropertyChangedBase
    {
        private readonly IServiceBus bus;

        public Search(IServiceBus bus)
        {
            this.bus = bus;
            Results = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> Results { get; private set; }

        public void FetchResultsFor(string query)
        {
            bus.Send(new SearchRequest
                         {
                             UserId = 1,
                             Query = query
                         });
        }
    }
}