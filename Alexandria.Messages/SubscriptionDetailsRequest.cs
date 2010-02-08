namespace Alexandria.Messages
{
	public class SubscriptionDetailsRequest : ICacheableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "SubscriptionDetails (UserId #" + UserId + ")"; }
		}

		public override string ToString()
		{
			return Key;
		}
	}
}