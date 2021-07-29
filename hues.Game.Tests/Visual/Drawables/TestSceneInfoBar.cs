using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneInfoBar : HuesTestScene
    {
        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private HueManager hueManager { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                respackLoader.LoadStream(TestResources.OpenResource("Respacks/DefaultsHQ.zip"));
                respackLoader.LoadStream(TestResources.OpenResource("Respacks/DefaultImages.zip"));
                hueManager.Add(Hue.All);
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new InfoBar
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Next song", () => { songManager.Next(); });
            AddStep("Previous song", () => { songManager.Previous(); });
            AddStep("Next hue", () => { hueManager.Next(); });
            AddStep("Previous hue", () => { hueManager.Previous(); });
            AddStep("Next image", () => { imageManager.Next(); });
            AddStep("Previous image", () => { imageManager.Previous(); });
            AddStep("Song stop", () => { playableSong.Value?.Stop(); });
            AddStep("Song start", () => { playableSong.Value?.Start(); });
            AddStep("Song reset", () => { playableSong.Value?.Reset(); });
        }
    }
}
