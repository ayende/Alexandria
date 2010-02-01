using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;
using Alexandria.Client.Infrastructure;

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
			applicationModel.UpdateInUIThread(() => applicationModel.Queue.UpdateFrom(message.Queue));
		}
	}
}