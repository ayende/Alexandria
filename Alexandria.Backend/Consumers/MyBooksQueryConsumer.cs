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
	public class MyBooksQueryConsumer : ConsumerOf<MyBooksQuery>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public MyBooksQueryConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}

		public void Consume(MyBooksQuery message)
		{
			var user = session.Get<User>(message.UserId);
			
			Console.WriteLine("{0}'s has {1} books at home", 
				user.Name, user.CurrentlyReading.Count);

			bus.Reply(new MyBooksResponse
			{
				UserId = message.UserId,
				Timestamp = DateTime.Now,
				Books = user.CurrentlyReading.ToBookDtoArray()
			});
		}
	}
}