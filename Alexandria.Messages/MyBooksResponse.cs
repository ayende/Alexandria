using System;

namespace Alexandria.Messages
{
	public class MyBooksResponse : ICachableResponse
	{
		public BookDTO[] Books { get; set; }

		public string Key
		{
			get { return "MyBooks [UserId #" + UserId + "]"; }
		}

		public long UserId { get; set; }

		public DateTime Timestamp
		{
			get; set;
		}
	}
}