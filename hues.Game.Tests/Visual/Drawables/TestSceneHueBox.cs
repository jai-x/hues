using osu.Framework.Allocation;
using osu.Framework.Bindables;

using hues.Game.RespackElements;
using hues.Game.Managers;
using hues.Game.Drawables;

using NUnit.Framework;

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
            Schedule(() =>
            {
                hueManager.Add(Hue.All);
                Child = new HueBox();;
            });
        }

        [Test]
        public void TestChangeColours()
        {
            AddStep("Next", () => { hueManager.Next(); });
            AddStep("Previous", () => { hueManager.Next(); });
        }
    }
}
