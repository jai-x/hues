using System;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;

using osuTK;

namespace hues.Game.Tests.Visual
{
    public class TestSceneBeatSyncedCompositeDrawable : HuesTestScene
    {
        [Cached]
        protected readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();

        private BeatmapManager manager;

        public TestSceneBeatSyncedCompositeDrawable()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                manager = new BeatmapManager(Beatmap.All),
                new TestBeatDrawable(),
            };

            AddStep("Next", manager.Next);
            AddStep("Previous", manager.Previous);
        }

        protected override void Dispose(bool isDisposing)
        {
            manager?.Dispose();
            base.Dispose(isDisposing);
        }

        private class TestBeatDrawable : BeatSyncedCompositeDrawable
        {
            private SpriteText section;
            private SpriteText index;
            private SpriteText name;
            private SpriteText beatchar;
            private Box flashRed;
            private Box flashBlue;

            [Resolved]
            private Bindable<WorkingBeatmap> workingBeatmap { get; set; }

            public TestBeatDrawable()
            {
                RelativeSizeAxes = Axes.Both;
            }

            protected override void LoadComplete()
            {
                InternalChildren = new Drawable[]
                {
                    flashBlue = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(600),
                        Colour = Colour4.Cyan,
                        Alpha = 0f,
                    },
                    flashRed = new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(600),
                        Colour = Colour4.Crimson,
                        Alpha = 0f,
                    },
                    name = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 40),
                        Y = -150,
                    },
                    section = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 40),
                        Y = -50,
                    },
                    index = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 40),
                        Y = 50,
                    },
                    beatchar = new SpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = FontUsage.Default.With(size: 60),
                        Y = 150,
                    },
                };
            }

            protected override void OnNewBeat(int beatIndex, Section beatSection, char beatChar, double beatLength)
            {
                base.OnNewBeat(beatIndex, beatSection, beatChar, beatLength);

                name.Text = workingBeatmap.Value?.Beatmap.Name ?? "none";
                index.Text = beatIndex.ToString();
                section.Text = beatSection.ToString();

                if (beatChar == '.')
                    return;

                beatchar.Text = beatChar.ToString();
                beatchar.FadeOutFromOne(400);

                if (beatChar == 'o')
                    flashRed.FadeOutFromOne(200);

                if (beatChar == 'x')
                    flashBlue.FadeOutFromOne(200);
            }
        }
    }
}
