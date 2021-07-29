using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.RespackElements;
using hues.Game.Managers;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneHueBox : HuesTestScene
    {
        [Resolved]
        private HueManager hueManager { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() => { hueManager.Add(Hue.All); });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new HueBox { RelativeSizeAxes = Axes.Both };
        }

        [Test]
        public void TestChangeColours()
        {
            AddStep("Next", () => { hueManager.Next(); });
            AddStep("Previous", () => { hueManager.Next(); });
        }
    }
}
