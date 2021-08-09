using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
{
    public class Settings : VisibilityContainer
    {
        [Resolved]
        private HuesGameBase game { get; set; }

        protected override void PopIn()
        {
            this.FadeIn(200);
        }

        protected override void PopOut()
        {
            this.FadeOut(200);
        }

        public Settings()
        {
            Height = 400;
            Width = 800;
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 1;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.LightGray,
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText
                {
                    Text = "hues",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 20),
                    Y = 30,
                },
                new SpriteText
                {
                    Text = (game.IsReleased ? "v" : "") + game.Version,
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = 50,
                },
                new SpriteText
                {
                    Text = $"running on osu-framework v{game.FrameworkVersion}",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = 65,
                },
                new SpriteText
                {
                    Text = "drag a respack into the window to load it",
                    Colour = Colour4.Black,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 14),
                },
                new SpriteText
                {
                    Text = "F11:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -100,
                },
                new SpriteText
                {
                    Text = "window mode",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -100,
                },
                new SpriteText
                {
                    Text = "Ctrl + F1:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -84,
                },
                new SpriteText
                {
                    Text = "draw visualiser",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -84,
                },
                new SpriteText
                {
                    Text = "Ctrl + F2:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -68,
                },
                new SpriteText
                {
                    Text = "global statistics",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -68,
                },
                new SpriteText
                {
                    Text = "Ctrl + F3:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -52,
                },
                new SpriteText
                {
                    Text = "texture visualiser",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -52,
                },
                new SpriteText
                {
                    Text = "Ctrl + F10:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -36,
                },
                new SpriteText
                {
                    Text = "debug log",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -36,
                },
                new SpriteText
                {
                    Text = "Ctrl + F11:",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomRight,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -20,
                },
                new SpriteText
                {
                    Text = "performance/FPS display",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomLeft,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -20,
                },
            };
        }
    }
}
