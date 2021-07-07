using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public class HuesTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HuesTestSceneTestRunner();
    }
}
