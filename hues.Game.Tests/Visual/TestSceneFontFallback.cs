using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Tests.Visual
{
    [TestFixture]
    public class TestSceneFontFallback : HuesTestScene
    {
        private const string testText = "test text 日本語 with CJK 한국어 in between";

        private FillFlowContainer flow;

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
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = true,
                    ScrollbarOverlapsContent = false,
                    Child = flow = new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Spacing = new Vector2(0, 20),
                    },
                },
            };
        }

        private Drawable makeText(int i, string text) => new Container
        {
            Masking = true,
            AutoSizeAxes = Axes.Both,
            BorderColour = Colour4.Red,
            BorderThickness = 2,
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent,
                },
                new SpriteText
                {
                    Text = $"Size {i}:{text}",
                    Colour = Colour4.Black,
                    Font = FontUsage.Default.With(size: i),
                }
            },
        };

        protected override void LoadComplete()
        {
            base.LoadComplete();

            for (var i = 9; i < 100; i++)
                flow.Add(makeText(i, testText));
        }

        [Test]
        public void TestVisual()
        { }
    }
}
