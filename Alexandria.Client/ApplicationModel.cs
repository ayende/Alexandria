namespace Alexandria.Client
{
    using System.ComponentModel;
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Screens;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class ApplicationModel : Screen
    {
        private readonly IServiceBus bus;
        private INotifyPropertyChanged potentialBooks;

        public ApplicationModel(IServiceBus bus)
        {
            this.bus = bus;

            MyBooks = new BindableCollection<BookModel>();

            Search = new Search(bus);
            MyQueue = new QueueManager(bus);
            SubscriptionDetails = new SubscriptionDetails(bus);
            Recommendations = new Recommendations();

            PotentialBooks = Recommendations;
        }

        public SubscriptionDetails SubscriptionDetails { get; set; }
        public QueueManager MyQueue { get; set; }
        public Search Search { get; set; }
        public Recommendations Recommendations { get; set; }

        public INotifyPropertyChanged PotentialBooks
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
                new SubscriptionDetailsRequest
                    {
                        UserId = Context.CurrentUserId
                    },
                new MyBooksRequest
                    {
                        UserId = Context.CurrentUserId
                    },
                new MyQueueRequest
                    {
                        UserId = Context.CurrentUserId
                    });
        }
    }
}