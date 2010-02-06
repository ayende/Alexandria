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
	public class MyBooksRequestConsumer : ConsumerOf<MyBooksRequest>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public MyBooksRequestConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}

		public void Consume(MyBooksRequest message)
		{
			var user =
				session.CreateQuery(@"from User u join fetch u.CurrentlyReading where u.Id = :id")
					.SetParameter("id", message.UserId)
					.SetResultTransformer(Transformers.DistinctRootEntity)
					.UniqueResult<User>();

			bus.Reply(new MyBooksResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Books = user.CurrentlyReading.ToBookDtoArray()
			});
		}
	}
}