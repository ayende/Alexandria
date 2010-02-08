using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class SearchResponse : ICacheableResponse
	{
		public BookDTO[] SearchResults { get; set; }
		public string Search { get; set; }

		public string Key
		{
			get { return "Search (" + Search + ")"; }
		}

		public DateTime Timestamp
		{
			get; set;
		}

		public override string ToString()
		{
			return Key;
		}
	}
}