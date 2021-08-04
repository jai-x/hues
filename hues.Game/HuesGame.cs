using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;
using hues.Game.Elements;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        [Cached]
        private SongList songList = new SongList
        {
            Anchor = Anchor.BottomCentre,
            Origin = Anchor.BottomCentre,
            Y = -55,
            X = 100,
        };

        [Cached]
        private ImageList imageList = new ImageList
        {
            Anchor = Anchor.BottomCentre,
            Origin = Anchor.BottomCentre,
            Y = -55,
            X = 250,
        };

        [Cached]
        private Settings settings = new Settings
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        };

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
                songList,
                imageList,
                settings,
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
