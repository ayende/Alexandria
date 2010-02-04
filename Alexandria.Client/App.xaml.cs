namespace Alexandria.Client
{
    using System;
    using System.Windows;
    using Caliburn.PresentationFramework.ApplicationModel;
    using Caliburn.PresentationFramework.Configuration;
    using Caliburn.PresentationFramework.ViewModels;
    using Caliburn.Windsor;
    using Microsoft.Practices.ServiceLocation;
    using ViewModels;
    using Views;
    using SubscriptionDetails=Alexandria.Client.Views.SubscriptionDetails;

    public partial class App : CaliburnApplication
    {
        protected override IServiceLocator CreateContainer()
        {
            return new WindsorAdapter(Program.Container);
        }

        protected override object CreateRootModel()
        {
            var model = Container.GetInstance<ApplicationModel>();
            model.Init();
            return model;
        }
        //protected override void ConfigurePresentationFramework(PresentationFrameworkConfiguration module)
        //{
        //    // using a custom view locator is not usually necessary if you accept Caliburn's default conventions
        //    module.Using(x => x.ViewLocator<CustomViewLocator>());
        //    base.ConfigurePresentationFramework(module);
        //}

        public class CustomViewLocator : IViewLocator
        {
            private readonly IServiceLocator _serviceLocator;

            public CustomViewLocator(IServiceLocator serviceLocator)
            {
                _serviceLocator = serviceLocator;
            }

            public DependencyObject Locate(Type modelType, DependencyObject displayLocation, object context)
            {
                if (modelType == typeof (ApplicationModel))
                    return _serviceLocator.GetInstance<ApplicationView>();

                if (modelType == typeof(ViewModels.SubscriptionDetails))
                    return _serviceLocator.GetInstance<SubscriptionDetails>();

                throw new NotImplementedException("The only view model we're expecting to bind is ApplicationModel.");
            }
        }
    }
}