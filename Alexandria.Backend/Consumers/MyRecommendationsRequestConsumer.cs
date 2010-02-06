using System;
using System.Linq;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using NHibernate.Transform;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class MyRecommendationsRequestConsumer : ConsumerOf<MyRecommendationsRequest>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public MyRecommendationsRequestConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}
		public void Consume(MyRecommendationsRequest message)
		{
			var user =
				session.CreateQuery(@"from User u join fetch u.Recommendations where u.Id = :id")
					.SetParameter("id", message.UserId)
					.SetResultTransformer(Transformers.DistinctRootEntity)
					.UniqueResult<User>();

			bus.Reply(new MyRecommendationsResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Recommendations = user.Recommendations.Select(book => new BookDTO
				{
					Id = book.Id,
					Image = book.Image,
					Name = book.Name,
					Author = book.Author
				}).ToArray()
			});
		}
	}
}