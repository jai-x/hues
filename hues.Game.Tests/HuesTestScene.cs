using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Testing;
using hues.Game.Managers;
using hues.Game.ResourceStores;

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
        private HueManager hueManager { get; set; }

        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [SetUp]
        public void BaseSetUp()
        {
            Schedule(() =>
            {
                songManager.Clear();
                imageManager.Clear();
                hueManager.Clear();
                trackResources.Clear();
                textureResources.Clear();
                respackLoader.Clear();
            });
        }
    }
}
