using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class SubscriptionDetailsResponse : ICachableResponse
	{
		public SubscriptionDetailsDTO SubscriptionDetails { get; set; }

		public string Key
		{
			get { return "SubscriptionDetails (UserId #" + UserId + ")"; }
		}

		public long UserId { get; set; }

		public DateTime Timestamp
		{
			get;
			set;
		}
	}
}