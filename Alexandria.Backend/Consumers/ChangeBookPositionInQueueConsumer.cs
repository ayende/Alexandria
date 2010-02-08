using System;
using Alexandria.Backend.Model;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class ChangeBookPositionInQueueConsumer : ConsumerOf<ChangeBookPositionInQueue>
	{
		private readonly ISession session;

		public ChangeBookPositionInQueueConsumer(ISession session)
		{
			this.session = session;
		}

		public void Consume(ChangeBookPositionInQueue message)
		{
			var user = session.Get<User>(message.UserId);
			var book = session.Get<Book>(message.BookId);

			Console.WriteLine("Changing {0}'s position in {1}'s queue to {2}",
				book.Name, user.Name, message.NewPosition);

			user.ChangePositionInQueue(book, message.NewPosition);
		}
	}
}