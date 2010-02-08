namespace Alexandria.Backend.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class UpdateDetailsConsumer : ConsumerOf<UpdateDetails>
    {
        private readonly IServiceBus bus;

        public UpdateDetailsConsumer(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Consume(UpdateDetails message)
        {
        	bus.Reply(new UpdateDetailsResult
        	{
        		Success = false,
				ErrorMessage = "you don't live in a place we like",
        		UserId = message.UserId
        	});
        }
    }
}