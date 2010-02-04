namespace Alexandria.Client
{
    using Caliburn.PresentationFramework.ApplicationModel;
    using Caliburn.Windsor;
    using Microsoft.Practices.ServiceLocation;

    public partial class App : CaliburnApplication
    {
        protected override IServiceLocator CreateContainer()
        {
            return new WindsorAdapter(Program.Container);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<ApplicationModel>();
        }
    }
}