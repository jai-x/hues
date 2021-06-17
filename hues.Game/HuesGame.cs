using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        [Cached]
        protected readonly HuesColourManager hcm = new HuesColourManager();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new HuesContainer();
        }
    }
}
