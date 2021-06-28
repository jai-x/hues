using System;

using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class WorkingBeatmap : Component
    {
        private Logger logger = Logger.GetLogger();

        private Beatmap beatmap;
        private AudioManager manager;

        public Track Buildup { get; private set; }
        public Track Loop { get; private set; }

        public double BuildupBeatlength { get; private set; }
        public double LoopBeatlength { get; private set; }

        public Beatmap Beatmap => beatmap;

        public bool TrackLoaded => (Buildup?.IsLoaded ?? true) && (Loop?.IsLoaded ?? false);

        public WorkingBeatmap(Beatmap b, AudioManager a)
        {
            beatmap = b;
            manager = a;
        }

        public void Load()
        {
            Buildup = manager.Tracks.Get(beatmap.BuildupSource);
            Loop = manager.Tracks.Get(beatmap.LoopSource);

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
