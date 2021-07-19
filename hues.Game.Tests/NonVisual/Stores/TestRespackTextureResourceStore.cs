using System;
using System.IO;
using System.Threading;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Stores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual.Stores
{
    [HeadlessTest]
    public class TestRespackTextureResourceStore : HuesTestScene
    {
        [Resolved]
        private GameHost gameHost { get; set; }

        [Resolved]
        private TextureStore globalTextures { get; set; }

        private readonly RespackTextureResourceStore textureResources = new RespackTextureResourceStore();

        private IResourceStore<TextureUpload> textureStore;

        [Test]
        public void TestAddTexture()
        {
            AddStep("Create texture store from resources", () =>
            {
                textureStore = gameHost.CreateTextureLoaderStore(textureResources);
            });

            AddStep("Add textureStore to global texture store", () =>
            {
                globalTextures.AddStore(textureStore);
            });

            AddStep("Add texture file to resources", () =>
            {
                using (var source = TestResources.OpenResource("Textures/sample-texture.png"))
                    textureResources.Add("sample-texture.png", source);
            });

            AddStep("Assert texture file has been added", () =>
            {
                Assert.NotNull(textureResources.Get("sample-texture.png"));
            });

            AddStep("Assert texture can be fetched from global texture store", () =>
            {
                Assert.NotNull(globalTextures.Get("sample-texture"));
            });
        }
    }
}
