using System;

using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class PlayableSong : IDisposable
    {
        public readonly Song Song;
        public readonly Track Buildup;
        public readonly Track Loop;

        public bool TrackLoaded => (Buildup?.IsLoaded ?? true) && (Loop?.IsLoaded ?? false);

        public PlayableSong(Song song, ITrackStore trackStore)
        {
            Song = song;
            Buildup = trackStore.Get(Song.BuildupSource);
            Loop = trackStore.Get(Song.LoopSource);

            if (Loop == null)
                throw new Exception();

            if (Buildup != null)
                Buildup.Completed += Loop.Start;

            Loop.Looping = true;
        }

        public void Reset()
        {
            Buildup?.Reset();
            Loop.Reset();
        }

        public void Start()
        {
            if (Buildup == null)
                Loop.Start();
            else
                Buildup.Start();
        }

        public void Stop()
        {
            Buildup?.Stop();
            Loop?.Stop();
        }

        public void Dispose()
        {
            Buildup?.Stop();
            Buildup?.Dispose();
            Loop?.Stop();
            Loop?.Dispose();
        }
    }
}
