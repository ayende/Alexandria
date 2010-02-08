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

        /// <summary>
        /// Pretend that application startup is showing the user some login screen
        /// and that we get the user id from the login process
        /// </summary>
        private const int userId = 1;

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
                        UserId = userId
                    },
                new MyQueueRequest
                    {
                        UserId = userId
                    },
                new MyRecommendationsRequest
                    {
                        UserId = userId
                    },
                new SubscriptionDetailsRequest
                    {
                        UserId = userId
                    });
        }
    }
}