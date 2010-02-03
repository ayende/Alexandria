using System.Linq;
using Alexandria.Messages;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Client.Infrastructure
{
	public class CachingMessageModule : IMessageModule
	{
		private IServiceBus bus;
		private readonly ICache cache;

		public CachingMessageModule(ICache cache)
		{
			this.cache = cache;
		}


		public void Init(ITransport transport, IServiceBus serviceBus)
		{
			bus = serviceBus;
			transport.MessageSent += TransportOnMessageSent;
			transport.MessageArrived += TransportOnMessageArrived;
		}


		public void Stop(ITransport transport, IServiceBus serviceBus)
		{
			transport.MessageSent -= TransportOnMessageSent;
			transport.MessageArrived -= TransportOnMessageArrived;
		}

		private bool TransportOnMessageArrived(CurrentMessageInformation currentMessageInformation)
		{
			var cachableResponse = currentMessageInformation.Message as ICachableResponse;
			if (cachableResponse == null)
				return false;

			var alreadyInCache = cache.Get(cachableResponse.Key);
			if (alreadyInCache == null || alreadyInCache.Timestamp < cachableResponse.Timestamp)
				cache.Put(cachableResponse.Key, cachableResponse.Timestamp, cachableResponse);

			return false;
		}


		private void TransportOnMessageSent(CurrentMessageInformation currentMessageInformation)
		{
			var responses =
				from msg in currentMessageInformation.AllMessages.OfType<ICachableRequest>()
				let response = cache.Get(msg.Key)
				where response != null
				select response.Value;

			var array = responses.ToArray();
			if (array.Length == 0)
				return;
			bus.ConsumeMessages(array);
		}
	}
}