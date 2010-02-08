namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class SearchConsumer : ConsumerOf<SearchResponse>
    {
        private readonly ApplicationModel applicationModel;

        public SearchConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(SearchResponse message)
        {
            applicationModel.Search.Results.UpdateFrom(message.Books);
            applicationModel.PotentialBooks = applicationModel.Search;
        }
    }
}