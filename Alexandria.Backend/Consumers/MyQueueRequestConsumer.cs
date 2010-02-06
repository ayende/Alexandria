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
			var user =
				session.CreateQuery(@"from User u join fetch u.Queue where u.Id = :id")
					.SetParameter("id", message.UserId)
					.SetResultTransformer(Transformers.DistinctRootEntity)
					.UniqueResult<User>();

			bus.Reply(new MyQueueResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Queue = user.Queue.Select(book => new BookDTO
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