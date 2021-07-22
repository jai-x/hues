using System;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Managers;
using hues.Game.ResourceStores;
using hues.Game.Tests;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Test.NonVisual.Managers
{
    [HeadlessTest]
    [TestFixture]
    public class TestRespackManager : HuesTestScene
    {
        [Cached]
        private readonly RespackTrackResourceStore trackResources = new RespackTrackResourceStore();

        [Cached]
        private readonly RespackTextureResourceStore textureResources = new RespackTextureResourceStore();

        [Cached]
        private readonly ImageManager imageManager = new ImageManager();

        private RespackManager manager;

        [SetUp]
        public void SetUp()
        {
            AddStep("Clear texture resources", () => { textureResources.Clear(); });

            AddStep("Clear track resources", () => { trackResources.Clear(); });

            AddStep("Expire manager instance", () => { manager?.Expire(); });

            AddStep("Recreate manage instance", () => { Child = manager = new RespackManager(); });
        }

        [Test]
        public void TestNoInfoFile()
        {
            var emptyRespack = TestResources.OpenResource("Respacks/emptyRespack.zip");
            RespackMissingFileException ex = null;

            AddStep("Add respack with no info.xml file", () =>
            {
                try
                {
                    manager.Add(emptyRespack);
                }
                catch (RespackMissingFileException e)
                {
                    ex = e;
                }
            });

            AddAssert("Exception was thrown", () => ex != null);

            AddAssert("Exception has correct message", () =>
            {
                return ex.Message == "Unable to find file `info.xml` in respack archive";
            });
        }

        [Test]
        public void TestNoSongFile()
        {
            var noSongRespack = TestResources.OpenResource("Respacks/noSongRespack.zip");
            RespackMissingFileException ex = null;

            AddStep("Add respack with missing song file", () =>
            {
                try
                {
                    manager.Add(noSongRespack);
                }
                catch (RespackMissingFileException e)
                {
                    ex = e;
                }
            });

            AddAssert("Exception was thrown", () => ex != null);

            AddAssert("Exception has correct message", () =>
            {
                return ex.Message == "Unable to find file `loop_file_not_exist` in respack archive";
            });
        }

        [Test]
        public void TestNoImageFile()
        {
            var noSongRespack = TestResources.OpenResource("Respacks/noImageRespack.zip");
            RespackMissingFileException ex = null;

            AddStep("Add respack with missing song file", () =>
            {
                try
                {
                    manager.Add(noSongRespack);
                }
                catch (RespackMissingFileException e)
                {
                    ex = e;
                }
            });

            AddAssert("Exception was thrown", () => ex != null);

            AddAssert("Exception has correct message", () =>
            {
                return ex.Message == "Unable to find file `image_file_not_exist` in respack archive";
            });
        }

        [Test]
        public void TestAddRespack()
        {
            var respack = TestResources.OpenResource("Respacks/fullRespack.zip");

            AddStep("Add respack with song and image files", () => { manager.Add(respack); });

            AddAssert("Respack added", () => manager.Respacks.Count == 1);

            AddAssert("Songs added", () => manager.Respacks.First().Songs.Count == 1);

            AddAssert("Images added", () => manager.Respacks.First().Images.Count == 1);

            AddAssert("Track resources stored", () => trackResources.Get("track_sample") != null);

            AddAssert("Texture resources stored", () => textureResources.Get("texture_sample") != null);
        }
    }
}
