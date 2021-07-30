using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using hues.Game.Extensions;
using hues.Game.Elements;

namespace hues.Game
{
    public class Respack
    {
        public readonly RespackInfo Info;
        public readonly IReadOnlyCollection<Song> Songs;
        public readonly IReadOnlyCollection<Image> Images;

        public override string ToString() =>
            $"<{nameof(Respack)}> " +
            $"Name: {Info.Name}, " +
            $"Songs: {Songs.Count}, " +
            $"Images: {Images.Count}";

        public Respack(string infoXml, string songsXml, string imagesXml)
        {
            Info = parseInfo(infoXml);
            Songs = parseSongs(songsXml);
            Images = parseImages(imagesXml);
        }

        private RespackInfo parseInfo(string infoXml)
        {
            if (infoXml == null)
                throw new ArgumentException("Parameter `infoXml` must not be null");

            return XDocument.Parse(infoXml)
                            .Element("info")
                            .Transform(info => new RespackInfo
                            {
                                Name = info.Element("name").Value,
                                Author = info.Element("author").Value,
                                Description = info.Element("description")?.Value,
                                Link = info.Element("link")?.Value,
                            });
        }

        private IReadOnlyCollection<Song> parseSongs(string songsXml)
        {
            if (songsXml == null)
                return new List<Song>().AsReadOnly();

            return XDocument.Parse(songsXml)
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

        private IReadOnlyCollection<Image> parseImages(string imagesXml)
        {
            if (imagesXml == null)
                return new List<Image>().AsReadOnly();

            return XDocument.Parse(imagesXml)
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
}
