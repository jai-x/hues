using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class ImageManager : Component
    {
        public readonly Bindable<Image> Current = new Bindable<Image>();

        private int position = 0;
        private Logger logger = Logger.GetLogger();

        public ImageManager()
        {
            Current.Value = Image.All[position];
            Current.ValueChanged += (hc) => logger.Debug($"{nameof(ImageManager)}: Image Current Changed ({hc.OldValue.Name} -> {hc.NewValue.Name})");
        }

        public void Next()
        {
            if (position == Image.All.Length - 1)
                position = 0;
            else
                position++;

            Current.Value = Image.All[position];
        }

        public void Previous()
        {
            if (position == 0)
                position = Image.All.Length - 1;
            else
                position--;

            Current.Value = Image.All[position];
        }
    }
}
