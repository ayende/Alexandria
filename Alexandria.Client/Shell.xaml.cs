using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Alexandria.Client.ViewModel;

namespace Alexandria.Client
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();

			var books = from img in Directory.GetFiles(@"C:\Work\Alexandria\Art", "*.jpg")
			select new BookModel
			{
				CheckedOutTime = TimeSpan.FromDays(14),
				Image = new BitmapImage(new Uri(img))
			};

			DataContext = new ApplicationModel
			{
				MyBooks = new ObservableCollection<BookModel>(books.Take(3)),
				Queue = new ObservableCollection<BookModel>(books.Skip(3).Take(3)),
				Recommendations = new ObservableCollection<BookModel>(books.Skip(6).Take(6)),
				SearchResults = new ObservableCollection<BookModel>(books.Skip(2).Take(3)),
				SubscriptionDetails = new SubscriptionDetails
				{
					City = "Haser",
					Country = "Israel",
					CreditCard = "xxxx-xxxx-xxx-8901",
					HouseNumber = "15",
					MonthlyCost = 21m,
					Name = "Oren Eini",
					NumberOfPossibleBooksOut = 3,
					Street = "Sikui",
					ZipCode = "39191"
				}
			};
		}
	}
}
