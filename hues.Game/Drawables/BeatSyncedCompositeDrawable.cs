using System;

using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace hues.Game.Drawables
{
    public class BeatSyncedCompositeDrawable : CompositeDrawable
    {
        protected string BuildupBeatchars => playableSong.Value?.Song.BuildupBeatchars ?? String.Empty;
        protected string LoopBeatchars => playableSong.Value?.Song.LoopBeatchars ?? String.Empty;

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        private SongSection lastSection;
        private int lastBeatIndex = -1;

        protected override void Update()
        {
            base.Update();

            var current = playableSong.Value;

            // sanity
            if (current == null)
                return;

            // sanity
            if (!current.TrackLoaded)
                return;

            // get track and section
            switch (current.Section)
            {
                case SongSection.Buildup:
                    update(current.Buildup, current.Song.BuildupBeatchars, current.Section);
                    break;
                case SongSection.Loop:
                    update(current.Loop, current.Song.LoopBeatchars, current.Section);
                    break;
            }
        }

        private void update(ITrack track, string beatchars, SongSection currentSection)
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

            // TODO: Find out if this needs to be Scheduled or put under a transform
            OnNewBeat(currentBeatIndex, currentSection, beatChar, beatLength);

            lastSection = currentSection;
            lastBeatIndex = currentBeatIndex;
        }

        protected virtual void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
        }
    }
}
