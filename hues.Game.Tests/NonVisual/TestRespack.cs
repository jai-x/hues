using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace hues.Game.Tests.NonVisual
{
    [TestFixture]
    public class TestRespack
    {
        private string respackPath;

        private const string infoXml = @"
        <info>
            <name>Test Name</name>
            <author>Test Author</author>
            <description>Test Description</description>
            <link>https://test.link</link>
        </info>
        ";

        private const string songsXml = @"
        <songs>
          <song name=""loop_test_1"">
            <title>Test Song Title 1</title>
            <source>https://song.link/1</source>
            <rhythm>xoxo1</rhythm>
            <buildup>build_test_1</buildup>
            <buildupRhythm>oxox1</buildupRhythm>
          </song>
        </songs>
        ";

        [SetUp]
        public void Init()
        {
            respackPath = Path.GetTempFileName();

            using (var archive = ZipFile.Open(respackPath, ZipArchiveMode.Update))
            {
                var infoEntry = archive.CreateEntry("info.xml");
                using (var infoWriter = new StreamWriter(infoEntry.Open(), Encoding.UTF8))
                    infoWriter.Write(infoXml);

                var songsEntry = archive.CreateEntry("songs.xml");
                using (var songsWriter = new StreamWriter(songsEntry.Open(), Encoding.UTF8))
                    songsWriter.Write(songsXml);
            }
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(respackPath);
        }

        [Test]
        public void TestRespackInfo()
        {
            var respack = new Respack(respackPath);
            var info = respack.Info;

            Assert.AreEqual(info.Name, "Test Name");
            Assert.AreEqual(info.Author, "Test Author");
            Assert.AreEqual(info.Description, "Test Description");
            Assert.AreEqual(info.Link, "https://test.link");
        }

        [Test]
        public void TestRespackSongs()
        {
            var respack = new Respack(respackPath);
            var songs = respack.Songs;

            Assert.AreEqual(songs.Count, 1);

            var firstSong = songs.First();

            Assert.AreEqual(firstSong.Respack, respack);

            Assert.AreEqual(firstSong.Title, "Test Song Title 1");
            Assert.AreEqual(firstSong.Source, "https://song.link/1");
            Assert.AreEqual(firstSong.BuildupSource, "build_test_1");
            Assert.AreEqual(firstSong.BuildupBeatchars, "oxox1");
            Assert.AreEqual(firstSong.LoopSource, "loop_test_1");
            Assert.AreEqual(firstSong.LoopBeatchars, "xoxo1");
        }
    }
}
