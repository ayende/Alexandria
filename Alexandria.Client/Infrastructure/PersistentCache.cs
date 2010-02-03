using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Alexandria.Client.Infrastructure
{
	/// <summary>
	/// Implement a trivial persistent cache.
	/// Any key is written to a file on disk. 
	/// This will _not_ scale properly when the number of keys in the cache grows over several thousands, but it 
	/// is perfectly fine for smaller quanitites of data.
	/// </summary>
	public class PersistentCache : ICache
	{
		private readonly string basePath;

		public PersistentCache(string basePath)
		{
			this.basePath = basePath;
			if (Directory.Exists(basePath) == false)
				Directory.CreateDirectory(basePath);
		}

		public void Put(string key, DateTime timestamp, object instance)
		{
			using (var file = File.Create(Path.Combine(basePath, EscapeKey(key))))
			{
				new BinaryFormatter().Serialize(file, new CachedData
				{
					Timestamp = timestamp,
					Value = instance
				});
				file.Flush();
			}
		}

		public void Remove(string key)
		{
			File.Delete(Path.Combine(basePath, EscapeKey(key)));
		}

		public CachedData Get(string key)
		{
			var path = Path.Combine(basePath, EscapeKey(key));
			if (File.Exists(path) == false)
				return null;
			using (var file = File.OpenRead(path))
			{
				return (CachedData)new BinaryFormatter().Deserialize(file);
			}
		}

		private static string EscapeKey(string key)
		{
			foreach (var invalidFileNameChar in Path.GetInvalidFileNameChars())
			{
				var stringToEscape = invalidFileNameChar.ToString();
				var escapedChar = Uri.EscapeDataString(stringToEscape);
				key = key.Replace(stringToEscape, escapedChar);
			}
			return key;
		}
	}
}