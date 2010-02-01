using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace Alexandria.Client.ViewModel
{
	public class ApplicationModel : INotifyPropertyChanged
	{
		private readonly Dispatcher dispatcher;

		public ApplicationModel(Dispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
			MyBooks = new ObservableCollection<BookModel>();
			Queue = new ObservableCollection<BookModel>();
			Recommendations = new ObservableCollection<BookModel>();
			SearchResults = new ObservableCollection<BookModel>();
			subscriptionDetails = new SubscriptionDetails();
		}

		private SubscriptionDetails subscriptionDetails;
		public SubscriptionDetails SubscriptionDetails
		{
			get { return subscriptionDetails; }
			set
			{
				subscriptionDetails = value;
				PropertyChanged(this, new PropertyChangedEventArgs("SubscriptionDetails"));
			}
		}

		public ObservableCollection<BookModel> Queue { get; set; }
		public ObservableCollection<BookModel> Recommendations { get; set; }
		public ObservableCollection<BookModel> MyBooks { get; set; }
		public ObservableCollection<BookModel> SearchResults { get; set;}

		public void UpdateInUIThread(Action action)
		{
			dispatcher.BeginInvoke(action);
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate { };
	}
}