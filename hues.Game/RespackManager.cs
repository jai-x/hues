using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

using hues.Game.Extensions;
using hues.Game.Stores;

namespace hues.Game
{
    public class RespackManager
    {
        private readonly RespackTrackResourceStore trackResources;
        private readonly List<Respack> respacks = new List<Respack>();

        public IReadOnlyCollection<Respack> Respacks => respacks;

        public RespackManager(RespackTrackResourceStore trackResources)
        {
            this.trackResources = trackResources;
        }

        public void Add(string path)
        {
            using (var fileStream = File.OpenRead(path))
                Add(fileStream);
        }

        public void Add(Stream stream)
        {
            Respack respack;

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                var infoXml = findAndReadEntry(archive, "info.xml");

                if (infoXml == null)
                    throw new RespackMissingFileException("info.xml");

                var songsXml = findAndReadEntry(archive, "songs.xml");
                var imagesXml = findAndReadEntry(archive, "images.xml");

                respack = new Respack(infoXml, songsXml, imagesXml);

                addSongsToResourceStore(archive, respack.Songs);
                addImagesToResourceStore(archive, respack.Images);
            }

            respacks.Add(respack);
        }

        private void addSongsToResourceStore(ZipArchive archive, IReadOnlyCollection<Song> songs)
        {
            foreach (var song in songs)
            {
                var loopEntry = findEntry(archive, song.LoopSource, true);

                if (loopEntry == null)
                    throw new RespackMissingFileException(song.LoopSource);

                using (var stream = loopEntry.Open())
                    trackResources.Add(song.LoopSource, stream);

                if (String.IsNullOrEmpty(song.BuildupSource))
                    continue;

                var buildupEntry = findEntry(archive, song.BuildupSource, true);

                if (buildupEntry == null)
                    throw new RespackMissingFileException(song.BuildupSource);

                using (var stream = buildupEntry.Open())
                    trackResources.Add(song.LoopSource, stream);
            }
        }

        private void addImagesToResourceStore(ZipArchive archive, IReadOnlyCollection<Image> images)
        {
            // TODO: Implement this after implementing an inmemory texture resource store
        }

        private ZipArchiveEntry findEntry(ZipArchive archive, string name, bool ignoreExtension = false)
        {
            return archive.Entries
                          .Where(entry =>
                          {
                              var entryName = (ignoreExtension) ? Path.GetFileNameWithoutExtension(entry.Name) : entry.Name;
                              return (entryName == name);
                          })
                          .FirstOrDefault();
        }

        private string findAndReadEntry(ZipArchive archive, string name)
        {
            var entry = findEntry(archive, name);

            if (entry == null)
                return null;

            using (var reader = new StreamReader(entry.Open()))
                return reader.ReadToEnd();
        }
    }

    public class RespackMissingFileException : Exception
    {
        public RespackMissingFileException(string fileName) :
            base($"Unable to find file `{fileName}` in respack archive")
        { }
    }
}
