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

using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Stores;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestBeatSyncedCompositeDrawable : HuesTestScene
    {
        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                var respack = TestResources.OpenResource("Respacks/DefaultsHQ.zip");
                respackLoader.LoadStream(respack);
                Child = new TestBeatDrawable();
            });
        }

        [Test]
        public void TestOnBeatWorks()
        {
            AddStep("Next song", () => { songManager.Next(); });
            AddStep("Previous song", () => { songManager.Previous(); });
            AddStep("Song stop", () => { playableSong.Value?.Stop(); });
            AddStep("Song start", () => { playableSong.Value?.Start(); });
            AddStep("Song reset", () => { playableSong.Value?.Reset(); });
        }

        private class TestBeatDrawable : BeatSyncedCompositeDrawable
        {
            private SpriteText section;
            private SpriteText index;
            private SpriteText title;
            private SpriteText beatchar;
            private Box flashRed;
            private Box flashBlue;

            [Resolved]
            private Bindable<Song> currentSong { get; set; }

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
                    title = new SpriteText
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

                currentSong.BindValueChanged(change => { title.Text = change.NewValue?.Title ?? "none"; }, true);
            }

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
