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

        private readonly Logger logger = Logger.GetLogger();

        private void l(string log) => logger.Debug(log);

        public Respack(string path)
        {
            using (var archive = ZipFile.OpenRead(path))
            {
                var infoEntry = findEntry(archive, "info.xml");
                Info = parseInfoEntry(infoEntry);

                var songsEntry = findEntry(archive, "songs.xml");
                Songs = parseSongsEntry(songsEntry);
            }
        }

        private IReadOnlyCollection<Song> parseSongsEntry(ZipArchiveEntry songsEntry)
        {
            return XDocument.Load(songsEntry.Open())
                            .Element("songs")
                            .Elements()
                            .Select(song => new Song
                            {
                                Name = song.Element("title").Value,
                                BuildupSource = song.Element("buildup")?.Value,
                                BuildupBeatchars = song.Element("buildupRhythm")?.Value,
                                LoopSource = song.Attribute("name").Value,
                                LoopBeatchars = song.Element("rhythm").Value,
                                IndependantBuild = song.Element("independentBuild")?.Value.ToBoolean() ?? false,
                                Respack = this,
                            })
                            .ToList()
                            .AsReadOnly();
        }

        private RespackInfo parseInfoEntry(ZipArchiveEntry infoEntry)
        {
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

            throw new ArgumentException($"Unable to find {name} file in respack archive");
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
}
