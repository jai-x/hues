using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

using osu.Framework.Logging;

using hues.Game.Extensions;

namespace hues.Game
{
    public class Respack
    {
        public readonly RespackInfo Info;
        public readonly IReadOnlyCollection<Song> Songs;
        public readonly IReadOnlyCollection<Image> Images;

        public Respack(string path)
        {
            using (var archive = ZipFile.OpenRead(path))
            {
                var infoEntry = findEntry(archive, "info.xml");
                Info = parseInfoEntry(infoEntry);

                var songsEntry = findEntry(archive, "songs.xml");
                Songs = parseSongsEntry(songsEntry);

                var imagesEntry = findEntry(archive, "images.xml");
                Images = parseImagesEntry(imagesEntry);

                if (Songs.Count == 0 && Images.Count == 0)
                    throw new RespackEmptyException();
            }
        }

        private IReadOnlyCollection<Image> parseImagesEntry(ZipArchiveEntry imagesEntry)
        {
            if (imagesEntry == null)
                return new List<Image>().AsReadOnly();

            return XDocument.Load(imagesEntry.Open())
                            .Element("images")
                            .Elements()
                            .Where(imageElement => imageElement.Element("frameDuration") == null) // filter out animations for now
                            .Select(imageElement => new Image
                            {
                                Name = imageElement.Element("fullname").Value,
                                Source = imageElement.Element("source").Value,
                                TexturePath = imageElement.Attribute("name").Value,
                                Respack = this,
                            })
                            .ToList()
                            .AsReadOnly();
        }

        private IReadOnlyCollection<Song> parseSongsEntry(ZipArchiveEntry songsEntry)
        {
            if (songsEntry == null)
                return new List<Song>().AsReadOnly();

            return XDocument.Load(songsEntry.Open())
                            .Element("songs")
                            .Elements()
                            .Select(songElement => new Song
                            {
                                Title = songElement.Element("title").Value,
                                Source = songElement.Element("source")?.Value,
                                BuildupSource = songElement.Element("buildup")?.Value,
                                BuildupBeatchars = songElement.Element("buildupRhythm")?.Value,
                                LoopSource = songElement.Attribute("name").Value,
                                LoopBeatchars = songElement.Element("rhythm").Value,
                                Respack = this,
                            })
                            .ToList()
                            .AsReadOnly();
        }

        private RespackInfo parseInfoEntry(ZipArchiveEntry infoEntry)
        {
            if (infoEntry == null)
                throw new RespackNoInfoException();

            return XDocument.Load(infoEntry.Open())
                            .Element("info")
                            .Transform(info => new RespackInfo
                            {
                                Name = info.Element("name").Value,
                                Author = info.Element("author").Value,
                                Description = info.Element("description")?.Value,
                                Link = info.Element("link")?.Value,
                            });
        }


        private ZipArchiveEntry findEntry(ZipArchive archive, string name)
        {
            foreach (var entry in archive.Entries)
                if (entry.Name == name)
                    return entry;

            return null;
        }

        private string readEntry(ZipArchiveEntry entry)
        {
            using (var reader = new StreamReader(entry.Open()))
                return reader.ReadToEnd();
        }
    }

    public struct RespackInfo
    {
        public string Name;
        public string Author;
        public string Description;
        public string Link;

        public override string ToString() =>
            $"<{nameof(RespackInfo)}> " +
            $"Name: {Name}, " +
            $"Author: {Author}, " +
            $"Description: {Description}, " +
            $"Link: {Link}";
    }

    public class RespackEmptyException : Exception
    {
        public override string Message => "No songs or images found in respack archive";
    }

    public class RespackNoInfoException : Exception
    {
        public override string Message => "Unable to find info.xml file in respack archive";
    }
}
