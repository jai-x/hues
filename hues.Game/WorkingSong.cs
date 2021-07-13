using System;

using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class WorkingSong : Component
    {
        private Logger logger = Logger.GetLogger();

        private Song song;
        private AudioManager manager;

        public Track Buildup { get; private set; }
        public Track Loop { get; private set; }

        public double BuildupBeatlength { get; private set; }
        public double LoopBeatlength { get; private set; }

        public Song Song => song;

        public bool TrackLoaded => (Buildup?.IsLoaded ?? true) && (Loop?.IsLoaded ?? false);

        public WorkingSong(Song s, AudioManager a)
        {
            song = s;
            manager = a;
        }

        public void Load()
        {
            Buildup = manager.Tracks.Get(song.BuildupSource);
            Loop = manager.Tracks.Get(song.LoopSource);

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

        protected override void Dispose(bool disposing)
        {
            Buildup?.Stop();
            Buildup?.Dispose();
            Loop?.Stop();
            Loop?.Dispose();

            base.Dispose(disposing);
        }
    }
}
