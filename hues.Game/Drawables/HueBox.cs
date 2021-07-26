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

        [BackgroundDependencyLoader]
        private void load()
        {
            currentHue.BindValueChanged(hueChange =>
            {
                var hue = hueChange.NewValue;

                if (hue == null)
                    Colour = Colour4.Black;
                else
                    Colour = hue.Colour4;
            });
        }
    }
}
