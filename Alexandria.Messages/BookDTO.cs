using System;

namespace Alexandria.Messages
{
	[Serializable]
	public class BookDTO
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string[] Authors { get; set; }
		public byte[] Image { get; set; }
	}
}