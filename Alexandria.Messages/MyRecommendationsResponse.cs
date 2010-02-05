using System;

namespace Alexandria.Messages
{
	public class MyRecommendationsResponse : ICachableResponse
	{
		public BookDTO[] Recommendations { get; set; }

		public string Key
		{
			get { return "Recommendations (UserId #" + UserId + ")"; }
		}

		public long UserId { get; set; }

		public DateTime Timestamp
		{
			get;
			set;
		}
	}
}