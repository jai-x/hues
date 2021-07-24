using System;

using osu.Framework.Audio.Track;
using osu.Framework.Graphics;

using hues.Game.RespackElements;

namespace hues.Game
{
    public class PlayableSong : IDisposable
    {
        public readonly Song Song;
        public readonly Track Buildup;
        public readonly Track Loop;

        public bool TrackLoaded => (Buildup?.IsLoaded ?? true) && (Loop?.IsLoaded ?? false);

        public bool IsPlaying => Buildup?.IsRunning ?? false || Loop.IsRunning;

        public override string ToString() =>
            $"<{nameof(PlayableSong)}> " +
            $"Title: {Song.Title} " +
            $"Buildup: {Buildup?.ToString() ?? "[null]"} " +
            $"Loop: {Loop.ToString()}";

        public PlayableSong(Song song, Track buildup, Track loop)
        {
            Song = song;
            Buildup = buildup;
            Loop = loop;

            if (Loop == null)
                throw new NullLoopTrackException(Song.LoopSource);

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
            Loop?.Stop();
            Buildup?.Dispose();
            Loop?.Dispose();
        }
    }

    public class NullLoopTrackException : Exception
    {
        public NullLoopTrackException(string fileName) :
            base($"Loop track `{fileName}` was null")
        { }
    }
}
