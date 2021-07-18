using System;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Stores;
using hues.Game.Tests;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Test.NonVisual
{
    [TestFixture]
    [HeadlessTest]
    public class TestRespackManager : HuesTestScene
    {
        [Cached]
        protected readonly RespackTrackResourceStore trackResources = new RespackTrackResourceStore();

        [Cached]
        protected readonly RespackTextureResourceStore textureResources = new RespackTextureResourceStore();

        private RespackManager manager;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = manager = new RespackManager();
        }

        [Test]
        public void TestNoInfoFile()
        {
            AddStep("Add respack with no info.xml file", () =>
            {
                var emptyRespack = TestResources.OpenResource("Respacks/emptyRespack.zip");

                var ex = Assert.Throws(typeof(RespackMissingFileException), () => { manager.Add(emptyRespack); });
                Assert.AreEqual(ex.Message, "Unable to find file `info.xml` in respack archive");
            });
        }

        [Test]
        public void TestNoSongFile()
        {
            AddStep("Add respack with missing song files", () =>
            {
                var noSongRespack = TestResources.OpenResource("Respacks/noSongRespack.zip");

                var ex = Assert.Throws(typeof(RespackMissingFileException), () => { manager.Add(noSongRespack); });
                Assert.AreEqual(ex.Message, "Unable to find file `loop_file_not_exist` in respack archive");
            });
        }

        [Test]
        public void TestAddRespack()
        {
            AddStep("Add respack with song and image files", () =>
            {
                var respack = TestResources.OpenResource("Respacks/fullRespack.zip");

                manager.Add(respack);

                Assert.AreEqual(manager.Respacks.Count, 1);
                Assert.AreEqual(manager.Respacks.First().Songs.Count, 1);
                Assert.AreEqual(manager.Respacks.First().Images.Count, 1);

                Assert.NotNull(trackResources.Get("track_sample"));
                Assert.NotNull(textureResources.Get("texture_sample"));
            });
        }
    }
}
