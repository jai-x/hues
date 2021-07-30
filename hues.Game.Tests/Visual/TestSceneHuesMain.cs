using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace hues.Game.Tests.Visual
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
