using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using hues.Game.Drawables.Editor;

namespace hues.Game.Tests.Visual.Drawables.Editor
{
    [TestFixture]
    public class TestSceneEditorTextbox : HuesTestScene
    {
        private SpriteText perRowText;
        private EditorTextbox editor;
        private string testString = "x..xo...x...o...x..xo...x...o...x..xo...x...o...x..xo...x...oxoox..xo...x...o...x..xo...x...o...x..xo...x...o...x...o...x...oooo";
        private int highlightIndex = 0;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                perRowText = new SpriteText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FrameworkFont.Regular,
                },
                editor = new EditorTextbox
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.5f,
                    Height = 0.5f,
                    Beatchars = testString,
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updatePerRowText(editor.MaxBeatcharsPerRow);
        }

        private void updatePerRowText(int row) => perRowText.Text = $"Max Beatchars Per Row: {row}";

        [Test]
        public void TestVisual()
        {
            AddStep("Reset Highlight", () =>
            {
                highlightIndex = 0;
                editor.HighlightPosition = null;
            });

            AddStep("Increment Highlight", () =>
            {
                editor.HighlightPosition = highlightIndex++;
            });

            AddStep("Decrement Highlight", () =>
            {
                editor.HighlightPosition = highlightIndex--;
            });

            AddStep("Increment MaxBeatcharsPerRow", () =>
            {
                editor.MaxBeatcharsPerRow++;
                updatePerRowText(editor.MaxBeatcharsPerRow);
            });

            AddStep("Decrement MaxBeatcharsPerRow", () =>
            {
                editor.MaxBeatcharsPerRow--;
                updatePerRowText(editor.MaxBeatcharsPerRow);
            });
        }
    }
}
