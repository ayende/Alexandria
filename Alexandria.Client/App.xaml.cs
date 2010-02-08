using System;
using System.IO;
using Alexandria.Client.Infrastructure;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Client
{
    using System.Linq;
    using System.Windows.Input;
    using Caliburn.PresentationFramework.ApplicationModel;
    using Caliburn.Windsor;
    using Castle.Core;
    using Castle.MicroKernel.ModelBuilder.Inspectors;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using Commands;
    using Consumers;
    using Microsoft.Practices.ServiceLocation;
    using Rhino.ServiceBus;
    using Rhino.ServiceBus.Impl;
    using Rhino.ServiceBus.Internal;

    public partial class App : CaliburnApplication
    {
        protected override IServiceLocator CreateContainer()
        {
            var windsor = new WindsorContainer(new XmlInterpreter());

            windsor.Kernel.AddFacility("rhino.esb", new RhinoServiceBusFacility());

            windsor.Register(
				Component.For<ICache>().ImplementedBy<PersistentCache>()
					.DependsOn(Property.ForKey("basePath").Eq(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache"))),
				Component.For<IMessageModule>().ImplementedBy<CachingMessageModule>(),
                AllTypes.FromAssemblyContaining<MyBooksResponseConsumer>()
                    .Where(x => typeof(IMessageConsumer).IsAssignableFrom(x))
                    .Configure(registration => registration.LifeStyle.Is(LifestyleType.Transient)),
                AllTypes.FromAssemblyContaining<AddToQueue>()
                    .Where(x => x.Namespace.StartsWith("Alexandria.Client.Commands"))
                    .Configure(registration => registration.LifeStyle.Is(LifestyleType.Transient)),
                Component.For<ApplicationModel>()
                );

            var serviceBus = windsor.Resolve<IStartableServiceBus>();
            serviceBus.Start();

        	return new WindsorAdapter(windsor);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<ApplicationModel>();
        }
    }
}