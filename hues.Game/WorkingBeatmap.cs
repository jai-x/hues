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

        public Beatmap Beatmap => beatmap;

        public bool TrackLoaded { get; private set; }

        public WorkingBeatmap(Beatmap b, AudioManager a)
        {
            beatmap = b;
            manager = a;
            TrackLoaded = false;
        }

        public void Load()
        {
            if (TrackLoaded)
            {
                logger.Debug($"WorkingBeatmap.Load: {beatmap.Name} already loaded!");
                return;
            }

            Buildup = manager.Tracks.Get(beatmap.BuildupSource);
            Loop = manager.Tracks.Get(beatmap.LoopSource);

            if (Loop == null)
                throw new Exception();

            if (Buildup != null)
                Buildup.Completed += Loop.Start;
            Loop.Looping = true;


            TrackLoaded = true;
        }

        public void Reset()
        {
            if (!TrackLoaded)
            {
                logger.Debug($"WorkingBeatmap.Reset: {beatmap.Name} not loaded!");
                return;
            }

            Buildup?.Reset();
            Loop.Reset();
        }

        public void Start()
        {
            if (!TrackLoaded)
            {
                logger.Debug($"WorkingBeatmap.Reset: {beatmap.Name} not loaded!");
                return;
            }

            if (Buildup == null)
                Loop.Start();
            else
                Buildup.Start();
        }

        public void Stop()
        {
            if (!TrackLoaded)
            {
                logger.Debug($"WorkingBeatmap.Stop: {beatmap.Name} not loaded!");
                return;
            }

            Buildup?.Stop();
            Loop?.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            TrackLoaded = false;

            Buildup?.Stop();
            Buildup?.Dispose();
            Loop?.Stop();
            Loop?.Dispose();

            base.Dispose(disposing);
        }
    }
}
