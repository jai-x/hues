using System;

using hues.Game.Stores;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Test.NonVisual
{
    [TestFixture]
    public class TestRespackManager
    {
        [Test]
        public void TestNoInfoFile()
        {
            var manager = new RespackManager(null);
            var emptyRespack = TestResources.OpenResource("Respacks/emptyRespack.zip");

            Assert.Multiple(() =>
            {
                var ex = Assert.Throws(typeof(RespackMissingFileException), () => { manager.Add(emptyRespack); });
                Assert.AreEqual(ex.Message, "Unable to find file `info.xml` in respack archive");
            });
        }

        [Test]
        public void TestNoSongFile()
        {
            var manager = new RespackManager(null);
            var noSongRespack = TestResources.OpenResource("Respacks/noSongRespack.zip");

            Assert.Multiple(() =>
            {
                var ex = Assert.Throws(typeof(RespackMissingFileException), () => { manager.Add(noSongRespack); });
                Assert.AreEqual(ex.Message, "Unable to find file `loop_file_not_exist` in respack archive");
            });
        }

        [Test]
        public void TestAddRespack()
        {
            var trackResources = new RespackTrackResourceStore();
            var manager = new RespackManager(trackResources);
            var respack = TestResources.OpenResource("Respacks/fullRespack.zip");

            manager.Add(respack);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(manager.Respacks.Count, 1);
                Assert.NotNull(trackResources.Get("track_sample"));
            });
        }
    }
}
