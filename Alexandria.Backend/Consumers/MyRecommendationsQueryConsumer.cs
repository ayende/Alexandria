using System;
using System.Linq;
using Alexandria.Backend.Model;
using Alexandria.Backend.Util;
using Alexandria.Messages;
using NHibernate;
using NHibernate.Transform;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class MyRecommendationsQueryConsumer : ConsumerOf<MyRecommendationsQuery>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public MyRecommendationsQueryConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}
		public void Consume(MyRecommendationsQuery message)
		{
			var user = session.Get<User>(message.UserId);

			Console.WriteLine("{0}'s has {1} book recommendations",
				user.Name, user.Recommendations.Count);

			bus.Reply(new MyRecommendationsResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Recommendations = user.Recommendations.ToBookDtoArray()
			});
		}
	}
}