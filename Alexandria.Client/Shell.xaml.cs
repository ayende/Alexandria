namespace Alexandria.Client
{
    using System.Windows;
    using Infrastructure;
    using ViewModel;

    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private readonly ApplicationModel applicationModel;

        public Shell()
            : this(Program.Container.Resolve<ApplicationModel>())
        {
        }

        public Shell(ApplicationModel applicationModel)
        {
            this.applicationModel = applicationModel;
            InitializeComponent();

            DataContext = this.applicationModel;

            this.WireTo(applicationModel);
        }
    }
}