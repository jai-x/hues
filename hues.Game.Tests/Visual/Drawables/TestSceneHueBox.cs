using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneHueBox : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new HueBox { RelativeSizeAxes = Axes.Both };
        }
    }
}
