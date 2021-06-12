using osu.Framework.Graphics;

namespace hues.Game.Tests.Visual
{
    public class TestSceneSpinningBox : HuesTestScene
    {
        public TestSceneSpinningBox()
        {
            Add(new SpinningBox
            {
                Anchor = Anchor.Centre,
            });
        }
    }
}
