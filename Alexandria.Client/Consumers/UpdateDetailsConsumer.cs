namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class UpdateDetailsConsumer : ConsumerOf<UpdateDetailsResponse>
    {
        private readonly ApplicationModel applicationModel;

        public UpdateDetailsConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(UpdateDetailsResponse message)
        {
            if(message.Success)
            {
                applicationModel.SubscriptionDetails.Details = applicationModel.SubscriptionDetails.Editable;
                applicationModel.SubscriptionDetails.Editable = new ContactInfo();
            }

            applicationModel.SubscriptionDetails.ViewMode =
                message.Success
                    ? ViewMode.Confirmed
                    : ViewMode.Error;
        }
    }
}