using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;

namespace Alexandria.Client
{
	public class Program
	{
		public static WindsorContainer Container { get; private set; }

		[STAThread]
		private static void Main(string[] args)
		{
			Container = new WindsorContainer(new XmlInterpreter());
			Container.Kernel.ComponentModelBuilder.RemoveContributor(Container.Kernel.ComponentModelBuilder.Contributors.OfType<PropertiesDependenciesModelInspector>().Single());
			Container.Kernel.AddFacility("rhino.esb", new RhinoServiceBusFacility());

			Container.Register(
				AllTypes.FromAssemblyContaining<App>()
					.Where(type => typeof(Window).IsAssignableFrom(type) ||
								typeof(UserControl).IsAssignableFrom(type) ||
								typeof(Application).IsAssignableFrom(type))
				);

			var serviceBus = Container.Resolve<IStartableServiceBus>();
			serviceBus.Start();

			var app = Container.Resolve<App>();
			app.InitializeComponent();

			app.Run();

		}
	}
}