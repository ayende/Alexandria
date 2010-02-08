namespace Alexandria.Messages
{
	public class RemoveBookFromQueue
	{
		public long UserId { get; set; }
		public long BookId { get; set; }
	}
}