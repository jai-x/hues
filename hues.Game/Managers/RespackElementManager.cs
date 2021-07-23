using System;
using System.Linq;
using System.Collections.Generic;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Framework.Utils;

using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class RespackElementManager<T> : Component
        where T : RespackElement
    {
        public enum AdvanceMode
        {
            Stopped,
            Next,
            Random,
        }

        [Resolved]
        private Bindable<T> current { get; set; }

        protected IReadOnlyCollection<T> AllElements => elements;

        private readonly List<T> elements = new List<T>();
        private readonly object elementLock = new object();

        private AdvanceMode mode = AdvanceMode.Next;

        public AdvanceMode Mode
        {
            get => mode;
            set
            {
                if (value == mode)
                    return;

                lock (elementLock)
                    mode = value;
            }
        }

        public void Add(T el)
        {
            lock (elementLock)
                elements.Add(el);
        }

        public void Add(IEnumerable<T> els)
        {
            lock (elementLock)
                elements.AddRange(els);
        }

        private bool canProgress()
        {
            if (elements.Count == 0)
                return false;

            if (current.Value == null)
            {
                current.Value = elements.First();
                return false;
            }

            return true;
        }

        public void Next()
        {
            lock (elementLock)
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
                        current.Value = elements.SkipWhile(obj => obj != oldItem)
                                             .Skip(1)
                                             .DefaultIfEmpty(elements.First())
                                             .First();
                        break;
                    }

                    case AdvanceMode.Random:
                    {
                        var idx = RNG.Next(elements.Count);
                        current.Value = elements[idx];
                        break;
                    }
                }

            }
        }

        public void Previous()
        {
            lock (elementLock)
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
                        current.Value = elements.TakeWhile(obj => obj != oldItem)
                                             .DefaultIfEmpty(elements.Last())
                                             .Last();
                        break;
                    }

                    case AdvanceMode.Random:
                    {
                        var idx = RNG.Next(elements.Count);
                        current.Value = elements[idx];
                        break;
                    }
                }

            }
        }

        public void Clear()
        {
            lock (elementLock)
            {
                current.Value = null;
                elements.Clear();
            }

            Logger.Log($"{this.GetType()} cleared!", level: LogLevel.Debug);
        }

        protected override void Dispose(bool isDisposing)
        {
            Clear();
            base.Dispose(isDisposing);
        }
    }
}
