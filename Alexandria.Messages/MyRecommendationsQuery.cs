namespace Alexandria.Messages
{
	public class MyRecommendationsQuery : ICacheableQuery
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