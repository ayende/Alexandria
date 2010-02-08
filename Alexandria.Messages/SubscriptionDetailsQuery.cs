namespace Alexandria.Messages
{
	public class SubscriptionDetailsQuery : ICacheableQuery
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