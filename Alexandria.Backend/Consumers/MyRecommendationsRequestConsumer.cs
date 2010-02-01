using System.Linq;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class MyRecommendationsRequestConsumer: ConsumerOf<MyRecommendationsRequest>
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
			var books =
				session.CreateQuery("select b from User u join u.Recommendations b join fetch b.Authors where u.Id = :id")
					.SetParameter("id", message.UserId)
					.List<Book>();

			bus.Reply(new MyRecommendationsResponse
			{
				Recommendations = books.Select(book => new BookDTO
				{
					Id = book.Id,
					ImageUrl = book.ImageUrl,
					Name = book.Name,
					Authors = book.Authors.Select(x => x.Name).ToArray()
				}).ToArray()
			});
		}
	}
}