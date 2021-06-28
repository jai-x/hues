using System;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;

namespace hues.Game.Tests.Visual
{
    public class TestSceneBeatmapManager : HuesTestScene
    {
        [Cached]
        protected readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();

        private BeatmapManager manager;

        private SpriteText title;
        private SpriteText buildupSource;
        private SpriteText buildupChars;
        private SpriteText loopSource;
        private SpriteText loopChars;

        public TestSceneBeatmapManager()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            manager?.Dispose();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                manager = new BeatmapManager(Beatmap.All),
                title = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 26),
                    Y = -40,
                },
                buildupSource = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                buildupChars = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 20,
                },
                loopSource = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 40,
                },
                loopChars = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Y = 60,
                },
            };

            update(workingBeatmap.Value);
            workingBeatmap.ValueChanged += (change) => { update(change.NewValue); };

            AddStep("Next Beatmap", manager.Next);
            AddStep("Previous Beatmap", manager.Previous);
        }

        private void update(WorkingBeatmap b)
        {
            title.Text = b?.Beatmap.Name ?? "none";
            buildupSource.Text = b?.Beatmap.BuildupSource ?? "none";
            buildupChars.Text = b?.Beatmap.BuildupBeatchars ?? "none";
            loopSource.Text = b?.Beatmap.LoopSource ?? "none";
            loopChars.Text = b?.Beatmap.LoopBeatchars ?? "none";
        }
    }
}
