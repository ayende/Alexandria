using System;
using Alexandria.Client.Infrastructure;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client.Consumers
{
	public class MyBooksResponseConsumer : ConsumerOf<MyBooksResponse>
	{
		private readonly ApplicationModel applicationModel;

		public MyBooksResponseConsumer(ApplicationModel applicationModel)
		{
			this.applicationModel = applicationModel;
		}

		public void Consume(MyBooksResponse message)
		{
			applicationModel.UpdateInUIThread(
				()=>applicationModel.MyBooks.UpdateFrom(message.Books)
				);
		}
	}
}