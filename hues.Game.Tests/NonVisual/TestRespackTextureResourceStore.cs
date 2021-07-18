using System;
using System.IO;
using System.Threading;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Stores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual
{
    [HeadlessTest]
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
            using (var source = TestResources.OpenResource("Textures/sample-texture.png"))
                textureResources.Add("sample-texture.png", source);

            Assert.NotNull(textureResources.Get("sample-texture.png"));

            var textureStore = gameHost.CreateTextureLoaderStore(textureResources);

            globalTextures.AddStore(textureStore);

            var addedTexture = globalTextures.Get("sample-texture");

            // FIXME: Find out why this works sometimes?
            Assert.NotNull(addedTexture);
        }
    }
}
