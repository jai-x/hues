using System;
using System.Collections.Generic;
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

        private const string imagesXml = @"
        <images>
          <image name=""image_path_1"">
            <source>https://image.link/1</source>
            <fullname>Test Image Name 1</fullname>
          </image>
        </images>
        ";

        private readonly List<string> testFiles = new List<string>();

        private string makeRespack(string info, string songs, string images)
        {
            var path = Path.GetTempFileName();

            using (var archive = ZipFile.Open(path, ZipArchiveMode.Update))
            {
                if (info != null)
                {
                    var infoEntry = archive.CreateEntry("info.xml");
                    using (var infoWriter = new StreamWriter(infoEntry.Open(), Encoding.UTF8))
                        infoWriter.Write(info);
                }

                if (songs != null)
                {
                    var songsEntry = archive.CreateEntry("songs.xml");
                    using (var songsWriter = new StreamWriter(songsEntry.Open(), Encoding.UTF8))
                        songsWriter.Write(songs);
                }

                if (images != null)
                {
                    var imagesEntry = archive.CreateEntry("images.xml");
                    using (var imagesWriter = new StreamWriter(imagesEntry.Open(), Encoding.UTF8))
                        imagesWriter.Write(images);
                }
            }

            testFiles.Add(path);

            return path;
        }

        [TearDown]
        public void Cleanup()
        {
            foreach (var file in testFiles)
                File.Delete(file);
        }

        [Test]
        public void TestRespackNoInfo()
        {
            var path = makeRespack(null, null, null);
            Assert.Throws(typeof(RespackNoInfoException), () => { new Respack(path); });
        }

        [Test]
        public void TestRespackEmpty()
        {
            var path = makeRespack(infoXml, null, null);
            Assert.Throws(typeof(RespackEmptyException), () => { new Respack(path); });
        }

        [Test]
        public void TestRespackInfo()
        {
            var path = makeRespack(infoXml, songsXml, null);

            var respack = new Respack(path);
            var info = respack.Info;

            Assert.AreEqual(info.Name, "Test Name");
            Assert.AreEqual(info.Author, "Test Author");
            Assert.AreEqual(info.Description, "Test Description");
            Assert.AreEqual(info.Link, "https://test.link");
        }

        [Test]
        public void TestRespackSongs()
        {
            var path = makeRespack(infoXml, songsXml, null);

            var respack = new Respack(path);

            Assert.AreEqual(respack.Songs.Count, 1);

            var firstSong = respack.Songs.First();

            Assert.AreEqual(firstSong.Respack, respack);

            Assert.AreEqual(firstSong.Title, "Test Song Title 1");
            Assert.AreEqual(firstSong.Source, "https://song.link/1");
            Assert.AreEqual(firstSong.BuildupSource, "build_test_1");
            Assert.AreEqual(firstSong.BuildupBeatchars, "oxox1");
            Assert.AreEqual(firstSong.LoopSource, "loop_test_1");
            Assert.AreEqual(firstSong.LoopBeatchars, "xoxo1");
        }

        public void TestRespackImages()
        {
            var path = makeRespack(infoXml, null, imagesXml);

            var respack = new Respack(path);

            Assert.AreEqual(respack.Images.Count, 1);

            var firstImage = respack.Images.First();

            Assert.AreEqual(firstImage.Respack, respack);

            Assert.AreEqual(firstImage.Name, "Test Image Name 1");
            Assert.AreEqual(firstImage.Source, "https://image.link/1");
            Assert.AreEqual(firstImage.TexturePath, "image_path_1");
        }
    }
}
