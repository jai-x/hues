using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneSettings : HuesRespackLoadedTestScene
    {
        private Settings settings;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                settings = new Settings
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Show", () => { settings.Show(); });
            AddStep("Hide", () => { settings.Hide(); });
        }
    }
}
