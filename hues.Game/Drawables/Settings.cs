using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
{
    public class Settings : VisibilityContainer
    {
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
                    Text = "by jai_",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = 70,
                },
                new SpriteText
                {
                    Text = "inspired by: mon, kepstin, and AMM",
                    Colour = Colour4.Black,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = 110,
                },
                new SpriteText
                {
                    Text = "drag respack into window to load it",
                    Colour = Colour4.Black,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = FontUsage.Default.With(size: 14),
                },
                new SpriteText
                {
                    Text = "F11: window mode",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -95,
                },
                new SpriteText
                {
                    Text = "Ctrl + F1: draw visualiser",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -80,
                },
                new SpriteText
                {
                    Text = "Ctrl + F2: global statistics",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -65,
                },
                new SpriteText
                {
                    Text = "Ctrl + F3: texture visualiser",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -50,
                },
                new SpriteText
                {
                    Text = "Ctrl + F10: debug log",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -35,
                },
                new SpriteText
                {
                    Text = "Ctrl + F11: peformance/fps overlay",
                    Colour = Colour4.Black,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Font = FontUsage.Default.With(size: 12),
                    Y = -20,
                },
            };
        }
    }
}
