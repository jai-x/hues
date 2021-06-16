using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Threading;
using osuTK;

namespace hues.Game
{
    public class HuesContainer : CompositeDrawable
    {
        private Box box;
        private ColourBox cbox;

        public HuesContainer()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                cbox = new ColourBox(),
                new SpriteText
                {
                    Y = 20,
                    Text = "0x40 Hues",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40),
                },
                box = new Box
                {
                    Colour = Colour4.Red,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(300)
                },
            };

            box.Loop(b => b.RotateTo(0).RotateTo(360, 2500));
            Scheduler.AddDelayed(cbox.NextColour, 500, true);
        }
    }
}
