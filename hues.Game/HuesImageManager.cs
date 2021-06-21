using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class HuesImageManager : Component
    {
        public readonly Bindable<HuesImage> Current = new Bindable<HuesImage>();

        private int position = 0;
        private Logger logger = Logger.GetLogger();

        public HuesImageManager()
        {
            Current.Value = HuesImage.All[position];
            Current.ValueChanged += (hc) => logger.Debug($"{nameof(HuesImageManager)}: HuesImage Current Changed ({hc.OldValue.Name} -> {hc.NewValue.Name})");
        }

        public void Next()
        {
            if (position == HuesImage.All.Length - 1)
                position = 0;
            else
                position++;

            Current.Value = HuesImage.All[position];
        }

        public void Previous()
        {
            if (position == 0)
                position = HuesImage.All.Length - 1;
            else
                position--;

            Current.Value = HuesImage.All[position];
        }
    }
}
