using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class BeatmapManager : Component
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        [Resolved]
        private Bindable<WorkingBeatmap> workingBeatmap { get; set; }

        private Logger logger = Logger.GetLogger();

        private WorkingBeatmap current;

        private Beatmap[] beatmaps;
        private int position;

        public BeatmapManager(ReadOnlyCollection<Beatmap> beatmaps)
        {
            this.beatmaps = beatmaps.ToArray();
            position = 0;
        }

        public void Next()
        {
            logger.Debug($"{nameof(BeatmapManager)} Next");

            if (position == beatmaps.Length - 1)
                position = 0;
            else
                position++;

            update();
        }

        public void Previous()
        {
            logger.Debug($"{nameof(BeatmapManager)} Previous");

            if (position == 0)
                position = beatmaps.Length - 1;
            else
                position--;

            update();
        }

        private void update()
        {
            var oldWorking = current;
            var newWorking = new WorkingBeatmap(beatmaps[position], audioManager);

            newWorking.Load();
            oldWorking?.Stop();

            workingBeatmap.Value = newWorking;
            current = newWorking;

            newWorking.Start();
            oldWorking?.Dispose();
        }

        protected override void LoadComplete()
        {
            update();
        }

        protected override void Dispose(bool isDisposing)
        {
            workingBeatmap.Value = null;
            workingBeatmap.UnbindAll();

            current?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
