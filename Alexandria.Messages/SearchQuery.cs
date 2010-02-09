namespace Alexandria.Messages
{
	public class SearchQuery : ICacheableQuery
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