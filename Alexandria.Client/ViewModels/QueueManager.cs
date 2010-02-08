namespace Alexandria.Client.ViewModels
{
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Screens;
    using Messages;
    using Rhino.ServiceBus;

    public class QueueManager : Screen
    {
        private readonly IServiceBus bus;

        public QueueManager(IServiceBus bus)
        {
            this.bus = bus;

            Queue = new BindableCollection<BookModel>();
        }

        public BindableCollection<BookModel> Queue { get; set; }

        public void MoveForwardInQueue(BookModel bookModel)
        {
            ExecuteQueueReorder(bookModel, -1);
        }

        public void MoveBackInQueue(BookModel bookModel)
        {
            ExecuteQueueReorder(bookModel, 1);
        }

        private void ExecuteQueueReorder(BookModel bookModel, int delta)
        {
            var oldIndex = Queue.IndexOf(bookModel);
            var newIndex = oldIndex + delta;
            Queue.Move(oldIndex, newIndex);

            bus.Send(
                        new ChangeBookPositionInQueue
                        {
                            UserId = Context.CurrentUserId,
                            BookId = bookModel.Id,
                            NewPosition = newIndex
                        },
                        new MyQueueRequest
                        {
                            UserId = Context.CurrentUserId
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

        public void RemoveFromQueue(BookModel book)
        {
            Queue.Remove(book);

            bus.Send(
                new RemoveBookFromQueue
                    {
                        UserId = Context.CurrentUserId,
                        BookId = book.Id
                    },
                new MyQueueRequest
                    {
                        UserId = Context.CurrentUserId
                    },
                new MyRecommendationsRequest
                    {
                        UserId = Context.CurrentUserId
                    });
        }
    }
}