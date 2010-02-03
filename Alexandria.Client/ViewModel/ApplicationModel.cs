using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using Alexandria.Messages;
using Rhino.ServiceBus;

namespace Alexandria.Client.ViewModel
{
	public class ApplicationModel : INotifyPropertyChanged
	{
		private readonly Dispatcher dispatcher;
		private readonly IServiceBus bus;

		public ApplicationModel(Dispatcher dispatcher, IServiceBus bus)
		{
			this.dispatcher = dispatcher;
			this.bus = bus;
			MyBooks = new ObservableCollection<BookModel>();
			Queue = new ObservableCollection<BookModel>();
			Recommendations = new ObservableCollection<BookModel>();
			SearchResults = new ObservableCollection<BookModel>();
			subscriptionDetails = new SubscriptionDetails();
		}

		public void Init()
		{
			bus.Send(
				new MyBooksRequest
				{
					UserId = 1
				},
				new MyQueueRequest
				{
					UserId = 1
				},
				new MyRecommendationsRequest
				{
					UserId = 1
				},
				new SubscriptionDetailsRequest
				{
					UserId = 1
				});

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

		public void ReorderQueue()
		{
			
		}

		public void AddToQueue()
		{

		}

		public void RemoveFromQueue()
		{

		}
	}
}