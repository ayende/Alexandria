namespace Alexandria.Client.Commands
{
    using System;
    using System.Windows.Input;
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
            applicationModel.MyQueue.Queue.Add(book);
        }

        public bool CanExecute(object parameter)
        {
            return (parameter is BookModel);
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}