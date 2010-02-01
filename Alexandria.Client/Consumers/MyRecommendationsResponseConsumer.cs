using Alexandria.Client.Infrastructure;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client.Consumers
{
	public class MyRecommendationsResponseConsumer : ConsumerOf<MyRecommendationsResponse>
	{
		private readonly ApplicationModel applicationModel;

		public MyRecommendationsResponseConsumer(ApplicationModel applicationModel)
		{
			this.applicationModel = applicationModel;
		}

		public void Consume(MyRecommendationsResponse message)
		{
			applicationModel.UpdateInUIThread(() => applicationModel.Recommendations.UpdateFrom(message.Recommendations));
		}
	}
}