namespace Alexandria.Client.Commands
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class AddToQueue : ICommand
    {
        private readonly ApplicationModel applicationModel;
        private readonly IServiceBus bus;

        public AddToQueue(IServiceBus bus, ApplicationModel applicationModel)
        {
            this.bus = bus;
            this.applicationModel = applicationModel;
        }

        public void Execute(object parameter)
        {
            var book = (BookModel) parameter;

            var queue = applicationModel.MyQueue.Queue;

            if (queue.Any(x => x.Id == book.Id) == false) // avoid adding twice
                queue.Add(book);
            bus.Send(
                new AddBookToQueue
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

        public bool CanExecute(object parameter)
        {
            return (parameter is BookModel);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}