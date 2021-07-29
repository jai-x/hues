using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneBeatCircle : HuesTestScene
    {
        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                var respack = TestResources.OpenResource("Respacks/DefaultsHQ.zip");
                respackLoader.LoadStream(respack);
                Child = new BeatCircle();
            });
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Next song", () => { songManager.Next(); });
            AddStep("Previous song", () => { songManager.Previous(); });
            AddStep("Song stop", () => { playableSong.Value?.Stop(); });
            AddStep("Song start", () => { playableSong.Value?.Start(); });
            AddStep("Song reset", () => { playableSong.Value?.Reset(); });
        }
    }
}
