using System;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class RemoveBookFromQueueConsumer : ConsumerOf<RemoveBookFromQueue>
	{
		private readonly ISession session;

		public RemoveBookFromQueueConsumer(ISession session)
		{
			this.session = session;
		}

		public void Consume(RemoveBookFromQueue message)
		{
			var user = session.Get<User>(message.UserId);
			var book = session.Get<Book>(message.BookId);

			Console.WriteLine("Removing {0} from {1}'s queue",
				book.Name, user.Name);

			user.RemoveFromQueue(book);
		}
	}
}