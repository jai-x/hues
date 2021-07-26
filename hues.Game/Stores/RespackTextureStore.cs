using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osuTK.Graphics.ES30;

namespace hues.Game.Stores
{
    public class RespackTextureStore : LargeTextureStore
    {
        // From osuTK/Graphics/ES10/Enum.cs
        // All.Nearest
        // All.Linear (default)
        private const All filterMode = All.Linear;

        public RespackTextureStore(IResourceStore<TextureUpload> store)
            : base(store, filterMode)
        { }
    }
}
