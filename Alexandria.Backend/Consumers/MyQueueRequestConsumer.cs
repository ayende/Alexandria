using System;
using System.Linq;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using NHibernate.Transform;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class MyQueueRequestConsumer : ConsumerOf<MyQueueRequest>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public MyQueueRequestConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}
		public void Consume(MyQueueRequest message)
		{
			var books =
				session.CreateQuery(
				                   	@"select b from Book b join fetch b.Authors 
						where b.Id in (select r from User u join u.Queue r where u.Id = :id)")
					.SetParameter("id", message.UserId)
					.SetResultTransformer(Transformers.DistinctRootEntity)
					.List<Book>();

			bus.Reply(new MyQueueResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Queue = books.Select(book => new BookDTO
				{
					Id = book.Id,
					Image = book.Image,
					Name = book.Name,
					Authors = book.Authors.Select(x => x.Name).ToArray()
				}).ToArray()
			});
		}
	}
}