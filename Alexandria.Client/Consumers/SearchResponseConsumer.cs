using System;
using Alexandria.Messages;
using Rhino.ServiceBus;
using Alexandria.Client.Infrastructure;

namespace Alexandria.Client.Consumers
{
	public class SearchResponseConsumer : ConsumerOf<SearchResponse>
	{
		private readonly ApplicationModel applicationModel;

		public SearchResponseConsumer(ApplicationModel applicationModel)
		{
			this.applicationModel = applicationModel;
		}

		public void Consume(SearchResponse message)
		{
			applicationModel.SearchResults.UpdateFrom(message.SearchResults);
		}
	}
}