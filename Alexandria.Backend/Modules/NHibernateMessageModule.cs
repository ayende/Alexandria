using System;
using NHibernate;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Backend.Modules
{
    using Rhino.ServiceBus;

    public class NHibernateMessageModule : IMessageModule
	{
		private readonly ISessionFactory sessionFactory;
		[ThreadStatic]
		private static ISession currentSession;

		public static ISession CurrentSession
		{
			get { return currentSession; }
		}

		public NHibernateMessageModule(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		public void Init(ITransport transport, IServiceBus bus)
		{
			transport.MessageArrived += TransportOnMessageArrived;
			transport.MessageProcessingCompleted += TransportOnMessageProcessingCompleted;
		}

		private static void TransportOnMessageProcessingCompleted(CurrentMessageInformation currentMessageInformation, Exception exception)
		{
			if (currentSession != null)
				currentSession.Dispose();
			currentSession = null;
		}

		private bool TransportOnMessageArrived(CurrentMessageInformation currentMessageInformation)
		{
			if (currentSession == null)
				currentSession = sessionFactory.OpenSession();
			return false;
		}

        public void Stop(ITransport transport, IServiceBus bus)
		{
			transport.MessageArrived -= TransportOnMessageArrived;
			transport.MessageProcessingCompleted -= TransportOnMessageProcessingCompleted;

		}
	}
}