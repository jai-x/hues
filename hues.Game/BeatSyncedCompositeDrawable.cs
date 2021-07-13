using System;

using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace hues.Game
{
    public class BeatSyncedCompositeDrawable : CompositeDrawable
    {
        public enum Section
        {
            Buildup,
            Loop,
        }

        protected string BuildupBeatchars => workingSong.Value?.Song.BuildupBeatchars ?? String.Empty;
        protected string LoopBeatchars => workingSong.Value?.Song.LoopBeatchars ?? String.Empty;

        [Resolved]
        private Bindable<WorkingSong> workingSong { get; set; }

        private Section lastSection;
        private int lastBeatIndex = -1;

        protected override void Update()
        {
            base.Update();

            var current = workingSong.Value;

            // sanity
            if (current == null)
                return;

            // sanity
            if (!current.TrackLoaded)
                return;

            // get track and section
            if (current.Buildup != null && current.Buildup.IsRunning)
            {
                update(current.Buildup, current.Song.BuildupBeatchars, Section.Buildup);
            }
            else if (current.Loop.IsRunning)
            {
                update(current.Loop, current.Song.LoopBeatchars, Section.Loop);
            }
        }

        private void update(ITrack track, string beatchars, Section currentSection)
        {
            if (String.IsNullOrEmpty(beatchars))
                return;

            if (currentSection != lastSection)
                lastBeatIndex = -1;

            var beatLength = track.Length / beatchars.Length;

            // TODO: Find out why this sometimes creates an index that is 1 larger than number of beatchars
            var currentBeatIndex = Math.Min((int)(track.CurrentTime / beatLength), beatchars.Length - 1);

            if (lastBeatIndex == currentBeatIndex)
                return;

            var beatChar = beatchars[currentBeatIndex];

            // TODO: Find out is this needs to be Scheduled or put under a transform
            OnNewBeat(currentBeatIndex, currentSection, beatChar, beatLength);

            lastSection = currentSection;
            lastBeatIndex = currentBeatIndex;
        }

        protected virtual void OnNewBeat(int beatIndex, Section beatSection, char beatChar, double beatLength)
        {
        }
    }
}
