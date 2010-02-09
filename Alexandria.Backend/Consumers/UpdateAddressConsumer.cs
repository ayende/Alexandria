using Alexandria.Backend.Model;
using NHibernate;

namespace Alexandria.Backend.Consumers
{
    using Messages;
    using Rhino.ServiceBus;

    public class UpdateAddressConsumer : ConsumerOf<UpdateAddress>
    {
        private readonly IServiceBus bus;
        private readonly ISession session;

        public UpdateAddressConsumer(IServiceBus bus, ISession session)
        {
            this.bus = bus;
            this.session = session;
        }

        public void Consume(UpdateAddress message)
        {
            int result;
            // pretend we call some address validation service
            if (int.TryParse(message.Details.HouseNumber, out result) == false || result % 2 == 0)
            {
                bus.Reply(new UpdateAddressResult
                {
                    Success = false,
                    ErrorMessage = "House number must be odd number",
                    UserId = message.UserId
                });
            }
            else
            {
                var user = session.Get<User>(message.UserId);
                user.ChangeAddress(message.Details.Street, message.Details.HouseNumber,
                                   message.Details.City, message.Details.Country, 
                                   message.Details.ZipCode);
                bus.Reply(new UpdateAddressResult
                {
                    Success = true,
                    UserId = message.UserId
                });
            }
        }
    }
}