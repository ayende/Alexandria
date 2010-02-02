using System;

namespace Alexandria.Messages
{
	public interface ICachableResponse
	{
		string Key { get; }
		DateTime Timestamp { get; }
	}
}