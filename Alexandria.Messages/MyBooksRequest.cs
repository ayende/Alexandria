namespace Alexandria.Messages
{
	public class MyBooksRequest : ICacheableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "MyBooks (UserId #" + UserId + ")"; }
		}

		public override string ToString()
		{
			return Key;
		}
	}
}