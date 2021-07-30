using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;
using osuTK;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneBeatCircle : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new BeatCircle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(60),
            };
        }
    }
}
