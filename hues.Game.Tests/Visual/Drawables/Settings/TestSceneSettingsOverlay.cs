using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables.Settings;

namespace hues.Game.Tests.Visual.Drawables.Settings
{
    [TestFixture]
    public class TestSceneSettingsOverlay : HuesRespackLoadedTestScene
    {
        private SettingsOverlay settings;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                settings = new SettingsOverlay
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
