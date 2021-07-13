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
        protected readonly Bindable<WorkingSong> workingSong = new Bindable<WorkingSong>();

        [Resolved]
        private AudioManager audioManager { get; set; }

        private WorkingSong testSong;

        public TestSceneBeatSyncedCompositeDrawable()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new TestBeatDrawable();

            testSong = new WorkingSong(Song.All[0], audioManager);

            testSong.Load();
            workingSong.Value = testSong;

            AddStep("Reset", testSong.Reset);
            AddStep("Start", testSong.Start);
        }

        protected override void Dispose(bool isDisposing)
        {
            testSong?.Dispose();
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
            private Bindable<WorkingSong> workingSong { get; set; }

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

                updateName(workingSong.Value);
                workingSong.ValueChanged += (change) => updateName(change.NewValue);
            }

            private void updateName(WorkingSong b) => name.Text = b?.Song.Name ?? "none";

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
