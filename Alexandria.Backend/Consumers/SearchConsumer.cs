namespace Alexandria.Backend.Consumers
{
    using System.Linq;
    using Messages;
    using Model;
    using NHibernate;
    using NHibernate.Transform;
    using Rhino.ServiceBus;

    public class SearchConsumer : ConsumerOf<SearchRequest>
    {
        private readonly IServiceBus bus;

        private readonly ISession session;

        public SearchConsumer(ISession session, IServiceBus bus)
        {
            this.session = session;
            this.bus = bus;
        }

        public void Consume(SearchRequest message)
        {
            var books =
                session.CreateQuery(
                    @"select b from Book b join fetch b.Authors
						where lower(b.Name) like :query")
                    .SetParameter("query", string.Format("%{0}%", message.Query))
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Book>();

            bus.Reply(new SearchResponse
                          {
                              Books = books.Select(book => new BookDTO
                                                               {
                                                                   Id = book.Id,
                                                                   ImageUrl = book.ImageUrl,
                                                                   Name = book.Name,
                                                                   Authors =
                                                                       book.Authors.Select(x => x.Name).
                                                                       ToArray()
                                                               }).ToArray()
                          });
        }
    }
}