namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;

    public class MyRecommendationsResponseConsumer : ConsumerOf<MyRecommendationsResponse>
    {
        private readonly ApplicationModel applicationModel;

        public MyRecommendationsResponseConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(MyRecommendationsResponse message)
        {
            //applicationModel.Recommendations.UpdateFrom(message.Recommendations);
        }
    }
}