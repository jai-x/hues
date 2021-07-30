using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneHuesMain : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new HuesMain
            {
                RelativeSizeAxes = Axes.Both,
            };
        }
    }
}
