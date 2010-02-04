namespace Alexandria.Client.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class SubscriptionDetailsResponseConsumer : ConsumerOf<SubscriptionDetailsResponse>
    {
        private readonly ApplicationModel applicationModel;

        public SubscriptionDetailsResponseConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(SubscriptionDetailsResponse message)
        {
            applicationModel.SubscriptionDetails.UpdateFrom(message.SubscriptionDetails);
        }
    }
}