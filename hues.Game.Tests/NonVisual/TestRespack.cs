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

        [Test]
        public void TestRespackNoInfo()
        {
            Assert.Throws(typeof(ArgumentException), () => { new Respack(null, null, null); });
        }

        [Test]
        public void TestRespackInfo()
        {
            var respack = new Respack(infoXml, null, null);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(respack.Songs.Count, 0);
                Assert.AreEqual(respack.Images.Count, 0);
            });

            var info = respack.Info;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(info.Name, "Test Name");
                Assert.AreEqual(info.Author, "Test Author");
                Assert.AreEqual(info.Description, "Test Description");
                Assert.AreEqual(info.Link, "https://test.link");
            });
        }

        [Test]
        public void TestRespackSongs()
        {
            var respack = new Respack(infoXml, songsXml, null);

            Assert.AreEqual(respack.Songs.Count, 1);

            var firstSong = respack.Songs.First();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstSong.Title, "Test Song Title 1");
                Assert.AreEqual(firstSong.Source, "https://song.link/1");
                Assert.AreEqual(firstSong.BuildupSource, "build_test_1");
                Assert.AreEqual(firstSong.BuildupBeatchars, "oxox1");
                Assert.AreEqual(firstSong.LoopSource, "loop_test_1");
                Assert.AreEqual(firstSong.LoopBeatchars, "xoxo1");
                Assert.AreEqual(firstSong.Respack, respack);
            });
        }

        public void TestRespackImages()
        {
            var respack = new Respack(infoXml, null, imagesXml);

            Assert.AreEqual(respack.Images.Count, 1);

            var firstImage = respack.Images.First();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(firstImage.Name, "Test Image Name 1");
                Assert.AreEqual(firstImage.Source, "https://image.link/1");
                Assert.AreEqual(firstImage.TexturePath, "image_path_1");
                Assert.AreEqual(firstImage.Respack, respack);
            });
        }
    }
}
