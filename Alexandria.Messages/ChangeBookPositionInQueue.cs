namespace Alexandria.Messages
{
	public class ChangeBookPositionInQueue
	{
		public long UserId { get; set; }
		public long BookId { get; set; }
		public int NewPosition { get; set; }
	}
}