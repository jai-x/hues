using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace hues.Game.Stores
{
    public class RespackTextureStore : LargeTextureStore
    {
        public RespackTextureStore(IResourceStore<TextureUpload> store)
            : base(store)
        { }
    }
}
