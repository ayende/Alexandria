namespace Alexandria.Messages
{
	public class MyQueueQuery : ICacheableQuery
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "MyQueue (UserId #" + UserId + ")"; }
		}

		public override string ToString()
		{
			return Key;
		}
	}
}