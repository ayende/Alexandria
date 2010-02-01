using System;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client.Consumers
{
	public class SubscriptionDetailsResponseConsumer : ConsumerOf<SubscriptionDetailsResponse>
	{
		private readonly ApplicationModel applicationModel;

		public SubscriptionDetailsResponseConsumer(ApplicationModel applicationModel)
		{
			this.applicationModel = applicationModel;
		}

		public void Consume(SubscriptionDetailsResponse message)
		{
			applicationModel.UpdateInUIThread(() =>
			{
				applicationModel.SubscriptionDetails.UpdateFrom(message.SubscriptionDetails);
			});
		}
	}
}