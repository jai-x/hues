using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables.Settings;

namespace hues.Game.Tests.Visual.Drawables.Settings
{
    [TestFixture]
    public class TestSceneSettingsTextBox : HuesTestScene
    {
        private SettingsTextBoxWithLabel option;

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
                option = new SettingsTextBoxWithLabel
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Label = "Only Accepts (hello)",
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            option.TextBox.TextCommitted += (newText) =>
            {
                if (newText == "hello")
                    return;

                option.FlashErrorMessage("Value needs to be (hello)");
                option.TextBox.FlashError();
            };
        }

        [Test]
        public void TestVisual()
        { }
    }
}
