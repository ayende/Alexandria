namespace Alexandria.Client
{
    using System.Linq;
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Filters;
    using Caliburn.PresentationFramework.Screens;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class ApplicationModel : Screen
    {
        private readonly IServiceBus bus;
        private bool displaySearchResults;
        private bool isCurrentlySearching;
        private SubscriptionDetails subscriptionDetails;
        private int userId = 1;
        private string searchText;

        public ApplicationModel(IServiceBus bus)
        {
            this.bus = bus;

            MyBooks = new BindableCollection<BookModel>();
            Queue = new BindableCollection<BookModel>();
            Recommendations = new BindableCollection<BookModel>();
            SearchResults = new BindableCollection<BookModel>();
            subscriptionDetails = new SubscriptionDetails(bus, userId);
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

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                NotifyOfPropertyChange(() => SearchText);
                NotifyOfPropertyChange(() => CanSearch);
            }
        }

        public bool IsCurrentlySearching
        {
            get { return isCurrentlySearching; }
            set
            {
                isCurrentlySearching = value;
                NotifyOfPropertyChange(() => IsCurrentlySearching);
                NotifyOfPropertyChange(() => CanSearch);
            }
        }

        public bool DisplaySearchResults
        {
            get { return displaySearchResults; }
            set
            {
                displaySearchResults = value;
                NotifyOfPropertyChange(() => DisplaySearchResults);
            }
        }

        public bool CanSearch
        {
            get { return !isCurrentlySearching && !string.IsNullOrEmpty(SearchText); }
        }

        public void Search()
        {
            IsCurrentlySearching = true;
            SearchResults.Clear();

            bus.Send(
                new SearchQuery
                {
                    Search = SearchText,
                    UserId = userId
                });
        }

        [AutoCheckAvailability]
        public void MoveForwardInQueue(BookModel book)
        {
            var currentIndex = Queue.IndexOf(book);
            ExecuteQueueReorder(currentIndex, currentIndex - 1);
        }

        [AutoCheckAvailability]
        public void MoveBackInQueue(BookModel book)
        {
            var currentIndex = Queue.IndexOf(book);
            ExecuteQueueReorder(currentIndex, currentIndex + 1);
        }

        private void ExecuteQueueReorder(int oldIndex, int newIndex)
        {
            var bookModel = Queue[oldIndex];
            Queue.Move(oldIndex, newIndex);

            bus.Send(
                new ChangeBookPositionInQueue
                {
                    UserId = userId,
                    BookId = bookModel.Id,
                    NewPosition = newIndex
                },
                new MyQueueQuery
                {
                    UserId = userId
                });
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
            Recommendations.Remove(book);
            if (Queue.Any(x => x.Id == book.Id) == false) // avoid adding twice
                Queue.Add(book);

            bus.Send(
                new AddBookToQueue
                {
                    UserId = userId,
                    BookId = book.Id
                },
                new MyQueueQuery
                {
                    UserId = userId
                },
                new MyRecommendationsQuery
                {
                    UserId = userId
                });
        }

        public void HideSearchResults()
        {
            DisplaySearchResults = false;
        }

        public void RemoveFromQueue(BookModel book)
        {
            Queue.Remove(book);

            bus.Send(
                new RemoveBookFromQueue
                {
                    UserId = userId,
                    BookId = book.Id
                },
                new MyQueueQuery
                {
                    UserId = userId
                },
                new MyRecommendationsQuery
                {
                    UserId = userId
                });
        }

        protected override void OnInitialize()
        {
            bus.Send(
                new MyBooksQuery
                {
                    UserId = userId
                },
                new MyQueueQuery
                {
                    UserId = userId
                },
                new MyRecommendationsQuery
                {
                    UserId = userId
                },
                new SubscriptionDetailsQuery
                {
                    UserId = userId
                });
        }
    }
}