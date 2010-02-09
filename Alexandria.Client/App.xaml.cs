namespace Alexandria.Client
{
    using System;
    using System.IO;
    using Caliburn.PresentationFramework.ApplicationModel;
    using Caliburn.Windsor;
    using Castle.Core;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using Consumers;
    using Infrastructure;
    using Microsoft.Practices.ServiceLocation;
    using Rhino.ServiceBus;
    using Rhino.ServiceBus.Impl;
    using Rhino.ServiceBus.Internal;
    using Rhino.ServiceBus.MessageModules;

    public partial class App : CaliburnApplication
    {
        protected override IServiceLocator CreateContainer()
        {
            var windsor = new WindsorContainer(new XmlInterpreter());

            windsor.Kernel.AddFacility("rhino.esb", new RhinoServiceBusFacility());

            windsor.Register(
                Component.For<ICache>().ImplementedBy<PersistentCache>()
                    .DependsOn(
                    Property.ForKey("basePath").Eq(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache"))),
                Component.For<IMessageModule>().ImplementedBy<CachingMessageModule>(),
                AllTypes.FromAssemblyContaining<MyBooksResponseConsumer>()
                    .Where(x => typeof (IMessageConsumer).IsAssignableFrom(x))
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