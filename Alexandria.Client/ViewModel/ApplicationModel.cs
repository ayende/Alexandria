using System.Collections.ObjectModel;

namespace Alexandria.Client.ViewModel
{
	public class ApplicationModel
	{
		public ApplicationModel()
		{
			MyBooks = new ObservableCollection<BookModel>();
			Queue = new ObservableCollection<BookModel>();
			Recommendations = new ObservableCollection<BookModel>();
			SearchResults = new ObservableCollection<BookModel>();
		}

		public SubscriptionDetails SubscriptionDetails { get; set; }
		public ObservableCollection<BookModel> Queue { get; set; }
		public ObservableCollection<BookModel> Recommendations { get; set; }
		public ObservableCollection<BookModel> MyBooks { get; set; }
		public ObservableCollection<BookModel> SearchResults { get; set;}
	}
}