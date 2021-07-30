using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using hues.Game.Managers;
using hues.Game.Stores;
using hues.Game.RespackElements;
using hues.Game.ResourceStores;
using hues.Game.Resources;

namespace hues.Game
{
    public class HuesGameBase : osu.Framework.Game
    {
        public HuesGameBase()
        {
            Name = "hues";
        }

        // Main content
        private Container<Drawable> content;
        protected override Container<Drawable> Content => content;

        // Override dependencies
        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));


        #region cached attributes

        // Backing in memory resources
        protected RespackTrackResourceStore trackResources;
        protected RespackTextureResourceStore textureResources;

        // Stores to fetch the respack tracks and textures
        protected RespackTrackStore trackStore;
        protected RespackTextureStore textureStore;

        // Bindables
        protected Bindable<Song> currentSong;
        protected Bindable<RespackElements.Image> currentImage;
        protected Bindable<Hue> currentHue;
        protected Bindable<PlayableSong> currentPlayable;

        // Managers
        protected SongManager songManager;
        protected ImageManager imageManager;
        protected HueManager hueManager;

        // Respack loader
        protected RespackLoader respackLoader;

        protected SongPlayer songPlayer;

        #endregion

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add resources assembly
            Resources.AddStore(new DllResourceStore(HuesResources.ResourceAssembly));

            // Add PetMe font
            AddFont(Resources, "Resources/Fonts/PetMe64/PetMe64");

            // Init and cache backing resources
            dependencies.CacheAs(trackResources = new RespackTrackResourceStore());
            dependencies.CacheAs(textureResources = new RespackTextureResourceStore());

            // Init and cache stores
            dependencies.CacheAs(trackStore = new RespackTrackStore(Audio.GetTrackStore(trackResources)));
            dependencies.CacheAs(textureStore = new RespackTextureStore(Host.CreateTextureLoaderStore(textureResources)));

            // Init and cache bindables
            dependencies.CacheAs(currentSong = new Bindable<Song>());
            dependencies.CacheAs(currentImage = new Bindable<RespackElements.Image>());
            dependencies.CacheAs(currentHue = new Bindable<Hue>());
            dependencies.CacheAs(currentPlayable = new Bindable<PlayableSong>());

            // Init and cache managers and respack loader
            dependencies.CacheAs(songManager = new SongManager());
            dependencies.CacheAs(imageManager = new ImageManager());
            dependencies.CacheAs(hueManager = new HueManager());
            dependencies.CacheAs(respackLoader = new RespackLoader());
            dependencies.CacheAs(songPlayer = new SongPlayer());

            // Add managers and respack loader to hierarchy
            AddInternal(songManager);
            AddInternal(imageManager);
            AddInternal(hueManager);
            AddInternal(respackLoader);
            AddInternal(songPlayer);

            // All game content is child of the DrawSizePreservingFillContainer
            content = new DrawSizePreservingFillContainer { TargetDrawSize = new Vector2(1366, 768) };
            base.Content.Add(content);
        }
    }
}
