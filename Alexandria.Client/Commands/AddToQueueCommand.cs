namespace Alexandria.Client.Commands
{
    using System.Linq;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class AddToQueueCommand
    {
        private readonly IServiceBus bus;
        private readonly QueueManager queueManager;

        public AddToQueueCommand(IServiceBus bus, QueueManager queueManager)
        {
            this.bus = bus;
            this.queueManager = queueManager;
        }

        public void Execute(BookModel bookModel)
        {
            var queue = queueManager.Queue;

            if (queue.Any(x => x.Id == bookModel.Id) == false) // avoid adding twice
                queue.Add(bookModel);

            bus.Send(
                new AddBookToQueue
                    {
                        UserId = Context.CurrentUserId,
                        BookId = bookModel.Id
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