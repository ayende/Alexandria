namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class UpdateDetailsResultConsumer : ConsumerOf<UpdateDetailsResult>
    {
        private readonly ApplicationModel applicationModel;

        public UpdateDetailsResultConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(UpdateDetailsResult message)
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