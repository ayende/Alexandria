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
            	applicationModel.SubscriptionDetails.CompleteEdit();
            }
			else
            {
            	applicationModel.SubscriptionDetails.ErrorEdit(message.ErrorMessage);
            }
        }
    }
}