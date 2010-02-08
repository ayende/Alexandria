namespace Alexandria.Client.ViewModels
{
    using Caliburn.PresentationFramework;
    using Caliburn.PresentationFramework.Screens;
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