using System;
using System.Windows.Media;

namespace Alexandria.Client.ViewModel
{
	public class BookModel
	{
		public ImageSource Image { get; set; }
		public TimeSpan CheckedOutTime { get; set; }
	}
}