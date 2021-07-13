using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public class HuesManualInputTestScene : ManualInputManagerTestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HuesTestSceneTestRunner();
    }
}
