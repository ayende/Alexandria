using System;
using System.Linq;
using System.Windows.Threading;
using Alexandria.Client.Consumers;
using Castle.Core;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;

namespace Alexandria.Client
{
    using Views;

    public class Program
	{
		public static WindsorContainer Container { get; private set; }

		[STAThread]
		private static void Main()
		{
			Container = new WindsorContainer(new XmlInterpreter());
			Container.Kernel.ComponentModelBuilder.RemoveContributor(Container.Kernel.ComponentModelBuilder.Contributors.OfType<PropertiesDependenciesModelInspector>().Single());


			Container.Kernel.AddFacility("rhino.esb", new RhinoServiceBusFacility());

			Container.Register(
			                  	AllTypes.FromAssemblyContaining<MyBooksResponseConsumer>()
			                  		.Where(x => typeof (IMessageConsumer).IsAssignableFrom(x))
			                  		.Configure(registration => registration.LifeStyle.Is(LifestyleType.Transient))
				);

			var serviceBus = Container.Resolve<IStartableServiceBus>();
			serviceBus.Start();

            Container.Register(Component.For<Shell>().ImplementedBy<Shell>());
            Container.Register(Component.For<SubscriptionDetails>().ImplementedBy<SubscriptionDetails>());

            var applicationModel = new ApplicationModel(Dispatcher.CurrentDispatcher, serviceBus);
            Container.Register(Component.For<ApplicationModel>().Instance(applicationModel));

			var app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}