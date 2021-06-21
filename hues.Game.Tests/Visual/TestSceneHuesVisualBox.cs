using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace hues.Game.Tests.Visual
{
    public class TestSceneHuesVisualBox : HuesTestScene
    {
        [Cached]
        protected readonly HuesColourManager hcm = new HuesColourManager();

        [Cached]
        protected readonly HuesImageManager him = new HuesImageManager();

        public TestSceneHuesVisualBox()
        {
            Child = new HuesVisualBox();
        }
    }
}
