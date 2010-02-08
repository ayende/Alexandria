using Alexandria.Backend.Modules;
using Castle.Core;
using Castle.Facilities.FactorySupport;
using Castle.MicroKernel.Registration;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.MessageModules;

namespace Alexandria.Backend
{
  public class AlexandriaBootStrapper : AbstractBootStrapper
  {
    public AlexandriaBootStrapper()
    {
      NHibernateProfiler.Initialize();
    }

    protected override void ConfigureContainer()
    {
      var cfg = new Configuration()
        .Configure("nhibernate.config");
      var sessionFactory = cfg.BuildSessionFactory();

      container.Kernel.AddFacility("factory", new FactorySupportFacility());

      container.Register(
        Component.For<ISessionFactory>()
          .Instance(sessionFactory),
        Component.For<IMessageModule>()
          .ImplementedBy<NHibernateMessageModule>(),
        Component.For<ISession>()
          .UsingFactoryMethod(() => NHibernateMessageModule.CurrentSession)
          .LifeStyle.Is(LifestyleType.Transient));

      base.ConfigureContainer();
    }
  }
}