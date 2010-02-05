namespace Alexandria.Messages
{
	public class MyRecommendationsRequest : ICachableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "Recommendations (UserId #" + UserId + ")"; }
		}

		public override string ToString()
		{
			return Key;
		}
	}
}