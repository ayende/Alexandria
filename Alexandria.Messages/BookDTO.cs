namespace Alexandria.Messages
{
	public class BookDTO
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string[] Authors { get; set; }
		public string ImageUrl { get; set; }
	}
}