using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using osu.Framework.IO.Stores;

namespace hues.Game.Stores
{
    public class InMemoryResourceStore : IResourceStore<byte[]>
    {
        protected readonly Dictionary<string, MemoryStream> store = new Dictionary<string, MemoryStream>();
        protected readonly object storeLock = new object();

        public byte[] Get(string name)
        {
            lock (storeLock)
            {
                if (!store.ContainsKey(name))
                    return null;

                var stream = store[name];

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public Task<byte[]> GetAsync(string name)
        {
            // TODO: Find out how C# async works and why this is cursed
            return new Task<byte[]>(() => Get(name));
        }

        public Stream GetStream(string name)
        {
            lock (storeLock)
            {
                if (!store.ContainsKey(name))
                    return null;

                return store[name];
            }
        }

        public IEnumerable<string> GetAvailableResources()
        {
            lock (storeLock)
            {
                return store.Keys;
            }
        }

        public void Dispose()
        {
            store.Clear();
        }
    }
}
