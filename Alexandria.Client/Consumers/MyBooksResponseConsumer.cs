namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class MyBooksResponseConsumer : ConsumerOf<MyBooksResponse>
    {
        private readonly ApplicationModel applicationModel;

        public MyBooksResponseConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(MyBooksResponse message)
        {
            applicationModel.UpdateInUIThread(
                () => applicationModel.MyBooks.UpdateFrom(message.Books)
                );
        }
    }
}