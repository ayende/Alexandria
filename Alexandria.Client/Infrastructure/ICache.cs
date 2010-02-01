namespace Alexandria.Client.Infrastructure
{
	public interface ICache
	{
		void Put(string key, object instance);
		void Remove(string key);
		CachedData Get(string key);
	}
}