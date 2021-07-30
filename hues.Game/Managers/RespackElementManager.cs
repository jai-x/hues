using System.Linq;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Framework.Utils;
using hues.Game.Elements;

namespace hues.Game.Managers
{
    public enum AdvanceMode
    {
        Stopped,
        Ordered,
        Random,
    }

    public class ElementManager<T> : Component
        where T : Element
    {
        [Resolved]
        private Bindable<T> current { get; set; }

        [Resolved]
        private BindableList<T> elements { get; set; }

        private readonly object elementLock = new object();

        private AdvanceMode mode = AdvanceMode.Ordered;

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

        private void advanceNext()
        {
            var oldItem = current.Value;
            current.Value = elements.SkipWhile(obj => obj != oldItem)
                                    .Skip(1)
                                    .DefaultIfEmpty(elements.First())
                                    .First();
        }

        private void advancePrevious()
        {
            var oldItem = current.Value;
            current.Value = elements.TakeWhile(obj => obj != oldItem)
                                    .DefaultIfEmpty(elements.Last())
                                    .Last();
        }

        private void advanceRandom()
        {
            var idx = RNG.Next(elements.Count);
            var next = elements[idx];
            if (current.Value == next)
                advanceNext();
            else
                current.Value = next;
        }

        public void Next(bool force = false)
        {
            lock (elementLock)
            {
                if (!canProgress())
                    return;

                if (force)
                {
                    advanceNext();
                    return;
                }

                switch (Mode)
                {
                    case AdvanceMode.Stopped:
                        break;

                    case AdvanceMode.Ordered:
                        advanceNext();
                        break;

                    case AdvanceMode.Random:
                        advanceRandom();
                        break;
                }
            }
        }

        public void Previous(bool force = false)
        {
            lock (elementLock)
            {
                if (!canProgress())
                    return;

                if (force)
                {
                    advancePrevious();
                    return;
                }

                switch (Mode)
                {
                    case AdvanceMode.Stopped:
                        break;

                    case AdvanceMode.Ordered:
                        advancePrevious();
                        break;

                    case AdvanceMode.Random:
                        advanceRandom();
                        break;
                }
            }
        }

        public void Random()
        {
            lock (elementLock)
            {
                if (!canProgress())
                    return;

                advanceRandom();
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
