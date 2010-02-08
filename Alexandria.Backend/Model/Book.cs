using System.Collections.Generic;

namespace Alexandria.Backend.Model
{
	public class Book
	{
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ImageUrl { get; set; }
		public virtual byte[] Image { get; set; }
		public virtual string Author { get; set; }
	}
}