using System;

namespace Alexandria.Client.Infrastructure
{
	[Serializable]
	public class CachedData
	{
		public DateTime Timestamp { get; set; }
		public object Value { get; set; }
	}
}