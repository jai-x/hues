using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace hues.Game
{
    public class HuesVisualBox : CompositeDrawable
    {
        public HuesVisualBox()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "White Background",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                new HuesImageBox(),
                new HuesColourBox
                {
                    Alpha = 0.7f,
                },
                new Box
                {
                    Name = "Blackout",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black,
                    Alpha = 0f,
                },
            };
        }
    }
}
