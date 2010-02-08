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
			var user = session.Get<User>(message.UserId);

			Console.WriteLine("{0}'s has {1} books queued for reading",
				user.Name, user.Queue.Count);

			bus.Reply(new MyQueueResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Queue = user.Queue.ToBookDtoArray()
			});
		}
	}
}