using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace hues.Game.Tests.Visual
{
    public class TestSceneHuesContainer : HuesTestScene
    {
        [Cached]
        protected readonly HuesColourManager hcm = new HuesColourManager();

        public TestSceneHuesContainer()
        {
            Add(new HuesContainer());
        }
    }
}
