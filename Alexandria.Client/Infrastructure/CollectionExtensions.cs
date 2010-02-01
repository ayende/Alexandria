using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Alexandria.Client.ViewModel;
using Alexandria.Messages;
using System.Linq;

namespace Alexandria.Client.Infrastructure
{
	public static class CollectionExtensions
	{
		public static void UpdateFrom(this ObservableCollection<BookModel> collection, IEnumerable<BookDTO> mergeSource)
		{
			foreach (var bookDTO in mergeSource)
			{
				var bookModel = collection.FirstOrDefault(model => model.Id == bookDTO.Id);
				if(bookModel==null)
				{
					bookModel =  new BookModel
					{
						Id = bookDTO.Id
					};
					collection.Add(bookModel);
				}
				MergeValues(bookModel, bookDTO);
			}
			var toRemove = collection.Where(orig => mergeSource.Any(merged => merged.Id == orig.Id) == false).ToArray();
			foreach (var model in toRemove)
			{
				collection.Remove(model);
			}
		}

		private static void MergeValues(BookModel bookModel, BookDTO bookDTO)
		{
			bookModel.Authors = bookDTO.Authors;
			bookModel.Name = bookDTO.Name;
			bookModel.Image = new BitmapImage(new Uri(bookDTO.ImageUrl));
		}
	}
}