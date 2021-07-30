using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;
using hues.Game.Elements;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new HuesMain
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new BeatBar
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                new InfoBar
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            // Load the hues
            hueManager.Add(Hue.All);
        }
    }
}
