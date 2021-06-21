using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

namespace hues.Game.Tests.Visual
{
    public class TestSceneHuesImageBox : HuesTestScene
    {
        [Cached]
        protected readonly HuesImageManager him = new HuesImageManager();

        public TestSceneHuesImageBox()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                new HuesImageBox(),
            };

            AddStep("Previous Image", him.Previous);
            AddStep("Next Image", him.Next);
        }
    }
}
