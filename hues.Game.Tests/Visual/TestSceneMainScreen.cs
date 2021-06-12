using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace hues.Game.Tests.Visual
{
    public class TestSceneMainScreen : HuesTestScene
    {
        public TestSceneMainScreen()
        {
            Add(new ScreenStack(new MainScreen()) { RelativeSizeAxes = Axes.Both }); }
    }
}
