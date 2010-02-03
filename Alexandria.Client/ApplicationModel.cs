namespace Alexandria.Client
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Threading;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class ApplicationModel : INotifyPropertyChanged
    {
        private readonly IServiceBus bus;
        private readonly Dispatcher dispatcher;

        private SubscriptionDetails subscriptionDetails;

        public ApplicationModel(Dispatcher dispatcher, IServiceBus bus)
        {
            this.dispatcher = dispatcher;
            this.bus = bus;
            MyBooks = new ObservableCollection<BookModel>();
            Queue = new ObservableCollection<BookModel>();
            Recommendations = new ObservableCollection<BookModel>();
            SearchResults = new ObservableCollection<BookModel>();
            subscriptionDetails = new SubscriptionDetails();
        }

        public SubscriptionDetails SubscriptionDetails
        {
            get { return subscriptionDetails; }
            set
            {
                subscriptionDetails = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SubscriptionDetails"));
            }
        }

        public ObservableCollection<BookModel> Queue { get; set; }
        public ObservableCollection<BookModel> Recommendations { get; set; }
        public ObservableCollection<BookModel> MyBooks { get; set; }
        public ObservableCollection<BookModel> SearchResults { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void Init()
        {
            this.bus.Send(
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

        public void UpdateInUIThread(Action action)
        {
            dispatcher.BeginInvoke(action);
        }

        public void ReorderQueue()
        {
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