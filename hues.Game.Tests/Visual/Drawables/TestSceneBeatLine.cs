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
                        Text = ">>",
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    new BeatLine
                    {
                        X = 50,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        MaxChars = 35,
                        CharSize = 30,
                    },
                };
            }
        }
    }
}
