using System;
using System.IO;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.ResourceStores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual.ResourceStores
{
    [HeadlessTest]
    [TestFixture]
    public class TestRespackTextureResourceStore : HuesTestScene
    {
        [Resolved]
        private GameHost gameHost { get; set; }

        [Resolved]
        private TextureStore globalTextures { get; set; }

        private readonly RespackTextureResourceStore textureResources = new RespackTextureResourceStore();

        [Test]
        public void TestAddTexture()
        {
            var textureStream = TestResources.OpenResource("Textures/sample-texture.png");
            IResourceStore<TextureUpload> textureStore = null;

            AddAssert("Texture store is null", () =>
            {
                return textureStore == null;
            });

            AddStep("Create texture store from resources", () =>
            {
                textureStore = gameHost.CreateTextureLoaderStore(textureResources);
            });

            AddAssert("Texture store is created", () =>
            {
                return textureStore != null;
            });

            AddStep("Add textureStore to global texture store", () =>
            {
                globalTextures.AddStore(textureStore);
            });

            AddStep("Add texture file to resources", () =>
            {
                textureResources.Add("sample-texture", textureStream, textureStream.Length);
            });

            AddAssert("Texture file has been added", () =>
            {
                return textureResources.Get("sample-texture") != null;
            });

            AddAssert("Texture can be fetched from global texture store", () =>
            {
                return globalTextures.Get("sample-texture") != null;
            });
        }
    }
}
