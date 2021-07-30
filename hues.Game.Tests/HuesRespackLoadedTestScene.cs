using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using hues.Game.Managers;
using hues.Game.Elements;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests
{
    public abstract class HuesRespackLoadedTestScene : HuesTestScene
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
        public void RespackSetUp()
        {
            Schedule(() =>
            {
                respackLoader.LoadStream(TestResources.OpenResource("Respacks/DefaultsHQ.zip"));
                respackLoader.LoadStream(TestResources.OpenResource("Respacks/DefaultImages.zip"));
                hueManager.Add(Hue.All);
            });

            AddStep("Next image", () => { imageManager.Next(); });
            AddStep("Previous image", () => { imageManager.Previous(); });
            AddStep("Next song", () => { songManager.Next(); });
            AddStep("Previous song", () => { songManager.Previous(); });
            AddStep("Next hue", () => { hueManager.Next(); });
            AddStep("Previous hue", () => { hueManager.Previous(); });
            AddStep("Song stop", () => { playableSong.Value?.Stop(); });
            AddStep("Song start", () => { playableSong.Value?.Start(); });
            AddStep("Song reset", () => { playableSong.Value?.Reset(); });
        }
    }
}
