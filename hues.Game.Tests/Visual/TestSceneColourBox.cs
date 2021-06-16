using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace hues.Game.Tests.Visual
{
    public class TestSceneColourBox : HuesTestScene
    {
        private ColourBox col;

        public TestSceneColourBox()
        {
            Child = col = new ColourBox();
            AddStep("Next Colour", col.NextColour);
            AddStep("Black", col.Black);
        }
    }
}
