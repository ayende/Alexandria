namespace Alexandria.Messages
{
	public class SubscriptionDetailsRequest : ICachableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "SubscriptionDetails [UserId #" + UserId + "]"; }
		}
	}
}