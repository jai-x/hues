using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneBeatLine : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new TestBeatLine();
        }

        private class TestBeatLine : CompositeDrawable
        {
            public TestBeatLine()
            {
                RelativeSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                InternalChildren = new Drawable[]
                {
                    new SpriteText
                    {
                        Y = -20,
                        Text = ">>",
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    new BeatLine
                    {
                        Y = -20,
                        X = 50,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Direction = BeatlineDirection.Left,
                        MaxChars = 35,
                        CharSize = 30,
                    },
                    new SpriteText
                    {
                        Y = 20,
                        Text = "<<",
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                    },
                    new BeatLine
                    {
                        Y = 20,
                        X = -50,
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Direction = BeatlineDirection.Right,
                        MaxChars = 35,
                        CharSize = 30,
                    },
                };
            }
        }
    }
}
