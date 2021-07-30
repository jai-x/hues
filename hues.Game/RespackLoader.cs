using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Framework.Platform;
using hues.Game.Managers;
using hues.Game.ResourceStores;
using hues.Game.Elements;

namespace hues.Game
{
    public class RespackLoader : Component
    {
        [Resolved]
        private RespackTrackResourceStore trackResources { get; set; }

        [Resolved]
        private RespackTextureResourceStore textureResources { get; set; }

        [Resolved]
        private GameHost host { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private BindableList<Respack> respacks { get; set; }
        private readonly object respackLock = new object();

        public IReadOnlyCollection<Respack> Respacks => respacks;

        public void LoadPath(string path)
        {
            using (var fileStream = File.OpenRead(path))
                LoadStream(fileStream);
        }

        public void LoadStream(Stream stream)
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

            lock (respackLock)
            {
                imageManager.Add(respack.Images);
                songManager.Add(respack.Songs);
                respacks.Add(respack);
            }
        }

        public void Clear()
        {
            lock (respackLock)
                respacks.Clear();

            Logger.Log($"{this.GetType()} cleared!", level: LogLevel.Debug);
        }

        private void addSongsToResourceStore(ZipArchive archive, IReadOnlyCollection<Song> songs)
        {
            foreach (var song in songs)
            {
                var loopEntry = findEntry(archive, song.LoopSource, true);

                if (loopEntry == null)
                    throw new RespackMissingFileException(song.LoopSource);

                using (var stream = loopEntry.Open())
                    trackResources.Add(song.LoopSource, stream, loopEntry.Length);

                if (String.IsNullOrEmpty(song.BuildupSource))
                    continue;

                var buildupEntry = findEntry(archive, song.BuildupSource, true);

                if (buildupEntry == null)
                    throw new RespackMissingFileException(song.BuildupSource);

                using (var stream = buildupEntry.Open())
                    trackResources.Add(song.BuildupSource, stream, loopEntry.Length);
            }
        }

        private void addImagesToResourceStore(ZipArchive archive, IReadOnlyCollection<Image> images)
        {
            foreach (var image in images)
            {
                var imageEntry = findEntry(archive, image.TexturePath, true);

                if (imageEntry == null)
                    throw new RespackMissingFileException(image.TexturePath);

                using (var stream = imageEntry.Open())
                    textureResources.Add(image.TexturePath, stream, imageEntry.Length);

            }
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
