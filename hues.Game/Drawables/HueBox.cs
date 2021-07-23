using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;

using hues.Game.RespackElements;

namespace hues.Game.Drawables
{
    public class HueBox : Box
    {
        [Resolved]
        private Bindable<Hue> currentHue { get; set; }

        public HueBox()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            currentHue.BindValueChanged(hueChange => update(hueChange.NewValue), true);
        }

        private void update(Hue hue)
        {
            if (hue == null)
                this.Colour = Colour4.Black;
            else
                this.Colour = hue.Colour4;
        }
    }
}
