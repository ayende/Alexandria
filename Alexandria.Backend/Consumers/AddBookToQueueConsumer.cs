using System;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class AddBookToQueueConsumer : ConsumerOf<AddBookToQueue>
	{
		private readonly ISession session;

		public AddBookToQueueConsumer(ISession session)
		{
			this.session = session;
		}

		public void Consume(AddBookToQueue message)
		{
			var user = session.Get<User>(message.UserId);
			var book = session.Get<Book>(message.BookId);

			Console.WriteLine("Adding {0} to {1}'s queue",
				book.Name, user.Name);

			user.AddToQueue(book);
		}
	}
}