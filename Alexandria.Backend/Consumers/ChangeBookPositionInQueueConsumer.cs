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
			var user = session
				.CreateQuery("from User u join fetch u.Queue where u.Id = :id")
				.SetParameter("id", message.UserId)
				.UniqueResult<User>();

			var book = session.Load<Book>(message.BookId);

			user.ChangePositionInQueue(book, message.NewPosition);
		}
	}
}