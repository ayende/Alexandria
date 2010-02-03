namespace Alexandria.Client.Consumers
{
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class UpdateCreditCardConsumer : ConsumerOf<UpdateCreditCardResponse>
    {
        private readonly ApplicationModel applicationModel;

        public UpdateCreditCardConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(UpdateCreditCardResponse message)
        {
            applicationModel.SubscriptionDetails.EditState =
                message.Success
                    ? EditState.Confirmed
                    : EditState.Error;
        }
    }
}