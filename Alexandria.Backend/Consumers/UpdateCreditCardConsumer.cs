namespace Alexandria.Backend.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class UpdateCreditCardConsumer : ConsumerOf<UpdateCreditCardRequest>
    {
        private readonly IServiceBus bus;

        public UpdateCreditCardConsumer(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Consume(UpdateCreditCardRequest message)
        {
            bus.Reply(new UpdateCreditCardResponse
                          {
                              Success = true
                          });
        }
    }
}