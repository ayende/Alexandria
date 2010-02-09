using System;
using Alexandria.Backend.Model;
using Alexandria.Backend.Util;
using Alexandria.Messages;
using NHibernate;
using Rhino.ServiceBus;

namespace Alexandria.Backend.Consumers
{
	public class SearchRequestConsumer : ConsumerOf<SearchQuery>
	{
		private readonly ISession session;
		private readonly IServiceBus bus;

		public SearchRequestConsumer(ISession session, IServiceBus bus)
		{
			this.session = session;
			this.bus = bus;
		}

		public void Consume(SearchQuery message)
		{
			// note: the search implementation shown here is 
			// just a demo and suffers from numerous performance issues
			// a much better approach would be to use NHibernate.Search or 
			// the database's full text indexing

			var books = session.CreateQuery("from Book b where b.Name like :search or b.Author like :search")
				.SetParameter("search", "%" + message.Search +"%")
				.List<Book>();

			Console.WriteLine("User {0} searched for '{1}' and got {2} results",
				message.UserId, message.Search, books.Count);

			// TODO: add search query to user's history, so we can run analysis on things the user likes

			bus.Reply(
			         	new SearchResponse
			         	{
			         		Search = message.Search,
			         		SearchResults = books.ToBookDtoArray(),
			         		Timestamp = DateTime.Now
			         	});
		}
	}
}