using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using osu.Framework.IO.Stores;

namespace hues.Game
{
    public class InMemoryResourceStore : IResourceStore<byte[]>
    {
        private readonly Dictionary<string, MemoryStream> store = new Dictionary<string, MemoryStream>();

        public byte[] Get(string name)
        {
            if (!store.ContainsKey(name))
                return null;

            var stream = store[name];

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        public Task<byte[]> GetAsync(string name)
        {
            return new Task<byte[]>(() => Get(name));
        }

        public Stream GetStream(string name)
        {
            if (!store.ContainsKey(name))
                return null;

            return store[name];
        }

        public void Add(string name, Stream stream)
        {
            var dest = new MemoryStream();
            stream.CopyTo(dest);
            store[name] = dest;
        }

        public IEnumerable<string> GetAvailableResources() => store.Keys;

        public void Dispose()
        {
            store.Clear();
        }
    }
}
