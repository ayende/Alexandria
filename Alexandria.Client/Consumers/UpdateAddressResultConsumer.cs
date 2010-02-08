namespace Alexandria.Client.Consumers
{
    using Infrastructure;
    using Messages;
    using Rhino.ServiceBus;
    using ViewModels;

    public class UpdateAddressResultConsumer : ConsumerOf<UpdateAddressResult>
    {
        private readonly ApplicationModel applicationModel;

        public UpdateAddressResultConsumer(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
        }

        public void Consume(UpdateAddressResult message)
        {
            if(message.Success)
            {
            	applicationModel.SubscriptionDetails.CompleteEdit();
            }
			else
            {
            	applicationModel.SubscriptionDetails.ErrorEdit(message.ErrorMessage);
            }
        }
    }
}