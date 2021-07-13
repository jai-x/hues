using System;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;

namespace hues.Game.Tests.Visual
{
    public class TestSceneSongManager : HuesTestScene
    {
        [Cached]
        protected readonly Bindable<WorkingSong> workingSong = new Bindable<WorkingSong>();

        private SongManager manager;

        private SpriteText title;
        private SpriteText buildupSource;
        private SpriteText buildupChars;
        private SpriteText loopSource;
        private SpriteText loopChars;

        public TestSceneSongManager()
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
                manager = new SongManager(Song.All),
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

            update(workingSong.Value);
            workingSong.ValueChanged += (change) => { update(change.NewValue); };

            AddStep("Next Song", manager.Next);
            AddStep("Previous Song", manager.Previous);
        }

        private void update(WorkingSong s)
        {
            title.Text = s?.Song.Name ?? "none";
            buildupSource.Text = s?.Song.BuildupSource ?? "none";
            buildupChars.Text = s?.Song.BuildupBeatchars ?? "none";
            loopSource.Text = s?.Song.LoopSource ?? "none";
            loopChars.Text = s?.Song.LoopBeatchars ?? "none";
        }
    }
}
