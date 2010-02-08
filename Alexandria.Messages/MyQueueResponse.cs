using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class MyQueueResponse : ICacheableResponse
	{
		public BookDTO[] Queue { get; set; }

		public long UserId { get; set; }

		public string Key
		{
			get { return "MyQueue (UserId #" + UserId + ")"; }
		}

		public DateTime Timestamp { get; set; }

		
	}
}