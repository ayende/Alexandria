using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client
{
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
		}
	}
}
