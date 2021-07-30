using System;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using hues.Game.Elements;

namespace hues.Game.Drawables
{
    public class BeatSyncedCompositeDrawable : CompositeDrawable
    {
        protected string BuildupBeatchars => playableSong.Value?.Song.BuildupBeatchars ?? String.Empty;
        protected string LoopBeatchars => playableSong.Value?.Song.LoopBeatchars ?? String.Empty;

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        private Song lastSong;
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

            // reset beat on new song
            if (current.Song != lastSong)
            {
                lastBeatIndex = -1;
                lastSong = current.Song;
            }

            // reset beat on new section
            if (current.Section != lastSection)
            {
                lastBeatIndex = -1;
                lastSection = current.Section;
            }

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
            double beatLength = 0;
            int currentBeatIndex = 0;
            char beatChar = '.';

            if (!String.IsNullOrEmpty(beatchars))
            {
                beatLength = track.Length / beatchars.Length;

                // TODO: Find out why this sometimes creates an index that is 1 larger than number of beatchars
                currentBeatIndex = Math.Min((int)(track.CurrentTime / beatLength), beatchars.Length - 1);

                beatChar = beatchars[currentBeatIndex];
            }

            // don't update on the same beat
            if (lastBeatIndex == currentBeatIndex)
                return;

            // TODO: Find out if this needs to be Scheduled or put under a transform
            OnNewBeat(currentBeatIndex, currentSection, beatChar, beatLength);

            // this is a new beat
            lastBeatIndex = currentBeatIndex;
        }

        protected virtual void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
        }
    }
}
