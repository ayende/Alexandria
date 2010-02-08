using Alexandria.Client.Infrastructure;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client.Consumers
{
    public class MyQueueResponseConsumer : ConsumerOf<MyQueueResponse>
    {
        private readonly ApplicationModel applicationModel;

        public MyQueueResponseConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(MyQueueResponse message)
        {
            applicationModel.MyQueue.Queue.UpdateFrom(message.Queue);
        }
    }
}