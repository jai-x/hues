using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Threading;
using osuTK;

namespace hues.Game
{
    public class HuesMain : CompositeDrawable
    {
        [Cached]
        protected readonly HuesColourManager hcm = new HuesColourManager();

        private Box box;
        private SpriteText colourText;

        public HuesMain()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new HuesColourBox(),
                new SpriteText
                {
                    Y = 20,
                    Text = "0x40 Hues",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40),
                },
                colourText = new SpriteText
                {
                    Y = -20,
                    Text = hcm.Current.Value.Name,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
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

            hcm.Current.ValueChanged += (hc) => colourText.Text = hc.NewValue.Name;

            box.Loop(b => b.RotateTo(0).RotateTo(360, 2500));

            Scheduler.AddDelayed(hcm.Next, 500, true);
        }
    }
}
