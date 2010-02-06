using System.Collections.Generic;
using System.Linq;
using Alexandria.Backend.Model;
using Alexandria.Messages;

namespace Alexandria.Backend.Util
{
	public static class DtoExtensions
	{
		public static BookDTO[] ToBookDtoArray(this IEnumerable<Book> books)
		{
			return books.Select(book => new BookDTO
			{
				Id = book.Id,
				Image = book.Image,
				Name = book.Name,
				Author = book.Author
			}).ToArray();
		}
	}
}