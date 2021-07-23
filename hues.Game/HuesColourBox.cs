using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace hues.Game
{
    public class HuesColourBox : Box
    {
        /*
        [Resolved]
        private HuesColourManager hcm { get; set; }

        public HuesColourBox()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Colour = hcm.Current.Value.Colour;
            hcm.Current.ValueChanged += (hc) => Colour = hc.NewValue.Colour;
        }
        */
    }
}
