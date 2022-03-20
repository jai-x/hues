using System;
using osu.Framework.Audio.Track;
using hues.Game.Elements;

namespace hues.Game
{
    public enum SongSection
    {
        Buildup,
        Loop,
    }

    public class PlayableSong : IDisposable
    {
        public readonly Song Song;
        public readonly Track Buildup;
        public readonly Track Loop;

        public bool TrackLoaded => (Buildup?.IsLoaded ?? true) && (Loop?.IsLoaded ?? false);
        public bool IsPlaying => Buildup?.IsRunning ?? false || Loop.IsRunning;
        public SongSection Section => (Buildup?.HasCompleted ?? true) ? SongSection.Loop : SongSection.Buildup;

        public PlayableSong(Song song, Track buildup, Track loop)
        {
            Song = song;
            Buildup = buildup;
            Loop = loop;

            if (Loop == null)
                throw new NullLoopTrackException(Song.LoopSource);

            Loop.Looping = true;

            if (Buildup != null)
                Buildup.Completed += Loop.Start;
        }

        public override string ToString() =>
            $"<{nameof(PlayableSong)}> " +
            $"Title: {Song.Title} " +
            $"Buildup: {Buildup?.ToString() ?? "[null]"} " +
            $"Loop: {Loop.ToString()}";

        public void Reset()
        {
            Buildup?.Reset();
            Loop.Reset();
        }

        public void Start()
        {
            switch (Section)
            {
                case SongSection.Buildup:
                    Buildup.Start();
                    break;
                case SongSection.Loop:
                    Loop.Start();
                    break;
            }
        }

        public void Stop()
        {
            Buildup?.Stop();
            Loop.Stop();
        }

        public void Dispose()
        {
            Buildup?.Stop();
            Loop.Stop();
            Buildup?.Dispose();
            Loop.Dispose();
        }
    }

    public class NullLoopTrackException : Exception
    {
        public NullLoopTrackException(string fileName) :
            base($"Loop track `{fileName}` was null")
        { }
    }
}
