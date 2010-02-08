using System;

namespace Alexandria.Messages
{
	public interface ICacheableResponse
	{
		string Key { get; }
		DateTime Timestamp { get; }
	}
}