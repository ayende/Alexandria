namespace Alexandria.Messages
{
	public class MyQueueRequest : ICachableRequest
	{
		public long UserId { get; set; }

		public string Key
		{
			get { return "MyQueue [UserId #" + UserId + "]"; }
		}
	}
}