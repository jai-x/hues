using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osuTK;

using hues.Game.Resources;

namespace hues.Game
{
    public class HuesGameBase : osu.Framework.Game
    {
        // Anything in this class is shared between the test browser and the game implementation.
        // It allows for caching global dependencies that should be accessible to tests, or changing
        // the screen scaling for all components including the test browser and framework overlays.

        // Ensure underlying `osu.Framework.Game` class Content cannot be directly written to
        protected override Container<Drawable> Content { get; }

        protected HuesGameBase()
        {
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(1366, 768),
            });
        }

        // Create local dependencies
        private DependencyContainer dependencies;

        // Ensure our local dependencies also contain parent dependencies
        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            // Load the hues.Game.Resources assembly
            Resources.AddStore(new DllResourceStore(HuesResources.ResourceAssembly));

            AddFont(Resources, "Fonts/PetMe64/PetMe64");

            // Cache the LargeTextureStore
            var textureResources = new NamespacedResourceStore<byte[]>(Resources, @"Textures");
            var largeTextureStore = new LargeTextureStore(Host.CreateTextureLoaderStore(textureResources));
            dependencies.Cache(largeTextureStore);
        }
    }
}
