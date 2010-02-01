using System;
using System.Windows.Media;

namespace Alexandria.Client.ViewModel
{
	public class BookModel
	{
		public string[] Authors { get; set; }
		public ImageSource Image { get; set; }
		public long Id { get; set; }
		public string Name { get; set; }
	}
}