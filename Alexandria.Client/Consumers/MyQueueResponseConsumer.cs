namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class MyQueueResponseConsumer : ConsumerOf<MyQueueResponse>
    {
        private readonly ApplicationModel applicationModel;

        public MyQueueResponseConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(MyQueueResponse message)
        {
            applicationModel.Queue.UpdateFrom(message.Queue);
        }
    }
}