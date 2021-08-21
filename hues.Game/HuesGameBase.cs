using System;
using System.Reflection;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osuTK;
using hues.Game.Managers;
using hues.Game.Stores;
using hues.Game.Elements;
using hues.Game.ResourceStores;
using hues.Game.Resources;
using hues.Game.Configuration;

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
        protected BindableList<Respack> allRespacks;
        protected BindableList<Song> allSongs;
        protected Bindable<Song> currentSong;
        protected BindableList<Elements.Image> allImages;
        protected Bindable<Elements.Image> currentImage;
        protected BindableList<Hue> allHues;
        protected Bindable<Hue> currentHue;
        protected Bindable<PlayableSong> currentPlayable;

        // Managers
        protected SongManager songManager;
        protected ImageManager imageManager;
        protected HueManager hueManager;

        // Respack loader
        protected RespackLoader respackLoader;

        // Song player
        protected SongPlayer songPlayer;

        // Config
        protected HuesConfigManager configManager;

        #endregion

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add resources assembly
            Resources.AddStore(new DllResourceStore(HuesResources.ResourceAssembly));

            // Add fonts
            AddFont(Resources, "Resources/Fonts/PetMe64/PetMe64");

            // TODO: Maybe not do this when CSS/Em-square aware font sizing is added to o!f
            // Font Silver is stored in a nested font store at scale 6 to better fit with the line height of PetMe64
            var fallback = new FontStore(null, 60);
            Fonts.AddStore(fallback);
            AddFont(Resources, "Resources/Fonts/Silver-Jai-Mod/Silver-Jai-Mod", fallback);

            // Init and cache backing resources
            dependencies.CacheAs(trackResources = new RespackTrackResourceStore());
            dependencies.CacheAs(textureResources = new RespackTextureResourceStore());

            // Init and cache stores
            dependencies.CacheAs(trackStore = new RespackTrackStore(Audio.GetTrackStore(trackResources)));
            dependencies.CacheAs(textureStore = new RespackTextureStore(Host.CreateTextureLoaderStore(textureResources)));

            // Init and cache bindables
            dependencies.CacheAs(allRespacks = new BindableList<Respack>());
            dependencies.CacheAs(allSongs = new BindableList<Song>());
            dependencies.CacheAs(currentSong = new Bindable<Song>());
            dependencies.CacheAs(allImages = new BindableList<Elements.Image>());
            dependencies.CacheAs(currentImage = new Bindable<Elements.Image>());
            dependencies.CacheAs(allHues = new BindableList<Hue>());
            dependencies.CacheAs(currentHue = new Bindable<Hue>());
            dependencies.CacheAs(currentPlayable = new Bindable<PlayableSong>());

            // Init and cache managers and respack loader
            dependencies.CacheAs(songManager = new SongManager());
            dependencies.CacheAs(imageManager = new ImageManager());
            dependencies.CacheAs(hueManager = new HueManager());
            dependencies.CacheAs(respackLoader = new RespackLoader());
            dependencies.CacheAs(songPlayer = new SongPlayer());
            dependencies.CacheAs(configManager);

            // Cache self :)
            dependencies.CacheAs(this);

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

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            configManager = new HuesConfigManager(host.Storage);
        }

        public string FrameworkVersion
        {
            get
            {
                var version = typeof(osu.Framework.Game).Assembly.GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        private Version gameVersion => Assembly.GetEntryAssembly().GetName().Version;

        public bool IsReleased => gameVersion.Major > 0;

        public string Version
        {
            get
            {
                var version = Assembly.GetEntryAssembly().GetName().Version;

                if (!IsReleased)
                    return $"local-" + (DebugUtils.IsDebugBuild ? "debug" : "release");

                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            configManager?.Dispose();
            songPlayer?.Dispose();
            respackLoader?.Dispose();
            songManager?.Dispose();
            imageManager?.Dispose();
            hueManager?.Dispose();
            trackStore?.Dispose();
            textureStore?.Dispose();
            trackResources?.Dispose();
            textureResources?.Dispose();
        }
    }
}
