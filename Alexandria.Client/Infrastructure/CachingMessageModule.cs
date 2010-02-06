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
			var containsNonCachableMessages = currentMessageInformation.AllMessages.Any(x=>x is ICachableRequest == false);

			if(containsNonCachableMessages) 
			{
				// since we are making a non cachable request, the 
				// _cachable_ requests part of this batch are likely to be
				// affected by this message, so we go ahead and expire them
				// to avoid showing incorrect data
				foreach (var cachableRequestToExpire in currentMessageInformation.AllMessages.OfType<ICachableRequest>())
				{
					cache.Remove(cachableRequestToExpire.Key);
				}
				return;
			}

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