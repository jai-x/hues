using System;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osuTK;

using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Stores;
using hues.Game.Tests.Resources;

using NUnit.Framework;

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
                var respackSongs = TestResources.OpenResource("Respacks/DefaultsHQ.zip");
                var respackImages = TestResources.OpenResource("Respacks/DefaultImages.zip");

                respackLoader.LoadStream(respackSongs);
                respackLoader.LoadStream(respackImages);

                hueManager.Add(Hue.All);

                Child = new InfoBar
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            });
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
