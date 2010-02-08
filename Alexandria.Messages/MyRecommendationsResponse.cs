using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class MyRecommendationsResponse : ICacheableResponse
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