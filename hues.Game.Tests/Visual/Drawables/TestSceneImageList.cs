using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneImageList : HuesRespackLoadedTestScene
    {
        private ImageList imageList;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = imageList = new ImageList
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Show", () => { imageList.Show(); });
            AddStep("Hide", () => { imageList.Hide(); });
        }
    }
}
