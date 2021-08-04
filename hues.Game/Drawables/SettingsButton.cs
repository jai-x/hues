using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;

namespace hues.Game.Drawables
{
    public class SettingsButton : CompositeDrawable
    {
        [Resolved(CanBeNull = true)]
        private Settings settings { get; set; }

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

        protected override bool OnClick(ClickEvent e)
        {
            settings?.ToggleVisibility();
            return true;
        }
    }
}
