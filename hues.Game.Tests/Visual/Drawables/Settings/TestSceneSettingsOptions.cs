using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables.Settings;

namespace hues.Game.Tests.Visual.Drawables.Settings
{
    [TestFixture]
    public class TestSceneSettingsOptions : HuesTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new SettingsOptions
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Padding = new MarginPadding(10),
                },
            };
        }

        [Test]
        public void TestVisual()
        { }
    }
}
