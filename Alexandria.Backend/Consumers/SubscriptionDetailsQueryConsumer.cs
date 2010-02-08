using System;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class SubscriptionDetailsQueryConsumer : ConsumerOf<SubscriptionDetailsQuery>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public SubscriptionDetailsQueryConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}
		
		public void Consume(SubscriptionDetailsQuery message)
		{
			var subscription =
				session.CreateQuery(
				            @"from Subscription s join fetch s.User 
							where :now between s.Start and s.End and s.User.Id = :userId")
					.SetParameter("now", DateTime.Now)
					.SetParameter("userId", message.UserId)
					.UniqueResult<Subscription>();

			bus.Reply(new SubscriptionDetailsResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				SubscriptionDetails = new SubscriptionDetailsDTO
				{
					City = subscription.User.Address.City,
					Country = subscription.User.Address.Country,
					CreditCard = subscription.CreditCard,
					HouseNumber = subscription.User.Address.HouseNumber,
					MonthlyCost = subscription.MonthlyCost,
					Name = subscription.User.Name,
					NumberOfPossibleBooksOut = subscription.NumberOfPossibleBooksOut,
					Street = subscription.User.Address.Street,
					ZipCode = subscription.User.Address.ZipCode
				}
			});
		}
	}
}