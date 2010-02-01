using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
	public partial class Shell : Window
	{
		private readonly IServiceBus bus;
		private ApplicationModel applicationModel;

		public Shell()
			: this(Program.Container.Resolve<IServiceBus>(), Program.Container.Resolve<ApplicationModel>())
		{
		}

		public Shell(IServiceBus bus, ApplicationModel applicationModel)
		{
			this.bus = bus;
			this.applicationModel = applicationModel;
			InitializeComponent();

			this.bus.Send(
				new MyBooksRequest
				{
					UserId = 1
				},
				new MyQueueRequest
				{
					UserId = 1
			    },
				new MyRecommendationsRequest
				{
					UserId = 1
				},
				new SubscriptionDetailsRequest
				{
					UserId = 1
				});

			DataContext = this.applicationModel;
		}
	}
}
