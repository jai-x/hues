using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneSettingsTabControl : HuesRespackLoadedTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Gray,
                },
                new SettingsTabControl
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 900,
                    Height = 50,
                },
            };
        }
    }
}
