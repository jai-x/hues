using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;
using hues.Game.Drawables.Settings;
using hues.Game.Elements;

namespace hues.Game
{
    public class HuesGame : HuesGameBase
    {
        // Override dependencies
        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            var settingsOverlay = new SettingsOverlay
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };

            var songList = new SongList
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Y = -55,
                X = 100,
            };

            var imageList = new ImageList
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Y = -55,
                X = 250,
            };

            dependencies.CacheAs(songList);
            dependencies.CacheAs(imageList);
            dependencies.CacheAs(settingsOverlay);

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
                settingsOverlay,
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
