using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;


using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneImageBox : HuesTestScene
    {
        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                var images = TestResources.OpenResource("Respacks/DefaultImages.zip");
                respackLoader.LoadStream(images);
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                new ImageBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Next", () => { imageManager.Next(); });
            AddStep("Previous", () => { imageManager.Next(); });
        }
    }
}
