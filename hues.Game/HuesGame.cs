using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                new HuesMain(),
            };
        }
    }
}
