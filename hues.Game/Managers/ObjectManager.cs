using System;
using System.Linq;
using System.Collections.Generic;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Framework.Utils;

namespace hues.Game.Managers
{
    public class ObjectManager<T> : Component
        where T : class
    {
        public enum AdvanceMode
        {
            Stopped,
            Next,
            Random,
        }

        [Resolved]
        private Bindable<T> current { get; set; }

        protected IReadOnlyCollection<T> AllItems => items;
        private readonly List<T> items = new List<T>();
        private readonly object itemLock = new object();

        private AdvanceMode mode = AdvanceMode.Next;

        public AdvanceMode Mode
        {
            get => mode;
            set
            {
                if (value == mode)
                    return;

                lock (itemLock)
                    mode = value;
            }
        }

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

                switch (Mode)
                {
                    case AdvanceMode.Stopped:
                    {
                        break;
                    }

                    case AdvanceMode.Next:
                    {
                        var oldItem = current.Value;
                        current.Value = items.SkipWhile(obj => obj != oldItem)
                                             .Skip(1)
                                             .DefaultIfEmpty(items.First())
                                             .First();
                        break;
                    }

                    case AdvanceMode.Random:
                    {
                        var idx = RNG.Next(items.Count);
                        current.Value = items[idx];
                        break;
                    }
                }

            }
        }

        public void Previous()
        {
            lock (itemLock)
            {
                if (!canProgress())
                    return;

                switch (Mode)
                {
                    case AdvanceMode.Stopped:
                    {
                        break;
                    }

                    case AdvanceMode.Next:
                    {
                        var oldItem = current.Value;
                        current.Value = items.TakeWhile(obj => obj != oldItem)
                                             .DefaultIfEmpty(items.Last())
                                             .Last();
                        break;
                    }

                    case AdvanceMode.Random:
                    {
                        var idx = RNG.Next(items.Count);
                        current.Value = items[idx];
                        break;
                    }
                }

            }
        }
    }
}
