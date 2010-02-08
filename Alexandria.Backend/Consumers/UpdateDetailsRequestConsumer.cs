namespace Alexandria.Backend.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class UpdateDetailsRequestConsumer : ConsumerOf<UpdateDetailsRequest>
    {
        private readonly IServiceBus bus;

        public UpdateDetailsRequestConsumer(IServiceBus bus)
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