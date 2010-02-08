namespace Alexandria.Client
{
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Screens;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class ApplicationModel : Screen
    {
        private readonly IServiceBus bus;
        private Screen potentialBooks;

        public ApplicationModel(IServiceBus bus)
        {
            this.bus = bus;

            Search = new Search(bus);
            MyQueue = new QueueManager(bus);
            SubscriptionDetails = new SubscriptionDetails(bus);
            Recommendations = new Recommendations();

            PotentialBooks = Recommendations;

            MyBooks = new BindableCollection<BookModel>();
        }

        public SubscriptionDetails SubscriptionDetails { get; set; }
        public QueueManager MyQueue { get; set; }
        public Search Search { get; set; }
        public Recommendations Recommendations { get; set; }

        public Screen PotentialBooks
        {
            get { return potentialBooks; }
            set
            {
                potentialBooks = value;
                NotifyOfPropertyChange(() => PotentialBooks);
            }
        }

        public BindableCollection<BookModel> MyBooks { get; set; }

        public void CloseSearchResults()
        {
            PotentialBooks = Recommendations;
        }

        protected override void OnInitialize()
        {
            bus.Send(
                new MyBooksRequest
                    {
                        UserId = 1
                    },
                new MyQueueRequest
                    {
                        UserId = 1
                    },
                new MyRecommendationsRequest
                    {
                        UserId = 1
                    },
                new SubscriptionDetailsRequest
                    {
                        UserId = 1
                    });
        }
    }
}