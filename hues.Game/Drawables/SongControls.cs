using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
{
    public class SongControls : ArrowControls
    {
        protected override string LabelText => "SONGS";
        protected override IconUsage CentreIcon => FontAwesome.Solid.Random;
    }
}
