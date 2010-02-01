using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Alexandria.Client.Infrastructure
{
	/// <summary>
	/// Implement a trivial persistent cache.
	/// Any key is written to a file on disk. 
	/// This will _not_ scale properly when the number of keys in the cache grows over several thousands, but it 
	/// is perfectly fine for smaller quanitites of data.
	/// </summary>
	public class Cache : ICache
	{
		private readonly string basePath;

		public Cache(string basePath)
		{
			this.basePath = basePath;
			if (Directory.Exists(basePath) == false)
				Directory.CreateDirectory(basePath);
		}

		public void Put(string key, object instance)
		{
			using(var file = File.Create(Path.Combine(basePath, Uri.EscapeDataString(key))))
			{
				new BinaryFormatter().Serialize(file, new CachedData
				{
					Timestamp = DateTime.Now,
					Value = instance
				});
			}
		}

		public void Remove(string key)
		{
			File.Delete(Path.Combine(basePath, Uri.EscapeDataString(key)));
		}

		public CachedData Get(string key)
		{
			var path = Path.Combine(basePath, Uri.EscapeDataString(key));
			if (File.Exists(path) == false)
				return null;
			using (var file = File.OpenRead(path))
			{
				return (CachedData)new BinaryFormatter().Deserialize(file);
			}
		}
	}
}