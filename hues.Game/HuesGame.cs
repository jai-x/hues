using osu.Framework.Allocation;
using osu.Framework.Graphics;

using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        [Resolved]
        private HueManager hueManager { get; set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Load the hues
            hueManager.Add(Hue.All);

            Children = new Drawable[]
            {
                new InfoBar
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
            };
        }
    }
}
