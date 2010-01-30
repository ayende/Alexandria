using System.Collections.Generic;

namespace Alexandria.Backend.Model
{
	public class User
	{
		public virtual long Id { get; set; }
		
		public virtual string Name { get; set; }
		public virtual Address Address { get; set; }

		public virtual IList<Book> Queue { get; set; }
		public virtual ICollection<Book> CurrentlyReading { get; set; }
	}
}