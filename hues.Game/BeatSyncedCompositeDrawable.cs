using System;

using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Logging;

namespace hues.Game
{
    public class BeatSyncedCompositeDrawable : CompositeDrawable
    {
        public enum Section
        {
            Reset,
            Buildup,
            Loop,
        }

        [Resolved]
        private Bindable<WorkingBeatmap> workingBeatmap { get; set; }

        protected Section CurrentBeatSection { get; private set; }

        protected int CurrentBeatIndex { get; private set; }

        protected char CurrentBeatChar { get; private set; }

        private int lastIndex = -1;
        private Section lastSection = Section.Reset;

        private Logger logger = Logger.GetLogger();

        protected override void Update()
        {
            base.Update();

            var current = workingBeatmap.Value;

            // sanity
            if (current == null)
                return;

            // sanity
            if (!current.TrackLoaded)
                return;

            // get track and section
            if (current.Buildup != null && current.Buildup.IsRunning)
            {
                updateSection(Section.Buildup);
                updateBeat(current.Buildup, current.Beatmap.BuildupBeatchars);
            }
            else if (current.Loop.IsRunning)
            {
                updateSection(Section.Loop);
                updateBeat(current.Loop, current.Beatmap.LoopBeatchars);
            }
            else
                return;
        }

        private void updateSection(Section s)
        {
            CurrentBeatSection = s;

            if (CurrentBeatSection != lastSection)
            {
                lastIndex = -1;
                lastSection = CurrentBeatSection;
            }
        }

        private void updateBeat(ITrack track, string beatchars)
        {
            if (beatchars == null || beatchars.Length == 0)
                return;

            var beatLength = track.Length / beatchars.Length;

            // TODO: Find out why this sometimes creates an index that is 1 larger than number of beatchars
            CurrentBeatIndex = Math.Min((int)(track.CurrentTime / beatLength), beatchars.Length - 1);

            if (lastIndex == CurrentBeatIndex)
                return;

            CurrentBeatChar = beatchars[CurrentBeatIndex];

            OnNewBeat(CurrentBeatIndex, CurrentBeatSection, CurrentBeatChar, beatLength);

            lastIndex = CurrentBeatIndex;
        }

        protected virtual void OnNewBeat(int beatIndex, Section beatSection, char beatChar, double beatLength)
        {
        }
    }
}
