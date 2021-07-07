using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public class HuesTestSceneTestRunner : HuesGameBase, ITestSceneTestRunner
    {
        private TestSceneTestRunner.TestRunner runner;

        protected override void LoadAsyncComplete()
        {
            base.LoadAsyncComplete();
            Add(runner = new TestSceneTestRunner.TestRunner());
        }

        public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
    }
}
