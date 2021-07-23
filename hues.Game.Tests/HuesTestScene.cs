using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Testing;

using hues.Game.Managers;
using hues.Game.Stores;
using hues.Game.RespackElements;
using hues.Game.ResourceStores;
using hues.Game.Resources;

using NUnit.Framework;

namespace hues.Game.Tests
{
    public abstract class HuesTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HuesTestSceneTestRunner();

        [Resolved]
        private RespackTrackResourceStore trackResources { get; set; }

        [Resolved]
        private RespackTextureResourceStore textureResources { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [SetUp]
        public void BaseSetUp()
        {
            AddStep("Clear songs", () => { songManager.Clear(); });
            AddStep("Clear images", () => { imageManager.Clear(); });
            AddStep("Clear tracks", () => { trackResources.Clear(); });
            AddStep("Clear textures", () => { textureResources.Clear(); });
            AddStep("Clear respacks", () => { respackLoader.Clear(); });
        }
    }
}
