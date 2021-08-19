using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using hues.Game.Drawables.Settings;

namespace hues.Game.Tests.Visual.Drawables.Settings
{
    [TestFixture]
    public class TestSceneSettingsDropdown : HuesTestScene
    {
        private SettingsDropdown<string> dropdown;
        private SpriteText text;

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
                dropdown = new SettingsDropdown<string>
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    Width = 200,
                    Items = new string[]
                    {
                        "Item 1",
                        "Item 2",
                        "Item 3",
                        "Item 4",
                    },
                },
                text = new SpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Colour = Colour4.Black,
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            dropdown.Current.BindValueChanged((change) => { text.Text = $"Selected: {change.NewValue}"; }, true);
        }

        [Test]
        public void TestVisual()
        { }
    }
}
