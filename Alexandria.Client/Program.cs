using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Alexandria.Client.Consumers;
using Alexandria.Client.Infrastructure;
using Alexandria.Client.ViewModel;
using Castle.Core;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Client
{
	public class Program
	{
		public static WindsorContainer Container { get; private set; }

		[STAThread]
		private static void Main()
		{
			Container = new WindsorContainer(new XmlInterpreter());
			Container.Kernel.AddFacility("rhino.esb", new RhinoServiceBusFacility());
			Container.Register(
			                  	Component.For<ICache>().ImplementedBy<PersistentCache>()
									.DependsOn(Property.ForKey("basePath").Eq(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache"))),
			                  	Component.For<IMessageModule>().ImplementedBy<CachingMessageModule>(),
			                  	AllTypes.FromAssemblyContaining<MyBooksResponseConsumer>()
			                  		.Where(x => typeof (IMessageConsumer).IsAssignableFrom(x))
			                  		.Configure(registration => registration.LifeStyle.Is(LifestyleType.Transient))
				);

			var serviceBus = Container.Resolve<IStartableServiceBus>();
			var applicationModel = new ApplicationModel(Dispatcher.CurrentDispatcher, serviceBus);
			Container.Register(Component.For<ApplicationModel>().Instance(applicationModel));

			serviceBus.Start();
			applicationModel.Init();

			var app = new App();
			app.InitializeComponent();
			app.Run();

		}
	}
}