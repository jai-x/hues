using System;
using System.Linq;
using System.Collections.Generic;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game.Managers
{
    public class ObjectManager<T> : Component
        where T : class
    {
        [Resolved]
        private Bindable<T> current { get; set; }

        private readonly List<T> items = new List<T>();
        private readonly object itemLock = new object();

        protected IReadOnlyCollection<T> AllItems => items;

        public void Add(T obj)
        {
            lock (itemLock)
                items.Add(obj);
        }

        public void Add(IEnumerable<T> objs)
        {
            lock (itemLock)
                items.AddRange(objs);
        }

        private bool canProgress()
        {
            if (items.Count == 0)
                return false;

            if (current.Value == null)
            {
                current.Value = items.First();
                return false;
            }

            return true;
        }

        public void Next()
        {
            lock (itemLock)
            {
                if (!canProgress())
                    return;

                var oldItem = current.Value;

                current.Value = items.SkipWhile(obj => obj != oldItem)
                                     .Skip(1)
                                     .DefaultIfEmpty(items.First())
                                     .First();
            }
        }

        public void Previous()
        {
            lock (itemLock)
            {
                if (!canProgress())
                    return;

                var oldItem = current.Value;

                current.Value = items.TakeWhile(obj => obj != oldItem)
                                     .DefaultIfEmpty(items.Last())
                                     .Last();
            }
        }
    }
}
