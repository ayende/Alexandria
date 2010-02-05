namespace Alexandria.Backend.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class UpdateDetailsConsumer : ConsumerOf<UpdateDetailsRequest>
    {
        private readonly IServiceBus bus;

        public UpdateDetailsConsumer(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Consume(UpdateDetailsRequest message)
        {
            bus.Reply(new UpdateDetailsResponse
                          {
                              Success = true
                          });
        }
    }
}