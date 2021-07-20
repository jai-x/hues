using System;
using System.Linq;
using System.Collections.Generic;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game.Managers
{
    public class ImageManager : Component
    {
        [Resolved]
        private Bindable<Image> current { get; set; }

        private readonly List<Image> images = new List<Image>();
        private readonly object imagesLock = new object();

        public IReadOnlyCollection<Image> Images => images;

        public void Add(Image image)
        {
            lock (imagesLock)
                images.Add(image);
        }

        public void Add(IEnumerable<Image> images)
        {
            lock (imagesLock)
                this.images.AddRange(images);
        }

        private bool canProgess()
        {
            if (images.Count == 0)
                return false;

            if (current.Value == null)
            {
                current.Value = images.First();
                return false;
            }

            return true;
        }

        public void Next()
        {
            lock (imagesLock)
            {
                if (!canProgess())
                    return;

                var oldImage = current.Value;

                current.Value = images.SkipWhile(img => oldImage != img)
                                      .Skip(1)
                                      .DefaultIfEmpty(images.First())
                                      .First();
            }
        }

        public void Previous()
        {
            lock (imagesLock)
            {
                if (!canProgess())
                    return;

                var oldImage = current.Value;

                current.Value = images.TakeWhile(img => oldImage != img)
                                      .DefaultIfEmpty(images.Last())
                                      .Last();
            }
        }
    }
}
