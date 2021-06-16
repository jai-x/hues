using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new HuesContainer();
        }
    }
}
