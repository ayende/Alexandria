namespace Alexandria.Messages
{
	public class SearchRequest : ICacheableRequest
	{
		public long UserId { get; set; }
		public string Search { get; set; }

		public string Key
		{
			get { return "Search (" + Search + ")"; }
		}

		public override string ToString()
		{
			return Key;
		}
	}
}