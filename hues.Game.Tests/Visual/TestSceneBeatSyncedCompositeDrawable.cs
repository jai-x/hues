using System;

using osu.Framework.Allocation;
using osu.Framework.Audio;
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

        [Resolved]
        private AudioManager audioManager { get; set; }

        private WorkingBeatmap testBeatmap;

        public TestSceneBeatSyncedCompositeDrawable()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new TestBeatDrawable();

            testBeatmap = new WorkingBeatmap(Beatmap.All[0], audioManager);

            testBeatmap.Load();
            workingBeatmap.Value = testBeatmap;

            AddStep("Reset", testBeatmap.Reset);
            AddStep("Start", testBeatmap.Start);
        }

        protected override void Dispose(bool isDisposing)
        {
            testBeatmap?.Dispose();
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
                        Alpha = 0f,
                    },
                };

                updateName(workingBeatmap.Value);
                workingBeatmap.ValueChanged += (change) => updateName(change.NewValue);
            }

            private void updateName(WorkingBeatmap b) => name.Text = b?.Beatmap.Name ?? "none";

            protected override void OnNewBeat(int beatIndex, Section beatSection, char beatChar, double beatLength)
            {
                base.OnNewBeat(beatIndex, beatSection, beatChar, beatLength);

                index.Text = "0x" + beatIndex.ToString("X2");
                section.Text = beatSection.ToString();

                if (beatChar == '.')
                    return;

                beatchar.Text = beatChar.ToString();
                beatchar.FadeIn().Then().Delay(beatLength).Then().FadeOut();

                if (beatChar == 'o')
                    flashRed.FadeOutFromOne(beatLength);

                if (beatChar == 'x')
                    flashBlue.FadeOutFromOne(beatLength);
            }
        }
    }
}
