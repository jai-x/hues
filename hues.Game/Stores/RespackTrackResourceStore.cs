using System;
using System.IO;

namespace hues.Game.Stores
{
    public class RespackTrackResourceStore : InMemoryResourceStore
    {
        public void Add(string name, Stream stream)
        {
            lock (storeLock)
            {
                var dest = new MemoryStream();
                stream.CopyTo(dest);
                store[name] = dest;
            }
        }
    }
}
