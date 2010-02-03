using Alexandria.Backend.Model;
using Alexandria.Backend.Modules;
using Castle.Core;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Backend
{
	public class AlexandriaBootStrapper : AbstractBootStrapper
	{
		public AlexandriaBootStrapper()
		{
			BasicConfigurator.Configure(new ConsoleAppender
			{
				Threshold = Level.Info,
				Layout = new SimpleLayout()
			});
			NHibernateProfiler.Initialize();
		}
		protected override void ConfigureContainer()
		{

			container.Kernel.AddFacility("factory", new FactorySupportFacility());

			var cfg = new Configuration()
				.Configure("nhibernate.config");

			new SchemaUpdate(cfg).Execute(true, true);

			var sessionFactory = cfg.BuildSessionFactory();
			container.Register(Component.For<ISessionFactory>().Instance(sessionFactory));

			container.Register(Component.For<IMessageModule>().ImplementedBy<NHibernateMessageModule>());

			container.Register(Component.For<ISession>()
				.UsingFactoryMethod(() => NHibernateMessageModule.CurrentSession)
				.LifeStyle.Is(LifestyleType.Transient));

			base.ConfigureContainer();
		}
	}
}