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
        private SubscriptionDetails subscriptionDetails;

        public ApplicationModel(IServiceBus bus)
        {
            this.bus = bus;

            MyBooks = new BindableCollection<BookModel>();
            Queue = new BindableCollection<BookModel>();
            Recommendations = new BindableCollection<BookModel>();
            SearchResults = new BindableCollection<BookModel>();
            subscriptionDetails = new SubscriptionDetails(bus);
        }

        public SubscriptionDetails SubscriptionDetails
        {
            get { return subscriptionDetails; }
            set
            {
                subscriptionDetails = value;
                NotifyOfPropertyChange(() => SubscriptionDetails);
            }
        }

        public BindableCollection<BookModel> Queue { get; set; }
        public BindableCollection<BookModel> Recommendations { get; set; }
        public BindableCollection<BookModel> MyBooks { get; set; }
        public BindableCollection<BookModel> SearchResults { get; set; }

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

        public void MoveForwardInQueue(BookModel book)
        {
            var currentIndex = Queue.IndexOf(book);
            ExecuteQueueReorder(currentIndex, currentIndex - 1);
        }

        public void MoveBackInQueue(BookModel book)
        {
            var currentIndex = Queue.IndexOf(book);
            ExecuteQueueReorder(currentIndex, currentIndex + 1);
        }

        private void ExecuteQueueReorder(int oldIndex, int newIndex)
        {
            Queue.Move(oldIndex, newIndex);

            //TODO: //TODO: send reorder msg
        }

        public bool CanMoveForwardInQueue(BookModel book)
        {
            return Queue.IndexOf(book) > 0;
        }

        public bool CanMoveBackInQueue(BookModel book)
        {
            var lastIndex = Queue.Count - 1;
            return Queue.IndexOf(book) < lastIndex;
        }

        public void AddToQueue(BookModel book)
        {
            if (Recommendations.Contains(book))
                Recommendations.Remove(book);

            Queue.Add(book);

            //TODO: send add msg
        }

        public void RemoveFromQueue(BookModel book)
        {
            Queue.Remove(book);

            //TODO: send remove msg
        }
    }
}