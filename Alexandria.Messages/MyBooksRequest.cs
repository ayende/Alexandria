namespace Alexandria.Messages
{
	public class MyBooksRequest : ICachableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "MyBooks [UserId #" + UserId + "]"; }
		}
	}
}