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

        private int currentIndex = -1;

        [Resolved]
        private BindableList<T> elements { get; set; }

        private readonly object elementLock = new object();

        private readonly HashSet<int> randomUnseenIndexes = new HashSet<int>();

        private AdvanceMode mode = AdvanceMode.Ordered;

        public AdvanceMode Mode
        {
            get => mode;
            set
            {
                if (value == mode)
                    return;

                lock (elementLock)
                {
                    mode = value;
                    resetRandomUnseen();
                }
            }
        }

        private bool canAdd(T el)
        {
            var exists = elements.Contains(el);

            if (exists)
                Logger.Log($"{this.GetType()} cannot add {el.ToString()} (already exists)");

            return !exists;
        }

        public void Add(T el)
        {
            lock (elementLock)
            {
                if (!canAdd(el))
                    return;

                elements.Add(el);
                resetRandomUnseen();
            }
        }

        public void Add(IEnumerable<T> els)
        {
            lock (elementLock)
            {
                elements.AddRange(els.Where(canAdd));
                resetRandomUnseen();
            }
        }

        private bool canProgress() => elements.Count > 0;

        private void advanceNext()
        {
            currentIndex++;

            if (currentIndex > elements.Count - 1)
                currentIndex = 0;

            current.Value = elements[currentIndex];
        }

        private void advancePrevious()
        {
            currentIndex--;

            if (currentIndex < 0)
                currentIndex = elements.Count - 1;

            current.Value = elements[currentIndex];
        }

        private void resetRandomUnseen()
        {
            randomUnseenIndexes.Clear();

            var newIndexes = Enumerable.Range(0, elements?.Count ?? 0);
            randomUnseenIndexes.UnionWith(newIndexes);
        }

        private void advanceRandom()
        {
            if (randomUnseenIndexes.Count == 0)
                resetRandomUnseen();

            var idx = randomUnseenIndexes.ElementAt(RNG.Next(randomUnseenIndexes.Count));
            randomUnseenIndexes.Remove(idx);

            currentIndex = idx;
            current.Value = elements[currentIndex];
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
                currentIndex = -1;
                current.Value = null;
                elements.Clear();
                resetRandomUnseen();
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
