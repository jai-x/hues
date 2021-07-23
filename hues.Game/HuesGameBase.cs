using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
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
        // Main content
        private Container<Drawable> content;
        protected override Container<Drawable> Content => content;

        // Override dependencies
        private DependencyContainer dependencies;
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));


        // Backing in memory resources
        private RespackTrackResourceStore trackResources;
        private RespackTextureResourceStore textureResources;

        // Stores to fetch the respack tracks and textures
        private RespackTrackStore trackStore;
        private RespackTextureStore textureStore;

        // Bindables
        private Bindable<Song> currentSong;
        private Bindable<RespackElements.Image> currentImage;
        private Bindable<Colour> currentColour;
        private Bindable<PlayableSong> currentPlayable;

        // Managers
        private SongManager songManager;
        private ImageManager imageManager;
        private ColourManager colourManager;

        // Respack loader
        private RespackLoader respackLoader;

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add resources assembly
            Resources.AddStore(new DllResourceStore(HuesResources.ResourceAssembly));

            // Add PetMe font
            AddFont(Resources, "Fonts/PetMe64/PetMe64");

            // Init and cache backing resources
            dependencies.CacheAs(trackResources = new RespackTrackResourceStore());
            dependencies.CacheAs(textureResources = new RespackTextureResourceStore());

            // Init and cache stores
            dependencies.CacheAs(trackStore = new RespackTrackStore(Audio.GetTrackStore(trackResources)));
            dependencies.CacheAs(textureStore = new RespackTextureStore(Host.CreateTextureLoaderStore(textureResources)));

            // Init and cache bindables
            dependencies.CacheAs(currentSong = new Bindable<Song>());
            dependencies.CacheAs(currentImage = new Bindable<RespackElements.Image>());
            dependencies.CacheAs(currentColour = new Bindable<Colour>());
            dependencies.CacheAs(currentPlayable = new Bindable<PlayableSong>());

            // Init and cache managers and respack loader
            dependencies.CacheAs(songManager = new SongManager());
            dependencies.CacheAs(imageManager = new ImageManager());
            dependencies.CacheAs(colourManager = new ColourManager());
            dependencies.CacheAs(respackLoader = new RespackLoader());

            // Add managers and respack loader to hierarchy
            AddInternal(songManager);
            AddInternal(imageManager);
            AddInternal(colourManager);
            AddInternal(respackLoader);

            // Song player
            AddInternal(new SongPlayer());

            // All game content is child of the DrawSizePreservingFillContainer
            content = new DrawSizePreservingFillContainer { TargetDrawSize = new Vector2(1366, 768) };
            base.Content.Add(content);
        }
    }
}
