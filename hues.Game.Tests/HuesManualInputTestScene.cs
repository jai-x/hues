using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public abstract class HuesManualInputTestScene : ManualInputManagerTestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HuesTestSceneTestRunner();
    }
}
