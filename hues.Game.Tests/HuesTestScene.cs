using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public abstract class HuesTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HuesTestSceneTestRunner();
    }
}
