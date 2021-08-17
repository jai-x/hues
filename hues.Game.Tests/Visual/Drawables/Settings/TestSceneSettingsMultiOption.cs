using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using hues.Game.Drawables.Settings;

namespace hues.Game.Tests.Visual.Drawables.Settings
{
    [TestFixture]
    public class TestSceneSettingsMultiOption : HuesTestScene
    {
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
                options = new SettingsMultiOptionWithLabel<string>
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Width = 0.5f,
                    Label = "Some Label",
                    Items = new string[]
                    {
                        "Item 1",
                        "Item 2",
                        "Item 3",
                        "Item 4",
                        "Item 5",
                    },
                },
                selectionText = new SpriteText
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Colour = Colour4.Black,
                },
            };
        }

        private SettingsMultiOptionWithLabel<string> options;
        private SpriteText selectionText;

        protected override void LoadComplete()
        {
            options.Current.BindValueChanged(change =>
            {
                selectionText.Text = $"Selected value: {change.NewValue}";
            }, true);
        }

        [Test]
        public void TestVisual()
        { }
    }
}
