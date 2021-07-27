using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
{
    public class SettingsButton : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new SpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Icon = FontAwesome.Solid.Cog,
            };
        }
    }
}
