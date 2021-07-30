using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osuTK;
using hues.Game.Drawables;
using hues.Game.Elements;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneBeatSyncedCompositeDrawable : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new TestBeatDrawable();
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

            [BackgroundDependencyLoader]
            private void load()
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
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                currentSong.BindValueChanged(change => { title.Text = change.NewValue?.Title ?? "none"; }, true);
            }

            protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
            {
                base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);

                index.Text = "0x" + beatIndex.ToString("X2");
                section.Text = songSection.ToString();

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
