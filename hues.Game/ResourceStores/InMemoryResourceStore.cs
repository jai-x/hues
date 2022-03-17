using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;

namespace hues.Game.ResourceStores
{
    public class InMemoryResourceStore : IResourceStore<byte[]>
    {
        protected readonly Dictionary<string, byte[]> store = new Dictionary<string, byte[]>();
        protected readonly object storeLock = new object();

        public void Add(string name, Stream stream)
        {
            lock (storeLock)
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                store[name] = ms.ToArray();
            }
        }

        public byte[] Get(string name)
        {
            lock (storeLock)
            {
                if (!store.ContainsKey(name))
                    return null;

                return store[name];
            }
        }

        // TODO: Find out how C# async works and why this is cursed
        public Task<byte[]> GetAsync(string name, CancellationToken cancellationToken = default)
            => Task.Run(() => Get(name), cancellationToken);

        public Stream GetStream(string name)
        {
            lock (storeLock)
            {
                if (!store.ContainsKey(name))
                    return null;

                return new MemoryStream(store[name]);
            }
        }

        public IEnumerable<string> GetAvailableResources()
        {
            lock (storeLock)
                return store.Keys;
        }

        public void Clear()
        {
            lock (storeLock)
                store.Clear();

            Logger.Log($"{this.GetType()} cleared!", level: LogLevel.Debug);
        }

        public void Dispose() => Clear();
    }
}
